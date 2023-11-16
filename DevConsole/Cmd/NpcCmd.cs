using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Commonder;
using Pathea.ActionNs;
using Pathea.ActorNs;
using Pathea.BehaviorNs;
using Pathea.EquipmentNs;
using Pathea.FrameworkNs;
using Pathea.ItemNs;
using Pathea.NpcNs;
using Pathea.NpcTaskNs;
using Pathea.ScenarioNs;
using Pathea.SocialNs;
using Pathea.TimeNs;
using UnityEngine;

namespace Pathea
{
    public class NpcCmd : MonoBehaviour, ICmd
    {
        public void Awake()
        {
            objStopAI = new object();
            Cmd.Instance.Register(this);
        }

        public void OnDestroy()
        {
            Cmd.Instance.ClearAll();
        }

        [Command("Npc", "NpcHide", "隐藏NPC", false)]
        public void NpcHide(int npcId)
        {
            Module<NpcMgr>.Self.HideNpc(npcId);
        }

        [Command("Npc", "NpcGenRandom", "创建随机NPC", false)]
        public void NpcGenRandom(int protoId)
        {
            Npc npc = Module<NpcMgr>.Self.GenRandomNpc(protoId, -1, true, "");
            if (npc != null)
            {
                npc.actor.SetScene(Module<Player>.Self.actor.Scene, Module<Player>.Self.actor.GamePos, Vector3.zero);
            }
        }

        [Command("Npc", "NpcSetPlayerPos", "将NPC拉到玩家位置并停止AI", false)]
        public void NpcSetPlayerPos(int npcId, bool stopAI = false)
        {
            Npc npc = Module<NpcMgr>.Self.GetNpc(npcId);
            if (npc != null)
            {
                if (stopAI)
                {
                    npc.StopAI(objStopAI);
                }
                Vector3 gamePos = Module<Player>.Self.actor.GamePos;
                Vector3 eulerAngles = Module<Player>.Self.actor.GameRot.eulerAngles;
                npc.actor.TryDoAction(ActionType.Translate, ActionData.Translate(Module<ScenarioModule>.Self.CurScene, gamePos, eulerAngles, true));
            }
        }

        [Command("Npc", "NpcSetFaraway", "将NPC拉到Faraway", false)]
        public void NpcSetFaraway(int npcId)
        {
            Npc npc = Module<NpcMgr>.Self.GetNpc(npcId);
            if (npc != null)
            {
                npc.actor.SetScene(AdditiveScene.Faraway, npc.actor.GamePos, npc.actor.GameRot.eulerAngles);
            }
        }

        [Command("Npc", "AllNpcSetPlayerPos", "将所有NPC拉到玩家位置并停止AI", false)]
        public void AllNpcSetPlayerPos(bool stopAI = false)
        {
            NpcCmd.AiClass aiClass = new NpcCmd.AiClass();
            aiClass.stopAI = stopAI;
            aiClass.npcCmd = this;
            Module<NpcMgr>.Self.ForeachNpc(new Action<Npc>(aiClass.SetNpcToPlayer));
        }

        [Command("Npc", "NpcStopActionAll", "停止指定NPC的所有行为", false)]
        public void NpcStopActionAll(int npcId, bool immediately)
        {
            Npc npc = Module<NpcMgr>.Self.GetNpc(npcId);
            if (npc != null)
            {
                npc.actor.StopActionAll(immediately, true);
            }
        }

        [Command("Npc", "NpcAIStop", "停止NPC AI", false)]
        public void NpcStopAI(int npcId)
        {
            Npc npc = Module<NpcMgr>.Self.GetNpc(npcId);
            if (npc != null)
            {
                npc.StopAI(objStopAI);
            }
        }

        [Command("Npc", "NpcAIStart", "启动NPC AI", false)]
        public void NpcStartAI(int npcId)
        {
            Npc npc = Module<NpcMgr>.Self.GetNpc(npcId);
            if (npc != null)
            {
                npc.RemoveStopAI(objStopAI);
            }
        }

