using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using BreedStore;
using Commonder;
using InputMethodNs;
using Pathea.AchievementNs;
using Pathea.ActorNs;
using Pathea.AdventureMachineNs;
using Pathea.AnimalCardFight;
using Pathea.Attr;
using Pathea.Audios;
using Pathea.BehaviorNs;
using Pathea.BlackboardNs;
using Pathea.BluePrintNs;
using Pathea.BlueprintResearchNs;
using Pathea.BubbleDragon;
using Pathea.CamCaptureNs;
using Pathea.CampsiteNs;
using Pathea.CompareGame;
using Pathea.ConversationNs;
using Pathea.CookingNs;
using Pathea.CreationNs;
using Pathea.CustomPlayer;
using Pathea.CutsceneNs;
using Pathea.DanceNs;
using Pathea.Delegation;
using Pathea.DeviceAdaptorNs;
using Pathea.DistributeChannelNs;
using Pathea.DlcNs;
using Pathea.DramaNs;
using Pathea.DrawlineNs;
using Pathea.DropItemTaskNs;
using Pathea.DynamicWishNs;
using Pathea.EquipmentNs;
using Pathea.FavorSystemNs;
using Pathea.FestivalNs;
using Pathea.FestivalNs.GiftNs;
using Pathea.FireworkNs;
using Pathea.FrameworkNs;
using Pathea.FunctionLockNs;
using Pathea.GameCenters;
using Pathea.GearNs;
using Pathea.GetItemNs;
using Pathea.GuideNs;
using Pathea.GuildRanking;
using Pathea.HandBookNs;
using Pathea.HomeNs;
using Pathea.HomeViewerNs;
using Pathea.IllustrationNs;
using Pathea.Indicators;
using Pathea.InfoTip;
using Pathea.InteractionCameraNs;
using Pathea.InteractionNs;
using Pathea.ItemAttrNs;
using Pathea.ItemBoxNs;
using Pathea.ItemContainers;
using Pathea.ItemNs;
using Pathea.MachineNs;
using Pathea.MagicMirror;
using Pathea.MailNs;
using Pathea.MapNs;
using Pathea.MatchModule;
using Pathea.MissionNs;
using Pathea.Mtas;
using Pathea.MuseumNs;
using Pathea.NaviMarks;
using Pathea.NewspaperNs;
using Pathea.NpcNs;
using Pathea.OnePunchGameNs;
using Pathea.OpenTreasureNs;
using Pathea.OptionNs;
using Pathea.OrderingFoodNs;
using Pathea.PatheaGDCNs;
using Pathea.PhotoAlbumNs;
using Pathea.Plants;
using Pathea.PlayerRidableNs;
using Pathea.ProficiencyNs;
using Pathea.RandomDungeonNs;
using Pathea.ReadingNs;
using Pathea.RepairTargetNs;
using Pathea.ResourcePointNs;
using Pathea.RestoreNs;
using Pathea.RidableHouseNs;
using Pathea.RideNs;
using Pathea.RideStoreNs;
using Pathea.SandFishingNs;
using Pathea.SandFishNs;
using Pathea.SandNs;
using Pathea.SaveNs;
using Pathea.ScenarioNs;
using Pathea.SceneInfoNs;
using Pathea.ScreenMaskNs;
using Pathea.SendGiftNs;
using Pathea.SleepNs;
using Pathea.SocialNs;
using Pathea.StarGazeNs;
using Pathea.StatisticsNs;
using Pathea.StoreNs;
using Pathea.StoryScript;
using Pathea.StoryScriptExt;
using Pathea.TimeNs;
using Pathea.TinyAnimalNs;
using Pathea.TreasureRevealerNs;
using Pathea.UISystemV2;
using Pathea.UISystemV2.UIControl;
using Pathea.UserReportings;
using Pathea.VoxelAimNs;
using Pathea.WeatherNs;
using Pathea.WhacMoleNs;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityExtensions;
using UtilNs;
using static AK.SWITCHES;

namespace Pathea
{
    public class MiscCmd : MonoBehaviour, ICmd
    {
        public void Start()
        {
            Cmd.Instance.Register(this);
        }

        public void OnDestroy()
        {
            Cmd.Instance.ClearAll();
        }

        [Command("TestLoganCountDown", "TestLoganCountDown", "测试洛根战斗倒计时", false)]
        public void LoganCountDown(bool flag, float time)
        {
            Module<TrialDungeonModule>.Self.OnTest(flag, time);
        }

        [Command("UserReport", "UserReport", "用户反馈", false)]
        public void UserReport()
        {
            Singleton<UserReportingActivator>.Instance.Open();
        }

        [Command("Airship", "StartAirShip", "测试黎明之日", false)]
        public void StartAirShip()
        {
            string data = string.Format("{0},{1}", Module<TimeModule>.Self.CurrentTime.Month, Module<TimeModule>.Self.CurrentTime.Day + 1);
            Module<FestivalModule>.Self.AddFestivalModifier(FestivalEnum.BrightSun, FestivalModifierType.Time, data);
            Module<FestivalModule>.Self.AddFestivalModifier(FestivalEnum.BrightSun, FestivalModifierType.Enable, "true");
        }

        [Command("WinterFestival", "OpenWinterFestivalUI", "测试冬日节烧烤UI", false)]
        public void StartWinterFestivalUI()
        {
            Module<WinterFestivalModule>.Self.OpenUI();
        }

        [Command("WinterFestival", "EndWinterFestival", "剧情结束冬日节装扮", false)]
        public void EndWinterFestival()
        {
            Module<WinterFestivalModule>.Self.StoryEndFestival();
        }

        [Command("WinterFestival", "StartBBQActivity", "测试冬日节烧烤架", false)]
        public void StartWinterFestival()
        {
            Module<WinterFestivalModule>.Self.StartBBQActivity();
        }

        [Command("WinterFestival", "StartFestivalDance", "测试冬日节跳舞", false)]
        public void StartWinterFestivalDance()
        {
            Module<WinterFestivalModule>.Self.StartDanceActivity();
        }

        [Command("WinterFestival", "StartWinterDance", "测试冬日节跳舞UI", false)]
        public void StartWinterDance()
        {
            Module<WinterFestivalModule>.Self.StartDanceUI();
        }

        [Command("OnePunch", "StartOnePunch", "测试一拳排行", false)]
        public void StartOnePunch()
        {
            Module<OnePunchGameModule>.Self.OpenGame();
        }

        [Command("Achor", "AchorFuncRank", "测试主播排行榜", false)]
        public void AchorFuncRank(string CustomerName, string value)
        {
        }

        [Command("Hair", "TestHairConfig", "测试发型是否适配", false)]
        public void TestHairConfig(int id, int otherid)
        {
        }

        [Command("Hair", "UnlockCosmetic", "解锁整容", false)]
        public void UnlockCosmetic()
        {
            Module<CustomPlayerModule>.Self.UnlockCosmetic();
        }

        [Command("Indicator", "IndicatorTest", "测试Npc头上出现图像", false)]
        public void TestNpcIndicator(int id, string path, float time)
        {
            Module<NpcMgr>.Self.GetNpc(id).actor.ShowNpcImg(path, time);
        }

        [Command("Read", "ReadHobby", "测试周边UI", false)]
        public void TestReadHobby()
        {
            Module<ReadingModule>.Self.OpenHobby();
        }

        [Command("ECA", "TestMagicMirrorCosmeic", "测试魔镜整容", false)]
        public void TestMagicMirrorCosmetic()
        {
            Module<CustomPlayerMagicMirrorModule>.Self.StartEdit();
        }

        [Command("ECA", "OrderCountAdd", "测试增加玩家的可接取订单数量", false)]
        public void TestOrderAdd(int count)
        {
            Module<GuildRankingManager>.Self.AddPlayerOrderCount(count);
        }

        [Command("ECA", "EarlySleep", "测试玩家早睡", false)]
        public void TestEarlySleep(bool test)
        {
            Module<SleepModule>.Self.SetEcaEralySleepOption(test);
        }

        [Command("ECA", "DelegationDiscount", "测试委托折扣", false)]
        public void TestDelegationDiscount(float discount)
        {
            Module<DelegationModule>.Self.KnowledgeSystemDiscount = discount;
            Module<DelegationModule>.Self.RefreshDelegationData();
        }

        [Command("ECA", "StoreECATest", "测试商店大笔交易", false)]
        public void TestStoreTest()
        {
            Module<StoreModule>.Self.Test();
        }

        [Command("UI", "starGaze", "测试观星", false)]
        public void TestStarGaze()
        {
            Module<MiniGameModule>.Self.EnterMiniGame(Module<StarGazeModule>.Self, new MiniGameContext
            {
                isSingle = false
            }, true);
        }

        [Command("UI", "starGazeSingle", "测试观星单人", false)]
        public void TestStarGazeSingle()
        {
            Module<MiniGameModule>.Self.EnterMiniGame(Module<StarGazeModule>.Self, new MiniGameContext
            {
                isSingle = true
            }, true);
        }

        [Command("Match", "PkMatch", "测试比武比赛", false)]
        public void TestPkMatch()
        {
            Module<PkMatchModule>.Self.StartMatch();
        }

        [Command("Match", "PkMatchEnd", "测试比武比赛结束", false)]
        public void TestPkMatchEnd()
        {
            Module<PkMatchModule>.Self.EndMatch();
        }

        [Command("Match", "PkMatchUI", "测试比武比赛组队UI", false)]
        public void TestPkMatcUI()
        {
            Module<PkMatchModule>.Self.OpenUI();
        }

        [Command("Match", "PkMatchGuessing", "测试比武比赛竞猜UI", false)]
        public void TestPkMatcGuessingUI()
        {
            Module<PkMatchModule>.Self.OpenGussing();
        }

        [Command("Conversation", "AddTalkStack", "加入npc临时对话", false)]
        public void NpcAddTalkStack(int npcId, string dialogUnit)
        {
            Module<ConversationManager>.Self.AddNpcTalkStack(npcId, dialogUnit);
        }

        [Command("Conversation", "ShowDialog", "加入缓存对话", false)]
        public void AddCachedConversation(string dialogUnitStr)
        {
            DialogOne dialogOneFromUnitStr = DialogUnit.GetDialogOneFromUnitStr(dialogUnitStr, null, true);
            Module<ConversationManager>.Self.AddConversationToCache(dialogOneFromUnitStr, -1, true, -1, -1, null);
        }

        [Command("Conversation", "AddConversationToCache", "加入缓存对话", false)]
        public void AddConversationToCache(string dialogUnitStr, int actorID, int cId, int missionID)
        {
            Module<ConversationManager>.Self.AddConversationToCache(dialogUnitStr, actorID, true, cId, missionID, null, null, null);
        }

        [Command("Conversation", "AddLaterCachedConversation", "加入延迟缓存对话", false)]
        public void AddLaterCachedConversation(int npcId, string dialogUnit)
        {
            Module<ConversationManager>.Self.AddLaterCachedConversation(DialogUnit.GetDialogOneFromUnitStr(dialogUnit, null, true), npcId, true, -1, -1, null);
        }

        [Command("Conversation", "ShowSegmentBase", "直接加入对话内容到缓存", false)]
        public void AddSegmentBaseConversation(string content)
        {
            Module<ConversationManager>.Self.AddSegmentBaseToCache(new ConvSegmentBase(content));
        }

        [Command("Conversation", "ConversationWakeUp", "对话系统起床刷新", false)]
        public void ConversationWakeUp()
        {
            Module<ConversationManager>.Self.OnPlayerWakeUp();
        }

        [Command("Conversation", "TestDialog", "测试对话系统", false)]
        public void ConversationTest()
        {
            DialogOne dialogOneFromUnitStr = DialogUnit.GetDialogOneFromUnitStr("1", null, true);
            Module<ConversationManager>.Self.AddConversationToCache(dialogOneFromUnitStr, 8001, true, -1, -1, null);
            dialogOneFromUnitStr = DialogUnit.GetDialogOneFromUnitStr("2", null, true);
            Module<ConversationManager>.Self.AddConversationToCache(dialogOneFromUnitStr, 8001, true, -1, -1, null);
            dialogOneFromUnitStr = DialogUnit.GetDialogOneFromUnitStr("3", null, true);
            Module<ConversationManager>.Self.AddConversationToCache(dialogOneFromUnitStr, 8001, true, -1, -1, null);
            dialogOneFromUnitStr = DialogUnit.GetDialogOneFromUnitStr("4", null, true);
            Module<ConversationManager>.Self.AddConversationToCache(dialogOneFromUnitStr, 8001, true, -1, -1, null);
            dialogOneFromUnitStr = DialogUnit.GetDialogOneFromUnitStr("5", null, true);
            Module<ConversationManager>.Self.AddLaterCachedConversation(dialogOneFromUnitStr, 8001, true, -1, -1, null);
            dialogOneFromUnitStr = DialogUnit.GetDialogOneFromUnitStr("6", null, true);
            Module<ConversationManager>.Self.AddLaterCachedConversation(dialogOneFromUnitStr, 8001, true, -1, -1, null);
            dialogOneFromUnitStr = DialogUnit.GetDialogOneFromUnitStr("4", null, true);
            Module<ConversationManager>.Self.AddConversationToCache(dialogOneFromUnitStr, 8001, true, -1, -1, null);
            dialogOneFromUnitStr = DialogUnit.GetDialogOneFromUnitStr("5", null, true);
            Module<ConversationManager>.Self.AddLaterCachedConversation(dialogOneFromUnitStr, 8001, true, -1, -1, null);
        }

        [Command("Conversation", "TestDialogEnd", "测试对话系统结束事件", false)]
        public void ConversationTestEnd(int type)
        {
            ConvSegmentBase item = new ConvSegmentBase("你好");
            ConvSegmentBase convSegmentBase = new ConvSegmentBase("选择吧");
            if (type == 0 || type == 2)
            {
                ConvSegmentBase convSegmentBase2 = convSegmentBase;
                convSegmentBase2.OnClick = (Action<int>)Delegate.Combine(convSegmentBase2.OnClick, new Action<int>(delegate (int index)
                {
                    Module<ConversationManager>.Self.AddSegmentBaseToCache(new List<ConvSegmentBase>
                    {
                        new ConvSegmentBase("EndBeforeClear3")
                    });
                }));
                ConvSegmentBase convSegmentBase3 = convSegmentBase;
                convSegmentBase3.OnClick = (Action<int>)Delegate.Combine(convSegmentBase3.OnClick, new Action<int>(delegate (int index)
                {
                    Module<ConversationManager>.Self.AddSegmentBaseToCache(new List<ConvSegmentBase>
                    {
                        new ConvSegmentBase("EndBeforeClear4")
                    });
                }));
            }
            if (type == 1 || type == 2)
            {
                ConvSegmentBase convSegmentBase4 = convSegmentBase;
                convSegmentBase4.OnEnd = (Action<int>)Delegate.Combine(convSegmentBase4.OnEnd, new Action<int>(delegate (int index)
                {
                    Module<ConversationManager>.Self.AddSegmentBaseToCache(new List<ConvSegmentBase>
                    {
                        new ConvSegmentBase("EndEvent3")
                    });
                }));
                ConvSegmentBase convSegmentBase5 = convSegmentBase;
                convSegmentBase5.OnEnd = (Action<int>)Delegate.Combine(convSegmentBase5.OnEnd, new Action<int>(delegate (int index)
                {
                    Module<ConversationManager>.Self.AddSegmentBaseToCache(new List<ConvSegmentBase>
                    {
                        new ConvSegmentBase("EndEvent4")
                    });
                }));
            }
            Module<ConversationManager>.Self.AddSegmentBaseToCache(new List<ConvSegmentBase>
            {
                item,
                convSegmentBase
            });
        }

        [Command("Conversation", "ContinueDialog", "继续对话", false)]
        public void ConversationContinueEnd()
        {
            Module<ConversationManager>.Self.ContinueDialog();
        }

        [Command("Conversation", "TestDialogMgr", "测试对话管理", false)]
        public void TestDialogMgr()
        {
            List<string> choices = new List<string>
            {
                "选择1选择1选择1选择1选择1选择1选择1选择1选择1选择1选择1",
                "选择2选择2选择2选择2选择2",
                "选择3"
            };
            Singleton<DialogMgr>.Instance.EnterDialog("名字", "内容", choices, true, delegate (CallBackData index)
            {
            });
        }

        [Command("Conversation", "TestEventTalk", "测试事件对话", false)]
        public void TestEventTalk()
        {
            EventTalk.TestAddEventTalk();
        }

        [Command("Bag", "AddItemLowDamage", "添加极低伤害的饰品", false)]
        public void AddItemLowDamage()
        {
            AddItemWithOneAttr(12400001, 49, 0, -19.9f);
        }

        [Command("Bag", "AddItemHighDamage", "添加无敌秒杀饰品，参数=是否秒杀", false)]
        public void AddItemHighDamage(int type)
        {
            ItemInstance itemInstance = Module<ItemInstance.Module>.Self.CreateAsDefault(12400001, 1);
            Module<ItemAttrGrowthModule>.Self.ClearAttrs(itemInstance);
            if (type == 1)
            {
                AttrValue value = new AttrValue(AttrType.attackEnhancement, GainType.A, 9999f);
                Module<ItemAttrGrowthModule>.Self.AddAttr(itemInstance, value);
                value = new AttrValue(AttrType.remoteEnhancement, GainType.A, 9999f);
                Module<ItemAttrGrowthModule>.Self.AddAttr(itemInstance, value);
            }
            AttrValue value2 = new AttrValue(AttrType.armor, GainType.A, 999999f);
            Module<ItemAttrGrowthModule>.Self.AddAttr(itemInstance, value2);
            Module<Player>.Self.bag.Add(itemInstance, true);
        }

        [Command("Bag", "AddItemWithAttr", "添加道具指定唯一属性:道具id,属性类型,增益类型,数值", false)]
        public void AddItemWithOneAttr(int itemId, int attrType, int gainType, float value)
        {
            ItemInstance itemInstance = Module<ItemInstance.Module>.Self.CreateAsDefault(itemId, 1);
            Module<ItemAttrGrowthModule>.Self.ClearAttrs(itemInstance);
            AttrValue value2 = new AttrValue((AttrType)attrType, (GainType)gainType, value);
            Module<ItemAttrGrowthModule>.Self.AddAttr(itemInstance, value2);
            Module<Player>.Self.bag.Add(itemInstance, true);
        }

        [Command("Bag", "AddGenItem", "生成道具到背包:GenId，次数", false)]
        public void AddItemByGeneratorId(int generatorId, int genCount)
        {
            List<ItemInstance> list = new List<ItemInstance>(10);
            for (int i = 0; i < genCount; i++)
            {
                ItemInstanceUtility.GenerateItemsContainsGrade(generatorId, list, 0f, 1f, -1, null);
            }
            Module<Player>.Self.bag.Add(list, true);
        }

        [Command("Bag", "AddGiftItemByTag", "生成道具到背包根据Tag", false)]
        public void AddGiftItemByTag(int tag)
        {
            foreach (GiftItemData giftItemData in GiftItemData.GetAll())
            {
                for (int i = 0; i < giftItemData.tagA.Length; i++)
                {
                    if (giftItemData.tagA[i] == tag)
                    {
                        ItemInstance item = Module<ItemInstance.Module>.Self.Create(giftItemData.id, 1, GradeType.Grade_3, true);
                        Module<Player>.Self.bag.storage.Add(item, false);
                        break;
                    }
                }
            }
        }

        [Command("Bag", "ClearBag", "清空背包", false)]
        public void ClearBag()
        {
            List<ReadOnlyItemInstance> remove = new List<ReadOnlyItemInstance>();
            Module<Player>.Self.bag.Foreach(delegate (ReadOnlyItemInstance item)
            {
                remove.Add(item);
            });
            foreach (ReadOnlyItemInstance readOnlyItemInstance in remove)
            {
                if (readOnlyItemInstance.HasItem)
                {
                    Module<Player>.Self.bag.Remove(readOnlyItemInstance, -1, false);
                }
            }
            InfoTipMgr.Instance.SendSimpleTip("已清空背包", "", -1f);
        }

        [Command("Bag", "AddAllItem", "添加所有道具", false)]
        public void AddAllItem()
        {
            int id = -1;
            try
            {
                Module<ItemPrototypeModule>.Self.ForeachPrototype(delegate (ItemPrototype t)
                {
                    id = t.id;
                    ItemInstance item = Module<ItemInstance.Module>.Self.Create(t.id, t.stackNumber, GradeType.Grade_3, true);
                    if (Module<Player>.Self.bag.storage.FreeCount <= 0)
                    {
                        Module<Player>.Self.bag.storage.AddSlotCount();
                    }
                    Module<Player>.Self.bag.storage.Add(item, false);
                });
                InfoTipMgr.Instance.SendSimpleTip("已添加所有道具", "", -1f);
            }
            catch (Exception message)
            {
                global::Debug.LogError(string.Format("Error Occured When Creating Item [{0}]", id));
                global::Debug.LogError(message);
            }
        }

        [Command("Bag", "AddItemByItemTag", "添加道具ByItemTag", false)]
        public void AddItemByItemTag(int itemTag, int num)
        {
            try
            {
                Module<ItemPrototypeModule>.Self.ForeachPrototype(delegate (ItemPrototype t)
                {
                    if (t.HasItemTag(itemTag))
                    {
                        ItemInstance item = Module<ItemInstance.Module>.Self.Create(t.id, (num > 0) ? num : t.stackNumber, GradeType.Grade_3, true);
                        if (Module<Player>.Self.bag.storage.FreeCount <= 0)
                        {
                            Module<Player>.Self.bag.storage.AddSlotCount();
                        }
                        Module<Player>.Self.bag.storage.Add(item, false);
                    }
                });
                InfoTipMgr.Instance.SendSimpleTip("已添加道具byItemTag", "", -1f);
            }
            catch (Exception message)
            {
                global::Debug.LogError(message);
            }
        }

        [Command("Bag", "AddItemByTag", "添加道具ByTag", false)]
        public void AddItemByTag(string tag, int num)
        {
            try
            {
                Tag tagE = (Tag)Enum.Parse(typeof(Tag), tag);
                Module<ItemPrototypeModule>.Self.ForeachPrototype(delegate (ItemPrototype t)
                {
                    if (t.HasTag(tagE))
                    {
                        ItemInstance item = Module<ItemInstance.Module>.Self.Create(t.id, (num > 0) ? num : t.stackNumber, GradeType.Grade_3, true);
                        if (Module<Player>.Self.bag.storage.FreeCount <= 0)
                        {
                            Module<Player>.Self.bag.storage.AddSlotCount();
                        }
                        Module<Player>.Self.bag.storage.Add(item, false);
                    }
                });
                InfoTipMgr.Instance.SendSimpleTip("已添加Tag道具", "", -1f);
            }
            catch (Exception message)
            {
                global::Debug.LogError(message);
            }
        }

