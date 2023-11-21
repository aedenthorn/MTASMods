using System;
using Commonder;
using Pathea;
using Pathea.ActorNs;
using Pathea.FrameworkNs;
using Pathea.MissionNs;
using Pathea.ScenarioNs;
using Pathea.StoryScript;
using UnityEngine;

namespace DevConsole
{
    public class SceneItemCmd : MonoBehaviour, ICmd
    {
        public void Awake()
        {
            Cmd.Instance.Register(this);
        }

        public void OnDestroy()
        {
            Cmd.Instance.ClearAll();
        }

        [Command("SceneItem", "AddMissionIconToSceneItem", "SceneItem添加任务图标", false)]
        public void AddMissionIconToSceneItem(int type, int id)
        {
            Module<SceneItemManager>.Self.AddRemoveMissionIcon((SceneItemType)type, id, true, 1, MissionIconType.MainRunning);
        }

        [Command("SceneItem", "RemoveMissionIconToSceneItem", "SceneItem删除任务图标", false)]
        public void RemoveMissionIconToSceneItem(int type, int id)
        {
            Module<SceneItemManager>.Self.AddRemoveMissionIcon((SceneItemType)type, id, false, 1, MissionIconType.MainRunning);
        }

        [Command("SceneItem", "CreateSceneItem", "测试创建sceneItem(在玩家前方)", false)]
        public void CreateSceneItem(string type, int id, string assetName)
        {
            Vector3 pos = Module<Player>.Self.GamePos + Module<Player>.Self.actor.Forward * 2f + new Vector3(0f, 1f, 0f);
            Vector3 rot = Module<Player>.Self.GameRot.eulerAngles + new Vector3(0f, 180f, 0f);
            AdditiveScene curScene = Module<ScenarioModule>.Self.CurScene;
            Module<SceneItemManager>.Self.CreateSceneItem(GameUtils.EnumParse<SceneItemType>(type), id, assetName, curScene, pos, rot, "", "");
        }

        [Command("SceneItem", "DeleteSceneItem", "测试删除sceneItem", false)]
        public void DeleteSceneItem(string type, int id)
        {
            Module<SceneItemManager>.Self.DeleteSceneItem(GameUtils.EnumParse<SceneItemType>(type), id);
        }

        [Command("SceneItem", "SetSceneItemInteract", "设置sceneItem交互状态", false)]
        public void SetSceneItemInteract(string type, int id, bool canInteract)
        {
            Module<SceneItemManager>.Self.SetItemInteract(GameUtils.EnumParse<SceneItemType>(type), id, canInteract);
        }

        [Command("SceneItem", "SetSceneItemInteractLocker", "给sceneItem交互状态加锁", false)]
        public void SetSceneItemInteractLocker(string type, int id, bool isAdd)
        {
            Module<StoryAssistant>.Self.SetSceneItemInteractLocker(GameUtils.EnumParse<SceneItemType>(type), id, isAdd);
        }

        [Command("SceneItem", "CreateBusStop", "测试创建公交站台(在玩家前方)", false)]
        public void CreateBusStop(int id, string extra)
        {
            Vector3 pos = Module<Player>.Self.GamePos + Module<Player>.Self.actor.Forward * 2f + new Vector3(0f, 1f, 0f);
            Vector3 rot = Module<Player>.Self.GameRot.eulerAngles + new Vector3(0f, 180f, 0f);
            AdditiveScene curScene = Module<ScenarioModule>.Self.CurScene;
            Module<SceneItemManager>.Self.CreateSceneItem(SceneItemType.BusStop, id, "SceneItem_TestCreatorItem", curScene, pos, rot, "", extra);
        }

        [Command("SceneItem", "CreateCreatorMark", "测试创建建造标记", false)]
        public void CreateCreatorMark(int id)
        {
            string extraParams = "SceneItem_TestCreatorItem|CreatorItem|19000001_1_0,19000002_2_1|1";
            Vector3 pos = Module<Player>.Self.GamePos + Module<Player>.Self.actor.Forward * 2f + new Vector3(0f, 1f, 0f);
            Vector3 rot = Module<Player>.Self.GameRot.eulerAngles + new Vector3(0f, 180f, 0f);
            AdditiveScene curScene = Module<ScenarioModule>.Self.CurScene;
            Module<SceneItemManager>.Self.CreateSceneItem(SceneItemType.CreatorMark, id, "SceneItem_TestCreatorMark", curScene, pos, rot, "", extraParams);
        }

        [Command("SceneItem", "TableLampSetState", "设置台灯状态", false)]
        public void TableLampSetState(int itemId, int state)
        {
            SceneItemTableLampData sceneItemTableLampData = (SceneItemTableLampData)Module<SceneItemManager>.Self.GetSceneItemData(SceneItemType.TableLamp, itemId);
            if (sceneItemTableLampData != null)
            {
                sceneItemTableLampData.State = state;
            }
        }

        [Command("SceneItem", "TableLampButton", "按台灯一次", false)]
        public void TableLampButton(int itemId)
        {
            SceneItemTableLampData sceneItemTableLampData = (SceneItemTableLampData)Module<SceneItemManager>.Self.GetSceneItemData(SceneItemType.TableLamp, itemId);
            if (sceneItemTableLampData != null)
            {
                SceneItemTableLampData sceneItemTableLampData2 = sceneItemTableLampData;
                int state = sceneItemTableLampData2.State;
                sceneItemTableLampData2.State = state + 1;
            }
        }

        public SceneItemCmd()
        {
        }
    }
}