        [Command("Npc", "NpcAIAddBehavior", "添加NPC行为", false)]
        public void NpcAIAddBehavior(int npcId, string behaviorAlter, string behavior)
        {
            Npc npc = Module<NpcMgr>.Self.GetNpc(npcId);
            BehaviorAlter alter;
            if (npc != null && !string.IsNullOrEmpty(behavior) && Enum.TryParse<BehaviorAlter>(behaviorAlter, out alter))
            {
                npc.AddBehavior(alter, behavior, null, BehaviorLabel.None);
            }
        }

        [Command("Npc", "NpcAIAddBehaviorParas", "添加NPC行为", false)]
        public void NpcAIAddBehaviorParas(int npcId, string behaviorAlter, string behavior, string paras)
        {
            Npc npc = Module<NpcMgr>.Self.GetNpc(npcId);
            BehaviorAlter alter;
            if (npc != null && !string.IsNullOrEmpty(behavior) && Enum.TryParse<BehaviorAlter>(behaviorAlter, out alter))
            {
                Hashtable hashtable = null;
                if (!string.IsNullOrEmpty(paras))
                {
                    hashtable = new Hashtable();
                    string[] array = paras.Split(';');
                    for (int i = 0; i < array.Length; i++)
                    {
                        string[] array2 = array[i].Split(':');
                        if (array2.Length == 2)
                        {
                            hashtable.Add(array2[0], array2[1]);
                        }
                    }
                }
                npc.AddBehavior(alter, behavior, hashtable, BehaviorLabel.None);
            }
        }

        [Command("Npc", "NpcAIRemoveBehavior", "添加NPC行为", false)]
        public void NpcAIRemoveBehavior(int npcId, string behaviorAlter, string behavior)
        {
            Npc npc = Module<NpcMgr>.Self.GetNpc(npcId);
            BehaviorAlter alter;
            if (npc != null && !string.IsNullOrEmpty(behavior) && Enum.TryParse<BehaviorAlter>(behaviorAlter, out alter))
            {
                npc.RemoveBehavior(alter, behavior);
            }
        }

        [Command("Npc", "NpcAIAddOrder", "添加NPC命令行为", false)]
        public void NpcAIAddOrder(int npcId, string behavior)
        {
            Npc npc = Module<NpcMgr>.Self.GetNpc(npcId);
            if (npc != null && !string.IsNullOrEmpty(behavior))
            {
                npc.AddBehavior(BehaviorAlter.Order, behavior, null, BehaviorLabel.None);
            }
        }

        [Command("Npc", "NpcAIAddOrderParas", "添加NPC命令行为", false)]
        public void NpcAIAddOrderParas(int npcId, string behavior, string paras)
        {
            Npc npc = Module<NpcMgr>.Self.GetNpc(npcId);
            if (npc != null && !string.IsNullOrEmpty(behavior))
            {
                Hashtable hashtable = null;
                if (!string.IsNullOrEmpty(paras))
                {
                    hashtable = new Hashtable();
                    string[] array = paras.Split(';');
                    for (int i = 0; i < array.Length; i++)
                    {
                        string[] array2 = array[i].Split(':');
                        if (array2.Length == 2)
                        {
                            hashtable.Add(array2[0], array2[1]);
                        }
                    }
                }
                npc.AddBehavior(BehaviorAlter.Order, behavior, hashtable, BehaviorLabel.None);
            }
        }

        [Command("Npc", "NpcAIRemoveOrder", "删除NPC命令行为", false)]
        public void NpcAIRemoveOrder(int npcId, string behavior)
        {
            Npc npc = Module<NpcMgr>.Self.GetNpc(npcId);
            if (npc != null && !string.IsNullOrEmpty(behavior))
            {
                npc.RemoveBehavior(BehaviorAlter.Order, behavior);
            }
        }

        [Command("Npc", "NpcAIAddMission", "添加Npc剧情行为", false)]
        public void NpcAIAddMission(int npcId, string behaviourName)
        {
            Npc npc = Module<NpcMgr>.Self.GetNpc(npcId);
            if (npc != null)
            {
                npc.AddBehavior(BehaviorAlter.Mission, behaviourName, null, BehaviorLabel.None);
                return;
            }
            global::Debug.LogError("not exist npc: " + npcId.ToString());
        }