        [Command("Bag", "AddAllItemRandomGrade", "添加所有道具", false)]
        public void AddAllItemRandomGrade()
        {
            try
            {
                Module<ItemPrototypeModule>.Self.ForeachPrototype(delegate (ItemPrototype t)
                {
                    ItemInstance item = Module<ItemInstance.Module>.Self.Create(t.id, t.stackNumber, (GradeType)UnityEngine.Random.Range(0, 4), true);
                    if (Module<Player>.Self.bag.storage.FreeCount <= 0)
                    {
                        Module<Player>.Self.bag.storage.AddSlotCount();
                    }
                    Module<Player>.Self.bag.storage.Add(item, false);
                });
                InfoTipMgr.Instance.SendSimpleTip("已添加所有道具(随机品质)", "", -1f);
            }
            catch (Exception message)
            {
                global::Debug.LogError(message);
            }
        }

        [Command("Bag", "AddItemToBag", "添加道具", false)]
        public void AddItemToBag(int id, int count)
        {
            ItemInstance[] items = Module<ItemInstance.Module>.Self.CreateArray(id, count, GradeType.None, true);
            Module<Player>.Self.bag.Add(items, true);
        }
        
        [Command("Bag", "AddItemByName", "Add item to bag by name", false)]
        public void AddItemByName(string id, int count)
        {
            string name = id.Replace('_', ' ');
            Module<ItemPrototypeModule>.Self.ForeachPrototype(delegate (ItemPrototype item)
            {
                if(Module<ItemPrototypeModule>.Self.GetItemName(item.id) == name)
                {
                    ItemInstance[] items = Module<ItemInstance.Module>.Self.CreateArray(item.id, count, GradeType.None, true);
                    Module<Player>.Self.bag.Add(items, true);
                }
            });
        }

        [Command("Bag", "RemoveItemFromBag", "移除道具", false)]
        public void RemoveItemFromBag(int id, int count)
        {
            Module<Player>.Self.bag.Remove(id, ref count, true);
        }

        [Command("Bag", "AddItemString", "添加道具 id[-idMax],count,grade[;……]", false)]
        public void AddItemToBagByString(string itemStr)
        {
            string[] array = itemStr.Split(';');
            for (int i = 0; i < array.Length; i++)
            {
                string[] array2 = array[i].Split(',');
                string[] array3 = array2[0].Split('-');
                int num = int.Parse(array3[0]);
                int num2 = (array3.Length > 1) ? int.Parse(array3[1]) : int.Parse(array3[0]);
                int num3 = (array2.Length > 1) ? int.Parse(array2[1]) : 1;
                if (num3 > 9999999)
                {
                    global::Debug.LogWarning("Add Item Failed: Error Item String");
                    num3 = 1;
                }
                GradeType grade = (GradeType)((array2.Length > 2) ? int.Parse(array2[2]) : -1);
                bool clampGrade = array2.Length <= 2;
                for (int j = num; j < num2 + 1; j++)
                {
                    if (Module<ItemPrototypeModule>.Self.Get(j) != null)
                    {
                        if (Module<Player>.Self.bag.storage.FreeCount <= 0)
                        {
                            Module<Player>.Self.bag.storage.AddSlotCount();
                        }
                        ItemInstance[] items = Module<ItemInstance.Module>.Self.CreateArray(j, num3, grade, clampGrade);
                        Module<Player>.Self.bag.Add(items, true);
                    }
                }
            }
        }

        [Command("Bag", "AddItemToBagWithGrade", "添加道具 (Id,Count,Grade[0-3])", false)]
        public void AddItemToBagWithGrade(int id, int count, int grade)
        {
            ItemInstance[] items = Module<ItemInstance.Module>.Self.CreateArray(id, count, (GradeType)grade, true);
            Module<Player>.Self.bag.Add(items, true);
        }

        [Command("Bag", "AddItemToBagWithGradeNoClamp", "添加道具 (Id,Count,Grade[0-3])", false)]
        public void AddItemToBagWithGradeNoClamp(int id, int count, int grade)
        {
            ItemInstance[] items = Module<ItemInstance.Module>.Self.CreateArray(id, count, (GradeType)grade, false);
            Module<Player>.Self.bag.Add(items, true);
        }

        [Command("Bag", "TestSubmitItem", "测试提交道具", false)]
        public void TestSubmitItem(bool add)
        {
            if (add)
            {
                Module<Player>.Self.bag.storage.AddSlotCounts(400);
                MiscCmd.TestSubmitItem(12000005, 1);
                MiscCmd.TestSubmitItem(14000008, 10);
                MiscCmd.TestSubmitItem(14000009, 10);
                MiscCmd.TestSubmitItem(14000010, 10);
                MiscCmd.TestSubmitItem(14000011, 10);
                MiscCmd.TestSubmitItem(14000012, 10);
                MiscCmd.TestSubmitItem(14000013, 10);
                MiscCmd.TestSubmitItem(14000014, 10);
            }
            ItemSubmitModule instance = Singleton<ItemSubmitModule>.Instance;
            List<ItemSubmitRequire> list2 = new List<ItemSubmitRequire>();
            list2.Add(new ItemSubmitRequire(12000005, 1, GradeType.Grade_0, "", null));
            list2.Add(new ItemSubmitRequire(14000008, 10, GradeType.Grade_0, "", null));
            list2.Add(new ItemSubmitRequire(14000009, 10, GradeType.Grade_1, "", null));
            list2.Add(new ItemSubmitRequire(14000010, 10, GradeType.Grade_0, "", null));
            list2.Add(new ItemSubmitRequire(14000011, 10, GradeType.Grade_0, "", null));
            list2.Add(new ItemSubmitRequire(14000012, 10, GradeType.Grade_0, "", null));
            list2.Add(new ItemSubmitRequire(14000013, 10, GradeType.Grade_0, "", null));
            list2.Add(new ItemSubmitRequire(14000014, 10, GradeType.Grade_0, "", null));
            instance.StartSubmit(list2, delegate (List<ItemInstance> list)
            {
                foreach (ItemInstance itemInstance in list)
                {
                    global::Debug.LogError(itemInstance.GetName(false) + " : " + itemInstance.Count.ToString());
                }
            }, null, 0, null);
        }

        public static void TestSubmitItem(int id, int count)
        {
            Module<Player>.Self.bag.Add(Module<ItemInstance.Module>.Self.CreateArray(id, count, GradeType.Grade_0, true), true);
            Module<Player>.Self.bag.Add(Module<ItemInstance.Module>.Self.CreateArray(id, count, GradeType.Grade_1, true), true);
            Module<Player>.Self.bag.Add(Module<ItemInstance.Module>.Self.CreateArray(id, count, GradeType.Grade_2, true), true);
            Module<Player>.Self.bag.Add(Module<ItemInstance.Module>.Self.CreateArray(id, count, GradeType.Grade_3, true), true);
        }

        [Command("Bag", "TestSetItemBarRef", "测试道具栏设定引用的功能", false)]
        public void TestSetItemBarRef()
        {
            for (int i = 0; i < Module<Player>.Self.bag.itemBar.SlotCount; i++)
            {
                Module<Player>.Self.bag.itemBar.SetSlot(i, i);
            }
        }

        [Command("Bag", "ChangeMoney", "改金币", false)]
        public void ChangeMoney(int count)
        {
            Module<Player>.Self.bag.ChangeGold(count);
        }
        /*
		[Command("Bag", "AddTestItemToBag", "添加测试用物品到背包", false)]
		public void AddTestItemToBag(string tag)
		{
			Module<Player>.Self.bag.storage.AddSlotCounts(999);
			IEnumerable<IdCountGrade> itemsByTag = TestItemTag.GetItemsByTag(tag);
			if (itemsByTag != null)
			{
				using (IEnumerator<IdCountGrade> enumerator = itemsByTag.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IdCountGrade idCountGrade = enumerator.Current;
						Module<Player>.Self.bag.Add(Module<ItemInstance.Module>.Self.Create(idCountGrade.id, idCountGrade.count, idCountGrade.grade, true), true);
					}
					return;
				}
			}
			global::Debug.LogError("tag is error!");
		}
		*/

        [Command("Bag", "TestAddMissionItem", "测试添加任务道具", false)]
        public void TestAddMissionItem()
        {
            if (missionItem == null)
            {
                missionItem = Module<ItemInstance.Module>.Self.Create(18000024, 1, GradeType.Grade_0, false);
                Module<Player>.Self.bag.missionStorage.Add(missionItem, true);
            }
        }

        [Command("Bag", "TestRemoveMissionItem", "测试删除任务道具", false)]
        public void TestRemoveMissionItem()
        {
            if (missionItem != null)
            {
                Module<Player>.Self.bag.missionStorage.Remove(missionItem, true);
                missionItem = null;
            }
        }

        [Command("Bag", "SetTempBagSlotCount", "测试设置额外背包格子数量", false)]
        public void SetTempBagSlotCount(int count)
        {
            Module<Player>.Self.bag.SetTempBagSlotCount(count);
        }

        [Command("OrderMission", "DeliverOrderMission", "测试发布订单", false)]
        public void DeliverOrderMission()
        {
            Module<OrderMissionManager>.Self.DeliverOrder();
            Module<OrderMissionManager>.Self.RefreshRandom();
        }

        [Command("OrderMission", "MissionBoardClear", "清除订单板", false)]
        public void MissionBoardClearItem()
        {
            Module<SceneItemManager>.Self.ClearMissionBoard(OrderMissionType.Item);
            Module<OrderMissionManager>.Self.ClearRecentDeliver();
        }

        [Command("OrderMission", "AddDuvosMission", "增加杜沃斯订单Trigger", false)]
        public void DuvosMission()
        {
            Module<OrderMissionManager>.Self.AddOpreation(0, "BlackListOpeartion_DuvosOrder");
            Module<OrderMissionManager>.Self.CloseNpcOrder(-1);
            Module<MonsterMissionManager>.Self.CloseNpcOrder(-1);
            Module<OrderMissionManager>.Self.DeliverOrder();
            Module<InfoTipMgr>.Self.SendSimpleTip(TextMgr.GetStr(7386), "", -1f);
        }

        [Command("OrderMission", "DuvosMission", "移除杜沃斯订单Trigger", false)]
        public void RemoveDuvosMission()
        {
            Module<OrderMissionManager>.Self.RemoveOpreation(0);
            Module<OrderMissionManager>.Self.CloseOrderByType(Pathea.MissionNs.OrderType.DuvosOrder);
        }

        [Command("OrderMission", "AddOperation", "订单系统增加操作", false)]
        public void AddOpeartion(int id, string op)
        {
            Module<OrderMissionManager>.Self.AddOpreation(id, op);
        }

        [Command("OrderMission", "RemoveOperation", "订单系统移除操作", false)]
        public void RemoveOpeartion(int id)
        {
            Module<OrderMissionManager>.Self.RemoveOpreation(id);
        }

        [Command("OrderMission", "DeliverOrderId", "发布指定订单", false)]
        public void DeliverMissionId(int id, int instId)
        {
            Module<OrderMissionManager>.Self.DeliverOrder(id, instId);
        }

        [Command("OrderMission", "OrderMissionTordayFinish", "当天完成订单奖励加成", false)]
        public void OrderMissionTordayFinish(float factor)
        {
            AttrModifier attr = new AttrModifier(ModifyAttrType.RatioRatio, factor);
            Module<OrderMissionManager>.Self.AddModifier(attr, OrderModifierType.TodayFinish);
        }

        [Command("OrderMission", "OrderMissionOverGradeEffect", "提交物品品质高于要求是奖励加成效果", false)]
        public void OrderMissionOverGradeEffect(float factor)
        {
            AttrModifier attr = new AttrModifier(ModifyAttrType.RatioRatio, factor);
            Module<OrderMissionManager>.Self.AddModifier(attr, OrderModifierType.OrderFactor);
        }

        [Command("OrderMission", "OpenOrderMissionUI", "打开订单板UI", false)]
        public void OpenOrderMissionUI(int factor)
        {
            List<SceneItemMissionBoard> allMissionBoard = Module<SceneItemManager>.Self.GetAllMissionBoard();
            Module<SceneItemManager>.Self.OpenMissionBoardUI(null, allMissionBoard[factor]);
        }

        [Command("OrderMission", "LockOrderMission", "锁定订单Npc", false)]
        public void OrderMissionNpcRemove(int factor)
        {
            Module<MissionManager>.Self.AddOrderNpcLock(factor);
        }

        [Command("RepairMission", "DeliverRepairMission", "测试发布维修任务", false)]
        public void DeliverRepairMission()
        {
            Module<RepairMissionManager>.Self.StartDeliverAllRepair();
            Module<RepairMissionManager>.Self.RefreshRandom();
        }

        [Command("RepairTarget", "RepairTargetMake", "生成维修对象", false)]
        public void MakeRepairTarget(int id, int brokenId)
        {
            Module<RepairTargetModule>.Self.CreateMakeBrokenTargets(id, brokenId);
        }

        [Command("RepairTarget", "TestRepairTargetMake", "测试生成维修对象", false)]
        public void TestMakeRepairTarget()
        {
            Module<RepairTargetModule>.Self.CreateMakeBrokenTargets(100000, 1);
        }

        [Command("Missionmanager", "MissionSubmitAll", "提交所有任务", false)]
        public void MissionSubmitAll()
        {
            List<Mission> missionRunning = Module<MissionManager>.Self.GetMissionRunning();
            for (int i = missionRunning.Count - 1; i >= 0; i--)
            {
                Module<MissionManager>.Self.SubmitMission(missionRunning[i].MissionId, false);
            }
        }

        [Command("Missionmanager", "ChangeTargetCounter", "修改任务计数", false)]
        public void ChangeTargetCounter(int missionId, int targetId, int transId, int changeCount)
        {
            MissionManager.GetInstance.ChangeTargetCounter(missionId, targetId, transId, changeCount);
        }

        [Command("AreaMission", "TestAreaMissionRepairGen", "测试区域任务发布维修任务多个", false)]
        public void TestAreaMissionRepairGen(int count, bool isLastDaySandStorm)
        {
        }

        [Command("AreaMission", "TestAreaMissionRepair", "测试区域任务发布维修任务", false)]
        public void TestAreaMissionRepair()
        {
        }

        [Command("AreaMission", "TestAreaMissionRepairId", "测试指定id的区域任务发布维修任务", false)]
        public void TestAreaMissionRepairId(int targetId)
        {
        }

        [Command("AreaMission", "TestAreaMissionRepairAllId", "测试指定所有的区域任务发布维修任务", false)]
        public void TestAreaMissionRepairAllId(int targetId, int brokenId, int npcId)
        {
        }

        [Command("AreaMission", "UnlockSceneMission", "解锁场景任务的发布", false)]
        public void UnlockSceneMission()
        {
            Module<AreaMissionModule>.Self.Unlock();
        }

        [Command("AreaMission", "DeliverRecaptureSceneMission", "尝试发布一个物品夺回场景任务", false)]
        public void DeliverRecaptureSceneMission()
        {
            Module<AreaMissionModule>.Self.DeliverAreaMission(AreaMissionType.RecaptureItem);
        }

        [Command("AreaMission", "DeliverClearSandSceneMission", "尝试发布一个除沙的场景任务", false)]
        public void DeliverClearSandSceneMission()
        {
            Module<AreaMissionModule>.Self.DeliverAreaMission(AreaMissionType.ClearSand);
        }

        [Command("AreaMission", "DeliverRepairTargetSceneMission", "尝试发布一个物品维修的场景任务", false)]
        public void DeliverRepairTargetSceneMission()
        {
            Module<AreaMissionModule>.Self.DeliverAreaMission(AreaMissionType.RepairTarget);
        }

        [Command("AreaMission", "DeliverSceneMissions", "尝试发布指定数量的场景任务", false)]
        public void DeliverSceneMissions(int count)
        {
            Module<AreaMissionModule>.Self.DeliverMissions(count);
        }

        [Command("AreaMission", "DeliverRecaptureSceneMissionById", "尝试发布一个物品夺回场景任务", false)]
        public void DeliverRecaptureSceneMissionById(int id)
        {
            Module<AreaMissionModule>.Self.DeliverAreaMission(AreaMissionType.RecaptureItem, id);
        }

        [Command("AreaMission", "DeliverClearSandSceneMissionById", "尝试发布一个除沙的场景任务", false)]
        public void DeliverClearSandSceneMissionById(int id)
        {
            Module<AreaMissionModule>.Self.DeliverAreaMission(AreaMissionType.ClearSand, id);
        }

        [Command("AreaMission", "DeliverRepairTargetSceneMissionById", "尝试发布一个物品维修的场景任务", false)]
        public void DeliverRepairTargetSceneMissionById(int id)
        {
            Module<AreaMissionModule>.Self.DeliverAreaMission(AreaMissionType.RepairTarget, id);
        }

        [Command("AreaMission", "DeliverClearBarrierSceneMissionById", "尝试发布一个清除障碍的场景任务", false)]
        public void DeliverClearBarrierSceneMissionById(int id)
        {
            Module<AreaMissionModule>.Self.DeliverAreaMission(AreaMissionType.ClearBarrier, id);
        }

        [Command("SandMission", "DeliverSandMission", "测试发布清沙任务", false)]
        public void DeliverSandMission()
        {
            Module<SandMissionModule>.Self.DeliverMissions();
        }

        [Command("NormalMission", "DeliverNormalMission", "发布普通任务", false)]
        public void DeliverNormalMission(int proto, int instId)
        {
            Module<NormalMissionManager>.Self.DeliverMission(proto, 0);
        }

        [Command("NormalMission", "DeliverNormalMissionAnReceive", "发布普通任务并接取任务", false)]
        public void DeliverNormalMissionAnReceive(int proto, int instId)
        {
            int num = Module<NormalMissionManager>.Self.DeliverMission(proto, 0);
            if (num != 0)
            {
                Module<MissionManager>.Self.StartMission(num);
            }
        }

        [Command("Workshop", "SetPlayerWorkshopGrade", "设置玩家工坊等级", false)]
        public void SetPlayerWorkshopGrade(int reputation)
        {
            Module<GuildRankingManager>.Self.SetPlayerWorkshopLevel(reputation);
        }

        [Command("Workshop", "AddPlayerWorkshopReputation", "新增玩家工坊点数", false)]
        public void AddPlayerWorkshopReputation(int reputation)
        {
            Module<GuildRankingManager>.Self.AddPlayerReputation(reputation, true, -1);
        }

        [Command("Workshop", "ShowPlayerWorkshop", "显示玩家工坊状态", false)]
        public void ShowPlayerWorkshop()
        {
            Module<GuildRankingManager>.Self.ShowPlayerWorkshop();
        }

        [Command("Workshop", "ShowAllWorkshop", "显示所有工坊状态", false)]
        public void ShowAllWorkshop()
        {
            foreach (WorkshopReputation workshopReputation in Module<GuildRankingManager>.Self.AllWorkshop)
            {
                workshopReputation.TipsShowWorkshop();
            }
        }

        [Command("Workshop", "SendGuildRankingReward", "发放工坊排名奖励", false)]
        public void SendGuildRankingReward()
        {
            Module<GuildRankingManager>.Self.SendReward();
        }

        [Command("Workshop", "SendGuildRankingAnualAward", "发放工坊年度积分排行奖励到背包", false)]
        public void SendGuildRankingAnnualAward()
        {
            Module<GuildRankingManager>.Self.SendAnnualAwardToBag();
        }

        [Command("Workshop", "SendGuildRankingAnualAwardMail", "发放工坊年度积分排行奖励邮件", false)]
        public void SendGuildRankingAnnualAwardMail()
        {
            Module<GuildRankingManager>.Self.SendAnnualAwardToBag();
        }

        [Command("Workshop", "ShowMonthRanking", "显示工坊月排名记录", false)]
        public void ShowGuildRankingMonth()
        {
            Module<GuildRankingManager>.Self.ShowGuildRankingMonth();
        }

        [Command("Workshop", "ShowWorkshopMonthData", "显示工坊月数据", false)]
        public void ShowWorkshopMonthData(int offsetMonth)
        {
            Module<GuildRankingManager>.Self.ShowGuildRankingMonthData(offsetMonth);
        }

        [Command("Workshop", "RemoveNpcData", "移除NPC数据", false)]
        public void RemoveNpcData(int npcid)
        {
            Module<GuildRankingManager>.Self.RemoveNpcWorkShopstate(npcid);
        }

        [Command("Workshop", "SetNpcDailyValue", "设置Npc每日数据", false)]
        public void SetNpcDailyValue(int npcid, int value)
        {
            Module<GuildRankingManager>.Self.AddNpcDailyValue(npcid, value);
        }

        [Command("Mission", "ShowMission", "显示当前任务(0=发布中,1=运行中,2=已结束)", false)]
        public void ShowMission(int type)
        {
            List<Mission> list;
            if (type != 0)
            {
                if (type != 1)
                {
                    list = Module<MissionManager>.Self.GetMissionEnd();
                }
                else
                {
                    list = Module<MissionManager>.Self.GetMissionRunning();
                }
            }
            else
            {
                list = Module<MissionManager>.Self.GetMissionActived();
            }
            foreach (Mission mission in list)
            {
            }
        }

        [Command("Mission", "ShowMissionRecord", "显示完成记录", false)]
        public void ShowMissionRecord()
        {
            foreach (MissionHistoryRecord missionHistoryRecord in Module<MissionManager>.Self.HistoryRecord)
            {
            }
        }

        [Command("Mission", "StartMission", "开始任务", false)]
        public void StartMission(int missionId)
        {
            Module<MissionManager>.Self.StartMission(missionId);
        }

        [Command("Mission", "SubmitMission", "提交任务", false)]
        public void SubmitMission(int missionId)
        {
            try
            {
                Module<MissionManager>.Self.SubmitMission(missionId, false);
            }
            catch (Exception message)
            {
                global::Debug.LogError(message);
            }
        }

        [Command("Mission", "AddMissionIcon", "添加任务图标", false)]
        public void AddMissionIcon(int npcId)
        {
            Module<MissionManager>.Self.AddNpcMissionIcon(npcId, 1, MissionIconType.MainReceive);
        }

        [Command("Mission", "RemoveMissionIcon", "删除任务图标", false)]
        public void RemoveMissionIcon(int npcId)
        {
            Module<MissionManager>.Self.RemoveNpcMissionIcon(npcId, 1, MissionIconType.MainReceive);
        }

        [Command("Mission", "AddMissionItem", "添加任务物品", false)]
        public void AddMissionItems()
        {
            foreach (Mission mission in Module<MissionManager>.Self.GetMissionRunning())
            {
                if (mission.curTarget != null)
                {
                    MissionTargetGetItem missionTargetGetItem = mission.curTarget as MissionTargetGetItem;
                    if (missionTargetGetItem != null)
                    {
                        foreach (IdCount idCount in missionTargetGetItem.generalArgs)
                        {
                            ItemInstance[] items = Module<ItemInstance.Module>.Self.CreateArray(idCount.id, idCount.count, GradeType.Grade_3, true);
                            Module<Player>.Self.bag.Add(items, true);
                        }
                    }
                }
            }
        }

