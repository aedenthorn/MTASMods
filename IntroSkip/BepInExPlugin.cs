using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using Pathea.DesignerConfig;
using Pathea.GreenModNs;
using Pathea.ItemNs;
using Pathea.OptionNs;
using Pathea.UISystemV2.UI;
using System;
using System.Reflection;
using UnityEngine;

namespace IntroSkip
{
    [BepInPlugin("aedenthorn.IntroSkip", "Intro Skip", "0.1.0")]
    public partial class BepInExPlugin : BaseUnityPlugin
    {
        private static BepInExPlugin context;

        public static ConfigEntry<bool> modEnabled;
        public static ConfigEntry<bool> isDebug;
        public static ConfigEntry<int> maxStack;

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
            maxStack = Config.Bind<int>("Options", "MaxStack", 999, "Maximum stack for all items");

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);

            Dbgl("Mod Loaded");
        }

        [HarmonyPatch(typeof(UIAssetBase), "LoadUI")]
        static class OptionMgr_GetLanguageGame_Patch
        {
            public static void Prefix(UIAssetBase __instance)
            {
                Dbgl($"LoadUI: \n\n{__instance}");
                foreach(var c in __instance.)
            }
        }
    }
}
