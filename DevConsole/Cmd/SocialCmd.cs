using System;
using Commonder;
using Pathea.FrameworkNs;
using Pathea.HomeNs;
using Pathea.InteractionNs;
using Pathea.SocialNs;
using Pathea.SocialNs.EngagementNs;
using Pathea.SocialNs.PartyNs;
using Pathea.SocialNs.PK;
using UnityEngine;

namespace Pathea
{
    public class SocialCmd : MonoBehaviour, ICmd
    {
        public void Start()
        {
            Cmd.Instance.Register(this);
        }

        public void OnDestroy()
        {
            Cmd.Instance.ClearAll();
        }

        [Command("Social", "SocialConfession", "表白", false)]
        public void SocialConfession(int npcId)
        {
            if (Module<SocialModule>.Self.IsLover(npcId))
            {
                return;
            }
            if (Module<SocialModule>.Self.IsMarriage())
            {
                Module<SocialModule>.Self.DoDivorce(false);
            }
            if (Module<SocialModule>.Self.IsPropose())
            {
                Module<SocialModule>.Self.DoProposeRegret();
            }
            Module<SocialModule>.Self.ClearBreakup(npcId);
            Module<SocialModule>.Self.ClearDivorce(npcId);
            Module<SocialModule>.Self.AddKnownNpc(npcId, true);
            Module<SocialModule>.Self.DoConfession(npcId);
        }

        [Command("Social", "SocialPropose", "求婚", false)]
        public void SocialPropose(int npcId)
        {
            if (Module<SocialModule>.Self.IsPropose(npcId))
            {
                return;
            }
            Module<SocialModule>.Self.DoPropose(npcId);
        }

        [Command("Social", "SocialMarriage", "结婚", false)]
        public void SocialMarriage(int npcId)
        {
            if (Module<SocialModule>.Self.IsMarriage(npcId))
            {
                return;
            }
            Module<SocialModule>.Self.DoMarriage(npcId);
        }

        [Command("Social", "SocialMarriageMission", "设置是否已完成婚礼任务", false)]
        public void SocialMarriageMission(bool hasMissionComplete)
        {
            Module<SocialModule>.Self.DoMarriageMission(hasMissionComplete);
        }

        [Command("Social", "SocialWedding", "婚礼", false)]
        public void SocialWedding(int npcId)
        {
            Module<SocialModule>.Self.DoWedding();
        }

        [Command("Social", "SocialBreakup", "分手", false)]
        public void SocialBreakup(int npcId, bool changeType)
        {
            if (Module<SocialModule>.Self.IsLover(npcId))
            {
                Module<SocialModule>.Self.DoBreakup(npcId, changeType, false);
            }
        }

        [Command("Social", "SocialBreakupAll", "分手所有", false)]
        public void SocialBreakupAll()
        {
            Module<SocialModule>.Self.DoBreakupAll();
        }

        [Command("Social", "SocialDivorce", "离婚", false)]
        public void SocialDivorce()
        {
            if (Module<SocialModule>.Self.IsMarriage())
            {
                Module<SocialModule>.Self.DoDivorce(false);
            }
        }

        [Command("Social", "SocialAddKnownNpc", "修改好感度", false)]
        public void SocialAddKnownNpc(int npcId)
        {
            if (!Module<SocialModule>.Self.IsKnownNpc(npcId))
            {
                Module<SocialModule>.Self.AddKnownNpc(npcId, false);
            }
        }

        [Command("Social", "SocialAddKnownNpcAll", "认识所有NPC", false)]
        public void SocialAddKnownNpcAll()
        {
            Module<SocialModule>.Self.AddKnownNpcAll();
        }

        [Command("Social", "SocialAddFavor", "修改好感度", false)]
        public void SocialAddFavor(int npcId, int favor)
        {
            if (!Module<SocialModule>.Self.IsKnownNpc(npcId))
            {
                Module<SocialModule>.Self.AddKnownNpc(npcId, false);
            }
            Module<SocialModule>.Self.AddSocialFavor(npcId, favor, true);
        }

        [Command("Social", "SocialAddFavorAll", "修改好感度", false)]
        public void SocialAddFavorAll(int favor)
        {
            Module<SocialModule>.Self.AddSocialFavorAll(favor);
        }

        [Command("Social", "SocialSetFavorType", "修改NPC关系类型", false)]
        public void SocialSetFavorType(int npcId, SocialType type)
        {
            Module<SocialModule>.Self.SetSocialType(npcId, type);
        }

        [Command("Social", "SocialSetFavorLevel", "修改NPC关系等级", false)]
        public void SocialSetFavorLevel(int npcId, SocialLevel level, float lerp)
        {
            Module<SocialModule>.Self.SetSocialLevel(npcId, level, lerp);
        }

        [Command("Social", "SocialInvitePartyNpc", "邀请NPC参加聚会", false)]
        public void SocialInvitePartyNpc(int npcId)
        {
            Module<SocialModule>.Self.DoInvitePartyNpc(npcId, PartyType.None);
        }

        [Command("Social", "SocialAddPartyFood", "添加食物", false)]
        public void SocialAddPartyFood(int idx, int itemId)
        {
            int allUnitCount = Unit<CabinetUnit>.AllUnitCount;
            if (idx >= 0 && idx < allUnitCount)
            {
                CabinetUnit unit = Unit<CabinetUnit>.GetUnit(idx);
                if (unit is PartyTableUnit)
                {
                    (unit as PartyTableUnit).AddFood(itemId);
                }
            }
        }

