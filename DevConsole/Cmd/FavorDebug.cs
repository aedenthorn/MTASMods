using System;
using System.Runtime.CompilerServices;
using Commonder;
using Pathea.BehaviorNs;
using Pathea.FrameworkNs;
using Pathea.NpcNs;
using UnityEngine;

namespace Pathea.SocialNs
{
    public class FavorDebug : MonoBehaviour, ICmd
    {
        public void Awake()
        {
            Cmd.Instance.Register(this);
        }

        public void OnDestroy()
        {
            Cmd.Instance.ClearAll();
        }

        [Command("FavorPanel", "FavorShowPanel", "显示&关闭好感度属性面板", false)]
        public void BattlePanelShow(bool isShow)
        {
            base.enabled = isShow;
        }

        public bool CanShow(int npcId)
        {
            return string.IsNullOrEmpty(filter) || npcId.ToString().Contains(filter) || Module<NpcMgr>.Self.GetNpcProtoName(npcId).ToLower().Contains(filter) || Enum.GetName(typeof(SocialType), Module<SocialModule>.Self.GetSocialType(npcId)).ToLower().Contains(filter) || Enum.GetName(typeof(SocialLevel), Module<SocialModule>.Self.GetSocialLevel(npcId)).ToLower().Contains(filter);
        }

        public void OnGUI()
        {
            int num = 600;
            int width = Mathf.CeilToInt((float)(num - 60) * 0.2f);
            GUILayout.BeginArea(new Rect(0f, 80f, (float)num, (float)(Screen.height - 160)), GUI.skin.box);
            GUILayout.BeginHorizontal();
            filter = GUILayout.TextField(filter, new GUILayoutOption[] { GUILayout.ExpandWidth(true) }).ToLower();
            if (GUILayout.Button("X", new GUILayoutOption[] { GUILayout.Width(60) }))
            {
                enabled = false;
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal(GUI.skin.box, Array.Empty<GUILayoutOption>());
            GUILayout.Label("ID", new GUILayoutOption[]
            {
                        GUILayout.Width((float)width)
            });
            GUILayout.Label("Name", new GUILayoutOption[]
            {
                        GUILayout.Width((float)width)
            });
            GUILayout.Label("SocialType", new GUILayoutOption[]
            {
                        GUILayout.Width((float)width)
            });
            GUILayout.Label("SocialLevel", new GUILayoutOption[]
            {
                        GUILayout.Width((float)width)
            });
            GUILayout.Label("SocialFavor", new GUILayoutOption[]
            {
                        GUILayout.Width((float)width)
            });
            GUILayout.EndHorizontal();

            scrollPos = GUILayout.BeginScrollView(scrollPos, GUI.skin.box);
            Module<NpcMgr>.Self.ForeachStoryIds(delegate (int npcId)
            {
                if (CanShow(npcId))
                {
                    GUILayout.BeginHorizontal(GUI.skin.box, Array.Empty<GUILayoutOption>());
                    if (GUILayout.Button(npcId.ToString(), new GUILayoutOption[]
                    {
                        GUILayout.Width((float)width)
                    }))
                    {
                        CmdCtr.Instance.inputField.text.Insert(CmdCtr.Instance.inputField.caretPosition, npcId.ToString());
                        CmdCtr.Instance.inputField.caretPosition += npcId.ToString().Length;
                    }
                    GUILayout.Label(npcId.ToString(), new GUILayoutOption[]
                    {
                        GUILayout.Width((float)width)
                    });
                    GUILayout.Label(Module<NpcMgr>.Self.GetNpcProtoName(npcId), new GUILayoutOption[]
                    {
                        GUILayout.Width((float)width)
                    });
                    GUILayout.Label(Enum.GetName(typeof(SocialType), Module<SocialModule>.Self.GetSocialType(npcId)), new GUILayoutOption[]
                    {
                        GUILayout.Width((float)width)
                    });
                    GUILayout.Label(Enum.GetName(typeof(SocialLevel), Module<SocialModule>.Self.GetSocialLevel(npcId)), new GUILayoutOption[]
                    {
                        GUILayout.Width((float)width)
                    });
                    GUILayout.Label(Module<SocialModule>.Self.GetSocialFavor(npcId).ToString(), new GUILayoutOption[]
                    {
                        GUILayout.Width((float)width - 80)
                    });
                    if (GUILayout.Button("+10", new GUILayoutOption[] { GUILayout.Width(40) }))
                    {
                        Module<SocialModule>.Self.AddSocialFavor(npcId, 10);
                    }
                    if (GUILayout.Button("+100", new GUILayoutOption[] { GUILayout.Width(40) }))
                    {
                        Module<SocialModule>.Self.AddSocialFavor(npcId, 100);
                    }
                    GUILayout.EndHorizontal();
                }
            });
            GUILayout.EndScrollView();
            GUILayout.EndArea();
        }

        public FavorDebug()
        {
        }

        public string filter;

        public Vector2 scrollPos = Vector2.zero;

    }
}