        [Command("Npc", "NpcAIAddMissionParas", "添加Npc剧情行为", false)]
        public void NpcAIAddMissionParas(int npcId, string behavior, string paras)
        {
            Npc npc = Module<NpcMgr>.Self.GetNpc(npcId);
            if (npc != null && !string.IsNullOrEmpty(behavior))
            {
                Hashtable hashtable = null;
                if (!string.IsNullOrEmpty(paras))
                {
                    hashtable = new Hashtable();
                    string[] array = paras.Split(';');
                    for (int i = 0; i < array.Length; i++)
                    {
                        string[] array2 = array[i].Split(':');
                        if (array2.Length == 2)
                        {
                            hashtable.Add(array2[0], array2[1]);
                        }
                    }
                }
                npc.AddBehavior(BehaviorAlter.Mission, behavior, hashtable, BehaviorLabel.None);
            }
        }

        [Command("Npc", "NpcAIRemoveMission", "删除Npc剧情行为", false)]
        public void NpcAIRemoveMission(int npcId, string behaviourName)
        {
            Npc npc = Module<NpcMgr>.Self.GetNpc(npcId);
            if (npc != null)
            {
                npc.RemoveBehavior(BehaviorAlter.Mission, behaviourName);
                return;
            }
            global::Debug.LogError("not exist npc: " + npcId.ToString());
        }

        [Command("Npc", "NpcAIBackTown", "命令NPC回到沙石镇", false)]
        public void NpcAIBackTown(int npcId, bool hide)
        {
            if (hide)
            {
                Module<NpcMgr>.Self.HideNpc(npcId);
            }
            Module<NpcMgr>.Self.TryBackTown(npcId);
        }

        [Command("Npc", "NpcAILeaveTown", "命令NPC离开城镇", false)]
        public void NpcAILeaveTown(int npcId)
        {
            Module<NpcMgr>.Self.TryLeaveTown(npcId);
        }

        [Command("Npc", "ForceDestroyRandomNpc", "强制删除随机Npc", false)]
        public void ForceDestroyRandomNpc()
        {
            Module<NpcMgr>.Self.ForceDestroyRandomNpc();
        }

        [Command("Npc", "NpcTaskSendGift", "开启Npc送礼任务", false)]
        public void NpcTaskSendGift(int npcId, int giftId)
        {
            GameDateTime start = Module<TimeModule>.Self.CurrentTime;
            start = new GameDateTime(start.Year, start.Month, start.Day, 7, 0, 0).AddDays(1.0);
            GameDateTime end = start.AddHours(1.0);
            GameTimeSpan timeSpan = new GameTimeSpan(1, 0, 0, 0);
            Module<NpcTaskMgr>.Self.AddTask(new NpcTask_SendGift(-1, npcId, BehaviorAlter.Daily, "NpcTask_SendGift", giftId, "BirthdayPartyGiftArea", start, end, timeSpan, NpcTaskFlag.None));
        }

        [Command("Npc", "NpcEquip", "向NPC添加装备", false)]
        public void NpcEquip(int npcId, int equipmentId)
        {
            Module<NpcMgr>.Self.GetNpc(npcId).PutEquip(Module<ItemInstance.Module>.Self.CreateAsDefault(equipmentId, 1));
        }

        [Command("Npc", "NpcUnequip", "卸除NPC的装备,0为Head,1为Body,2为Foor,3为Shoes", false)]
        public void NpcUnequip(int npcId, int partId)
        {
            switch (partId)
            {
                case 0:
                    Module<NpcMgr>.Self.GetNpc(npcId).PutUnequip(EquipPart.Head);
                    return;
                case 1:
                    Module<NpcMgr>.Self.GetNpc(npcId).PutUnequip(EquipPart.Body);
                    return;
                case 2:
                    Module<NpcMgr>.Self.GetNpc(npcId).PutUnequip(EquipPart.Foot);
                    return;
                case 3:
                    Module<NpcMgr>.Self.GetNpc(npcId).PutUnequip(EquipPart.Shoes);
                    return;
                default:
                    return;
            }
        }

        [Command("Npc", "NpcUnequipAccessory", "卸除NPC的装饰品", false)]
        public void NpcUnequipAccessory(int npcId, int partId)
        {
            Module<NpcMgr>.Self.GetNpc(npcId).UnequipAccessory((AccessoryPart)partId);
        }