        [Command("Mission", "GiveUpMission", "放弃任务", false)]
        public void GiveUpMission(int missionId)
        {
            Module<MissionManager>.Self.GiveUpMission(missionId);
        }

        [Command("Mission", "MissionRequireItemAdd", "添加任务需求物品", false)]
        public void MissionrequireItemAdd(int keyId, int missionId, string itemStr)
        {
            Module<MissionManager>.Self.AddScriptRequire(keyId, missionId, itemStr);
        }

        [Command("Mission", "MissionRequireItemRemove", "删除任务需求物品", false)]
        public void MissionrequireItemRemove(int keyId)
        {
            Module<MissionManager>.Self.RemoveScriptRequire(keyId);
        }

        [Command("Mission", "MissionTraceNpc", "运行中的第一个任务追踪npc", false)]
        public void MissionTestTraceNpc(int npcId)
        {
            Module<MissionManager>.Self.TestTraceNpc(npcId);
        }

        [Command("Mission", "MissionRemoveTraceNpc", "运行中的第一个任务删除追踪npc", false)]
        public void MissionTestRemoveTraceNpc(int npcId)
        {
            Module<MissionManager>.Self.TestRemoveTraceNpc(npcId);
        }

        [Command("Mission", "MissionSetDateLockerActive", "设置日期锁状态", false)]
        public void MissionSetDateLockerActive(int id, bool state)
        {
            Module<SceneItemManager>.Self.SetDateLockerActiveState(id, state);
        }

        [Command("Mission", "MissionSetDateLockerPayed", "设置日期锁支付完成", false)]
        public void MissionSetDateLockerPayed(int id)
        {
            Module<SceneItemManager>.Self.SetDateLockerPayed(id);
        }

        [Command("Search", "SearchItemName", "通过Id查询物品名字", false)]
        public void SearchItemName(int itemId)
        {
            ItemPrototype itemPrototype = Module<ItemPrototypeModule>.Self.Get(itemId);
            Cmd.Instance.Log("\t" + TextMgr.GetStr(itemPrototype.nameId) + "\t" + itemPrototype.id.ToString(), Cmd.CallBackType.Normal);
        }

        [Command("FadeMask", "TestFadeMask", "测试FadeMask", false)]
        public void TestFadeMask()
        {
            Singleton<FadeMask>.Instance.FadeIn(0f, 2f, "1", true, CustomizableInterpolator.Type.Decelerate, null);
        }

        [Command("FadeMask", "TestFadeMaskOut", "测试FadeMaskOut", false)]
        public void TestFadeMaskOut()
        {
            Singleton<FadeMask>.Instance.FadeOut(0f, 2f, "1", CustomizableInterpolator.Type.Decelerate);
        }

        [Command("Mail", "SendMailTest", "测试发送邮件", false)]
        public void SendMailTest(int id)
        {
            try
            {
                Module<MailModule>.Self.SendMail(id);
            }
            catch (Exception)
            {
            }
        }

        [Command("Mail", "SendMailAnchor", "测试发送主播邮件", false)]
        public void SendMailAnchor(string sender, string content, int templateid)
        {
            try
            {
                Module<MailModule>.Self.SendMail(sender, content, templateid);
            }
            catch (Exception)
            {
            }
        }

        [Command("Mail", "SendAllMailTest", "测试所有发送邮件", false)]
        public void SendAllMailTest()
        {
            Module<MailModule>.Self.SendAllMail();
        }

        [Command("Mail", "FinishReadMail", "测试阅读邮件", false)]
        public void TestFinishReadMail(int id)
        {
            Module<MailModule>.Self.FinishReadMail(id);
        }

        [Command("Mail", "ShowPlayerMailBox", "显示邮箱邮件", false)]
        public void SHowPlayerMailBox()
        {
            Module<MailModule>.Self.ShowPlayerMailBox();
        }

        [Command("Mail", "SendTempMail", "发送临时邮件", false)]
        public void SendTempMail()
        {
            MailAttachData[] attachData = new MailAttachData[]
            {
                new MailAttachData(MailAttachType.Money, new IdCount(10, 0)),
                new MailAttachData(MailAttachType.Item, new IdCount(19700000, 2))
            };
            Module<MailModule>.Self.SendTempMail("测试邮件内容内容内容内容内容内容内容内容内容内容", "测试结尾", attachData, null);
        }

        [Command("Mail", "SendMailOpen", "发送邮件并打开", false)]
        public void SendMailOpen(int id)
        {
            Module<MailModule>.Self.SendMailOpen(id);
        }

        [Command("Mail", "SendMailCustom", "发送邮件并打开", false)]
        public void SendMailCustom(int id, int customer)
        {
            Module<MailModule>.Self.SendMailCustom(id, delegate (MailData data)
            {
                data.AddFavor(new Vector2Int(customer, 100));
            });
        }

        [Command("Scene", "TestSceneChange", "触发场景切换事件", false)]
        public void TestLeaveScene(string fromScene, string toScene)
        {
            Module<ScenarioModule>.Self.TestSceneEvent(fromScene, toScene);
        }

        [Command("Scene", "SceneChange", "触发场景切换事件", false)]
        public void SceneChange(string toScene)
        {
            Module<ScenarioModule>.Self.LoadScenario(UtilNs.UtilParse.ParseEnum<AdditiveScene>(toScene));
        }

        [Command("Scene", "SceneChangeExtrance", "切换到出口,一般是场景名+In,例如SaloonIn", false)]
        public void SceneChangeExtrance(string extranceName)
        {
            SceneExtranceInst infoByName = Module<SceneInfoMgr>.Self.GetInfoByName<SceneExtranceInst>(extranceName);
            AdditiveScene scene = infoByName.exitArea.scene;
            Module<ScenarioModule>.Self.LoadScenario(scene, infoByName.exitArea.GetPointByIndex(0));
        }

        [Command("Scene", "SceneChangeQuick:Metro_Dungeon", "切换到地铁副本（打蜥蜴人的）", false)]
        public void SceneChangeExtranceMetro_Dungeon()
        {
            SceneChangeExtrance("Metro_DungeonIn");
        }

        [Command("Scene", "SceneChangeQuick:Gecko_Dungeon", "切换到失乐园（打机器人的）", false)]
        public void SceneChangeExtranceGecko_Dungeon()
        {
            SceneChangeExtrance("Gecko_Dungeon_In");
        }

        [Command("Scene", "SceneChangeQuick:Wreck_Dungeon", "切换到沉船", false)]
        public void SceneChangeExtranceWreck_Dungeon()
        {
            SceneChangeExtrance("Wreck_DungeonIn");
        }

        [Command("Scene", "SceneChangeQuick:MysteriousCave_Dungeon", "切换到洞穴", false)]
        public void SceneChangeExtranceMysteriousCave_Dungeon()
        {
            SceneChangeExtrance("MysteriousCave_DungeonIn");
        }

        [Command("Scene", "SceneChangeQuick:Breach_Dungeon", "切换到比奇塔副本", false)]
        public void SceneChangeExtranceBreach_Dungeon()
        {
            SceneChangeExtrance("Breach_DungeonIn");
        }

        [Command("Scene", "SceneChangeQuick:SandSkiing", "切换到滑沙场", false)]
        public void SceneChangeExtranceSandSkiing()
        {
            SceneChangeExtrance("SandSkiingIn");
        }

        [Command("Scene", "SceneChangeQuick:PabloHome", "切换到理发店（帕布罗家）", false)]
        public void SceneChangeExtrancePabloHome()
        {
            SceneChangeExtrance("PabloHomeIn");
        }

        [Command("Scene", "SceneChangeQuick:PortiaTunnel", "切换到波西亚隧道", false)]
        public void SceneChangeExtrancePortiaTunnel()
        {
            SceneChangeExtrance("PortiaTunnel_DungeonIn");
        }

        [Command("Scene", "SceneChangeQuick:Mole_Dungeon", "切换到鼹鼠人副本", false)]
        public void SceneChangeExtranceMole_Dungeon()
        {
            SceneChangeExtrance("Mole_DungeonIn");
        }

        [Command("Scene", "SceneChangeQuick:LoganCave_Dungeon", "切换到洛根营地副本", false)]
        public void SceneChangeExtranceLoganCave_Dungeon()
        {
            SceneChangeExtrance("LoganCave_DungeonIn");
        }

        [Command("Scene", "SceneChangeQuick:Warehouse_Dungeon", "切换到仓库副本", false)]
        public void SceneChangeExtranceWarehouse_Dungeon()
        {
            SceneChangeExtrance("Warehouse_DungeonIn");
        }

        [Command("Scene", "SceneChangeQuick:Aviation_Dungeon", "切换到航天遗迹副本", false)]
        public void SceneChangeExtranceAviation_Dungeon()
        {
            SceneChangeExtrance("Aviation_DungeonIn");
        }

        [Command("Scene", "SceneChangeQuick:Conference_Storyboard", "切换到联盟会议", false)]
        public void SceneChangeExtranceConference_Storyboard()
        {
            SceneChangeExtrance("Conference_Storyboard_In");
        }

        [Command("Scene", "SceneChangeQuick:Univese_Storyboard", "切换到外太空", false)]
        public void SceneChangeUnivese_Storyboard()
        {
            SceneChange("Univese_Storyboard");
        }

        [Command("Item", "SearchItemByName", "查询物品ID", false)]
        public void SearchItemByName(string nameStr)
        {
            if (string.IsNullOrEmpty(nameStr))
            {
                return;
            }
            nameStr = nameStr.Replace('_', ' ');
            foreach (ItemPrototype itemPrototype in Module<ItemPrototypeModule>.Self.GetDataListByName(nameStr))
            {
                if (itemPrototype == null)
                {
                    Cmd.Instance.Log("找不到对象:" + nameStr, Cmd.CallBackType.Normal);
                }
                else
                {
                    Cmd.Instance.Log("\t" + TextMgr.GetStr(itemPrototype.nameId) + "\t" + itemPrototype.id.ToString(), Cmd.CallBackType.Normal);
                }
            }
        }

        [Command("Weather", "CheckCurrentWeather", "查询当前天气", false)]
        public void CheckCurrentWeather()
        {
            Cmd.Instance.Log(Module<WeatherModule>.Self.CurWeather.ToString(), Cmd.CallBackType.Normal);
        }

        [Command("Weather", "CheckWeatherAtTime", "查询某天的天气", false)]
        public void CheckWeatherAtTime(int year, int month, int day)
        {
            Weather weather = Module<WeatherModule>.Self.GetFixedWeather(new GameDateTime(year, month, day));
            Cmd.Instance.Log(weather.ToString(), Cmd.CallBackType.Normal);
        }

        [Command("Weather", "ChangeTomorrowWeather", "切换明天天气,0=普通晴天，1=下雨天，2=沙尘暴，3=多云晴天，4=酷热晴天，5=下雪", false)]
        public void ChangeWeather(int type)
        {
            Module<WeatherModule>.Self.SetHistoryWeather(Module<TimeModule>.Self.CurrentTime.AddDays(1.0), (Weather)type);
        }

        [Command("Weather", "LogActivatedWeathers", "打印激活的天气类型", false)]
        public void LogActivatedWeathers()
        {
            Weather[] array = Enum.GetValues(typeof(Weather)) as Weather[];
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Weather type in array)
            {
                if (Module<WeatherModule>.Self.IsWeatherCanByRandom(type))
                {
                    stringBuilder.Append(type.ToString());
                    stringBuilder.Append(',');
                }
            }
        }

        [Command("BlueprintResearch", "ShowCurResearchState", "显示当前研究状态", false)]
        public void ShowCurResearchState()
        {
            ResearchProcess curResearch = Module<BlueprintResearchModule>.Self.curResearch;
            Cmd.Instance.Log(string.Concat(new string[]
            {
                curResearch.blueprintId.ToString(),
                " ",
                Module<ItemPrototypeModule>.Self.GetItemName(curResearch.blueprintId),
                ": ",
                curResearch.pastDays.ToString(),
                "/",
                curResearch.totalDays.ToString()
            }), Cmd.CallBackType.Normal);
        }

        [Command("BlueprintResearch", "ShowAllResearchState", "显示所有研究对象状态", false)]
        public void ShowAllResearchState()
        {
            foreach (ResearchItem researchItem in ResearchItem.GetAll())
            {
                ResearchItemState researchItemState = Module<BlueprintResearchModule>.Self.GetResearchItemState(researchItem);
                string itemName = Module<ItemPrototypeModule>.Self.GetItemName(researchItem.Id);
                string text = "";
                if (researchItemState == ResearchItemState.Locked)
                {
                    string text2 = "";
                    foreach (int id in researchItem.preIds)
                    {
                        text2 = string.Concat(new string[]
                        {
                            text2,
                            id.ToString(),
                            " ",
                            Module<ItemPrototypeModule>.Self.GetItemName(id),
                            " "
                        });
                    }
                    text = " 需要前置研究: " + text2;
                }
                global::Debug.LogError(string.Concat(new string[]
                {
                    "蓝图研究: ",
                    researchItem.Id.ToString(),
                    " ",
                    itemName,
                    " ",
                    researchItemState.ToString(),
                    text
                }));
            }
        }

        [Command("BlueprintResearch", "CheckResearch", "检查可研究状态及原因", false)]
        public void CheckResearch(int id)
        {
            CannotResearchReason cannotResearchReason;
            if (Module<BlueprintResearchModule>.Self.CheckCanResearch(id, out cannotResearchReason))
            {
                global::Debug.LogError("可以研究! " + id.ToString());
                return;
            }
            global::Debug.LogError("不能研究! " + id.ToString() + " " + cannotResearchReason.ToString());
        }

        [Command("BlueprintResearch", "StartResearch", "开始某个研究", false)]
        public void StartResearch(int id)
        {
            CannotResearchReason cannotResearchReason;
            if (Module<BlueprintResearchModule>.Self.CheckCanResearch(id, out cannotResearchReason))
            {
                Module<BlueprintResearchModule>.Self.StartResearch(id);
                ShowCurResearchState();
                return;
            }
            global::Debug.LogError("不能研究! " + cannotResearchReason.ToString());
        }

        [Command("BlueprintResearch", "SpeedUpResearch", "加速研究", false)]
        public void SpeedupResearch()
        {
            CannotSpeedupReason cannotSpeedupReason;
            if (Module<BlueprintResearchModule>.Self.CheckCanSpeedUp(out cannotSpeedupReason))
            {
                Module<BlueprintResearchModule>.Self.DoSpeedUp(1);
                return;
            }
            global::Debug.LogError("不能加速! " + cannotSpeedupReason.ToString());
        }

        [Command("BlueprintResearch", "ShowSpentCdCount", "显示花费cd统计", false)]
        public void ShowSpentCdCount()
        {
            Cmd.Instance.Log("花费Cd: " + Module<BlueprintResearchModule>.Self.GetSpentCd().ToString(), Cmd.CallBackType.Normal);
        }

        [Command("BlueprintResearch", "UnlockBluePrint", "解锁图纸", false)]
        public void UnlockBluePrint(int id)
        {
            Module<BluePrintModule>.Self.Unlock(id, Module<TimeModule>.Self.CurrentTime, true);
        }

        [Command("BlueprintResearch", "UnlockAllBluePrints", "解锁所有图纸", false)]
        public void UnlockAllBluePrints()
        {
            Module<BluePrintModule>.Self.UnlockAll(Module<TimeModule>.Self.CurrentTime);
        }

        [Command("Player", "AddExp", "增加玩家经验值", false)]
        public void AddExp(int addExp)
        {
            Module<Player>.Self.AddExp(addExp, null, true);
        }

        [Command("Player", "ChangePlayerHp", "改变玩家生命", false)]
        public void ChangePlayerHp(int changeValue)
        {
            Module<Player>.Self.actor.ApplyAttrChange(ActorRunTimeAttrType.Hp, (float)changeValue);
            Module<Player>.Self.actor.ShowHpChangeUI((float)changeValue);
        }

        [Command("Player", "ChangePlayerSp", "改变玩家体力", false)]
        public void ChangePlayerSp(int changeValue)
        {
            Module<Player>.Self.actor.ApplyAttrChange(ActorRunTimeAttrType.Sp, (float)changeValue);
        }

        [Command("Player", "PlayerForceSleep", "玩家强制睡覺", false)]
        public void PlayerForceSleep()
        {
            Module<SleepModule>.Self.PlayerForceSleep(true);
        }

        [Command("Player", "GoHome", "玩家强制回家", false)]
        public void GoHome()
        {
            if (Module<ScenarioModule>.Self.CurScene != AdditiveScene.Main)
            {
                Module<ScenarioModule>.Self.LoadScenario(AdditiveScene.Main, new PosRot(new Vector3(149.2807f, -2.114995f, -28.64307f), Vector3.zero));
                return;
            }
            Module<Player>.Self.GamePos = new Vector3(149.2807f, -2.114995f, -28.64307f);
        }

        [Command("Player", "AddPlayerCombo", "增加玩家连击段数", false)]
        public void AddPlayerCombo(int addCount)
        {
            AttrModifier attrModifier = new AttrModifier(ModifyAttrType.BasePlus, (float)addCount);
            for (MeleeWeaponType meleeWeaponType = MeleeWeaponType.Sword; meleeWeaponType < MeleeWeaponType.Max; meleeWeaponType++)
            {
                Module<Player>.Self.AddAttckNumModifier(meleeWeaponType, attrModifier);
            }
        }

        [Command("Player", "UnlockPlayerDashAttack", "解锁玩家冲刺攻击", false)]
        public void UnlockPlayerDashAttack()
        {
            for (MeleeWeaponType meleeWeaponType = MeleeWeaponType.Sword; meleeWeaponType < MeleeWeaponType.Max; meleeWeaponType++)
            {
                Module<Player>.Self.SetDashAttackEnable(meleeWeaponType, true);
            }
        }

        [Command("Player", "SetPlayerRush", "屏蔽玩家冲刺", false)]
        public void SetPlayerRushLocker(bool addOrRemove)
        {
            Module<StoryAssistant>.Self.SetPlayerRushLocker(addOrRemove);
        }

        [Command("Player", "PlayerSPCost0", "玩家不耗体力", false)]
        public void SetPlayerSPCost0()
        {
            Module<Player>.Self.actor.AddBuff(11);
        }

        [Command("Player", "AddBuff", "添加Buff", false)]
        public void AddBuffToActor(int actorID, int buffID)
        {
            Actor actor = Module<ActorMgr>.Self.GetActor(actorID);
            if (actor != null)
            {
                actor.AddBuff(buffID);
            }
        }

        [Command("Player", "RemoveBuff", "移除Buff", false)]
        public void RemoveBuffFromActor(int actorID, int buffID)
        {
            Actor actor = Module<ActorMgr>.Self.GetActor(actorID);
            if (actor != null)
            {
                actor.RemoveBuff(buffID);
            }
        }

        [Command("Player", "HidePlayer", "隐藏玩家", false)]
        public void HidePlayer()
        {
            Actor actor = Module<ActorMgr>.Self.GetActor(8000);
            if (actor != null)
            {
                actor.AddHideLocker(locker);
            }
        }

        [Command("Player", "ShowPlayer", "显示玩家", false)]
        public void ShowPlayer()
        {
            Actor actor = Module<ActorMgr>.Self.GetActor(8000);
            if (actor != null)
            {
                actor.RemoveHideLocker(locker);
            }
        }

        [Command("Player", "LockPlayerFallInjury", "关闭玩家掉落伤害", false)]
        public void LockPlayerFallInjury()
        {
            Actor actor = Module<ActorMgr>.Self.GetActor(8000);
            if (actor != null)
            {
                actor.AddFallInjuryLocker(locker);
            }
        }

        [Command("Player", "UnlockPlayerFallInjury", "开启玩家掉落伤害", false)]
        public void UnlockPlayerFallInjury()
        {
            Actor actor = Module<ActorMgr>.Self.GetActor(8000);
            if (actor != null)
            {
                actor.RemoveFallInjuryLocker(locker);
            }
        }

        [Command("Actor", "ActorShowBubble", "actor显示气泡对话", false)]
        public void ActorShowBubble(int npcId, int transId, float duration)
        {
            Actor actor = StoryHelper.GetActor(npcId, "");
            if (actor == null)
            {
                global::Debug.LogError("actor is null!");
                return;
            }
            actor.ShowBubble(transId, duration);
        }

        [Command("Actor", "AddBehaviorTag", "增加AI系统tag", false)]
        public void AddBehaviorTag(string tag)
        {
            Module<BehaviorModule>.Self.AddTag(tag);
        }

        [Command("Actor", "AddBehaviorTagTimer", "增加AI系统有时限的tag", false)]
        public void AddBehaviorTagTimer(string tag, int day, int hour, int minute)
        {
            Module<BehaviorModule>.Self.AddTag(tag, new GameTimeSpan(day, hour, minute, 0));
        }

        [Command("Actor", "RemoveBehaviorTag", "移除AI系统tag", false)]
        public void RemoveBehaviorTag(string tag)
        {
            Module<BehaviorModule>.Self.RemoveTag(tag);
        }

        [Command("Actor", "AddBehaviorActorTag", "增加AI系统tag", false)]
        public void AddBehaviorActorTag(int id, string tag)
        {
            Module<BehaviorModule>.Self.AddTag(id, tag);
        }

        [Command("Actor", "AddBehaviorActorTagTimer", "增加AI系统有时限的tag", false)]
        public void AddBehaviorActorTagTimer(int id, string tag, int day, int hour, int minute)
        {
            Module<BehaviorModule>.Self.AddTag(id, tag, new GameTimeSpan(day, hour, hour, 0));
        }

        [Command("Actor", "RemoveBehaviorActorTag", "移除AI系统tag", false)]
        public void RemoveBehaviorActorTag(int id, string tag)
        {
            Module<BehaviorModule>.Self.RemoveTag(id, tag);
        }

        [Command("Actor", "StartModalVoice", "测试语气音", false)]
        public void StartModalVoice(int id, string modal)
        {
            Actor actor = Module<ActorMgr>.Self.GetActor(id);
            if (actor == null)
            {
                global::Debug.LogError("actor 不存在! " + id.ToString());
                return;
            }
            if (!actor.HasModalConfig())
            {
                global::Debug.LogError("actor 没有语气配置config! " + id.ToString());
                return;
            }
            actor.StartModalVoice(modal);
        }

        [Command("Actor", "AddActorBuffFxLocker", "关闭buff特效", false)]
        public void AddActorBuffFxLocker(int id)
        {
            Actor actor = Module<ActorMgr>.Self.GetActor(id);
            if (actor != null)
            {
                actor.AddFxLocker(locker);
            }
        }

