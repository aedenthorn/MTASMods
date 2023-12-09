using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using Pathea.MissionNs;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UnlimitedOrders
{
    [BepInPlugin("aedenthorn.UnlimitedOrders", "Unlimited Orders", "0.2.0")]
    public partial class BepInExPlugin : BaseUnityPlugin
    {
        private static BepInExPlugin context;

        public static ConfigEntry<bool> modEnabled;
        public static ConfigEntry<bool> isDebug;
        public static ConfigEntry<bool> customOrderAmounts;
        public static ConfigEntry<int> maxSlots;
        public static ConfigEntry<string> orderPanelSlots;
        public static ConfigEntry<string> orderTypeGenRate;

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
            customOrderAmounts = Config.Bind<bool>("Options", "CustomOrderAmounts", false, "Set to true to use the options in this config file.");
            maxSlots = Config.Bind<int>("Options", "MaxSlots", 7, "Max slots");
            orderPanelSlots = Config.Bind<string>("Options", "OrderPanelSlots", "1,1,2,7", "Max order slots per type (huge, big, medium, small)");
            orderTypeGenRate = Config.Bind<string>("Options", "OrderTypeGenRate", "0.1,0.15,0.2,0.5", "Generation rate per type  (huge, big, medium, small)");

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);

            Dbgl("Mod Loaded");
        }

        [HarmonyPatch(typeof(OrderMissionManager), nameof(OrderMissionManager.GetRandomOrderMission))]
        static class OrderMissionManager_GetRandomOrderMission_Patch
        {
            public static void Prefix(OrderMissionManager __instance)
            {
                if (!modEnabled.Value || !customOrderAmounts.Value)
                    return;
                __instance.Config.OrderMaxSlot = maxSlots.Value;
                __instance.Config.OrderPanelSlot = orderPanelSlots.Value.Split(',').Select(s => int.Parse(s)).ToArray();
                __instance.Config.OrderTypeGenRate = orderTypeGenRate.Value.Split(',').Select(s => float.Parse(s, System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            }
        }


        [HarmonyPatch(typeof(OrderMissionManager), nameof(OrderMissionManager.CheckCanReceive))]
        static class OrderMissionManager_CheckCanReceive_Patch
        {
            public static bool Prefix(ref bool __result)
            {
                if (!modEnabled.Value)
                    return true;
                __result = true;
                return false;
            }
        }

    }
}