        [Command("Npc", "NpcRemoveAccessoryModelByPart", "基于部位隐藏饰品模型", false)]
        public void NpcStronglyRemoveAccessory(int npcId, int partId)
        {
            Module<NpcMgr>.Self.GetNpc(npcId).RemoveAccessoryModelByPart((AccessoryPart)partId);
        }

        [Command("Npc", "NpcRecoverAccessoryModelByPart", "基于部位恢复隐藏饰品模型", false)]
        public void RecoverAccessoryModelByPart(int npcId, int partId)
        {
            Module<NpcMgr>.Self.GetNpc(npcId).RecoverAccessoryModelByPart((AccessoryPart)partId);
        }

        [Command("Npc", "NpcGetRemoveParts", "获取列表内容", false)]
        public void NpcGetRemoveParts(int npcId)
        {
            Module<NpcMgr>.Self.GetNpc(npcId).GetRemoveParts();
        }

        [Command("Npc", "NpcAddFlag", "添加Npc Flag", false)]
        public void NpcAddFlag(int npcId, string flagString)
        {
            Module<NpcMgr>.Self.GetNpc(npcId).AddFlag(flagString);
        }

        [Command("Npc", "NpcRemoveFlag", "删除Npc Flag", false)]
        public void NpcRemoveFlag(int npcId, string flagString)
        {
            Module<NpcMgr>.Self.GetNpc(npcId).RemoveFlag(flagString);
        }

        [Command("Npc", "NpcAICheckBehaviorMissing", "检查NPC是否丢失行为树", false)]
        public void NpcAICheckBehaviorMissing(int npcId)
        {
            Module<NpcMgr>.Self.GetNpc(npcId).CheckBehaviorMissing();
        }

        [Command("Npc", "NpcAICheckBehaviorMissingAll", "检查所有NPC是否丢失行为树", false)]
        public void NpcAICheckBehaviorMissingAll()
        {
            Module<NpcMgr>.Self.ForeachStoryNpc(delegate (Npc npc)
            {
                npc.CheckBehaviorMissing();
            });
        }

        [Command("Npc", "NpcRelpaceSuit", "NPC换装", false)]
        public void NpcRelpaceSuit(int npcSuitID)
        {
            Module<NpcReplaceSuitModule>.Self.NpcRelpaceSuit(npcSuitID);
        }

        [Command("Npc", "NpcReplaceIcon", "NPC换头像", false)]
        public void NpcRelpaceIconProto(int npcID, int protoID)
        {
            Module<NpcMgr>.Self.GetNpc(npcID).SetIconReplaceProto(protoID);
        }

        [Command("Npc", "NpcAddNpcRandomLocker", "添加屏蔽随机NPC", false)]
        public void NpcAddNpcRandomLocker(int id, int[] npcIds)
        {
            Module<NpcMgr>.Self.AddRandomNpcLocker(id, npcIds);
        }

        [Command("Npc", "NpcRemoveNpcRandomLocker", "删除屏蔽随机NPC", false)]
        public void NpcRemoveNpcRandomLocker(int id)
        {
            Module<NpcMgr>.Self.RemoveRandomNpcLocker(id);
        }

        [Command("Npc", "NpcChangeSuit", "NPC换装", false)]
        public void NpcChangeSuit(int id)
        {
            Module<NpcReplaceSuitModule>.Self.NpcRelpaceSuit(id);
        }

        [Command("Npc", "ShowChildRenameDialog", "Show child rename dialog", false)]
        public void ShowChildRenameDialog(int id)
        {
            Module<SocialModule>.Self.ShowChildRenameDialog(id, delegate { });
        }

        public NpcCmd()
        {
        }

        public Rect rect;

        public object objStopAI;

        public class AiClass
        {
            public AiClass()
            {
            }

            public void SetNpcToPlayer(Npc npc)
            {
                if (npc != null)
                {
                    if (stopAI)
                    {
                        npc.StopAI(npcCmd.objStopAI);
                    }
                    Vector3 gamePos = Module<Player>.Self.actor.GamePos;
                    Vector3 eulerAngles = Module<Player>.Self.actor.GameRot.eulerAngles;
                    npc.actor.TryDoAction(ActionType.Translate, ActionData.Translate(Module<ScenarioModule>.Self.CurScene, gamePos, eulerAngles, true));
                }
            }

            public bool stopAI;

            public NpcCmd npcCmd;
        }

    }
}
