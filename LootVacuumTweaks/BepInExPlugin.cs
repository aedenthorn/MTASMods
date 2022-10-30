using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using Pathea.ItemNs;
using System;
using System.Reflection;
using UnityEngine;

namespace LootVacuumTweaks
{
    [BepInPlugin("aedenthorn.LootVacuumTweaks", "Loot Vacuum Tweaks", "0.1.0")]
    public partial class BepInExPlugin : BaseUnityPlugin
    {
        private static BepInExPlugin context;

        public static ConfigEntry<bool> modEnabled;
        public static ConfigEntry<bool> isDebug;
        public static ConfigEntry<float> pickupDistanceMult;
        public static ConfigEntry<float> vacuumSpeedMult;
        public static ConfigEntry<float> waitTimeMult;

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
            pickupDistanceMult = Config.Bind<float>("Options", "PickupDistanceMult", 2f, "Multiply pickup distance by this amount");
            vacuumSpeedMult = Config.Bind<float>("Options", "VacuumSpeedMult", 2f, "Multiply vacuum speed by this amount");
            waitTimeMult = Config.Bind<float>("Options", "WaitTimeMult", 0.5f, "Multiply time to begin vacuuming by this amount (0 = instant drop)");

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);

            Dbgl("Mod Loaded");
        }

        [HarmonyPatch(typeof(DropItemObject), nameof(DropItemObject.Init), new Type[] { typeof(ItemInstance), typeof(DropAnim), typeof(float), typeof(float) })]
        static class DropItemObject_Init_Patch_1
        {
            public static void Prefix(DropItemObject __instance, ref float pickUpDistance, ref float waitTime)
            {
                if (!modEnabled.Value)
                    return;
                pickUpDistance *= pickupDistanceMult.Value;
                waitTime *= waitTimeMult.Value;
            }
        }
        [HarmonyPatch(typeof(DropItemObject), nameof(DropItemObject.Init), new Type[] { typeof(ItemInstance), typeof(Vector3), typeof(float), typeof(float), typeof(bool) })]
        static class DropItemObject_Init_Patch_2
        {
            public static void Prefix(DropItemObject __instance, ref float pickUpDistance, ref float waitTime)
            {
                if (!modEnabled.Value)
                    return;
                pickUpDistance *= pickupDistanceMult.Value;
                waitTime *= waitTimeMult.Value;
            }
        }
        [HarmonyPatch(typeof(DropItemObject), "UpdatePos")]
        static class DropItemObject_UpdatePos_Patch
        {
            public static void Prefix(ref float deltaTime)
            {
                if (!modEnabled.Value)
                    return;
                deltaTime *= vacuumSpeedMult.Value;
            }
        }
    }
}
