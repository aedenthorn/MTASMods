﻿using Commonder;
using Pathea.FrameworkNs;
using Pathea.MissionNs;
using Pathea.NpcNs;
using System;
using UnityEngine;

namespace DevConsole.SocialNs
{
    public class MissionDebug : MonoBehaviour, ICmd
    {
        public void Awake()
        {
            Cmd.Instance.Register(this);
        }

        public void OnDestroy()
        {
            Cmd.Instance.ClearAll();
        }

        [Command("Mission", "MissionShowPanel", "Show quests", false)]
        public void OrderPanelShow(bool isShow)
        {
            base.enabled = isShow;
        }

        public bool CanShow(Mission mission)
        {
            return string.IsNullOrEmpty(filter) || mission.MissionNameStr.Contains(filter) || mission.MissionDescribe.Contains(filter) || Module<NpcMgr>.Self.GetNpcName(mission.deliverNpcId).Contains(filter);
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
            GUILayout.Label("Title", new GUILayoutOption[]
            {
                        GUILayout.Width((float)width)
            });
            GUILayout.Label("Description", new GUILayoutOption[]
            {
                        GUILayout.Width((float)width)
            });
            GUILayout.EndHorizontal();

            scrollPos = GUILayout.BeginScrollView(scrollPos, GUI.skin.box);
            foreach(var o in Module<MissionManager>.Self.GetMissionActived())
            {
                if (CanShow(o))
                {
                    var id = o.MissionId.ToString();
                    GUILayout.BeginHorizontal(GUI.skin.box, Array.Empty<GUILayoutOption>());
                    if (GUILayout.Button(id, new GUILayoutOption[]
                    {
                        GUILayout.Width((float)width)
                    }))
                    {
                        CmdCtr.Instance.inputField.text = CmdCtr.Instance.inputField.text.Insert(CmdCtr.Instance.inputField.caretPosition, id);
                        CmdCtr.Instance.inputField.caretPosition += id.Length;
                    }
                    GUILayout.Label(o.MissionNameStr, new GUILayoutOption[]
                    {
                        GUILayout.Width((float)width)
                    });
                    GUILayout.Label(o.MissionDescribe, new GUILayoutOption[]
                    {
                        GUILayout.Width((float)width)
                    });
                    if (GUILayout.Button("Finish", new GUILayoutOption[] { GUILayout.Width(width) }))
                    {
                        Module<MissionManager>.Self.SubmitMission(o.MissionId);
                    }
                    GUILayout.EndHorizontal();
                }
            }
            GUILayout.EndScrollView();
            GUILayout.EndArea();
        }

        public string filter;

        public Vector2 scrollPos = Vector2.zero;

    }
}
