using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using Pathea;
using Pathea.FrameworkNs;
using Pathea.SleepNs;
using Pathea.TimeNs;
using Pathea.UISystemV2.UI;
using Pathea.UISystemV2.UIControl;
using System.Reflection;

namespace NoSleep
{
    [BepInPlugin("aedenthorn.NoSleep", "No Sleep", "0.1.2")]
    public partial class BepInExPlugin : BaseUnityPlugin
    {
        private static BepInExPlugin context;

        public static ConfigEntry<bool> modEnabled;
        public static ConfigEntry<bool> isDebug;
        public static ConfigEntry<bool> alwaysWellRested;
        public static ConfigEntry<bool> disableReminders;
        public static bool fakeSleep;

        public static void Dbgl(string str = "", LogLevel logLevel = LogLevel.Debug)
        {
            if (isDebug.Value)
                context.Logger.Log(logLevel, str);
        }
        private void Awake()
        {

            context = this;
            modEnabled = Config.Bind<bool>("General", "Enabled", true, "Enable this mod");
            isDebug = Config.Bind<bool>("General", "IsDebug", true, "Enable debug logs");
            disableReminders = Config.Bind<bool>("Options", "DisableReminders", true, "Disable sleep reminders");
            alwaysWellRested = Config.Bind<bool>("Options", "AlwaysWellRested", true, "Always well rested after sleep, no matter the time of sleep");

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);

            Dbgl("Mod Loaded");
        }

        [HarmonyPatch(typeof(TimeModule), nameof(TimeModule.IsCrossSleepTime))]
        static class TimeModule_IsCrossSleepTime_Patch
        {
            public static bool Prefix(TimeModule __instance, GameDateTime beginTime, GameTimeSpan span, ref bool __result)
            {
                if (!modEnabled.Value)
                    return true;
                __result = false;
                return false;
            }
        }
        [HarmonyPatch(typeof(TimeModule), "OnMinuteChange")]
        static class TimeModule_OnMinuteChange_Patch
        {
            public static void Postfix(TimeModule __instance)
            {
                if (!modEnabled.Value)
                    return;
                if (__instance.CurrentTime.Hour == __instance.EndSleepHour - 1 && __instance.CurrentTime.Minute == 59)
                {
                    Dbgl("Starting fake sleep");
                    fakeSleep = true;
                    GameTimeSpan gameTimeSpan = new GameTimeSpan(1, 0, 0);
                    AccessTools.PropertySetter(typeof(TimeModule), "CurrentTime").Invoke(__instance, new object[] { __instance.CurrentTime - gameTimeSpan });
                    Module<SleepModule>.Self.PlayerSleep();
                }

            }
        }
        [HarmonyPatch(typeof(SleepModule), "OnReminderSleep")]
        static class SleepModule_OnReminderSleep_Patch
        {
            public static bool Prefix()
            {
                return (!modEnabled.Value || !disableReminders.Value);
            }
        }
        [HarmonyPatch(typeof(SleepModule), "SetSleepState")]
        static class SleepModule_SetSleepState_Patch
        {
            public static void Prefix(ref SleepState sleepState)
            {
                if (!modEnabled.Value)
                    return;
                if (alwaysWellRested.Value)
                    sleepState = SleepState.EarlySleep;
                else if (fakeSleep && sleepState == SleepState.LateSleep)
                    sleepState = SleepState.NormalSleep;
            }
        }
        [HarmonyPatch(typeof(SleepMaskUI), nameof(SleepMaskUI.SetDisplay))]
        static class SleepMaskUI_SetDisplay_Patch
        {
            public static bool Prefix(SleepMaskUI __instance)
            {
                if (!modEnabled.Value || !fakeSleep)
                    return true;
                AccessTools.Method(typeof(SleepMaskUI), "Exit").Invoke(__instance, null);
                return false;
            }
        }
        [HarmonyPatch(typeof(SleepControl), "OnShow")]
        static class SleepControl_OnShow_Patch
        {
            public static bool Prefix(SleepControl __instance)
            {
                return (!modEnabled.Value || !fakeSleep);
            }
        }
        [HarmonyPatch(typeof(SleepControl), "OnCanControl")]
        static class SleepControl_OnCanControl_Patch
        {
            public static bool Prefix(SleepControl __instance)
            {
                if (!modEnabled.Value || !fakeSleep)
                    return true;
                fakeSleep = false;
                Module<SleepModule>.Self.GetUp();
                return false;
            }
        }
    }
}
