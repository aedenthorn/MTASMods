using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using Pathea;
using Pathea.ActionNs;
using Pathea.ActorNs;
using Pathea.FrameworkNs;
using Pathea.ScenarioNs;
using Pathea.SceneInfoNs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

namespace UnlockedDoors
{
    [BepInPlugin("aedenthorn.UnlockedDoors", "Unlocked Doors", "0.1.0")]
    public partial class BepInExPlugin : BaseUnityPlugin
    {
        private static BepInExPlugin context;

        public static ConfigEntry<bool> modEnabled;
        public static ConfigEntry<bool> isDebug;
        public static ConfigEntry<bool> unlockPrivateDoors;
        public static ConfigEntry<bool> unlockScheduleDoors;

        //public static ConfigEntry<int> nexusID;

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
            unlockPrivateDoors = Config.Bind<bool>("Options", "UnlockPrivateDoors", true, "Unlock interior private doors");
            unlockScheduleDoors = Config.Bind<bool>("Options", "UnlockScheduleDoors", true, "Unlock exterior doors that have schedules");

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);

            Dbgl("Mod Loaded");
        }

        [HarmonyPatch(typeof(Portal), "OnInteract")]
        static class Portal_OnInteract_Patch
        {
            public static bool Prefix(Portal __instance, ActionInfoGaming actionInfo)
            {
                if (!modEnabled.Value || actionInfo.ActionName != "ChangeScene" || !unlockScheduleDoors.Value)
                    return true;
                SceneExtranceInst infoByName = Module<SceneInfoMgr>.Self.GetInfoByName<SceneExtranceInst>((string)AccessTools.PropertyGetter(typeof(Portal), "curExtranceName").Invoke(__instance, null));
                AdditiveScene scene = infoByName.exitArea.scene;
                Singleton<CoroutineMgr>.Instance.StartCoroutine((IEnumerator)AccessTools.Method(typeof(Portal), "GoToScenario").Invoke(__instance, new object[] { scene, infoByName }));
                return false;
            }
        }
        [HarmonyPatch(typeof(AutoDoor), "CheckSocialLevel")]
        static class AutoDoor_CheckSocialLevel_Patch
        {
            public static bool Prefix(AutoDoor __instance, ref bool __result)
            {
                if (!modEnabled.Value || !unlockPrivateDoors.Value)
                    return true;
                __result = true;
                return false;
            }
        }

    }
}
