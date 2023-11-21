using Commonder;
using DevConsole.ActorNs;
using DevConsole.SocialNs;
using Pathea;
using Pathea.ActorNs;
using Pathea.FrameworkNs;
using Pathea.SocialNs;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using Object = UnityEngine.Object;

namespace DevConsole
{
    public class DebugToolModule : Module<DebugToolModule>
    {
        public static string[] translatable = { };
        public static string[] translations = { };

        protected override IEnumerator GetLoadCoroutine()
        {
            BepInExPlugin.Dbgl("Loading debug tools");
            LoadAssetOperation<GameObject> operation = LoadAssetAsync<GameObject>("debugmodule", "DebugTools");
            yield return operation;
            var go = operation.GetAsset();
            go.AddComponent<ActorDebug>().enabled = false;
            go.AddComponent<AssetMgrCmd>();
            go.AddComponent<BattleDebug>().enabled = false;
            go.AddComponent<BehaviorDebug>().enabled = false;
            go.AddComponent<ClusterCmd>();
            go.AddComponent<DonateCmd>();
            go.AddComponent<FavorDebug>().enabled = false;
            go.AddComponent<FestivalCmd>();
            go.AddComponent<FollowCmd>();
            go.AddComponent<HatredCmd>().enabled = false;
            go.AddComponent<InteractiveCmd>();
            go.AddComponent<MessageCmd>();
            go.AddComponent<MiscCmd>();
            go.AddComponent<MonsterCmd>();
            go.AddComponent<NpcCmd>().enabled = false;
            go.AddComponent<PartyDebug>().enabled = false;
            go.AddComponent<PathfinderCmd>().enabled = false;
            go.AddComponent<PetCmd>();
            go.AddComponent<Player_Cmd>();
            go.AddComponent<PreOrderCmd>();
            go.AddComponent<RunbullCmd>().enabled = false;
            go.AddComponent<SceneItemCmd>();
            go.AddComponent<SpawnCmd>().enabled = false;
            go.AddComponent<StoryCmd>();
            go.AddComponent<TimeCmd>();

            go.AddComponent<ModCmd>();
            go.AddComponent<ItemDebug>().enabled = false;
            go.AddComponent<MissionDebug>().enabled = false;

            var ctr = go.transform.Find("Commander").gameObject.AddComponent<CmdCtr>();
            var select = go.transform.Find("Commander/Panel/Completion View").gameObject.AddComponent<SelectorCtr>();
            var oldTips = go.transform.Find("Commander/Panel/Completion View/Content/Completion Item").gameObject;
            var newTips = new GameObject("Completion Item");
            oldTips.name = "Completion Item Old";
            newTips.transform.SetParent(oldTips.transform.parent, false);

            CopyComponent(oldTips.GetComponent<RectTransform>(), newTips);
            CopyComponent(oldTips.GetComponent<CanvasRenderer>(), newTips);
            var image = CopyComponent(oldTips.GetComponent<Image>(), newTips);
            image.color = SelectorCtr.normalColor;
            var hlg = CopyComponent(oldTips.GetComponent<HorizontalLayoutGroup>(), newTips);
            hlg.childForceExpandWidth = false;
            hlg.spacing = 8;
            hlg.padding = new RectOffset(6, 12, 4, 4);
            
            foreach (Transform oldChild in oldTips.transform)
            {
                GameObject child = Object.Instantiate(oldChild.gameObject, newTips.transform);
                child.name = oldChild.name;
                foreach (var comp in oldChild.gameObject.GetComponents(typeof(Component)))
                {
                    BepInExPlugin.Dbgl($"Copying {comp.GetType()} for {oldChild.name}");
                    CopyComponent(comp, child);
                }
            }
            newTips.SetActive(false);
            //Object.DestroyImmediate(oldTips.gameObject);  

            var tips = newTips.AddComponent<TipsItemCtr>();

            ctr.canvasGroup = ctr.GetComponentInChildren<CanvasGroup>();
            ctr.graphicRaycaster = ctr.GetComponent<GraphicRaycaster>();
            ctr.inputField = ctr.GetComponentInChildren<TMP_InputField>();
            ctr.inputField.onValueChanged.AddListener(delegate (string str)
            {
                BepInExPlugin.Dbgl($"Changed input field value to '{str}'");
            });
            ctr.text = ctr.transform.Find("Panel/Scroll View/Viewport/Content/Text").GetComponent<TextMeshProUGUI>();
            ctr.selector = select;
            ctr.stringBuilder = new System.Text.StringBuilder("<color=yellow>Url 已经存在 Pathea.MiscCmd.ShowVoiceLog</color>");

            select.cmdCtr = ctr;
            select.inputText = ctr.inputField;
            select.itemPrefab = newTips;
            //select.scroll = select.GetComponent<ScrollRect>();
            //select.container = select.transform.Find("Content");
            //select.container.GetComponent<VerticalLayoutGroup>().spacing = 2;

            tips.label = tips.GetComponentInChildren<Text>();

            GameUtils.AddChild(null, go, false, true);

            string path = AedenthornUtils.GetAssetPath(BepInExPlugin.context, true);
            try
            {
                translatable = File.ReadAllLines(Path.Combine(path, "translate.txt"));
                translations = File.ReadAllLines(Path.Combine(path, "translated.txt"));
            }
            catch { }


            yield break;
        }


        public static T CopyComponent<T>(T original, GameObject destination) where T : Component
        {
            var type = original.GetType();
            var copy = destination.GetComponent(type);
            if(copy is null)
            {
                copy = destination.AddComponent(type);
            }

            var fields = type.GetFields();
            foreach (var field in fields) field.SetValue(copy, field.GetValue(original));
            return copy as T;
        }
    }
}