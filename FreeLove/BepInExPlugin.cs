using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using Pathea.SocialNs;
using Pathea.SocialNs.EngagementNs;
using System;
using System.Reflection;

namespace FreeLove
{
    [BepInPlugin("aedenthorn.FreeLove", "FreeLove", "0.1.0")]
    public partial class BepInExPlugin : BaseUnityPlugin
    {
        private static BepInExPlugin context;

        public static ConfigEntry<bool> modEnabled;
        public static ConfigEntry<bool> isDebug;


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
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);

            Dbgl("Mod Loaded");
        }

        [HarmonyPatch(typeof(SocialModule), nameof(SocialModule.CanConfession))]
        static class SocialModule_CanConfession_Patch
        {
            public static void Postfix(ref SocialFailType failureType, ref bool __result)
            {
                if (!modEnabled.Value || __result)
                    return;
                if (failureType == SocialFailType.Confession_Marriage_Other)
                {
                    Dbgl("Overriding already married for confession");
                    failureType = SocialFailType.None;
                    __result = true;
                }
                else if (failureType == SocialFailType.Confession_Divorce)
                {
                    Dbgl("Overriding divorced for confession");
                    failureType = SocialFailType.None;
                    __result = true;
                }
            }
        }

        [HarmonyPatch(typeof(SocialModule), nameof(SocialModule.CanPropose))]
        static class SocialModule_CanPropose_Patch
        {
            public static void Postfix(ref SocialFailType failureType, ref bool __result)
            {
                if (!modEnabled.Value || __result)
                    return;
                if (failureType == SocialFailType.Propose_Marriage_Other)
                {
                    Dbgl("Overriding already married for proposal");
                    failureType = SocialFailType.None;
                    __result = true;
                }
            }
        }

        [HarmonyPatch(typeof(SocialModule), nameof(SocialModule.DoBreakupAll))]
        static class SocialModule_DoBreakupAll_Patch
        {
            public static bool Prefix()
            {
                if (!modEnabled.Value)
                    return true;
                Dbgl("Preventing breakup all");
                return false;
            }
        }
        
        [HarmonyPatch(typeof(SocialModule), nameof(SocialModule.DoEngagementJealous))]
        static class SocialModule_DoEngagementJealous_Patch
        {
            public static bool Prefix()
            {
                if (!modEnabled.Value)
                    return true;
                Dbgl("Preventing engagement jealousy");
                return false;
            }
        }
        
        [HarmonyPatch(typeof(SocialModule), nameof(SocialModule.AddEngagementJealous))]
        static class SocialModule_AddEngagementJealous_Patch
        {
            public static bool Prefix()
            {
                if (!modEnabled.Value)
                    return true;
                Dbgl("Preventing engagement jealousy");
                return false;
            }
        }
        
        
        [HarmonyPatch(typeof(Engagement), nameof(Engagement.DoJealous))]
        static class Engagement_DoJealous_Patch
        {
            public static bool Prefix()
            {
                if (!modEnabled.Value)
                    return true;
                Dbgl("Preventing engagement jealousy");
                return false;
            }
        }
        
        
        [HarmonyPatch(typeof(Engagement), nameof(Engagement.AddJealous))]
        static class Engagement_AddJealous_Patch
        {
            public static bool Prefix()
            {
                if (!modEnabled.Value)
                    return true;
                Dbgl("Preventing engagement jealousy");
                return false;
            }
        }
        
        
        [HarmonyPatch(typeof(Engagement), nameof(Engagement.CanJealous), new Type[] { typeof(int) })]
        static class Engagement_CanJealous_Patch
        {
            public static bool Prefix()
            {
                if (!modEnabled.Value)
                    return true;
                Dbgl("Preventing engagement jealousy");
                return false;
            }
        }
        
        [HarmonyPatch(typeof(Engagement), nameof(Engagement.CanJealous), new Type[] { })]
        static class Engagement_CanJealous_Patch2
        {
            public static bool Prefix()
            {
                if (!modEnabled.Value)
                    return true;
                Dbgl("Preventing engagement jealousy");
                return false;
            }
        }
        
        [HarmonyPatch(typeof(Engagement), "TryJealous")]
        static class Engagement_TryJealous_Patch
        {
            public static bool Prefix()
            {
                if (!modEnabled.Value)
                    return true;
                Dbgl("Preventing engagement jealousy");
                return false;
            }
        }
        
        [HarmonyPatch(typeof(EngagementData), nameof(EngagementData.IsJealous))]
        static class Engagement_IsJealous_Patch
        {
            public static void Postfix(EngagementData __instance, int npcId, ref bool __result)
            {
                if (!modEnabled.Value || !__result)
                    return;

                Dbgl("Removing jealousy");
                __instance.ClearJealous(npcId);
                __result = false;
            }
        }

    }
}
