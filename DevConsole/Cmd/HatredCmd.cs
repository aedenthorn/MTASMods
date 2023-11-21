using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Commonder;
using Pathea.FrameworkNs;
using Pathea.HatredNs;
using UnityEngine;

namespace DevConsole
{
    public class HatredCmd : MonoBehaviour, ICmd
    {
        public void Awake()
        {
            Cmd.Instance.Register(this);
            datas = new List<HatredCmd.HatredData>();
        }

        public void OnDestroy()
        {
            Cmd.Instance.ClearAll();
            datas = null;
        }

        public void OnGUI()
        {
            curIdx = 0;
            GUILayout.BeginArea(rect);
            Module<HatredModule>.Self.ForeachHatredAgent(delegate (HatredAgent ret)
            {
                DrawHatredAgent(ret);
            });
            GUILayout.EndArea();
        }

        public void DrawHatredAgent(HatredAgent agent)
        {
            int num = curIdx;
            curIdx = num + 1;
            GUILayout.BeginArea(GetRect(num), GUI.skin.box);
            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            GUILayout.Label(string.Format("Self:{0}", agent.Agent.ID), Array.Empty<GUILayoutOption>());
            GUILayout.Label(string.Format("Target:{0}", (agent.Target == null) ? 0 : agent.Target.ID), Array.Empty<GUILayoutOption>());
            GUILayout.Label(string.Format("Hatred Max:{0}", agent.HatredDamageMax), new GUILayoutOption[]
            {
                GUILayout.Width(100f)
            });
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            GUILayout.Label("ID", new GUILayoutOption[]
            {
                GUILayout.Width(100f)
            });
            GUILayout.Label("Hatred", new GUILayoutOption[]
            {
                GUILayout.Width(100f)
            });
            GUILayout.Label("Base Hatred", new GUILayoutOption[]
            {
                GUILayout.Width(100f)
            });
            GUILayout.Label("Damage Hatred", new GUILayoutOption[]
            {
                GUILayout.Width(100f)
            });
            GUILayout.EndHorizontal();
            datas.Clear();
            for (int i = 0; i < agent.Count; i++)
            {
                HatredCmd.HatredData item;
                item.id = agent[i].agent.ID;
                item.hatred = agent[i].hatred;
                item.baseHatred = agent[i].baseHatred;
                item.damageHatred = agent[i].damageHatred;
                datas.Add(item);
            }
            datas.Sort(delegate (HatredCmd.HatredData l, HatredCmd.HatredData r)
            {
                if (l.hatred <= r.hatred)
                {
                    return 1;
                }
                return -1;
            });
            for (int j = 0; j < datas.Count; j++)
            {
                GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
                HatredCmd.HatredData hatredData = datas[j];
                GUILayout.Label(hatredData.id.ToString(), new GUILayoutOption[]
                {
                    GUILayout.Width(100f)
                });
                hatredData = datas[j];
                GUILayout.Label(hatredData.hatred.ToString(), new GUILayoutOption[]
                {
                    GUILayout.Width(100f)
                });
                hatredData = datas[j];
                GUILayout.Label(hatredData.baseHatred.ToString(), new GUILayoutOption[]
                {
                    GUILayout.Width(100f)
                });
                hatredData = datas[j];
                GUILayout.Label(hatredData.damageHatred.ToString(), new GUILayoutOption[]
                {
                    GUILayout.Width(100f)
                });
                GUILayout.EndHorizontal();
            }
            GUILayout.EndArea();
        }

        public Rect GetRect(int idx)
        {
            return new Rect
            {
                x = rect.x,
                y = rect.y + (float)idx * (height + 5f),
                width = rect.width,
                height = height
            };
        }

        [Command("Hatred", "HatredPanelShow", "显示仇恨数据面板", false)]
        public void HatredPanelShow()
        {
            base.enabled = true;
        }

        [Command("Hatred", "HatredPanelHide", "停止显示仇恨数据面板", false)]
        public void HatredPanelHide()
        {
            base.enabled = false;
        }

        public HatredCmd()
        {
        }


        [SerializeField]
        public Rect rect = new Rect(5f, 5f, 600f, 1200f);

        [SerializeField]
        public float height = 50f;

        public int curIdx;

        public List<HatredCmd.HatredData> datas;

        public struct HatredData
        {
            public int id;

            public float hatred;

            public float baseHatred;

            public float damageHatred;
        }

    }
}