        [Command("Actor", "RemoveActorBuffFxLocker", "开启buff特效", false)]
        public void RemoveActorBuffFxLocker(int id)
        {
            Actor actor = Module<ActorMgr>.Self.GetActor(id);
            if (actor != null)
            {
                actor.RemoveFxLocker(locker);
            }
        }

        [Command("Npc", "NpcChangeSkinColor", "Npc变色", false)]
        public void NpcChangeSkinColor(int npcID, int r, int g, int b)
        {
            Npc npc = Module<NpcMgr>.Self.GetNpc(npcID);
            if (npc != null)
            {
                npc.AddSkinColorChangeRequest(1000000, new Color((float)r / 255f, (float)g / 255f, (float)b / 255f));
            }
        }

        [Command("Npc", "RemoveNpcChangeSkinColor", "开启buff特效", false)]
        public void RemoveNpcChangeSkinColor(int npcID)
        {
            Npc npc = Module<NpcMgr>.Self.GetNpc(npcID);
            if (npc != null)
            {
                npc.RemoveSkinColorChangeRequest(1000000);
            }
        }

        [Command("Npc", "ActorSetIgnoreShowDistance", "设置actor无视生成距离", false)]
        public void ActorSetIgnoreShowDistance(int actorID, bool ignore)
        {
            Actor actor = Module<ActorMgr>.Self.GetActor(actorID);
            if (actor != null)
            {
                actor.SetIgnoreShowDistance(ignore);
            }
        }

        [Command("Time", "PauseTime", "暂停时间", false)]
        public void PauseTime()
        {
            if (pauseTimeLocker == null)
            {
                pauseTimeLocker = new Locker();
                Module<TimeModule>.Self.AddPauseLock(pauseTimeLocker);
                return;
            }
            Module<TimeModule>.Self.RemovePauseLock(pauseTimeLocker);
            pauseTimeLocker = null;
        }

        [Command("Time", "JumpToDate", "跳到日期", false)]
        public void JumpToDate(int year, int month, int day, int hour, int minute, int second)
        {
            Module<TimeModule>.Self.JumpToTimePoint(new GameDateTime(year, month, day, hour, minute, second));
        }

        [Command("Time", "SetTimeScale", "设置TimeScale", false)]
        public void SetTimeScale(float timeScale)
        {
            Module<TimeScaleMgr>.Self.SetTimeScale(timeScale);
        }

        [Command("Time", "IgnoreTimeScaleFix", "设置TimeScale", false)]
        public void IgnoreTimeScale()
        {
            Module<TimeScaleMgr>.Self.SetIgnoreFix();
        }

        [Command("UITest", "ResetUIAndInput", "重置UI和输入状态", false)]
        public void ResetUIAndInput()
        {
            if (Module<UIModule>.Self.CanReset())
            {
                Module<InputModule>.Self.Mgr.ResetMgr();
                base.StartCoroutine(Module<UIModule>.Self.Reset());
            }
        }

        [Command("UITest", "OpenReadingText", "阅读测试", false)]
        public void OpenReading(int bookid)
        {
            ReadingModule.onOpenBook(bookid);
        }

        [Command("UITest", "OpenPhotoText", "相册", false)]
        public void OpenPhoto()
        {
            ReadingModule.onOpenPhoto();
        }

        [Command("UITest", "OpenFreeCamera", "打开自由相机", false)]
        public void OpenFreeCamearMode()
        {
            Module<CamCaptureModule>.Self.EnterCaptureMode(true, false);
        }

        public static int count;

        [Command("UITest", "OpenItemReward", "道具取得", false)]
        public void OpenItemReward()
        {
            List<ItemInstance> items = new List<ItemInstance>();
            Module<ItemPrototypeModule>.Self.ForeachPrototype(delegate (ItemPrototype t)
            {
                if (count > 100)
                {
                    return;
                }
                count++;
                ItemInstance item = Module<ItemInstance.Module>.Self.Create(t.id, t.stackNumber, (GradeType)UnityEngine.Random.Range(0, 4), true);
                items.Add(item);
            });
            Singleton<ItemRewardMgr>.Instance.OpenRewardUI(items);
        }

        [Command("UITest", "OpenResearch", "研究", false)]
        public void OpenResearch()
        {
            Module<BlueprintResearchModule>.Self.OpenResearchUI();
        }

        [Command("UITest", "OpenHandBook", "组装手册", false)]
        public void OpenHandBook()
        {
            Module<HandBookModule>.Self.OpenHandBookUI(PageType.Items, HandBookTag.Items, null);
        }

        [Command("UITest", "SetUIDisplayActive", "显示/关闭UI显示", false)]
        public void SetUIDisplayActive(bool active)
        {
            if (uidisable && active)
            {
                Module<UIModule>.Self.SetUIDisplayActive(true);
                Module<IndicatorMgr>.Self.RemoveDisable(locker);
                uidisable = false;
                return;
            }
            if (!uidisable && !active)
            {
                Module<UIModule>.Self.SetUIDisplayActive(false);
                Module<IndicatorMgr>.Self.AddDisable(locker);
                uidisable = true;
            }
        }

        [Command("UITest", "OpenHomeAttrUI", "查看家园属性", false)]
        public void OpenHomeAttrUI()
        {
            Singleton<HomeAttrManager>.Instance.OpenUI();
        }

        [Command("UITest", "OpenRestoreMachineUIOutside", "打开家园外古物修复机", false)]
        public void OpenRestoreMachineUIOutside()
        {
            Module<RestoreModule>.Self.OpenUIOutside();
        }

        [Command("UITest", "TestCredit", "测试名单", false)]
        public void TestCredit()
        {
            Module<CreditsMgr>.Self.StartCredit();
        }

        [Command("UITest", "TestPkDisplayForceEnd", "测试PK强制结束", false)]
        public void TestPkDisplayForceEnd()
        {
            Singleton<PkDisplayMgr>.Instance.EndPk();
        }

        [Command("UITest", "StartPlantingUI", "测试进入种植模式", false)]
        public void StartPlantingUI(int level)
        {
            Module<PlantManager>.Self.EnterPlanting(Module<PlantTookitsConfigModule>.Self.GetConfigByLevel(level).id, null, false);
        }

        [Command("UITest", "AddVirtualMouse", "测试虚拟鼠标打开", false)]
        public void AddVirtualMouse()
        {
            Module<InputModule>.Self.VirtualMouse.AddEnable(this);
        }

        [Command("UITest", "RemoveVirtualMouse", "测试虚拟鼠标关闭", false)]
        public void RemoveVirtualMouse()
        {
            Module<InputModule>.Self.VirtualMouse.RemoveEnable(this);
        }

        [Command("UITest", "AddUseCamerModeCanvas", "测试Canvas的camera mode", false)]
        public void AddUseNonCamerModeCanvas()
        {
            UICameraManager.Instance.AddUseNonCameraMode(this);
        }

        [Command("UITest", "RemoveUseCamerModeCanvas", "测试Canvas的camera mode", false)]
        public void RemoveUseNonCamerModeCanvas()
        {
            UICameraManager.Instance.RemoveUseNonCameraMode(this);
        }

        [Command("UITest", "OpenBuildingPresetUI", "打开家园预设界面", false)]
        public void OpenBuildingPresetUI()
        {
            Singleton<BuildingPresetBookMgr>.Instance.Open();
        }

        [Command("Planting", "AutoPlantingTestRepeat", "测试随机自动种植多次", false)]
        public void AutoPlantingTestRepeat(int time)
        {
            for (int i = 0; i < time; i++)
            {
                AutoPlantingTestOnGround();
            }
        }

        [Command("Planting", "AutoPlantingTestNoLimit", "测试随机自动种植(无限制)", false)]
        public void AutoPlantingTestNoLimit()
        {
            PresetRegionContainer presetRegion = Module<RegionModule>.Self.GetPresetRegion("FarmCustomMesh");
            List<PlantConfig> plantingConfigs = new List<PlantConfig>();
            Module<PlantConfigModule>.Self.Foreach(delegate (PlantConfig o)
            {
                plantingConfigs.Add(o);
            });
            PlantConfig plantConfig = plantingConfigs[UnityEngine.Random.Range(0, plantingConfigs.Count - 1)];
            Singleton<HomePlantCreateTool>.Instance.TryPutDownNoLimit(presetRegion, plantConfig.Id);
        }

        [Command("Planting", "AutoPlantingTestOnGround", "测试随机自动种植(在土上)", false)]
        public void AutoPlantingTestOnGround()
        {
            PresetRegionContainer presetRegion = Module<RegionModule>.Self.GetPresetRegion("FarmCustomMesh");
            List<PlantConfig> plantingConfigs = new List<PlantConfig>();
            Module<PlantConfigModule>.Self.Foreach(delegate (PlantConfig o)
            {
                plantingConfigs.Add(o);
            });
            PlantConfig plantConfig = plantingConfigs[UnityEngine.Random.Range(0, plantingConfigs.Count - 1)];
            global::Debug.LogError(Singleton<HomePlantCreateTool>.Instance.TryPutDownOnGround(presetRegion, plantConfig.Id));
        }

        [Command("Planting", "AutoPlantingAddWaterAndNeutrient", "测试自动加水和养料", false)]
        public void AutoPlantingAddWaterAndNeutrient()
        {
            Unit<PlantGroundGroupUnit>.ForeachUnit(delegate (PlantGroundGroupUnit u)
            {
                Singleton<HomePlantCreateTool>.Instance.AddWaterFull(u);
                Singleton<HomePlantCreateTool>.Instance.AddNeutrientFull(u);
            });
        }

        [Command("Planting", "AutoPlantingHarvest", "测试自动收获", false)]
        public void AutoPlantingHarvest()
        {
            List<PlantGroundGroupUnit> units = new List<PlantGroundGroupUnit>();
            Unit<PlantGroundGroupUnit>.ForeachUnit(delegate (PlantGroundGroupUnit u)
            {
                units.Add(u);
            });
            foreach (PlantGroundGroupUnit unit in units)
            {
                Singleton<HomePlantCreateTool>.Instance.Harvest(unit);
            }
        }

        [Command("Planting", "AutoPlantingDestroy", "测试自动摧毁", false)]
        public void AutoPlantingDestroy()
        {
            List<PlantGroundGroupUnit> units = new List<PlantGroundGroupUnit>();
            Unit<PlantGroundGroupUnit>.ForeachUnit(delegate (PlantGroundGroupUnit u)
            {
                units.Add(u);
            });
            foreach (PlantGroundGroupUnit unit in units)
            {
                Singleton<HomePlantCreateTool>.Instance.Destroy(unit);
            }
        }

        [Command("Archive", "AutoSave", "自动存档", false)]
        public void AutoSave()
        {
            Module<Pathea.SaveNs.Save>.Self.RequestAuto();
        }

        [Command("Archive", "AutoSaveTimes", "多次自动存档", false)]
        public void AutoSaveTimes(int times)
        {
            if (times <= 0)
            {
                return;
            }
            for (int i = 0; i < times; i++)
            {
                Module<Pathea.SaveNs.Save>.Self.RequestAuto();
            }
        }
        /*
		[Command("Dungeon", "SetAbandonedDungeonVoxelDisplay", "Voxel开启关闭", false)]
		public void SetAbandonedDungeonVoxelDisplay(bool flag)
		{
			AbandonedDungeonController dungeonController = Module<AbandonedDungeonModule>.Self.GetDungeonController();
			if (dungeonController != null)
			{
				VoxelRoot[] componentsInChildren = dungeonController.GetComponentsInChildren<VoxelRoot>(true);
				if (componentsInChildren != null)
				{
					VoxelRoot[] array = componentsInChildren;
					for (int i = 0; i < array.Length; i++)
					{
						array[i].gameObject.SetActive(flag);
					}
				}
			}
		}
		*/
        [Command("Dungeon", "ClearDungeon", "重置副本", false)]
        public void ClearDungeon(string sceneName)
        {
            Module<DunGenRecordModule>.Self.ClearData(UtilNs.UtilParse.ParseEnum<AdditiveScene>(sceneName));
        }

        [Command("Dungeon", "AbandonedDungeonLevel", "设置VoxelDungeon副本解锁深度", false)]
        public void AbandonedDungeonLevel(int level)
        {
            Module<AbandonedDungeonModule>.Self.SetVoxelDungeonDepth(level);
        }

        [Command("Dungeon", "AbandonedDungeon2Level", "设置VoxelDungeon2副本解锁深度", false)]
        public void AbandonedDungeon2Level(int level)
        {
            Module<AbandonedDungeonModule>.Self.SetVoxelDungeon2Depth(level);
        }

        [Command("Dungeon", "AbandonedDungeon3Level", "设置VoxelDungeon3副本解锁深度", false)]
        public void AbandonedDungeon3Level(int level)
        {
            Module<AbandonedDungeonModule>.Self.SetVoxelDungeon3Depth(level);
        }

        [Command("Dungeon", "AbandonedDungeon4Level", "设置VoxelDungeon4副本解锁深度", false)]
        public void AbandonedDungeon4Level(int level)
        {
            Module<AbandonedDungeonModule>.Self.SetVoxelDungeon4Depth(level);
        }

        [Command("Dungeon", "AbandonedDungeonTransferToEntrance", "返回入口", false)]
        public void AbandonedDungeonTransferToEntrance()
        {
            Module<AbandonedDungeonModule>.Self.TransferToEntrance();
        }

        [Command("Dungeon", "AddDungeonKey", "增加副本钥匙事件（测UI用的，不实际增加）", false)]
        public void AddDungeonKey(string path)
        {
            Action<string> onAddKeyIcon = Module<GearModule>.Self.OnAddKeyIcon;
            if (onAddKeyIcon == null)
            {
                return;
            }
            onAddKeyIcon(path);
        }

        [Command("Dungeon", "RemoveDungeonKey", "删除副本钥匙事件（测UI用的，不实际删除）", false)]
        public void RemoveDungeonKey(string path)
        {
            Action<string> onRemoveKeyIcon = Module<GearModule>.Self.OnRemoveKeyIcon;
            if (onRemoveKeyIcon == null)
            {
                return;
            }
            onRemoveKeyIcon(path);
        }

        [Command("Dungeon", "ReGenTrialDungeon", "测试危险遗迹生成", false)]
        public void ReGenTrialDungeon(int level)
        {
            Module<Player>.Self.actor.StopActionAll(true, true);
            Module<TrialDungeonModule>.Self.EnterDungeon(Module<ScenarioModule>.Self.CurScene, level, false);
        }

        [Command("Dungeon", "ReGenAbandonedDungeon", "测试废弃遗迹生成", false)]
        public void ReGenAbandonedDungeon(int depth)
        {
            Module<AbandonedDungeonModule>.Self.ReGenDungeon(Module<ScenarioModule>.Self.CurScene, depth);
        }

        /*
		[Command("Dungeon", "TestVoxelDungeonOne", "测试挖掘voxel", false)]
		public void TestVoxelDungeonOne()
		{
			this.AddTestItemToBag("Voxel");
			this.SceneChange("VoxelDungeon_One");
		}

		[Command("Dungeon", "TestVoxel", "进入voxel测试场景", false)]
		public void TestVoxel()
		{
			this.AddTestItemToBag("Voxel");
			this.SceneChange("VoxelTest");
		}
		*/

        [Command("Dungeon", "CreateVoxelBomb", "生成voxel炸弹", false)]
        public void CreateVoxelBomb(int count)
        {
            Module<VoxelAimModule>.Self.TestCreateBombGroup(count);
        }

        [Command("Dungeon", "TestTrialDungeonParadise", "测试随机副本比奇塔场景", false)]
        public void TestTrialDungeonParadise()
        {
            Action<AdditiveScene, bool> onTrialDungeonUIShow = Module<RandomDungeonModule>.Self.OnTrialDungeonUIShow;
            if (onTrialDungeonUIShow == null)
            {
                return;
            }
            onTrialDungeonUIShow(AdditiveScene.TrialDungeon_Paradise, false);
        }

        [Command("Dungeon", "TestTrialDungeonWreck", "测试随机副本沉船场景", false)]
        public void TestTrialDungeonWreck()
        {
            Action<AdditiveScene, bool> onTrialDungeonUIShow = Module<RandomDungeonModule>.Self.OnTrialDungeonUIShow;
            if (onTrialDungeonUIShow == null)
            {
                return;
            }
            onTrialDungeonUIShow(AdditiveScene.TrialDungeon_Wreck, false);
        }

        [Command("Dungeon", "TestTrialDungeonSave", "测试随机试炼副本场景快速存档", false)]
        public void TestTrialDungeonSave()
        {
            Module<Pathea.SaveNs.Save>.Self.Remove(Module<RandomDungeonModule>.Self.saveLocker);
            Module<Pathea.SaveNs.Save>.Self.RequestAuto();
            Module<Pathea.SaveNs.Save>.Self.AddIfNotExist(Module<RandomDungeonModule>.Self.saveLocker);
        }

        [Command("Dungeon", "TestAbandonedDungeon", "测试挖掘副本场景", false)]
        public void TestAbandonedDungeon(bool HasExit)
        {
            Action<AdditiveScene, int, bool> onAbandonedDungeonUIShow = Module<RandomDungeonModule>.Self.OnAbandonedDungeonUIShow;
            if (onAbandonedDungeonUIShow == null)
            {
                return;
            }
            onAbandonedDungeonUIShow(AdditiveScene.VoxelDungeon, Module<AbandonedDungeonModule>.Self.GetAllLevel(AdditiveScene.VoxelDungeon), HasExit);
        }

        [Command("Dungeon", "TestAbandonedDungeon2", "测试挖掘副本2场景", false)]
        public void TestAbandonedDungeon2(bool HasExit)
        {
            Action<AdditiveScene, int, bool> onAbandonedDungeonUIShow = Module<RandomDungeonModule>.Self.OnAbandonedDungeonUIShow;
            if (onAbandonedDungeonUIShow == null)
            {
                return;
            }
            onAbandonedDungeonUIShow(AdditiveScene.VoxelDungeon2, Module<AbandonedDungeonModule>.Self.GetAllLevel(AdditiveScene.VoxelDungeon2), HasExit);
        }

        [Command("Dungeon", "TestAbandonedDungeon3", "测试挖掘副本3场景", false)]
        public void TestAbandonedDungeon3(bool HasExit)
        {
            Action<AdditiveScene, int, bool> onAbandonedDungeonUIShow = Module<RandomDungeonModule>.Self.OnAbandonedDungeonUIShow;
            if (onAbandonedDungeonUIShow == null)
            {
                return;
            }
            onAbandonedDungeonUIShow(AdditiveScene.VoxelDungeon3, Module<AbandonedDungeonModule>.Self.GetAllLevel(AdditiveScene.VoxelDungeon3), HasExit);
        }

        [Command("Dungeon", "TestAbandonedDungeon4", "测试挖掘副本4场景", false)]
        public void TestAbandonedDungeon4(bool HasExit)
        {
            Action<AdditiveScene, int, bool> onAbandonedDungeonUIShow = Module<RandomDungeonModule>.Self.OnAbandonedDungeonUIShow;
            if (onAbandonedDungeonUIShow == null)
            {
                return;
            }
            onAbandonedDungeonUIShow(AdditiveScene.VoxelDungeon4, Module<AbandonedDungeonModule>.Self.GetAllLevel(AdditiveScene.VoxelDungeon4), HasExit);
        }

        [Command("Dungeon", "TestLeaveAbandonedDungeon", "测试离开挖掘副本", false)]
        public void TestLeaveAbandonedDungeon()
        {
            Module<AbandonedDungeonModule>.Self.LeaveDungeon();
        }

        [Command("Store", "ShowStore", "打印所有商店商品", false)]
        public void ShowStore()
        {
            global::Debug.LogError("PriceIndex = " + (Module<StoreModule>.Self.CurPriceIndex * 100f).ToString() + "%");
            List<Store> allStore = Module<StoreModule>.Self.GetAllStore();
            foreach (Store store in allStore)
            {
                global::Debug.LogError("store: " + store.id.ToString() + "_" + store.Name);
                int num = 0;
                foreach (SellProduct sellProduct in store.singleProducts)
                {
                    global::Debug.LogError("    productId: " + sellProduct.data.id.ToString());
                    foreach (SellProductItem sellProductItem in sellProduct.sellProductItem)
                    {
                        global::Debug.LogError(string.Concat(new string[]
                        {
                            "        ",
                            num.ToString(),
                            "-",
                            sellProductItem.item.GetName(false),
                            "_",
                            sellProductItem.item.Count.ToString(),
                            "_",
                            sellProductItem.item.Grade.ToTransStr(),
                            ": ",
                            sellProductItem.UnitNum.ToString(),
                            " = ",
                            (sellProductItem.currency == -1) ? "金币" : Module<ItemPrototypeModule>.Self.GetItemName(sellProductItem.currency),
                            " ",
                            sellProductItem.CurrencyNum.ToString()
                        }));
                        num++;
                    }
                }
                num = 0;
                foreach (RepurchaseItem repurchaseItem in store.repurchaseItems)
                {
                    string[] array = new string[10];
                    array[0] = "    ";
                    array[1] = num.ToString();
                    array[2] = "-回购: ";
                    array[3] = repurchaseItem.item.GetName(false);
                    array[4] = "_";
                    array[5] = repurchaseItem.item.Count.ToString();
                    array[6] = "_";
                    array[7] = repurchaseItem.item.Grade.ToTransStr();
                    array[8] = " = ";
                    int num2 = 9;
                    DoubleInt price = repurchaseItem.price;
                    array[num2] = price.ToString();
                    global::Debug.LogError(string.Concat(array));
                    num++;
                }
            }
            Module<StoreModule>.Self.OpenStore(allStore[UnityEngine.Random.Range(0, allStore.Count)].id);
        }

        [Command("Store", "LockNpcStore", "锁定某个Npc的商店数据", false)]
        public void RemoveNpcStore(int npcid, int storeID)
        {
            Module<StoreModule>.Self.AddNpcLockStoreData(npcid, storeID);
        }

        [Command("Store", "RemoveLockNpcStore", "去除某个Npc的锁定", false)]
        public void RemoveLockNpcStore(int npcid, int storeID)
        {
            Module<StoreModule>.Self.RemoveNpcLockStoreData(npcid, storeID);
        }

        [Command("Store", "RefreshStore", "刷新所有商店商品", false)]
        public void RefreshStore()
        {
            foreach (Store store in Module<StoreModule>.Self.GetAllStore())
            {
                store.Refresh();
            }
        }

        [Command("Store", "RefreshOneStore", "刷新指定商店商品", false)]
        public void RefreshOneStore(int storeId)
        {
            Module<StoreModule>.Self.GetStore(storeId).Refresh();
        }

