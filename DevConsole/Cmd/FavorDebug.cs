using System;
using System.Runtime.CompilerServices;
using Commonder;
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
            return string.IsNullOrEmpty(filter) || npcId.ToString().Contains(filter) || Module<NpcMgr>.Self.GetNpcProtoName(npcId).Contains(filter) || Module<SocialModule>.Self.GetSocialType(npcId).ToString().Contains(filter) || Module<SocialModule>.Self.GetSocialLevel(npcId).ToString().Contains(filter);
        }

        public void OnGUI()
        {
            int num = 600;
            int width = Mathf.CeilToInt((float)(num - 60) * 0.2f);
            GUILayout.BeginArea(new Rect(0f, 80f, (float)num, (float)(Screen.height - 160)), GUI.skin.box);
            filter = GUILayout.TextField(filter, Array.Empty<GUILayoutOption>());
            scrollPos = GUILayout.BeginScrollView(scrollPos, GUI.skin.box);
            Module<NpcMgr>.Self.ForeachStoryIds(delegate (int npcId)
            {
                if (CanShow(npcId))
                {
                    GUILayout.BeginHorizontal(GUI.skin.box, Array.Empty<GUILayoutOption>());
                    GUILayout.Label(npcId.ToString(), new GUILayoutOption[]
                    {
                        GUILayout.Width((float)width)
                    });
                    GUILayout.Label(Module<NpcMgr>.Self.GetNpcProtoName(npcId), new GUILayoutOption[]
                    {
                        GUILayout.Width((float)width)
                    });
                    GUILayout.Label(Module<SocialModule>.Self.GetSocialType(npcId).ToString(), new GUILayoutOption[]
                    {
                        GUILayout.Width((float)width)
                    });
                    GUILayout.Label(Module<SocialModule>.Self.GetSocialLevel(npcId).ToString(), new GUILayoutOption[]
                    {
                        GUILayout.Width((float)width)
                    });
                    GUILayout.Label(Module<SocialModule>.Self.GetSocialFavor(npcId).ToString(), new GUILayoutOption[]
                    {
                        GUILayout.Width((float)width)
                    });
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
