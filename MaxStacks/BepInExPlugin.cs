using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using Pathea.DesignerConfig;
using Pathea.ItemNs;
using System;
using System.Reflection;
using UnityEngine;

namespace LootVacuumTweaks
{
    [BepInPlugin("aedenthorn.MaxStacks", "Max Stacks", "0.1.0")]
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

        [HarmonyPatch(typeof(ItemPrototypeModule), "OnLoad")]
        static class ItemPrototypeModule_OnLoad
        {
            public static void Postfix(ConfigReaderId<ItemPrototype> ___items)
            {
                if (!modEnabled.Value)
                    return;
                for(int i = 0;i < ___items.Count; i++)
                {
                    ___items[i].stackNumber = maxStack.Value;
                }
            }
        }
    }
}
