using System;
using Commonder;
using Pathea.FrameworkNs;
using Pathea.NpcNs;
using Pathea.SocialNs;
using Pathea.SocialNs.PartyNs;
using UnityEngine;

namespace Pathea
{
    public class PartyDebug : MonoBehaviour, ICmd
    {
        public void Awake()
        {
            Cmd.Instance.Register(this);
        }

        public void OnDestroy()
        {
            Cmd.Instance.ClearAll();
        }

        public void OnGUI()
        {
            if (Module<SocialModule>.Self.PartyData.active)
            {
                rect = GUI.Window(base.GetInstanceID(), rect, new GUI.WindowFunction(DoMyWindow), "Party");
            }
        }

        public void DrawNpcHeader()
        {
            float width = rect.width / 4f;
            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            GUILayout.Label("NPC", new GUILayoutOption[]
            {
                GUILayout.Width(width)
            });
            GUILayout.Label("NPC Name", new GUILayoutOption[]
            {
                GUILayout.Width(width)
            });
            GUILayout.Label("Favor", new GUILayoutOption[]
            {
                GUILayout.Width(width)
            });
            GUILayout.Label("Food", new GUILayoutOption[]
            {
                GUILayout.Width(width)
            });
            GUILayout.EndHorizontal();
        }

        public void DrawPartyNpc(PartyNpcArchive data)
        {
            float width = rect.width / 4f;
            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            GUILayout.Label(data.npcId.ToString(), new GUILayoutOption[]
            {
                GUILayout.Width(width)
            });
            GUILayout.Label(Module<NpcMgr>.Self.GetNpcProtoName(data.npcId), new GUILayoutOption[]
            {
                GUILayout.Width(width)
            });
            GUILayout.Label(data.favor.ToString("00.00"), new GUILayoutOption[]
            {
                GUILayout.Width(width)
            });
            GUILayout.Label(data.foodValue.ToString("00.00"), new GUILayoutOption[]
            {
                GUILayout.Width(width)
            });
            GUILayout.EndHorizontal();
        }

        public void DoMyWindow(int windowID)
        {
            PartyData partyData = Module<SocialModule>.Self.PartyData;
            GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
            DrawNpcHeader();
            for (int i = 0; i < partyData.npcs.Count; i++)
            {
                DrawPartyNpc(partyData.npcs[i]);
            }
            GUILayout.EndVertical();
            GUI.DragWindow(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height));
        }

        [Command("Party", "PartyShowDebug", "显示&关闭聚会属性面板", false)]
        public void BattlePanelShow(bool isShow)
        {
            base.enabled = isShow;
        }

        public PartyDebug()
        {
        }

        public Rect rect = new Rect(5f, 5f, 600f, 800f);
    }
}
