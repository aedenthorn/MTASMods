using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using Pathea;
using Pathea.ActorNs;
using Pathea.BedSelect;
using Pathea.DistributeChannelNs;
using Pathea.FrameworkNs;
using Pathea.InteractionNs;
using Pathea.InteractiveNs;
using Pathea.NpcNs;
using Pathea.SocialNs;
using Pathea.SocialNs.EngagementNs;
using Pathea.SocialNs.MarriageNs;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace FreeLove
{
    [BepInPlugin("aedenthorn.FreeLove", "FreeLove", "0.2.1")]
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
                else if (failureType == SocialFailType.Confession_Lover_Exist)
                {
                    Dbgl("Overriding has lover for confession");
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
        
        [HarmonyPatch(typeof(ChannelMgr), nameof(ChannelMgr.EnablePloygamy))]
        static class ChannelMgr_EnablePloygamy_Patch
        {
            public static bool Prefix(ref bool __result)
            {
                if (!modEnabled.Value)
                    return true;
                __result = true;
                return false;
            }
        }
        [HarmonyPatch(typeof(ChannelMgr), nameof(ChannelMgr.IsHomosexualEnabled))]
        static class ChannelMgr_IsHomosexualEnabled_Patch
        {
            public static bool Prefix(ref bool __result)
            {
                if (!modEnabled.Value)
                    return true;
                __result = true;
                return false;
            }
        }
        [HarmonyPatch(typeof(ChannelMgr), nameof(ChannelMgr.Is18XEnabled))]
        static class ChannelMgr_Is18XEnabled_Patch
        {
            public static bool Prefix(ref bool __result)
            {
                if (!modEnabled.Value)
                    return true;
                __result = true;
                return false;
            }
        }
        [HarmonyPatch(typeof(SocialModule), nameof(SocialModule.IsMarriage), new Type[] { typeof(int) })]
        static class SocialModule_IsMarriage_Patch
        {
            public static bool Prefix(int npcId, ref bool __result)
            {
                if (!modEnabled.Value || Module<SocialModule>.Self.GetSocialType(npcId) != SocialType.Mate)
                    return true;
                __result = true;
                return false;
            }
        }
        [HarmonyPatch(typeof(BedSelectModule), nameof(BedSelectModule.GetAllBedSelectNPCIds))]
        static class BedSelectModule_GetAllBedSelectNPCIds_Patch
        {
            public static void Postfix(List<NPCInfo> __result)
            {
                if (!modEnabled.Value)
                    return;
                Module<NpcMgr>.Self.ForeachNpc(delegate (Npc npc)
                {
                    if (Module<SocialModule>.Self.GetSocialType(npc.id) == SocialType.Mate)
                    {
                        __result.Add(new NPCInfo
                        {
                            npcId = npc.id,
                            isAdult = true,
                            isPlayer = false
                        });
                    }
                });
            }
        }
        [HarmonyPatch(typeof(NpcInteractionManager), nameof(NpcInteractionManager.Talk2Actor))]
        static class NpcInteractionManager_Talk2Actor_Patch
        {
            public static void Prefix(int actorId)
            {
                if (!modEnabled.Value || Module<SocialModule>.Self.GetSocialType(actorId) != SocialType.Mate)
                    return;
                Dbgl("Setting spouse to current NPC");
                AccessTools.FieldRefAccess<SocialModule, MarriageData>(Module<SocialModule>.Self, "mMarriageData").npcId = actorId;
            }
        }
        [HarmonyPatch(typeof(InteractiveMgr), "GetTargetConfig")]
        static class InteractiveMgr_GetTargetConfig_Patch
        {
            public static void Postfix(InteractiveTargetConfig __result)
            {
                if (!modEnabled.Value)
                    return;
                if(__result.lvlMax <= SocialLevel.MateHappiest)
                {
                    __result.lvlMax = SocialLevel.MateHappiest;
                }
            }
        }
        [HarmonyPatch(typeof(UpdateMgr), nameof(UpdateMgr.Update))]
        static class UpdateMgr_Update_Patch
        {
            public static void Postfix()
            {
                if (!modEnabled.Value)
                    return;

                if (Input.GetKeyDown(KeyCode.RightBracket))
                {
                    Dbgl("Pressed hotkey 1");
                    Module<NpcMgr>.Self.ForeachNpc(delegate (Npc npc)
                    {
                        Module<SocialModule>.Self.AddSocialFavor(npc.id, 100);
                    });
                }
                else if (Input.GetKeyDown(KeyCode.LeftBracket))
                {
                    Dbgl("Pressed hotkey 2");
                    Module<Player>.Self.bag.ChangeGold(1000);
                }
            }
        }

    }
}