        [Command("Store", "RefreshOneStoreMulti", "多次刷新指定商店商品并检查", false)]
        public void RefreshOneStoreMulti(int storeId, int times)
        {
            Store store = Module<StoreModule>.Self.GetStore(storeId);
            for (int i = 0; i < times; i++)
            {
                store.Refresh();
                store.CheckRepeat();
            }
        }

        [Command("Store", "StoreRegain", "商店物品恢复", false)]
        public void StoreRegain()
        {
            foreach (Store store in Module<StoreModule>.Self.GetAllStore())
            {
                store.PastDay();
            }
        }

        [Command("Store", "RefreshPriceIndex", "物价指数刷新", false)]
        public void PriceIndexRefresh()
        {
            global::Debug.LogError("PriceIndex before = " + (Module<StoreModule>.Self.CurPriceIndex * 100f).ToString() + "%");
            Module<StoreModule>.Self.RefreshPriceIndex();
            global::Debug.LogError("PriceIndex later = " + (Module<StoreModule>.Self.CurPriceIndex * 100f).ToString() + "%");
        }

        [Command("Store", "PriceIndexShow", "打印当前物价指数", false)]
        public void PriceIndexShow()
        {
            global::Debug.LogError("PriceIndex current = " + (Module<StoreModule>.Self.CurPriceIndex * 100f).ToString() + "%");
        }

        [Command("Store", "RefreshPriceIndexMulti", "连续多次刷新物价指数", false)]
        public void PriceIndexRefreshMulti(int count)
        {
            for (int i = 0; i < count; i++)
            {
                float curPriceIndex = Module<StoreModule>.Self.CurPriceIndex;
                Module<StoreModule>.Self.RefreshPriceIndex();
                global::Debug.LogError(string.Concat(new string[]
                {
                    "PriceIndex: ",
                    (curPriceIndex * 100f).ToString(),
                    "% -> ",
                    (Module<StoreModule>.Self.CurPriceIndex * 100f).ToString(),
                    "%"
                }));
            }
        }

        [Command("Store", "BuyItem", "购买物品", false)]
        public void BuyItem(int storeId, int index, int count)
        {
            Store store = Module<StoreModule>.Self.GetStore(storeId);
            SellProductItem sellProductItem = null;
            foreach (SellProduct sellProduct in store.singleProducts)
            {
                foreach (SellProductItem sellProductItem2 in sellProduct.sellProductItem)
                {
                    index--;
                    if (index < 0)
                    {
                        sellProductItem = sellProductItem2;
                        break;
                    }
                }
                if (sellProductItem != null)
                {
                    break;
                }
            }
            if (sellProductItem != null)
            {
                StoreController.Self.Buy(store, ref sellProductItem, count);
                return;
            }
            global::Debug.LogError("索引的商品不存在!");
        }

        [Command("Store", "RecycleItem", "回收物品", false)]
        public void RecycleItem(int storeId, int itemId, int grade, int count)
        {
            Store store = Module<StoreModule>.Self.GetStore(storeId);
            ReadOnlyItemInstance item = ItemContainerExtension.GetItem(itemId, (GradeType)grade, count);
            if (item != null)
            {
                StoreController.Self.Recycle(store, item, count);
                return;
            }
            global::Debug.LogError("背包物品不存在!");
        }

        [Command("Store", "RepurchaseItem", "回购物品", false)]
        public void RepurchaseItem(int storeId, int index, int count)
        {
            Store store = Module<StoreModule>.Self.GetStore(storeId);
            RepurchaseItem repurchaseItem = null;
            foreach (RepurchaseItem repurchaseItem2 in store.repurchaseItems)
            {
                index--;
                if (index < 0)
                {
                    repurchaseItem = repurchaseItem2;
                    break;
                }
            }
            if (repurchaseItem != null)
            {
                StoreController.Self.Repurchase(store, ref repurchaseItem, count);
                return;
            }
            global::Debug.LogError("索引的回购商品不存在!");
        }

        [Command("Store", "OpenStore", "打开商店", false)]
        public void OpenStore(int id)
        {
            Module<StoreModule>.Self.OpenStore(id);
        }

        [Command("Store", "AllStoreAddModifier", "给所有商店施加价格影响", false)]
        public void AllStoreAddModifier(float value)
        {
            AttrModifier modifier = new AttrModifier(ModifyAttrType.RatioRatio, value);
            Module<StoreModule>.Self.AddAllStorePriceModifier(modifier);
        }

        [Command("Store", "AllStoreAddRecyclePriceModifier", "给所有商店施加回收价格影响", false)]
        public void AllStoreAddRecyclePriceModifier(float value)
        {
            AttrModifier modifier = new AttrModifier(ModifyAttrType.RatioRatio, value);
            Module<StoreModule>.Self.AddAllStoreRecyclePriceModifier(modifier);
        }
        /*
		[Command("BubbleDragon", "ShowBubbleDragon", "打开泡泡龙", false)]
		public void ShowBubbleDragon(int levelId)
		{
			ElementReward[] eleRewards = new ElementReward[]
			{
				new ElementReward(null, "A"),
				new ElementReward(null, "B"),
				new ElementReward(null, "C")
			};
			Module<BubbleDragonModule>.Self.(levelId, 0f, eleRewards, false);
		}
		[Command("BubbleDragon", "ShowBubbleDragonTimer", "打开泡泡龙j计时", false)]
		public void ShowBubbleDragonTimer(int levelId, float seconds)
		{
			Module<BubbleDragonModule>.Self.BeginGame(levelId, seconds, null, true);
		}
		*/

        [Command("AnimalCardFight", "ShowAnimalCardFight", "打开斗兽牌", false)]
        public void ShowAnimalCardFight(int levelId)
        {
            Module<AnimalCardFightModule>.Self.BeginGame(levelId);
        }

        [Command("CustomBubbleChoice", "AddCustomBubbleChoice", "添加自定义气泡", false)]
        public void AddCustomBubbleChoice(int npcID, int instanceID, string iconInfo, int iconTransId, string dialog, int cId, int missionId)
        {
            Module<NpcInteractionManager>.Self.AddCustomBubble(npcID, instanceID, iconInfo, iconTransId, dialog, cId, missionId);
        }

        [Command("ShowNewDateTime", "ShowNewTime", "新时间格式", false)]
        public void ShowNewTime(int year, int month, int day, int hour, int minute)
        {
            global::Debug.LogError(Module<GlobalModule>.Self.GlobalMisc.GetYearSeasonMonthDayStr(year, month, day, hour, minute));
        }

        [Command("Audio", "StartTestSound", "打开音效测试工具", false)]
        public void StartTestSound()
        {
            Module<AudioTestModeMgr>.Self.Start();
        }

        [Command("Audio", "SetLanguageVoiceEnglish", "设置语音为英文", false)]
        public void SetLanguageVoiceEnglish()
        {
            Singleton<OptionMgr>.Instance.SetLanguageVoice(Language.English);
        }

        [Command("Audio", "SetLanguageVoiceChinese", "设置语音为中文", false)]
        public void SetLanguageVoiceChinese()
        {
            Singleton<OptionMgr>.Instance.SetLanguageVoice(Language.SimplifiedChinese);
        }

        [Command("Audio", "EndTestSound", "关闭音效测试工具", false)]
        public void EndTestSound()
        {
            Module<AudioTestModeMgr>.Self.End();
        }

        [Command("Audio", "PostEvent", "触发音频事件", false)]
        public void PostEvent(string eventName)
        {
            Module<AudioManagerForWwise>.Self.PostEvent(eventName, base.gameObject);
        }

        [Command("Audio", "PostEventAtPlayerPos", "触发音频事件到玩家位置", false)]
        public void PostEventAtPlayerPos(string eventName)
        {
            Module<AudioManagerForWwise>.Self.PostDummySound(Module<Player>.Self.HeadPos, default(Quaternion), eventName, null);
        }

        [Command("Audio", "LoadBank", "加载声音bank", false)]
        public void LoadBank(string bankName)
        {
            Module<AudioManagerForWwise>.Self.LoadBank(bankName);
        }

        [Command("Audio", "UnloadBank", "卸载声音bank", false)]
        public void UnloadBank(string bankName)
        {
            Module<AudioManagerForWwise>.Self.UnloadBank(bankName);
        }

        [Command("Audio", "SetBgmSeason", "强制设置Bgm的Season(0-3)", false)]
        public void SetBgmSeason(int season)
        {
            Module<AudioManagerForWwise>.Self.Bgm.normalLayer.SetSeason(season);
        }

        [Command("Audio", "TestBgmLock", "TestBgmLock", false)]
        public void TestBgmLock()
        {
            bgmLocker = new Locker();
            Singleton<BgmLock>.Instance.AddLock(bgmLocker);
        }

        [Command("Audio", "TestBgmUnlock", "TestBgmUnlock", false)]
        public void TestBgmUnlock()
        {
            Singleton<BgmLock>.Instance.RemoveLock(bgmLocker);
        }

        [Command("Audio", "AddBossBattleBgm", "AddBossBattleBgm", false)]
        public void AddBossBattleBgm(string key)
        {
            Module<AudioManagerForWwise>.Self.Bgm.battleLayer.AddBossKey(key);
        }

        [Command("Audio", "RemoveBossBattleBgm", "RemoveBossBattleBgm", false)]
        public void RemoveBossBattleBgm(string key)
        {
            Module<AudioManagerForWwise>.Self.Bgm.battleLayer.RemoveBossKey(key, true);
        }

        [Command("Guide", "AddControlGuide", "增加控制指引", false)]
        public void AddControlGuide(params int[] ids)
        {
            Module<GuideModule>.Self.AddControlGuides(ids, true);
        }

        [Command("Guide", "AddAllControlGuide", "增加所有的控制指引", false)]
        public void AddAllControlGuide()
        {
            Module<GuideModule>.Self.AddAllControlGuide();
        }

        [Command("Guide", "AddNormalGuide", "增加普通指引", false)]
        public void AddNormalGuide(GuideType guideType)
        {
            Module<GuideModule>.Self.AddNormalGuide(guideType, true);
        }

        [Command("Guide", "AddAllNormalGuide", "增加所有的普通指引", false)]
        public void AddAllNormalGuide()
        {
            for (int i = 0; i < 50; i++)
            {
                Module<GuideModule>.Self.AddNormalGuide((GuideType)i, true);
            }
        }

        [Command("Guide", "ClearAllShownControlGuides", "清除全部已出现控制指引", false)]
        public void ClearAllShownControlGuides()
        {
            Module<GuideModule>.Self.ClearAllShownControlGuides();
        }

        [Command("ItemBox", "GenItemBox", "生成动态宝箱", false)]
        public void GenItemBox(int generatorId)
        {
            Module<ItemBoxModule>.Self.GenDynamicItemBox(GetPlayerFrontFloorPos(), Vector3.zero, generatorId, "TestItemBoxCube", false, false, null);
        }

        [Command("ItemBox", "GenItemBoxToScene", "生成动态宝箱到指定场景", false)]
        public void GenItemBoxToScene(string scene, int generatorId)
        {
            Module<ItemBoxModule>.Self.GenDynamicItemBox(UtilNs.UtilParse.ParseEnum<AdditiveScene>(scene), Vector3.zero, Vector3.zero, generatorId, "TestItemBoxCube", false);
        }

        [Command("ItemBox", "RemoveItemBox", "移除动态宝箱", false)]
        public void RemoveDynamicItemBox(string scene)
        {
            Module<ItemBoxModule>.Self.RemoveDynamicItemBox(UtilNs.UtilParse.ParseEnum<AdditiveScene>(scene), Vector3.zero, Vector3.zero);
        }

        [Command("ItemBox", "SetItemBoxMapIcon", "设置宝箱地图图标", false)]
        public void SetItemBoxMapIcon(bool show, float length)
        {
            Module<ItemBoxModule>.Self.SetItemBoxMapIcon(show, length);
        }

        [Command("ItemBox", "SetItemBoxOpenChange", "设置宝箱打开后消失", false)]
        public void SetItemBoxOpenChangeToResource(bool show)
        {
            Module<ItemBoxModule>.Self.SetOpenChangeResource(show);
        }

        [Command("Map", "HideMapOcclusion", "隐藏地图遮蔽", false)]
        public void HideMapOcclusion()
        {
            Module<MapModule>.Self.ShowMapOcclusion(false);
        }

        [Command("Map", "ShowMapOcclusion", "显示地图遮蔽", false)]
        public void ShowMapOcclusion()
        {
            Module<MapModule>.Self.ShowMapOcclusion(true);
        }

        [Command("Map", "OpenTransportMapUI", "打开传送地图界面", false)]
        public void OpenTransportMapUI()
        {
            Module<MapModule>.Self.OpenTransportMapUI();
        }

        [Command("Map", "SetHideMapIconInActivity", "设置活动中隐藏地图图标", false)]
        public void SetHideMapIconInActivity()
        {
            Module<MapModule>.Self.SetHideInActivity(!Module<MapModule>.Self.HideInActivity);
        }

        [Command("Map", "ShowBuildingText", "显示建筑文本", false)]
        public void ShowBuildingText(int id, bool isShow)
        {
            Module<MapModule>.Self.ShowBuildingText(id, isShow);
        }

        [Command("ResourceArea", "RefreshResourceArea", "刷新资源区域", false)]
        public void RefreshResourceArea()
        {
            Module<ResourceAreaModule>.Self.RefreshResourceArea(-1, false);
        }

        [Command("ResourceArea", "RefreshResourceAreaMission", "刷新任务资源区域", false)]
        public void RefreshResourceAreaMission(int id)
        {
            Module<ResourceAreaModule>.Self.RefreshResourceArea(id, true);
        }

        [Command("ResourceArea", "AddResourcePoint", "添加主场景资源点在玩家坐标", false)]
        public void AddResourcePoint(int id)
        {
            PosRot posRot = new PosRot
            {
                pos = Module<Player>.Self.GamePos,
                rot = Module<Player>.Self.GameRot.eulerAngles
            };
            Module<ResourceAreaModule>.Self.AddResourcePoint(AdditiveScene.Main, id, posRot, 0f, 0f, 0f);
        }

        [Command("ResourceArea", "ClearAllResourcePoints", "清除主场景全部资源点", false)]
        public void ClearAllResourcePoints()
        {
            Module<ResourceAreaModule>.Self.ClearAllResourcePoints(AdditiveScene.Main);
        }

        [Command("ResourceArea", "ResourcePointAddDisableHud", "资源点添加禁止Hud", false)]
        public void ResourcePointAddDisableHud()
        {
            if (resourcePointHudLocker == null)
            {
                resourcePointHudLocker = new Locker();
            }
            Module<ResourceAreaModule>.Self.AddDisableHud(resourcePointHudLocker);
        }

        [Command("ResourceArea", "ResourcePointRemoveDisableHud", "资源点移除禁止Hud", false)]
        public void ResourcePointRemoveDisableHud()
        {
            Module<ResourceAreaModule>.Self.RemoveDisableHud(resourcePointHudLocker);
        }

        [Command("Creation", "UnlockCreationBook", "解锁工坊手册/组装台", false)]
        public void CreationBookUnlock()
        {
            Module<FunctionLockModule>.Self.UnlockFunction(Function.HandBook);
        }

        [Command("Creation", "FinishCurSelectedPart", "立即组装当前选中部位", false)]
        public void CreationFinishCurSelectedPart()
        {
            if (Module<Player>.Self.actor.IsAnimPlaying("GetGift"))
            {
                global::Debug.LogError("正在组装");
                return;
            }
            CreationBuilder creationBuilder = UnityEngine.Object.FindObjectOfType<CreationBuilder>();
            if (null == creationBuilder)
            {
                global::Debug.LogError("组装未开启");
                return;
            }
            creationBuilder.FinishCurrentPartImmediately();
        }

        [Command("Creation", "OpenFreeAssembly", "开启无消耗组装", false)]
        public void OpenFreeAssembly()
        {
            CreationBuilder.isFreeAssembly = true;
        }

        [Command("Creation", "CloseFreeAssembly", "关闭无消耗组装", false)]
        public void CloseFreeAssembly()
        {
            CreationBuilder.isFreeAssembly = false;
        }

        [Command("Cutscene", "PlayCutscene", "播放过场动画", false)]
        public void PlayCutscene(string cutsceneName)
        {
            Module<CutsceneModule>.Self.CutsceneStart(cutsceneName, 1f, 1f, CutsceneAudioType.NormalOff, true);
        }

        [Command("Cutscene", "PlayCutscene2", "播放过场动画", false)]
        public void PlayCutscene2(string cutsceneName1, string cutsceneName2)
        {
            Module<CutsceneModule>.Self.CutsceneStart(cutsceneName1, cutsceneName2, 1f, 1f, CutsceneAudioType.NormalOff);
        }

        [Command("Cutscene", "PlayCG_START", "坐火车到沙石镇", false)]
        public void PlayCG_START()
        {
            PlayCutscene("CG_START");
        }

        [Command("Cutscene", "PlayCG_OCTOPUSATTACK", "章鱼怪", false)]
        public void PlayCG_OCTOPUSATTACK()
        {
            PlayCutscene("CG_OCTOPUSATTACK");
        }

        [Command("Cutscene", "PlayCG_CHURCH_ENTRANCE", "彭虎认为你是洛根同伙", false)]
        public void PlayCG_CHURCH_ENTRANCE()
        {
            PlayCutscene("CG_CHURCH_ENTRANCE");
        }

        [Command("Cutscene", "PlayCG_POLLUTION", "蜥蜴人丢垃圾到马特湖", false)]
        public void PlayCG_POLLUTION()
        {
            PlayCutscene("CG_POLLUTION");
        }

        [Command("Cutscene", "PlayCG_YAKMEL_DINNER", "库珀一家的晚餐", false)]
        public void PlayCG_YAKMEL_DINNER()
        {
            PlayCutscene("CG_YAKMEL_DINNER");
        }

        [Command("Cutscene", "PlayCG_FIGHT_INFARM", "温室遇袭", false)]
        public void PlayCG_FIGHT_INFARM()
        {
            PlayCutscene("CG_FIGHT_INFARM");
        }

        [Command("Cutscene", "PlayCG_BAOSHIQIDONE", "打开保湿器", false)]
        public void PlayCG_BAOSHIQIDONE()
        {
            PlayCutscene("CG_BAOSHIQIDONE");
        }

        [Command("Cutscene", "PlayCG_DIAOCHE", "安装吊车", false)]
        public void PlayCG_DIAOCHE()
        {
            PlayCutscene("CG_DIAOCHE");
        }

        [Command("Cutscene", "PlayCG_MASONPARTY", "欢送会", false)]
        public void PlayCG_MASONPARTY()
        {
            PlayCutscene("CG_MASONPARTY");
        }

        [Command("Cutscene", "PlayCG_XIYIRENDONG", "索道上掉下来", false)]
        public void PlayCG_XIYIRENDONG()
        {
            PlayCutscene("CG_XIYIRENDONG");
        }

        [Command("Cutscene", "PlayCG_SUODAO", "安装索道", false)]
        public void PlayCG_SUODAO()
        {
            PlayCutscene("CG_SUODAO");
        }

        [Command("Cutscene", "PlayCG_ZUOSUODAO", "坐索道", false)]
        public void PlayCG_ZUOSUODAO()
        {
            PlayCutscene("CG_ZUOSUODAO");
        }

        [Command("Cutscene", "PlayCG_XIYILIECHE", "蜥蜴人王登上列车", false)]
        public void PlayCG_XIYILIECHE()
        {
            PlayCutscene("CG_XIYILIECHE");
        }

        [Command("Cutscene", "PlayCG_XIYILOSE", "火车掉下去，贾斯迪救玩家", false)]
        public void PlayCG_XIYILOSE()
        {
            PlayCutscene("CG_XIYILOSE");
        }

        [Command("Cutscene", "PlayCG_OWENTREAT", "欧文请客吃饭", false)]
        public void PlayCG_OWENTREAT()
        {
            PlayCutscene("CG_OWENTREAT");
        }

        [Command("Cutscene", "PlayCG_INCHAMBER", "矿工在商会威胁颜", false)]
        public void PlayCG_INCHAMBER()
        {
            PlayCutscene("CG_INCHAMBER");
        }

        [Command("Cutscene", "PlayCG_CANYONBRIDGE", "铁路桥修建庆祝仪式", false)]
        public void PlayCG_CANYONBRIDGE()
        {
            PlayCutscene("CG_CANYONBRIDGE");
        }

        [Command("Cutscene", "PlayCG_CEREMONY", "博物馆开幕式", false)]
        public void PlayCG_CEREMONY()
        {
            PlayCutscene("CG_CEREMONY");
        }

        [Command("Cutscene", "PlayCG_SENDNEWS", "素馨送信", false)]
        public void PlayCG_SENDNEWS()
        {
            PlayCutscene("CG_SENDNEWS");
        }

        [Command("Cutscene", "PlayCG_PENEND", "彭虎秒杀", false)]
        public void PlayCG_PENEND()
        {
            PlayCutscene("CG_PENEND");
        }

        [Command("Cutscene", "PlayCG_BACKBYCABLECART", "大家一起坐索道回沙石镇", false)]
        public void PlayCG_BACKBYCABLECART()
        {
            PlayCutscene("CG_BACKBYCABLECART");
        }

        [Command("Cutscene", "PlayCG_AFTERSTORM", "沙尘暴之后的镇上状况", false)]
        public void PlayCG_AFTERSTORM()
        {
            PlayCutscene("CG_AFTERSTORM");
        }

        [Command("Cutscene", "PlayCG_MINTCOME", "明特到来", false)]
        public void PlayCG_MINTCOME()
        {
            PlayCutscene("CG_MINTCOME");
        }

        [Command("Cutscene", "ReplayCutsceneById", "使用id回放一个cutscene", false)]
        public void ReplayCutsceneById(int id)
        {
            Module<PhotoAlbumMgr>.Self.PlayCgByPresorePhotoId(id);
        }

        [Command("Drama", "PlayDrama", "播放戏剧", false)]
        public void PlayDrama(string assetName)
        {
            if (dramaGo != null)
            {
                UnityEngine.Object.Destroy(dramaGo);
            }
            dramaGo = new GameObject("TestDramaGo");
            dramaGo.transform.position = Module<Player>.Self.GamePos;
            DramaOtherData dramaOtherData = new DramaOtherData
            {
                isActivateCamera = true,
                posRot = new PosRot
                {
                    pos = Module<Player>.Self.GamePos,
                    rot = Module<Player>.Self.GameRot.eulerAngles
                }
            };
            Module<Player>.Self.DoDrama(Module<Player>.Self.GamePos, Module<Player>.Self.GameRot.eulerAngles, Module<Player>.Self.GamePos, Module<Player>.Self.GameRot.eulerAngles, assetName, dramaOtherData, out runningDramaHandle);
        }

        [Command("Drama", "PlaySitDrama", "播放坐下戏剧", false)]
        public void PlaySitDrama()
        {
            PlayDrama("Timeline_Sit");
        }

