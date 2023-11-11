using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using Pathea.Plants;
using System.Reflection;

namespace PlantMod
{
    [BepInPlugin("aedenthorn.PlantMod", "Plant Mod", "0.1.0")]
    public partial class BepInExPlugin : BaseUnityPlugin
    {
        private static BepInExPlugin context;

        public static ConfigEntry<bool> modEnabled;
        public static ConfigEntry<bool> isDebug;
        public static ConfigEntry<float> growSpeedMult;
        public static ConfigEntry<float> fruitGrowSpeedMult;
        public static ConfigEntry<float> waterMult;
        public static ConfigEntry<float> nutrientMult;

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
            growSpeedMult = Config.Bind<float>("Options", "GrowSpeedMult", 2, "Grow speed multiplier");
            fruitGrowSpeedMult = Config.Bind<float>("Options", "FruitGrowSpeedMult", 2, "Fruit growth speed multiplier");
            waterMult = Config.Bind<float>("Options", "WaterMult", 0.5f, "Water requirement multiplier");
            nutrientMult = Config.Bind<float>("Options", "NutrientMult", 0.5f, "Nutrient requirement multiplier");

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);

            Dbgl("Mod Loaded");
        }

        [HarmonyPatch(typeof(PlantConfig), nameof(PlantConfig.GetWaterNeedPerMin))]
        static class PlantConfig_GetWaterNeedPerMin_Patch
        {
            public static void Postfix(ref float __result)
            {
                if (!modEnabled.Value)
                    return;
                __result *= waterMult.Value;
            }
        }
        [HarmonyPatch(typeof(PlantConfig), nameof(PlantConfig.GetNutrientNeedPerMin))]
        static class PlantConfig_manurePerMinuteCost_Patch
        {
            public static void Postfix(ref float __result)
            {
                if (!modEnabled.Value)
                    return;
                __result *= nutrientMult.Value;
            }
        }
        [HarmonyPatch(typeof(PlantConfig), nameof(PlantConfig.GetGrowthRatePerMin))]
        static class PlantConfig_GetGrowthRatePerMin_Patch
        {
            public static void Postfix(ref float __result)
            {
                if (!modEnabled.Value)
                    return;
                __result *= growSpeedMult.Value;
            }
        }
        [HarmonyPatch(typeof(PlantConfig), nameof(PlantConfig.GetFruitGrowthRatePerMin))]
        static class PlantConfig_GetFruitGrowthRatePerMin_Patch
        {
            public static void Postfix(ref float __result)
            {
                if (!modEnabled.Value)
                    return;
                __result *= fruitGrowSpeedMult.Value;
            }
        }
    }
}
