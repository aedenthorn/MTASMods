﻿using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using Pathea.MachineNs;
using System.Reflection;

namespace ProductionSpeed
{
    [BepInPlugin("aedenthorn.ProductionSpeed", "Production Speed", "0.1.0")]
    public partial class BepInExPlugin : BaseUnityPlugin
    {
        private static BepInExPlugin context;

        public static ConfigEntry<bool> modEnabled;
        public static ConfigEntry<bool> isDebug;
        public static ConfigEntry<float> speedMult;
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
            speedMult = Config.Bind<float>("Options", "SpeedMult", 2, "Speed multiplier");

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);

            Dbgl("Mod Loaded");
        }

        [HarmonyPatch(typeof(MachineProductQueue), nameof(MachineProductQueue.Work))]
        static class MachineProductQueue_Work_Patch
        {
            public static void Prefix(ref float minutes)
            {
                if (!modEnabled.Value)
                    return;
                minutes *= speedMult.Value;
            }
        }
    }
}