        [Command("Drama", "UpdateDramaTransPara", "更新戏剧位置测试", false)]
        public void UpdateDramaTransPara()
        {
            TransPara transPara = new TransPara
            {
                beginPos = Module<Player>.Self.GamePos,
                beginRot = Module<Player>.Self.GameRot.eulerAngles,
                endPos = Module<Player>.Self.GamePos,
                endRot = Module<Player>.Self.GameRot.eulerAngles
            };
            dramaGo.transform.Translate(Vector3.forward);
            Module<DramaModule>.Self.UpdateTrans(runningDramaHandle, transPara, dramaGo.transform);
            PosRot posRot = new PosRot
            {
                pos = Module<Player>.Self.GamePos,
                rot = Module<Player>.Self.GameRot.eulerAngles
            };
            Module<DramaModule>.Self.UpdateOtherInfoTrans(runningDramaHandle, posRot);
        }

        [Command("Cooking", "UnlockAllCookings", "解锁全部菜谱", false)]
        public void UnlockAllCookings()
        {
            Module<CookingModule>.Self.UnlockAllCookingFormulas();
        }

        [Command("Cooking", "UnlockCooking", "解锁菜谱", false)]
        public void UnlockCooking(int cookingId)
        {
            Module<CookingModule>.Self.UnlockCookingFormula(cookingId);
        }

        [Command("Home", "SetHomeExtraUnlockLayer", "解锁家园层数", false)]
        public void SetHomeExtraUnlockLayer(int count)
        {
            Module<RegionModule>.Self.GetPresetRegion("FarmCustomMesh").SetExtraUnlockLayer(count);
        }

        [Command("Home", "SetSandWallUnlock", "解锁锁定防沙墙", false)]
        public void SetSandWallUnlock(bool unlock)
        {
            Module<SandWallModule>.Self.SetUnlcok(unlock);
        }

        [Command("Home", "ShowRegionEdit", "打开Region界面", false)]
        public void ShowRegionEdit()
        {
            Module<RegionModule>.Self.StartHomeEdit(Module<RegionModule>.Self.GetPresetRegion("FarmCustomMesh") as PresetRegionContainerFarm);
        }

        [Command("Home", "ShowBuildingEdit", "打开Building界面", false)]
        public void ShowBuildingEdit()
        {
            PresetRegionContainerFarm region = Module<RegionModule>.Self.GetPresetRegion("FarmCustomMesh") as PresetRegionContainerFarm;
            Singleton<BuildingEditorActivator>.Instance.OpenBuildingEditor(region, null);
        }

        [Command("Home", "ShowBuildingInDoorEdit", "打开InDoorBuilding界面", false)]
        public void ShowBuildingInDoorEdit()
        {
            Singleton<BuildingEditorInDoorMgr>.Instance.TryOpenIndoorEditor();
        }

        [Command("Home", "HomeNameEdit", "打开设置工坊名字界面", false)]
        public void HomeNameEdit()
        {
            Module<HomeModule>.Self.OpenHomeNameEdit();
        }

        [Command("Home", "SetFarmLevel", "设置农场等级", false)]
        public void SetFarmLevel(int level)
        {
            Module<HomeModule>.Self.FarmLevel = level;
        }

        [Command("Home", "CheckPlayerHasPortal", "打开Region界面", false)]
        public void CheckPlayerHasPortal()
        {
            HomeCombineMgr self = Module<HomeCombineMgr>.Self;
            RoomRegionContainerViewer playerInsideViewer = Singleton<BuildingEditorInDoorMgr>.Instance.GetPlayerInsideViewer();
            global::Debug.LogError(self.CheckPortalRoom((playerInsideViewer != null) ? playerInsideViewer.Container : null));
        }

        [Command("TrialDungeon", "TestTrialDungeonResultCount", "获得危险遗迹结果", false)]
        public void TestTrialDungeonResultCount(int level, int result)
        {
        }

        [Command("TrialDungeon", "TestTrialDungeonLatestResult", "获得最新危险遗迹结果", false)]
        public void TestTrialDungeonLatestResult(int level)
        {
        }

        [Command("TrialDungeon", "SetAllTrialDungeonClearedLevelMax", "设置所有试炼副本清除的等级最大", false)]
        public void SetAllTrialDungeonClearedLevelMax()
        {
            List<AdditiveScene> allScenes = TrialDungeonRuleData.GetAllScenes();
            for (int i = 0; i < allScenes.Count; i++)
            {
                int maxLevel = TrialDungeonRuleData.GetMaxLevel(allScenes[i]);
                Module<TrialDungeonModule>.Self.SetClearedLevel(allScenes[i], maxLevel);
            }
        }

        [Command("TrialDungeon", "LeaveTrialDungeon", "离开试炼副本", false)]
        public void LeaveTrialDungeon()
        {
            Module<TrialDungeonModule>.Self.LeaveDungeon();
        }

        [Command("TrialDungeon", "UnlockTrialDungeonMaxLevel", "解锁试炼副本最大长度", false)]
        public void UnlockTrialDungeonMaxLevel()
        {
            Module<TrialDungeonModule>.Self.SetTrialDungeonDepth();
        }

        [Command("Actor", "ChangeAttr", "修改actor属性", false)]
        public void ChangeAttr(int actorID, string attr, float changeValue)
        {
            Actor actor = Module<ActorMgr>.Self.GetActor(actorID);
            ActorRunTimeAttrType attrType;
            if (actor != null && Enum.TryParse<ActorRunTimeAttrType>(attr, out attrType))
            {
                actor.ApplyAttrChange(attrType, changeValue);
            }
        }

        [Command("Actor", "ShowActorState", "显示actor属性", false)]
        public void ShowActorState(int actorID)
        {
            if (null == stateViewer && null != Module<ActorMgr>.Self.RootTrans)
            {
                stateViewer = Module<ActorMgr>.Self.RootTrans.gameObject.AddComponent<ActorStateViewer>();
            }
            if (null != stateViewer)
            {
                stateViewer.ResetActor(actorID);
            }
        }

        [Command("Actor", "CloseActorState", "关闭actor属性", false)]
        public void CloseActorState()
        {
            if (null != stateViewer)
            {
                UnityEngine.Object.Destroy(stateViewer);
            }
        }

        [Command("Actor", "CreateRidable", "创建坐骑", false)]
        public void CreatRidable(int id)
        {
            Ridable ridable = Module<RideModule>.Self.CreateRidable(id, Module<Player>.Self.GamePos + Vector3.one, Vector3.zero, -1, 8000, Module<ScenarioModule>.Self.CurScene);
            if (ridable != null)
            {
                ridable.ridableData.InitAttrByDefault();
                ridable.ridableData.UpdateLoyaltyDataEffect(true);
                ridable.SetFollowerID(8000, false);
                ridable.SetState(Ridable.RidableState.Follow);
                return;
            }
            global::Debug.LogError(string.Format("CreateRidable failed: {0}", id));
        }

        [Command("Actor", "CreatRidableWithInstanceID", "创建坐骑指定InstanceID", false)]
        public void CreatRidableWithInstanceID(int id, int instanceID)
        {
            Ridable ridable = Module<RideModule>.Self.CreateRidable(id, Module<Player>.Self.GamePos + Vector3.one, Vector3.zero, instanceID, 8000, Module<ScenarioModule>.Self.CurScene);
            ridable.ridableData.InitAttrByDefault();
            ridable.ridableData.UpdateLoyaltyDataEffect(true);
            ridable.SetFollowerID(8000, false);
            ridable.SetState(Ridable.RidableState.Follow);
        }

        [Command("Actor", "DestroyRidable", "删除坐骑", false)]
        public void DestroyRidable(int instanceID)
        {
            Module<RideModule>.Self.DestroyRidable(instanceID);
        }

        [Command("Actor", "ShowRidableFastRunSpeed", "查看坐骑冲刺速度", false)]
        public void ShowRidableFastRunSpeed(int instanceID)
        {
            Ridable ridable = Module<RideModule>.Self.GetRidable(instanceID);
            if (ridable != null)
            {
                RidableData ridableData = ridable.ridableData;
                ridableData.GetAttr(Pathea.RideNs.AttrType.SpeedF);
                float num = (ridableData.protoData.speedLimit.y + ridable.ridableData.protoData.speedLimit.x) / 2f;
                Module<PlayerRidableStateModule>.Self.ridableSetting.GetSpeedSizeByType(ridableData.protoData.type);
            }
        }

        [Command("Actor", "AddRidableLoyalty", "加减坐骑忠诚", false)]
        public void AddRidableLoyalty(int instanceID, float addvalue)
        {
            Ridable ridable = Module<RideModule>.Self.GetRidable(instanceID);
            if (ridable != null)
            {
                ridable.ridableData.ChangeAttr(RuntimeAttrType.Loyalty, addvalue, false);
            }
        }

        [Command("Actor", "AddRidableSp", "加减坐骑体力", false)]
        public void AddRidableSp(int instanceID, float addvalue)
        {
            Ridable ridable = Module<RideModule>.Self.GetRidable(instanceID);
            if (ridable != null)
            {
                ridable.ridableData.ChangeAttr(RuntimeAttrType.Sp, addvalue, false);
            }
        }

        [Command("Actor", "PlayerGetOffRidable", "玩家下坐骑", false)]
        public void PlayerGetOffRidable()
        {
            Module<Player>.Self.GetOffRidable(true);
        }

        [Command("Debug", "DisableShadow", "关闭阴影", false)]
        public void DisableShadow()
        {
            QualitySettings.shadows = ShadowQuality.Disable;
        }

        [Command("Debug", "EnableShadow", "开启阴影", false)]
        public void EnableShadow()
        {
            QualitySettings.shadows = ShadowQuality.All;
        }

        [Command("Debug", "ToggleDebugIME", " 调试IME", false)]
        public void ToggleDebugIME()
        {
            if (imeDebug == null)
            {
                new GameObject("IME Debug").AddComponent<InputMethodDebug>();
                return;
            }
            UnityEngine.Object.Destroy(imeDebug.gameObject);
        }

        [Command("Debug", "ToggleMipmapStreaming", "mipmap流式加载", false)]
        public void ToggleMipmapStreaming()
        {
            QualitySettings.streamingMipmapsActive = !QualitySettings.streamingMipmapsActive;
        }

        [Command("Debug", "MipmapMemoryBudget", "mipmap贴图内存总开销", false)]
        public void MipmapStreamingMemoryBudget(int budgetInMb)
        {
            QualitySettings.streamingMipmapsMemoryBudget = (float)budgetInMb;
        }

        [Command("Debug", "UnloadUnusedAssets", "释放没有引用的资源", false)]
        public void UnloadUnusedAssets()
        {
            Resources.UnloadUnusedAssets();
        }

        [Command("CompareGame", "SetCompareShowcase", "设置对比展台", false)]
        public void SetCompareShowcase(int sceneItemId, CompareShowcaseData.ItemState itemState)
        {
            Module<CompareGameManager>.Self.SetCompareShowcase(sceneItemId, itemState, 0, 0, int.MaxValue);
        }

        [Command("Museum", "DeliverFragment", "发布碎片交换订单", false)]
        public void DeliverFragment()
        {
            Module<MuseumModule>.Self.DeliverExchange();
        }

        [Command("Museum", "ShowMuseumDisplay", "打印博物馆摆放状态", false)]
        public void ShowMuseumDisplay()
        {
            Module<MuseumModule>.Self.PrintAllSeatState();
        }

        [Command("Museum", "ShowMuseumSubmit", "打印博物馆提交记录", false)]
        public void ShowMuseumSubmit()
        {
            Module<MuseumModule>.Self.PrintSubmitRecord();
        }

        [Command("Museum", "UpgradeMuseum", "升级博物馆房间", false)]
        public void UpgradeMuseum()
        {
            Module<MuseumModule>.Self.Upgrade();
        }

        /*
		[Command("Museum", "TestMuseum", "测试博物馆", false)]
		public void MuseumUnloadAssetAll()
		{
			this.AddTestItemToBag("Exhibition");
			this.SceneChangeExtrance("MuseumIn");
		}
		*/

        [Command("Museum", "ToggleMuseumRewardUI", "显示/隐藏博物馆奖励UI", false)]
        public void ToggleMuseumRewardUI()
        {
            MuseumRewardGUI component = base.GetComponent<MuseumRewardGUI>();
            if (component == null)
            {
                base.gameObject.AddComponent<MuseumRewardGUI>();
                return;
            }
            UnityEngine.Object.Destroy(component);
        }

        [Command("Museum", "AddMuseumSubmitCount", "添加博物馆提交(没用了)", false)]
        public void AddMuseumSubmitCount(int i)
        {
        }

        [Command("Museum", "AddMuseumSubmit", "添加博物馆仓库提交", false)]
        public void SubmitItem(int itemID)
        {
            Module<MuseumModule>.Self.AddSubmitItemPublic(Module<ItemInstance.Module>.Self.CreateAsDefault(itemID, 1));
        }

        [Command("Museum", "SetMuseumDisplayActive", "开关博物馆展品显示", false)]
        public void SetMuseumDisplayActive(bool show)
        {
            Module<MuseumModule>.Self.SetMuseumDisplayActive(show);
        }

        [Command("Museum", "SetMuseumLevel", "设置博物馆等级", false)]
        public void SetMuseumLevel(int level)
        {
            Module<MuseumModule>.Self.SetCurrentMuseumLevel(level);
        }

        [Command("Proficiency", "UnlockProficiency", "解锁知识", false)]
        public void UnlockProficiency(int id, int index)
        {
            Module<ProficiencyModule>.Self.Unlock(id, index);
        }

        [Command("Proficiency", "SetAllProficiencyExpMax", "设置所有知识经验最大", false)]
        public void SetAllProficiencyExpMax()
        {
            Module<ProficiencyModule>.Self.SetAllProficiencyExpMax();
        }

        [Command("Proficiency", "ResetAllProficiencyApplyPoint", "重置所有知识加点", false)]
        public void ResetAllProficiencyApplyPoint()
        {
            Module<ProficiencyModule>.Self.ResetAllProficiencyApplyPoint();
        }

        [Command("Proficiency", "ResetAllProficiencyApplyPointType", "按类型重置所有知识加点", false)]
        public void ResetAllProficiencyApplyPointType(ProficiencyType proficiencyType)
        {
            Module<ProficiencyModule>.Self.ResetAllProficiencyApplyPoint(proficiencyType);
        }

        [Command("Statistics", "ShowStatistics", "显示一条统计信息", false)]
        public void ShowStatistics(int id)
        {
            InfoTipMgr.Instance.SendSimpleTip(string.Format("统计项{0} [{1}]", id, Module<StatisticsModule>.Self.GetNumber(id)), "", -1f);
        }

        [Command("Firework", "ShowFirework", "打开烟花", false)]
        public void ShowFirework(int npcId)
        {
            Module<FireworkModule>.Self.BeginGame(npcId, null);
        }

        [Command("AssetBundle", "OpenAssetBundleViewer", "AB调试器", false)]
        public void OpenAssetBundleViewer()
        {
            UnityEngine.Object.DontDestroyOnLoad(new GameObject("AssetBundleViewer", new System.Type[]
            {
                typeof(AssetBundleRuntimeViewer)
            }));
        }

        [Command("AssetBundle", "CloseAssetBundleViewer", "AB调试器", false)]
        public void CloseAssetBundleViewer()
        {
            GameObject gameObject = GameObject.Find("AssetBundleViewer");
            if (gameObject != null)
            {
                UnityEngine.Object.Destroy(gameObject);
            }
        }

        [Command("AdventureMachine", "AdventureMachinePlayerApplyBattleEvent", "冒险机玩家应用战斗事件", false)]
        public void AdventureMachinePlayerApplyBattleEvent(int index)
        {
            Module<AdventureMachineModule>.Self.PlayerApplyBattleEvent(index);
        }

        [Command("AdventureMachine", "AdventureMachineEnemyApplyBattleEvent", "冒险机敌人应用战斗事件", false)]
        public void AdventureMachineEnemyApplyBattleEvent(int index)
        {
            Module<AdventureMachineModule>.Self.EnemyApplyBattleEvent(index);
        }

        [Command("AdventureMachine", "UpdateAdventureMachineConfig", "刷新冒险机配置", false)]
        public void UpdateAdventureMachineConfig()
        {
            Module<AdventureMachineModule>.Self.UpdateAdventureMachineConfig();
        }

        [Command("RidableHouse", "UnlockRidableHouse", "解锁坐骑马棚", false)]
        public void UnlockRidableHouse()
        {
            Module<RegionModule>.Self.RidableHouseUnlocked = true;
            AddFarmAnimalItems();
        }

        [Command("RidableHouse", "CallRidable", "召唤坐骑", false)]
        public void CallRidable()
        {
            Module<RidableHouseModule>.Self.Call();
        }

        [Command("RideStore", "RefreshRidableStore", "刷新坐骑商店", false)]
        public void RefreshRidableStore()
        {
            Module<RideStoreModule>.Self.RefreshRidableStore();
        }

        [Command("RideStore", "SetAllRidableStoresRentFree", "设置所有坐骑商店租赁免费", false)]
        public void SetAllRidableStoresRentFree()
        {
            Module<RideStoreModule>.Self.SetAllRidableStoresRentFree(true);
        }

        [Command("RideStore", "SetRidableStore", "设置坐骑商店坐骑为指定原型Id", false)]
        public void SetRidableStore(int sceneItemId, int index, int protoId)
        {
            Module<RideStoreModule>.Self.SetRidableStore(sceneItemId, index, protoId);
        }

        [Command("Lod", "SetLodGroupLevel", "设置LodGroupLevel", false)]
        public void SetLodGroupLevel(int level)
        {
            foreach (LODGroup lodgroup in UnityEngine.Object.FindObjectsOfType<LODGroup>())
            {
                lodgroup.fadeMode = LODFadeMode.None;
                lodgroup.ForceLOD(Mathf.Min(level, lodgroup.lodCount - 1));
            }
        }

        [Command("Achievement", "IsAchievementUnlock", "是否解锁成就", false)]
        public void IsAchievementUnlock(int id)
        {
            Module<AchievementModule>.Self.IsUnloked(id);
        }

        [Command("Achievement", "UnlockAchievement", "解锁成就", false)]
        public void UnlockAchievement(int id)
        {
            Module<AchievementModule>.Self.Unlock(id);
        }

        [Command("Achievement", "LockAchievement", "锁定成就", false)]
        public void LockAchievement(int id)
        {
            Module<AchievementModule>.Self.Lock(id);
        }

        [Command("Achievement", "LockAllAchievements", "锁定所有成就", false)]
        public void LockAllAchievements()
        {
            Module<AchievementModule>.Self.LockAll();
        }

        [Command("Achievement", "CheckAllAchievements", "检测所有成就达成", false)]
        public void CheckAllAchievements()
        {
            Module<AchievementModule>.Self.CheckAll();
        }

        [Command("StoryScript", "NpcScriptAddBehaviour", "脚本npc添加行为", false)]
        public void NpcScriptAddBehaviour(int npcId, string behaviour, string idName, string param)
        {
            Npc npc = Module<NpcMgr>.Self.GetNpc(npcId);
            if (npc != null)
            {
                string behaviourName = BehaviorUtil.Combine(behaviour, idName);
                StoryHelper.ScriptAddNpcBehaviour(npc, behaviourName, param);
                return;
            }
            global::Debug.LogError("not exist npc: " + npcId.ToString());
        }

        [Command("StoryScript", "NpcScriptRemoveBehaviour", "脚本npc删除行为", false)]
        public void NpcScriptRemoveBehaviour(int npcId, string behaviourName, string idName)
        {
            Npc npc = Module<NpcMgr>.Self.GetNpc(npcId);
            if (npc != null)
            {
                StoryHelper.ScriptRemoveNpcBehaviour(npc, behaviourName);
                return;
            }
            global::Debug.LogError("not exist npc: " + npcId.ToString());
        }

        [Command("StoryScript", "ScriptAddModifier", "脚本加入modifier", false)]
        public void ScriptAddModifier(string typeParam, int id)
        {
            Module<StoryAssistant>.Self.AddModifier(typeParam, id);
        }

        [Command("StoryScript", "ScriptRemoveModifier", "脚本移除modifier", false)]
        public void ScriptRemoveModifier(int id)
        {
            Module<StoryAssistant>.Self.RemoveModifier(id);
        }

        [Command("StoryScript", "ScriptSetNpcCanInteract", "脚本设置npc不可交互状态", false)]
        public void ScriptSetNpcCanInteract(int npcId, bool add)
        {
            Module<StoryAssistant>.Self.SetNpcInteractLocker(npcId, add);
        }

        [Command("StoryScript", "ScriptSetAllCanInteract", "脚本设置所有物品不可交互状态", false)]
        public void ScriptSetAllCanInteract(bool add)
        {
            Module<StoryAssistant>.Self.SetAllInteractLocker(add);
        }

        [Command("StoryScript", "ScriptRun", "运行剧情脚本", false)]
        public void ScriptRun(int scriptId)
        {
            StoryHelper.RunScript(scriptId);
        }

        [Command("Sand", "SetAllSandUnitIsFull", "填满所有积沙物", false)]
        public void SetAllSandUnitIsFull()
        {
            Module<SandModule>.Self.ForeachAllSandUnit(delegate (SandUnit t)
            {
                t.Data.SetCurValue(SandUnitCatalog.Sand, t.Data.GetMaxValue(SandUnitCatalog.Sand));
            });
        }

        [Command("Sand", "SetAllSandUnitIsEmpty", "清空所有积沙物", false)]
        public void SetAllSandUnitIsEmpty()
        {
            Module<SandModule>.Self.ForeachAllSandUnit(delegate (SandUnit t)
            {
                t.Data.SetCurValue(SandUnitCatalog.Sand, 0f);
            });
        }

        [Command("Global", "GlobalSetInfo", "设置GlobalInfo", false)]
        public void GlobalSetInfo(string key, string info)
        {
            Module<GlobalBlackboard>.Self.SetInfo(key, info);
        }

        [Command("Global", "GlobalRemoveKey", "移除GlobalKey", false)]
        public void GlobalRemoveKey(string key)
        {
            Module<GlobalBlackboard>.Self.RemoveInfo(key);
        }

        [Command("Stopwatch", "RealTimeStopwatchAdd", "添加现实时间计时器", false)]
        public void RealTimeStopwatchAdd(int id, float seconds)
        {
            Module<RealTimeStopWatchMgr>.Self.Start(id, seconds);
        }

        [Command("Stopwatch", "RealTimeStopwatchRemove", "移除现实时间计时器", false)]
        public void RealTimeStopwatchRemove(int id)
        {
            Module<RealTimeStopWatchMgr>.Self.Stop(id);
        }

