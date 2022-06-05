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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GameSpeed
{
    [BepInPlugin("aedenthorn.GameSpeed", "Game Speed", "0.1.0")]
    public partial class BepInExPlugin : BaseUnityPlugin
    {
        private static BepInExPlugin context;

        public static ConfigEntry<bool> modEnabled;
        public static ConfigEntry<bool> isDebug;

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

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);

            Dbgl("Mod Loaded");
        }

        [HarmonyPatch(typeof(OptionPartControl), nameof(OptionPartControl.RefreshUI))]
        static class OptionPartControl_RefreshUI_Patch
        {
            public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                Dbgl("Transpiling OptionPartControl.RefreshUI");
                List<CodeInstruction> codes = instructions.ToList();
                for(int i = 0; i < codes.Count; i++)
                {
                    if (i < codes.Count - 3 && codes[i].operand != null && codes[i].operand is FieldInfo && (FieldInfo)codes[i].operand == AccessTools.Field(typeof(OptionPartUI), nameof(OptionPartUI.timeScale)) && codes[i + 3].operand != null && codes[i + 3].operand is MethodInfo && (MethodInfo)codes[i + 3].operand == AccessTools.Method(typeof(OptionSliderValueElement), nameof(OptionSliderValueElement.SetMinMax)))
                    {
                        codes[i + 1].operand = 0.01f;
                        codes[i + 2].operand = 10f;
                    }
                }
                return codes.AsEnumerable();
            }
        }
    }
}