        [Command("Social", "SocialDoEngagement", "开启约会", false)]
        public void SocialDoEngagement(int npcId, bool isDate, int power, bool debug)
        {
            if (!isDate && Module<SocialModule>.Self.GetSocialLevel(npcId) < SocialLevel.Friend)
            {
                Module<SocialModule>.Self.SetSocialLevel(npcId, SocialLevel.Friend, 1f);
            }
            if (isDate && Module<SocialModule>.Self.GetSocialLevel(npcId) < SocialLevel.Lover)
            {
                Module<SocialModule>.Self.SetSocialLevel(npcId, SocialLevel.Lover, 1f);
            }
            EGFlag flag = EGFlag.None;
            if (debug)
            {
                flag = (EGFlag)7;
            }
            Module<SocialModule>.Self.DoEngagement(npcId, isDate, power, flag);
        }

        [Command("Social", "SocialStopEngagement", "终止约会", false)]
        public void SocialStopEngagement(EGStopType stopType)
        {
            Module<SocialModule>.Self.StopEngagement(stopType);
        }

        [Command("Social", "SocialTryEngagementEvent", "尝试开启约会事件", false)]
        public bool SocialTryEngagementEvent(string eventName, bool isPlayerStart)
        {
            return Module<SocialModule>.Self.CanEngagementEvent() && NpcInteractionUtils.CanShowConversation() && Module<SocialModule>.Self.TryEngagementEvent(eventName, isPlayerStart);
        }

        [Command("Social", "SocialDoEngagementEvent", "开启约会事件", false)]
        public void SocialDoEngagementEvent(string eventName)
        {
            if (Module<SocialModule>.Self.CanEngagementEvent())
            {
                Module<SocialModule>.Self.DoEngagementEvent(eventName);
            }
        }

        [Command("Social", "SocialStopEngagementEvent", "终止约会事件", false)]
        public void SocialStopEngagementEvent()
        {
            Module<SocialModule>.Self.StopEngagementEvent();
        }

        [Command("Social", "SocialDoEngagementJealous", "NPC吃醋", false)]
        public void SocialDoEngagementJealous(int jealousId, bool isCheck)
        {
            if (!isCheck || Module<SocialModule>.Self.CanEngagementJealous(jealousId))
            {
                Module<SocialModule>.Self.DoEngagementJealous(jealousId);
            }
        }

        [Command("Social", "SocialAddJealous", "NPC吃醋", false)]
        public void SocialAddJealous(int jealousId)
        {
            Module<SocialModule>.Self.AddEngagementJealous(jealousId);
        }

        [Command("Social", "SocialAddNpcRelationModifier", "添加NPC关系修改", true)]
        public int SocialAddNpcRelationModifier(int instId, int npcId1, int npcId2, ModifierNpcRelationType type, SocialRelation relation)
        {
            return Module<SocialModule>.Self.AddNpcRelationModifier(instId, npcId1, npcId2, type, relation);
        }

        [Command("Social", "SocialAddNpcRelationMaskModifier", "添加NPC关系标签修改", true)]
        public int SocialAddNpcRelationMaskModifier(int instId, int npcId1, int npcId2, ModifierNpcRelationType type, SocialRelationMask mask)
        {
            return Module<SocialModule>.Self.AddNpcRelationModifier(instId, npcId1, npcId2, type, mask);
        }

        [Command("Social", "SocialRemoveNpcRelationModifier", "删除NPC关系修改", true)]
        public bool SocialRemoveNpcRelationModifier(int instId, int npcId1, int npcId2)
        {
            return Module<SocialModule>.Self.RemoveNpcRelationModifier(instId, npcId1, npcId2);
        }

        [Command("Social", "SocialShowRelation", "显示NPC关系", true)]
        public string SocialShowRelation(int npcId1, int npcId2)
        {
            return Module<SocialModule>.Self.GetNpcRelationString(npcId1, npcId2);
        }

        [Command("Social", "SocialStartPK", "启动PK", false)]
        public void SocialStartPK(int[] sponsorId, int[] recipientId, string fieldName)
        {
            Module<SocialModule>.Self.StartPK(sponsorId, recipientId, fieldName, PKFlag.None, 180f);
        }

        [Command("Social", "SocialUnlockBirthday", "解锁生日显示", false)]
        public void SocialUnlockBirthday(int npcId)
        {
            Module<SocialModule>.Self.AddSocialMask(npcId, SocialMask.UnlockBirthday);
        }

        [Command("Social", "SocialUnlockBirthdayAll", "解锁所有生日显示", false)]
        public void SocialUnlockBirthdayAll()
        {
            Module<SocialModule>.Self.AddSocialMaskAll(SocialMask.UnlockBirthday);
        }

        [Command("Social", "SocialShowWeddingEdit", "婚礼时间显示", false)]
        public void SocialShowWeddingEdit()
        {
            Module<SocialModule>.Self.OnMarriageTriggerInvoke(null);
        }

        [Command("Social", "SocialStoryLock", "婚姻系统Lock控制", false)]
        public void SocialLockControl(int type, bool state, int translateID)
        {
            Module<SocialModule>.Self.SetLockState(type, state, translateID);
        }

        [Command("Social", "SocialStopParty", "结束聚会", false)]
        public void SocialStopParty(string stopType, bool immediately, bool showTip)
        {
            PartyStopType stopType2;
            if (Enum.TryParse<PartyStopType>(stopType, out stopType2))
            {
                Module<SocialModule>.Self.StopParty(stopType2, immediately, showTip);
            }
        }

        [Command("Social", "SocialAddLeaveNpc", "Npc离开事件", false)]
        public void SocialAddLeaveNpc(int npcid)
        {
            Module<SocialModule>.Self.AddLeaveNpc(npcid);
        }

        public SocialCmd()
        {
        }
    }
}