        [Command("Stopwatch", "GameTimeStopwatchAdd", "添加游戏时间计时器", false)]
        public void GameTimeStopwatchAdd(int id, int minutes)
        {
            Module<GameTimeStopWatchMgr>.Self.StartLaterGameTimeWatch(id, minutes);
        }

        [Command("Stopwatch", "GameTimeStopwatchRemove", "移除游戏时间计时器", false)]
        public void GameTimeStopwatchRemove(int id)
        {
            Module<GameTimeStopWatchMgr>.Self.StopGameTimeWatch(id);
        }

        [Command("Resource", "CheckResourceAreaValid", "检查资源区域是否还有资源点", false)]
        public void CheckResourceAreaValid(int groupId)
        {
        }

        [Command("HomeMachine", "WaterTowerChangeValue", "水塔设置水量", false)]
        public void WaterTowerChangeValue(float addValue)
        {
            Module<MachineModule>.Self.GetFirstMachineByTag(MachineTag.WaterSupply, null).GetSupport(MachineSupportType.Water).Add(addValue);
        }

        [Command("SandFishing", "SandFishingGenPrey", "生成猎物算法测试", false)]
        public void GenPreyTest(int count)
        {
            Module<SandFishingModule>.Self.TestGenPrey(count);
        }

        [Command("SandFishing", "SetShowBitePrey", "设置是否显示咬钩的猎物", false)]
        public void SetShowBitePrey(bool show)
        {
            Module<SandFishingModule>.Self.SetShowBitePrey(show);
        }

        [Command("SandFish", "AddSandFishBait", "添加沙捕用的鱼饵", false)]
        public void AddSandFishBait(int count)
        {
            Module<SandFishModule>.Self.AddFishBait(count);
        }

        [Command("SandFishing", "ShowSandFishUI", "设置是否显示咬钩的猎物", false)]
        public void ShowSandFishUI()
        {
            Module<SandFishModule>.Self.OpenUI();
        }

        [Command("SandFish", "SandFishAddFish", "添加一条鱼", false)]
        public void SandFishAddFish(int fishId)
        {
            if (Module<SandFishModule>.Self.controller != null)
            {
                Module<SandFishModule>.Self.controller.AddFish(fishId, null);
            }
        }

        public Vector3 GetPlayerFrontFloorPos()
        {
            RaycastHit raycastHit;
            Physics.Raycast(Module<Player>.Self.GamePos + Vector3.up * 2f + Vector3.forward * 1f, Vector3.down, out raycastHit, 10f, 1024);
            return raycastHit.point;
        }

        [Command("Tip", "AddCentralTip", "添加中心提示", false)]
        public void AddCentralTip(string tip)
        {
            CentralMgr.Instance.AddNormalDisplay(tip, null, false, false);
        }

        [Command("Tip", "TipsSystemShow", "显示提示", false)]
        public void TipsSystemShow(int type, int content)
        {
            if (type < 0)
            {
                InfoTipMgr.Instance.SendSimpleTip(TextMgr.GetStr(content), "", -1f);
                return;
            }
            InfoTipMgr.Instance.SendSystemTip(TextMgr.GetStr(content), (SystemTipType)type);
        }

        [Command("Gift", "UnlimitedTodayGift", "当天无限送礼", false)]
        public void UnlimitedTodayGift()
        {
            Module<SendGiftModule>.Self.Config.UnlimitedTodayGift = true;
        }

        [Command("Gift", "CancelUnlimitedTodayGift", "取消当天无限送礼", false)]
        public void CancelUnlimitedTodayGift()
        {
            Module<SendGiftModule>.Self.Config.UnlimitedTodayGift = false;
        }

        [Command("Gift", "UnlockNpcAllPreferences", "显示npc所有喜好", false)]
        public void UnlockNpcAllPreferences(int npcId)
        {
            Module<SendGiftModule>.Self.UnlockNpcAllPreferences(npcId);
        }

        [Command("Gift", "UnlockAllNpcAllPreferences", "显示所有npc所有喜好", false)]
        public void UnlockAllNpcAllPreferences()
        {
            Module<SendGiftModule>.Self.UnlockAllNpcAllPreferences();
        }

        [Command("Wish", "MakeWishes", "测试许愿", false)]
        public void MakeWishes(int count)
        {
            Module<DynamicWishModule>.Self.TestMakeWish(count);
        }

        [Command("Wish", "MakeWishesNpc", "测试许愿", false)]
        public void MakeWishesNpc(int npcId, int wishItemId)
        {
            Module<DynamicWishModule>.Self.MakeWish(npcId, wishItemId);
        }

        [Command("OpenTreasure", "OpenOpenTreasureUI", "打开开宝箱界面", false)]
        public void OpenOpenTreasureUI(string level)
        {
            Module<OpenTreasureModule>.Self.OpenOpenTreasureUI(level, delegate (bool success)
            {
            });
        }

        [Command("Dance", "StartDanceQualifications", "开启跳舞资格", false)]
        public void StartDanceQualifications(bool isQualifications)
        {
            Module<DanceModule>.Self.SetQualifications(isQualifications, false);
        }

        [Command("Dance", "StartDanceActivity", "开始跳舞活动", false)]
        public void StartDanceActivity()
        {
            Module<DanceModule>.Self.StartActivity(null);
        }

        [Command("Dance", "EndDanceActivity", "结束跳舞活动", false)]
        public void EndDanceActivity()
        {
            Module<DanceModule>.Self.EndActivity();
        }

        [Command("Dance", "CreateSceneItemDanceStage", "创建跳舞牌牌", false)]
        public void CreateSceneItemDanceStage(int id = 0)
        {
            Vector3 pos = Module<Player>.Self.GamePos + Module<Player>.Self.actor.Forward * 2f + new Vector3(0f, 1f, 0f);
            Vector3 rot = Module<Player>.Self.GameRot.eulerAngles + new Vector3(0f, 180f, 0f);
            AdditiveScene curScene = Module<ScenarioModule>.Self.CurScene;
            Module<SceneItemManager>.Self.CreateSceneItem(GameUtils.EnumParse<SceneItemType>("DanceStage"), id, "SceneItem_DanceStage", curScene, pos, rot, "", "");
        }

        [Command("GetItem", "GetItem", "获得道具", false)]
        public void GetItem(params int[] itemIds)
        {
            List<ItemInstance> list = new List<ItemInstance>();
            for (int i = 0; i < itemIds.Length; i++)
            {
                list.Add(Module<ItemInstance.Module>.Self.CreateAsDefault(itemIds[i], 1));
            }
            Module<GetItemModule>.Self.Add(list, true);
        }

        [Command("Camera", "CameraFocusNpc", "摄像机锁定npc", false)]
        public void CameraFocusNpc(int npcId)
        {
            Actor actor = Module<ActorMgr>.Self.GetActor(npcId);
            Module<InteractionCameraModule>.Self.ActiveCamera(actor);
        }

        [Command("Camera", "CameraDefocusNpc", "摄像机解除锁定", false)]
        public void CameraDefocusNpc()
        {
            Module<InteractionCameraModule>.Self.DeactiveCamera();
        }

        [Command("Camera", "EnterCaptureMode", "进入拍照模式", false)]
        public void EnterCaptureMode()
        {
            Module<CamCaptureModule>.Self.EnterCaptureMode4Cmd();
        }

        [Command("Camera", "UnlockCaptureMode", "添加一个相机道具以解锁拍照模式", false)]
        public void UnlockCaptureMode()
        {
            AddItemToBag(15400019, 1);
        }

        [Command("Camera", "UnlockAllPresorePhoto", "解锁所有的预存照片到相册", false)]
        public void UnlockAllPresorePhoto()
        {
            Module<PhotoAlbumMgr>.Self.UnlockAllPresorePhoto();
        }

        [Command("Camera", "UnlockPresorePhotoById", "解锁一个指定的预存照片到相册", false)]
        public void UnlockPresorePhotoById(int id)
        {
            Module<PhotoAlbumMgr>.Self.UnlockPresorePhoto(id);
        }

        [Command("Camera", "UnlockCutscenePhotoByName", "解锁cutscene对应的预存截图到相册", false)]
        public void UnlockCutscenePhotoByName(string cutsceneName)
        {
            Module<PhotoAlbumMgr>.Self.UnlockPresorePhotoByCutsceneName(cutsceneName);
        }

        [Command("Camera", "UnlockAllCutscenePhotos", "解锁所有的cutscene预存截图到相册", false)]
        public void UnlockAllCutscenePhotos(string cutsceneName)
        {
            Module<PhotoAlbumMgr>.Self.UnlockAllCutscenePhoto();
        }

        [Command("Camera", "EnterStoryCaptureMode", "进入拍照模式", false)]
        public void EnterStoryCaptureMode()
        {
            Module<CamCaptureModule>.Self.EnterStoryCaptureMode(100, Module<ActorMgr>.Self.GetActor(8017), new int[]
            {
                3005,
                3006
            });
        }

        [Command("Camera", "EnterStoryCaptureModeWithPara", "进入拍照模式", false)]
        public void EnterStoryCaptureModeWithPara(int npc, string pose)
        {
            string[] array = pose.Split(',');
            int[] array2 = new int[array.Length];
            for (int i = 0; i < array2.Length; i++)
            {
                array2[i] = int.Parse(array[i]);
            }
            Module<CamCaptureModule>.Self.EnterStoryCaptureMode(100, Module<ActorMgr>.Self.GetActor(npc), array2);
        }

        [Command("Camera", "AddRecognizableObject", "添加一个可识别物品的id", false)]
        public void AddRecognizableObject(int id)
        {
            Module<CamCaptureModule>.Self.AddRecognizableObject(id);
        }

        [Command("Camera", "RemoveRecognizableObject", "移除一个可识别物品的id", false)]
        public void RemoveRecognizableObject(int id)
        {
            Module<CamCaptureModule>.Self.RemoveRecognizableObject(id);
        }

        [Command("Newspaper", "OpenNewspaperUI", "打开报纸UI", false)]
        public void OpenNewspaperUI()
        {
        }

        [Command("Newspaper", "OpenNewspaperHistoryUI", "打开历史报纸UI", false)]
        public void OpenNewspaperHistoryUI()
        {
            Module<NewspaperModule>.Self.OpenHistory();
        }

        [Command("Util", "GreenBar", "文字过滤", false)]
        public void GreenBar(string text)
        {
            Singleton<ChannelMgr>.Instance.HasDirtyWord(text);
        }

        public void TriggerException()
        {
            throw new Exception("excption test " + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
        }

        [Command("Util", "Exception", "触发异常", false)]
        public void Exception()
        {
            base.Invoke("TriggerException", 0.1f);
        }

        [Command("Util", "TriggerAbort", "Abort", false)]
        public void Abort()
        {
            Utils.ForceCrash(ForcedCrashCategory.Abort);
        }

        [Command("Util", "TriggerAccessViolation", "AccessViolation", false)]
        public void AccessViolation()
        {
            Utils.ForceCrash(ForcedCrashCategory.AccessViolation);
        }

        [Command("Util", "TriggerFatalError", "FatalError", false)]
        public void FatalError()
        {
            Utils.ForceCrash(ForcedCrashCategory.FatalError);
        }

        [Command("Util", "TriggerPureVirtualFunction", "PureVirtualFunction", false)]
        public void PureVirtualFunction()
        {
            Utils.ForceCrash(ForcedCrashCategory.PureVirtualFunction);
        }

        [Command("Campsite", "UnlockCampsite", "解锁一个营地", false)]
        public void UnlockCampsite(int id)
        {
            Campsite campsite = Module<CampsiteModule>.Self.GetCampsite(id);
            if (campsite == null)
            {
                global::Debug.LogError(string.Format("没有找到 ID{0} 对应营地", id));
                return;
            }
            campsite.Unlock();
        }

        [Command("Campsite", "UpgradeCampsite", "升级一个营地", false)]
        public void UpgradeCampsite(int id)
        {
            Campsite campsite = Module<CampsiteModule>.Self.GetCampsite(id);
            if (campsite == null)
            {
                global::Debug.LogError(string.Format("没有找到 ID{0} 对应营地", id));
                return;
            }
            campsite.SetLevel(campsite.Level + 1);
        }

        [Command("Campsite", "UpgradeCampsiteUnit", "升级一个营地设施", false)]
        public void UpgradeCampsiteUnit(int id)
        {
            CampsiteUnit campsiteUnit = Module<CampsiteModule>.Self.GetCampsiteUnit(id);
            if (campsiteUnit == null)
            {
                global::Debug.LogError(string.Format("没有找到 ID{0} 对应营地设施", id));
                return;
            }
            campsiteUnit.SetLevel(campsiteUnit.Level + 1);
        }

        [Command("NaviMark", "TestAddNaviMarkScenePosition", "测试添加导航标记场景位置", false)]
        public void TestAddNaviMarkScenePosition(NaviMarkType naviMarkType, AdditiveScene scene)
        {
            naviMarkHandle = Module<NaviMarkModule>.Self.AddNaviMark(naviMarkType, Vector3.zero, scene, false);
        }

        [Command("NaviMark", "TestRemoveNaviMarkScenePosition", "测试移除导航标记场景位置", false)]
        public void TestRemoveNaviMarkScenePosition()
        {
            Module<NaviMarkModule>.Self.RemoveNaviMark(naviMarkHandle);
        }

        [Command("Debug", "DebugSetGameObjectVisible", "根据exe目录下的gameobject.vis文件中的路径查找当前场景，并设置active", false)]
        public void DebugSetGameObjectVisible(int visible)
        {
            if (File.Exists("gameobject.vis"))
            {
                string[] array = File.ReadAllLines("gameobject.vis");
                for (int i = 0; i < array.Length; i++)
                {
                    Transform transform = null;
                    string text = "";
                    int num = array[i].IndexOf('/');
                    if (-1 == num)
                    {
                        string name = array[i];
                        int num2 = array[i].IndexOf('\t');
                        if (-1 != num2)
                        {
                            name = array[i].Substring(0, num2);
                            text = array[i].Substring(num2 + 1);
                        }
                        GameObject gameObject = GameObject.Find(name);
                        if (gameObject)
                        {
                            transform = gameObject.transform;
                        }
                    }
                    else
                    {
                        string text2 = array[i].Substring(0, num);
                        GameObject gameObject2 = GameObject.Find(text2);
                        if (gameObject2)
                        {
                            text2 = array[i].Substring(num + 1);
                            int num3 = text2.IndexOf('\t');
                            if (-1 != num3)
                            {
                                text = text2.Substring(num3 + 1);
                                text2 = text2.Substring(0, num3);
                            }
                            transform = gameObject2.transform.Find(text2);
                        }
                    }
                    if (transform)
                    {
                        if (string.IsNullOrEmpty(text))
                        {
                            transform.gameObject.SetActive(visible != 0);
                        }
                        else
                        {
                            string[] array2 = text.Split(',');
                            for (int j = 0; j < array2.Length; j++)
                            {
                                Component component = transform.gameObject.GetComponent(array2[j]);
                                if (null != component)
                                {
                                    Component[] components = transform.gameObject.GetComponents(component.GetType());
                                    for (int k = 0; k < components.Length; k++)
                                    {
                                        Behaviour behaviour = components[k] as Behaviour;
                                        if (null != behaviour)
                                        {
                                            behaviour.enabled = (visible != 0);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        /*
		[Command("Debug", "DebugDeviceAdapte", "对根目录下deviceCPU.txt和deviceGPU.txt进行适配算法分析，并返回结果", false)]
		public void DebugDeviceAdapte()
		{
			if (File.Exists("deviceCPU.txt"))
			{
				string[] array = File.ReadAllLines("deviceCPU.txt");
				List<string> list = new List<string>();
				for (int i = 0; i < array.Length; i++)
				{
					int num = Module<DeviceAdaptor>.Self.CalculateQualityLevelByCPU(array[i]);
					int cpuMatchIndex = Module<DeviceAdaptor>.Self.GetCpuMatchIndex();
					list.Add(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\"", new object[]
					{
						array[i],
						num,
						cpuMatchIndex + 4,
						cpuMatchIndex
					}));
				}
				if (list.Count != 0)
				{
					list.Insert(0, "\"name\",\"level\",\"line\",\"match index\"");
					File.WriteAllLines("AdapteCPU.csv", list);
				}
			}
			if (File.Exists("deviceGPU.txt"))
			{
				string[] array2 = File.ReadAllLines("deviceGPU.txt");
				List<string> list2 = new List<string>();
				for (int j = 0; j < array2.Length; j++)
				{
					int num2 = Module<DeviceAdaptor>.Self.CalculateQualityLevelByGPU(array2[j]);
					int gpuMatchIndex = Module<DeviceAdaptor>.Self.GetGpuMatchIndex();
					list2.Add(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\"", new object[]
					{
						array2[j],
						num2,
						gpuMatchIndex + 4,
						gpuMatchIndex
					}));
				}
				if (list2.Count != 0)
				{
					list2.Insert(0, "\"name\",\"level\",\"line\",\"match index\"");
					File.WriteAllLines("AdapteGPU.csv", list2);
				}
			}
		}
		*/
        [Command("Debug", "DebugCheckCheat", "作弊检查", false)]
        public void DebugCheckCheat()
        {
            global::Debug.LogWarning(string.Format("Is cheat = {0}", Module<PatheaGDCModule>.Self.IsChanged()));
        }

        [Command("Debug", "DebugCrashTest", "测试崩溃", false)]
        public void DebugCrashTest()
        {
            int num = 0;
            int num2 = 10 / num;
            global::Debug.LogError(string.Format("Manual crash divide 0 {0}", num2));
        }

        [Command("Debug", "DebugUseOC", "设置使用遮挡剔除", false)]
        public void DebugUseOC(int closeCulling)
        {
            Camera.main.useOcclusionCulling = (closeCulling != 0);
        }

        [Command("Debug", "StartDropTask", "开启一个掉落任务", false)]
        public void StartDropTask(int id)
        {
            Module<DropItemTaskModule>.Self.StartDropTask(id);
        }

        [Command("Debug", "CreateTinyAnimal", "根据给定的Id创建一个小动物", false)]
        public void CreateTinyAnimalById(int id)
        {
            Module<TinyAnimalModule>.Self.CreateTinyAnimal(id, Module<Player>.Self.GamePos, Module<Player>.Self.GameRot);
        }

        /*
		[Command("Debug", "DebugOutPutPrefab", "打印内存中的预制体", false)]
		public void DebugOutPutPrefab()
		{
			string[] array = File.ReadAllLines("findlist.txt");
			GameObject[] array2 = Resources.FindObjectsOfTypeAll<GameObject>();
			foreach (string a in array)
			{
				foreach (GameObject gameObject in array2)
				{
					a == gameObject.name;
				}
			}
		}
		*/

        [Command("OrderingFood", "BeginOrderingFoodDate", "开启点餐约会", false)]
        public void OrderingFoodDate()
        {
            if (!Module<SocialModule>.Self.IsEngagement())
            {
                return;
            }
            int engagementID = Module<SocialModule>.Self.GetEngagementID();
            OrderingFoodContext context = new OrderingFoodContext
            {
                npcActorId = engagementID,
                isSingle = false
            };
            Module<MiniGameModule>.Self.EnterMiniGame(Module<OrderingFoodModule>.Self, context, true);
        }

        [Command("WhacMole", "WhacMoleAddPlayerScore", "增加玩家分数", false)]
        public void WhacMoleAddPlayerScore(int score)
        {
            WhacMoleParty currentParty = Module<WhacMoleModule>.Self.GetCurrentParty();
            if (currentParty == null)
            {
                return;
            }
            currentParty.AddActorScore(Module<Player>.Self.actor, score);
        }

        [Command("WhacMole", "WhacMoleFullyPlayerRage", "充满玩家怒气", false)]
        public void WhacMoleFullyPlayerRage()
        {
            WhacMoleParty currentParty = Module<WhacMoleModule>.Self.GetCurrentParty();
            if (currentParty == null)
            {
                return;
            }
            currentParty.FullyActorRage(Module<Player>.Self.actor);
        }

        [Command("WhacMole", "WhacMoleFullyNpcage", "充满Npc怒气", false)]
        public void WhacMoleFullyNpcage()
        {
            WhacMoleParty currentParty = Module<WhacMoleModule>.Self.GetCurrentParty();
            if (currentParty == null)
            {
                return;
            }
            Npc npc = Module<NpcMgr>.Self.GetNpc(currentParty.GetNpcId());
            currentParty.FullyActorRage(npc.actor);
        }

        [Command("BreedStore", "OpenBreedStoreUI", "打开养殖商店UI", false)]
        public void OpenBreedUI()
        {
            Module<BreedStoreModule>.Self.OnBreedStoreUIShow();
        }

        [Command("MiniGameDrawLine", "DrawingLineTest", "默认测试画沙游戏", false)]
        public void DrawingLine()
        {
            Module<DrawLineModule>.Self.DrawLineEntrance(8001);
        }

        [Command("MiniGameDrawLine", "DrawingLineWithNpc", "测试画沙游戏和npc ", false)]
        public void DrawingLine(int npcID)
        {
            Module<DrawLineModule>.Self.DrawLineEntrance(npcID);
        }

        [Command("Delegation", "DelegationUI", "测试委托ui", false)]
        public void DelegationUI()
        {
            Module<DelegationModule>.Self.OpenUItest();
        }

        [Command("FarmAnimal", "AddFarmAnimalItems", "添加养殖动物的测试道具", false)]
        public void AddFarmAnimalItems()
        {
            AddItemToBag(19710001, 99);
            AddItemToBag(19710002, 99);
            AddItemToBag(19710003, 99);
            AddItemToBag(19710004, 99);
            AddItemToBag(16200003, 99);
            AddItemToBag(16200004, 99);
            AddItemToBag(19300030, 99);
            AddItemToBag(19300035, 99);
            AddItemToBag(19300036, 99);
            AddItemToBag(19112023, 99);
            AddItemToBag(19112006, 99);
            AddItemToBag(19010002, 99);
            AddItemToBag(19999998, 99);
            AddItemToBag(19999999, 99999999);
        }

        [Command("FarmAnimal", "UnlockFarmAnimal", "解锁养殖动物功能", false)]
        public void UnlockFarmAnimal()
        {
            Module<HomeModule>.Self.AnimalHouseUnlocked = true;
            if (Module<HomeModule>.Self.FarmLevel < 6)
            {
                Module<HomeModule>.Self.FarmLevel = 6;
            }
            AddFarmAnimalItems();
        }

        [Command("Newspaper", "ChangeNewspaperContent", "测试更换新闻内容", false)]
        public void TestChangeNewspaperContent(int contentId)
        {
            Module<NewspaperModule>.Self.ChangeEventNewspaper(contentId);
        }

        [Command("Newspaper", "OpenNewspaperNewUI", "打开新闻UI", false)]
        public void OpenNewspaperNewUI()
        {
            Module<NewspaperModule>.Self.OpenNewUI(false, null);
        }

        [Command("Advert", "TestConfirmAdvert", "测试添加广告", false)]
        public void TestConfirmAdvert()
        {
            Module<NewspaperModule>.Self.ConfirmAdvert(null);
        }

        [Command("Advert", "TestCancelAdvert", "测试取消广告", false)]
        public void TestCancelAdvert()
        {
            Module<NewspaperModule>.Self.CancelAdvert(null);
        }

        [Command("TreasureRevealer", "CheckTreasureRevealerLevel", "检查当前探矿等级", false)]
        public void CheckTreasureRevealerLevel(int groupId)
        {
            int currentLevel = Module<TreasureRevealerManager>.Self.GetCurrentLevel(groupId);
            Cmd.Instance.Log(string.Format("当前等级 {0}", currentLevel), Cmd.CallBackType.Normal);
        }

        [Command("TreasureRevealer", "UpgradeTreasureRevealerLevel", "探矿等级+1", false)]
        public void UpgradeTreasureRevealerLevel(int groupId)
        {
            Module<TreasureRevealerManager>.Self.AddCurrentLevel(groupId, 1);
        }

        [Command("TreasureRevealer", "UpgradeTreasureRevealerLevelTop", "探矿等级到最大", false)]
        public void UpgradeTreasureRevealerLevelTop(int groupId)
        {
            int maxLevel = Module<TreasureRevealerManager>.Self.GetMaxLevel(groupId);
            Module<TreasureRevealerManager>.Self.SetCurrentLevel(groupId, maxLevel);
        }

        [Command("TreasureRevealer", "OpenTreasureRevealerUI", "打开探矿器UI", false)]
        public void OpenTreasureRevealerUI()
        {
            Module<TreasureRevealerManager>.Self.OpenUI();
        }

        [Command("Illustration", "IllustrationUnlock", "解锁指定收集", false)]
        public void IllustrationUnlock(int id)
        {
            Module<IllustrationModule>.Self.SetState(id, StateType.Unlocked);
        }

        [Command("Illustration", "IllustrationUnlockAll", "解锁所有收集", false)]
        public void IllustrationUnlockAll()
        {
            foreach (IllustrationConfig illustrationConfig in Module<IllustrationModule>.Self.Configs)
            {
                Module<IllustrationModule>.Self.SetState(illustrationConfig.id, StateType.Unlocked);
            }
        }

        [Command("Illustration", "IllustrationUnlockAllItem", "解锁所有道具收集", false)]
        public void IllustrationUnlockAllItem()
        {
            foreach (IllustrationConfig illustrationConfig in Module<IllustrationModule>.Self.Configs)
            {
                if (illustrationConfig.type == Pathea.IllustrationNs.CatalogType.Item)
                {
                    Module<IllustrationModule>.Self.SetState(illustrationConfig.id, StateType.Unlocked);
                }
            }
        }

        [Command("Illustration", "IllustrationUnlockAllLife", "解锁所有生物收集", false)]
        public void IllustrationUnlockAllLife()
        {
            foreach (IllustrationConfig illustrationConfig in Module<IllustrationModule>.Self.Configs)
            {
                if (illustrationConfig.type == Pathea.IllustrationNs.CatalogType.Life)
                {
                    Module<IllustrationModule>.Self.SetState(illustrationConfig.id, StateType.Unlocked);
                }
            }
        }

        [Command("Illustration", "IllustrationUnlockAllLocation", "解锁所有场景收集", false)]
        public void IllustrationUnlockAllLocation()
        {
            foreach (IllustrationConfig illustrationConfig in Module<IllustrationModule>.Self.Configs)
            {
                if (illustrationConfig.type == Pathea.IllustrationNs.CatalogType.Location)
                {
                    Module<IllustrationModule>.Self.SetState(illustrationConfig.id, StateType.Unlocked);
                }
            }
        }

        [Command("Festival", "FestivalCreateGift", "创建礼物", false)]
        public void FestivalCreateGift(int npcId, int giftId)
        {
            Vector3 gamePos = Module<Player>.Self.actor.GamePos;
            Vector3 eulerAngles = Module<Player>.Self.actor.GameRot.eulerAngles;
            AdditiveScene curScene = Module<ScenarioModule>.Self.CurScene;
            FestivalGiftPool.GenGift(npcId, giftId, curScene, gamePos, eulerAngles, FestivalGiftFlag.None, -1f);
        }

        [Command("HomeItemCustom", "HomeItemCustomUnlock", "解锁对应id的家具部件", false)]
        public void HomeItemCustomUnlock(int itemId)
        {
            Module<HomeItemCustomModule>.Self.UnlockCustomItem(itemId);
        }

        [Command("MagicMirror", "ActiveMagicMirrorChip", "激活魔镜芯片", false)]
        public void ActiveMagicMirrorChip(int type)
        {
            Module<MagicMirrorModule>.Self.ActiveMagicMirrorChip((MagicMirrorChipType)type);
        }

        [Command("MagicMirror", "GetMagicMirrorChips", "获取芯片配置列表", false)]
        public void GetMagicMirrorChips(int type)
        {
            foreach (MagicMirrorChipConfig magicMirrorChipConfig in Module<MagicMirrorModule>.Self.GetMagicMirrorChipConfigs())
            {
                foreach (MagicMirrorEventType magicMirrorEventType in magicMirrorChipConfig.eventTypes)
                {
                    global::Debug.LogError(magicMirrorChipConfig.chipType.ToString() + " " + magicMirrorEventType.ToString());
                }
            }
        }

        [Command("MagicMirror", "GetMagicMirrorEvents", "获取任务列表", false)]
        public void GetMagicMirrorEvents()
        {
            foreach (MagicMirrorEventType magicMirrorEventType in Module<MagicMirrorModule>.Self.GetMagicMirrorEventList())
            {
                global::Debug.LogError(magicMirrorEventType);
            }
        }

        [Command("MagicMirror", "OpenMagicMirrorWeatherUI", "进入天气预报UI", false)]
        public void OpenMagicMirrorWeatherUI()
        {
            Module<MagicMirrorModule>.Self.OpenWeatherUI();
        }

        [Command("MagicMirror", "GetItemCountInMagicMirrorStorage", "获取魔镜可用的物品数量", false)]
        public void GetItemCountInMagicMirrorStorage(int itemId)
        {
        }

        [Command("MagicMirror", "AddTaskDateToday", "增加今日工作日志(npcinstanceId,物品id, unitName,事件类型,物品品质,物品数量)", false)]
        public void AddTaskDateToday(int instanceId, int itemId, string unitName, int eventType, int grade = -1, int count = 1)
        {
            List<Vector3Int> list = new List<Vector3Int>();
            list.Add(new Vector3Int(itemId, grade, count));
            Module<MagicMirrorModule>.Self.AddTaskDataToday(instanceId, unitName, -1, (MagicMirrorEventType)eventType, list, null);
        }

        [Command("MagicMirror", "AddTaskDateTodayByItemId", "增加今日工作日志(npcinstanceId,物品id, unitItemId,事件类型,物品品质,物品数量)", false)]
        public void AddTaskDateToday(int instanceId, int itemId, int unitItemId, int eventType, int grade = -1, int count = 1)
        {
            List<Vector3Int> list = new List<Vector3Int>();
            list.Add(new Vector3Int(itemId, grade, count));
            Module<MagicMirrorModule>.Self.AddTaskDataToday(instanceId, null, unitItemId, (MagicMirrorEventType)eventType, list, null);
        }

        [Command("MagicMirror", "AddWorkData", "增加家园助手", false)]
        public void AddWorkData(int instanceId)
        {
            Module<MagicMirrorModule>.Self.AddWorkData(instanceId);
        }

        [Command("FavorInfluence", "AddFavorInfluence", "添加好感影响", false)]
        public void AddFavorInfluence(int npcId, string param)
        {
            Module<FavorInfluenceModule>.Self.AddFavorInfluence(npcId, param);
        }

        [Command("FavorInfluence", "RemoveFavorInfluence", "移除好感影响", false)]
        public void RemoveFavorInfluence(int npcId, string param)
        {
            Module<FavorInfluenceModule>.Self.RemoveFavorInfluence(npcId, param);
        }

        [Command("FavorInfluence", "RemoveAllFavorInfluenceExtra", "移除所有额外好感影响", false)]
        public void RemoveAllFavorInfluenceExtra(int npcId)
        {
            Module<FavorInfluenceModule>.Self.RemoveAllFavorInfluenceExtra(npcId);
        }

        [Command("FavorInfluence", "AddFavorInfluenceLock", "添加单个好感影响Lock", false)]
        public void AddFavorInfluenceLock(int npcId, string param)
        {
            Module<FavorInfluenceModule>.Self.AddFavorInfluenceLock(npcId, param);
        }

        [Command("FavorInfluence", "RemoveFavorInfluenceLock", "删除单个好感影响Lock", false)]
        public void RemoveFavorInfluenceLock(int npcId, string param)
        {
            Module<FavorInfluenceModule>.Self.RemoveFavorInfluenceLock(npcId, param);
        }

        [Command("FavorInfluence", "AddLockAllNormalInfluence", "添加所有普通好感影响Lock", false)]
        public void AddLockAllNormalInfluence(int npcId)
        {
            Module<FavorInfluenceModule>.Self.AddLockAllNormalInfluence(npcId);
        }

        [Command("FavorInfluence", "RemoveLockAllNormalInfluence", "删除所有普通好感影响Lock", false)]
        public void RemoveLockAllNormalInfluence(int npcId)
        {
            Module<FavorInfluenceModule>.Self.RemoveLockAllNormalInfluence(npcId);
        }

        [Command("FavorInfluence", "AddLockAllExtraInfluence", "添加所有额外好感影响Lock", false)]
        public void AddLockAllExtraInfluence(int npcId)
        {
            Module<FavorInfluenceModule>.Self.AddLockAllExtraInfluence(npcId);
        }

        [Command("FavorInfluence", "RemoveLockAllExtraInfluence", "删除所有额外好感影响Lock", false)]
        public void RemoveLockAllExtraInfluence(int npcId)
        {
            Module<FavorInfluenceModule>.Self.RemoveLockAllExtraInfluence(npcId);
        }

        [Command("FavorInfluence", "AddLockAllInfluence", "添加所有好感影响Lock", false)]
        public void AddLockAllInfluence(int npcId)
        {
            Module<FavorInfluenceModule>.Self.AddLockAllInfluence(npcId);
        }

        [Command("FavorInfluence", "RemoveLockAllInfluence", "删除所有好感影响Lock", false)]
        public void RemoveLockAllInfluence(int npcId)
        {
            Module<FavorInfluenceModule>.Self.RemoveLockAllInfluence(npcId);
        }

        [Command("SendGift", "SendGiftSetRule", "设置送礼特殊规则", false)]
        public void SendGiftSetSpecialGiftRuleState(int ruleID, bool state)
        {
            Module<SendGiftModule>.Self.SetSpecialGiftRuleState(ruleID, state);
        }

        [Command("GameCenter", "SetGameCenterLevel", "设置游戏中心等级", false)]
        public void SetGameCenterLevel(int level)
        {
            Module<GameCenterMgr>.Self.SetLevel(level);
        }

        [Command("CentralTip", "CentralTipTest", "测试中央提示组信息", false)]
        public void ShowCentralTip(string prefabsName)
        {
            CentralMgr.Instance.AddCentralGroupUI(prefabsName, -1);
        }

        [Command("Dlc", "PrintInstalledDlc", "打印已经安装的dlc", false)]
        public void PrintInstalledDlc()
        {
            foreach (Dlc dlc in Singleton<ChannelMgr>.Instance.GetAllInstalledDlc())
            {
            }
        }

        [Command("GM_CMD", "GM_MoveLocation", "GM指令，玩家移动到非本场景的位置,前位置后朝向,度数0-360度", false)]
        public void GMMoveSceneVector(string SceneName, float x, float y, float z, float rx = 0f, float ry = 0f, float rz = 0f)
        {
            AdditiveScene scene;
            Enum.TryParse<AdditiveScene>(SceneName, false, out scene);
            Vector3 pos = new Vector3(x, y, z);
            Vector3 rot = new Vector3(rx, ry, rz);
            Module<ScenarioModule>.Self.LoadScenario(scene, new PosRot(pos, rot));
        }

        [Command("GM_CMD", "GM_MoveLocation1", "GM指令，玩家移动到非本场景的位置，定死朝向 180度或者0度", false)]
        public void GMMoveSceneVector1(string SceneName, float x, float y, float z, bool isZero)
        {
            AdditiveScene scene;
            Enum.TryParse<AdditiveScene>(SceneName, false, out scene);
            Vector3 pos = new Vector3(x, y, z);
            Vector3 zero = Vector3.zero;
            if (!isZero)
            {
                zero = new Vector3(0f, 180f, 0f);
            }
            Module<ScenarioModule>.Self.LoadScenario(scene, new PosRot(pos, zero));
        }

        [Command("GM_CMD", "GM_TpTransport", "GM指令，玩家移动到本场景的位置", false)]
        public void GMMove(float x, float y, float z, float rx = 0f, float ry = 0f, float rz = 0f)
        {
            Vector3 gamePos = new Vector3(x, y, z);
            Vector3 vector = new Vector3(rx, ry, rz);
            Module<Player>.Self.GamePos = gamePos;
            Module<Player>.Self.GameRot = Quaternion.LookRotation(vector.normalized, Vector3.up);
        }

        [Command("GM_CMD", "GM_SwitchGraphQuality", "GM指令切换画质", false)]
        public void SwitchQuality(int level)
        {
            Singleton<OptionMgr>.Instance.SetGraphicQuality(level);
        }

        [Command("GM_CMD", "GM_SetTextureQuality", "GM指令切换贴图质量", false)]
        public void SetTextureQuality(int level)
        {
            Singleton<OptionMgr>.Instance.SetTextureQuality(level);
        }

        [Command("GM_CMD", "GM_SetPixelLightCount", "GM指令每像素光照数量", false)]
        public void SetPixelLightCount(int level)
        {
            Singleton<OptionMgr>.Instance.SetPixelLightCount(level);
        }

        [Command("GM_CMD", "GM_SetShadowQuality", "GM指令阴影质量设置", false)]
        public void SetShadowQuality(int level)
        {
            Singleton<OptionMgr>.Instance.SetShadowGroupQuality(level);
        }

        [Command("GM_CMD", "GM_SetShadowsDistance", "GM指令阴影距离设置", false)]
        public void SetShadowsDistance(int level)
        {
            Singleton<OptionMgr>.Instance.SetDistantShadow(level);
        }
        /*

		[Command("GM_CMD", "GM_SetShadowResolution", "GM指令阴影分辨率", false)]
		public void SetShadowResolution(int level)
		{
			Singleton<OptionMgr>.Instance.shadow(level);
		}

		[Command("GM_CMD", "GM_SetShadowsCascade", "GM指令分级阴影", false)]
		public void SetShadowsCascade(int level)
		{
			Singleton<OptionMgr>.Instance.SetShadowsCascade(level);
		}
		*/
        [Command("GM_CMD", "GM_SetSkinWeight", "GM指令顶点骨骼数", false)]
        public void SetSkinWeight(int level)
        {
            Singleton<OptionMgr>.Instance.SetSkinWeight(level);
        }

        [Command("GM_CMD", "GM_SetVerticalSync", "GM指令垂直同步", false)]
        public void SetVerticalSync(int level)
        {
            Singleton<OptionMgr>.Instance.SetVerticalSync(level);
        }

        [Command("GM_CMD", "GM_SetLodBias", "GM指令lod（细节层次倍数）", false)]
        public void SetLodBias(float level)
        {
            Singleton<OptionMgr>.Instance.SetLodBias(level);
        }

        [Command("GM_CMD", "GM_SetDistantShadow", "GM指令远景阴影", false)]
        public void SetDistantShadow(int level)
        {
            Singleton<OptionMgr>.Instance.SetDistantShadow(level);
        }

        [Command("GM_CMD", "GM_SetTreeShaking", "GM指令树摇动（植被动画）", false)]
        public void SetTreeShaking(int level)
        {
            if (level == 0)
            {
                Singleton<OptionMgr>.Instance.SetTreeShaking(false);
                return;
            }
            Singleton<OptionMgr>.Instance.SetTreeShaking(true);
        }

        [Command("GM_CMD", "GM_SetVolumeLight", "GM指令体积光", false)]
        public void SetVolumeLight(int level)
        {
            Singleton<OptionMgr>.Instance.SetVolumeLight(level);
        }

        [Command("GM_CMD", "GM_SetGrassLod", "GM指令草lod", false)]
        public void SetGrassLod(int level)
        {
            Singleton<OptionMgr>.Instance.SetGrassLod(level);
        }

        [Command("GM_CMD", "GM_SetSSR", "GM指令Scene Space Reflections（屏幕空间反射）", false)]
        public void SetSSR(int level)
        {
            Singleton<OptionMgr>.Instance.SetSSR(level);
        }

        [Command("GM_CMD", "GM_SetAO", "GM指令Ambient Occlusion（环境光遮蔽））", false)]
        public void SetAO(int level)
        {
            if (level == 0)
            {
                Singleton<OptionMgr>.Instance.SetAO(0);
                return;
            }
            Singleton<OptionMgr>.Instance.SetAO(1);
        }

        [Command("GM_CMD", "GM_SetBloom", "GM指令Bloom全屏泛光）", false)]
        public void SetBloom(int level)
        {
            if (level == 0)
            {
                Singleton<OptionMgr>.Instance.SetBloom(false);
                return;
            }
            Singleton<OptionMgr>.Instance.SetBloom(true);
        }

        [Command("GM_CMD", "GM_SetAntiAlising", "GM指令抗锯齿", false)]
        public void SetAntiAlising(int level)
        {
            Singleton<OptionMgr>.Instance.SetAntiAlising(level);
        }

        [Command("GM_CMD", "GM_SetAutoExposure", "GM指令自动曝光", false)]
        public void SetAutoExposure(int level)
        {
            if (level == 0)
            {
                Singleton<OptionMgr>.Instance.SetAutoExposure(false);
                return;
            }
            Singleton<OptionMgr>.Instance.SetAutoExposure(true);
        }

        [Command("GM_CMD", "GM_SetHeightFog", "GM指令高度雾", false)]
        public void SetHeightFog(int level)
        {
            if (level == 0)
            {
                Singleton<OptionMgr>.Instance.SetHeightFog(false);
                return;
            }
            Singleton<OptionMgr>.Instance.SetHeightFog(true);
        }

        [Command("GM_CMD", "GM_SetTerrain", "GM地形质量", false)]
        public void SetTerrain(int level)
        {
            Singleton<OptionMgr>.Instance.SetTerrain(level);
        }

        [Command("GM_CMD", "GM_SetCapsuleShadow", "GM角色增强阴影", false)]
        public void SetCapsuleShadow(int level)
        {
            if (level == 0)
            {
                Singleton<OptionMgr>.Instance.SetCapsuleShadow(false);
                return;
            }
            Singleton<OptionMgr>.Instance.SetCapsuleShadow(true);
        }

        [Command("GM_CMD", "GM_SetEffectQuality", "GM特效质量", false)]
        public void SetEffectQuality(int level)
        {
            Singleton<OptionMgr>.Instance.SetEffectQuality(level);
        }

        [Command("GM_CMD", "GM_SetDynamicBone", "GM应用动态骨骼", false)]
        public void SetDynamicBone(int level)
        {
            if (level == 0)
            {
                Singleton<OptionMgr>.Instance.SetDynamicBone(false);
                return;
            }
            Singleton<OptionMgr>.Instance.SetDynamicBone(true);
        }

        [Command("GM_CMD", "GM_SetClothVisibleDistance", "GM布料可视距离", false)]
        public void SetClothVisibleDistance(int level)
        {
            Singleton<OptionMgr>.Instance.SetClothVisibleDistance(level);
        }

        [Command("GM_CMD", "GM_SetMotionBlur", "GM动态模糊", false)]
        public void SetMotionBlur(int level)
        {
            Singleton<OptionMgr>.Instance.SetMotionBlur(level);
        }

        [Command("GM_CMD", "GM_SetActorViewDistance", "GM角色模型显示的最大距离", false)]
        public void SetActorViewDistance(float level)
        {
            Singleton<OptionMgr>.Instance.SetActorViewDistance(level);
        }

        [Command("GM_CMD", "GM_SetModelCloud", "GM模型云（云质量）", false)]
        public void SetModelCloud(int level)
        {
            Singleton<OptionMgr>.Instance.SetModelCloud(level);
        }

        [Command("GM_CMD", "GM_SetRefles", "GM reflex", false)]
        public void SetRefles(int level)
        {
            Singleton<OptionMgr>.Instance.SetRefles((OptionMgr.Option3Level)level);
        }

        [Command("GM_CMD", "GM_ApplyAllOption", "保存所有设置", false)]
        public void ApplayAll()
        {
            Singleton<OptionMgr>.Instance.SaveAllSettings();
            Singleton<OptionMgr>.Instance.ApplyGraphicDetails();
        }

        [Command("Player", "PlayerAddBehavior", "添加玩家行为", false)]
        public void PlayerAddBehavior(int npcId, string behavior)
        {
            if (!string.IsNullOrEmpty(behavior))
            {
                Module<Player>.Self.AddBehavior(behavior, null, false);
            }
        }

        [Command("Player", "PlayerAddBehaviorParas", "添加玩家行为", false)]
        public void PlayerAddBehaviorParas(int npcId, string behavior, string paras)
        {
            if (!string.IsNullOrEmpty(behavior))
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
                Module<Player>.Self.AddBehavior(behavior, hashtable, false);
            }
        }

        [Command("Player", "PlayerRemoveBehavior", "删除玩家行为", false)]
        public void PlayerRemoveBehavior(int npcId, string behavior)
        {
            if (!string.IsNullOrEmpty(behavior))
            {
                Module<Player>.Self.RemoveBehavior(behavior);
            }
        }

        [Command("Log", "CentralTipTest", "测试中央提示组信息", false)]
        public void ShowVoiceLog(bool show)
        {
            Module<ConversationManager>.Self.showVoiceLog = show;
        }

        [Command("Log", "SendUserReport", "测试发送系统报错信息", false)]
        public void SendUserReport(string showText)
        {
            Singleton<GameMgr>.Instance.SendUserReport("测试发送UserReport", "测试发送UserReport详细内容", showText, true, true);
        }

        public MiscCmd()
        {
        }

        public Locker locker = new Locker();

        public ItemInstance missionItem;

        public Locker pauseTimeLocker;

        public bool uidisable;

        public Locker bgmLocker;

        public Locker resourcePointHudLocker;

        public Pathea.DramaNs.Handle runningDramaHandle;

        public GameObject dramaGo;

        public ActorStateViewer stateViewer;

        public InputMethodDebug imeDebug;

        public Pathea.NaviMarks.Handle naviMarkHandle;

    }
}
