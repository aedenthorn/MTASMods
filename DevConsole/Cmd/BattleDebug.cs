using System;
using System.Runtime.CompilerServices;
using Commonder;
using Pathea.BattleFieldNs;
using Pathea.FrameworkNs;
using UnityEngine;

namespace DevConsole
{
    public class BattleDebug : MonoBehaviour, ICmd
    {
        public void Awake()
        {
            Cmd.Instance.Register(this);
        }

        public void OnDestroy()
        {
            Cmd.Instance.ClearAll();
        }

        public void OnDrawGizmosSelected()
        {
            if (Module<BattleFieldModule>.Self != null)
            {
                Module<BattleFieldModule>.Self.OnDrawGizmos();
            }
        }

        public void OnGUI()
        {
            GUILayout.BeginArea(rect);
            Module<BattleFieldModule>.Self.ForeachBattle(8000, delegate (int agentId, BattleResult result)
            {
                DrawActorBattle(agentId, result.actionId, result.statuss, result.weights);
            });
            GUILayout.EndArea();
        }

        public void DrawActorBattle(int agentId, int actionId, float[] statuss, float[] weights)
        {
            float width = rect.width / (float)Mathf.Max(7, 7);
            GUILayout.BeginVertical(GUI.skin.box, Array.Empty<GUILayoutOption>());
            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            GUILayout.Label(string.Format("Actor ID : {0}", agentId.ToString()), Array.Empty<GUILayoutOption>());
            string format = "Action result : {0}";
            BMAction bmaction = (BMAction)actionId;
            GUILayout.Label(string.Format(format, bmaction.ToString()), Array.Empty<GUILayoutOption>());
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            for (int i = 0; i < 7; i++)
            {
                GUILayout.BeginVertical(new GUILayoutOption[]
                {
                    GUILayout.Width(width)
                });
                BMStatus bmstatus = (BMStatus)i;
                GUILayout.Label(bmstatus.ToString(), Array.Empty<GUILayoutOption>());
                GUILayout.Label(statuss[i].ToString("0.00"), Array.Empty<GUILayoutOption>());
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            for (int j = 0; j < 7; j++)
            {
                GUILayout.BeginVertical(new GUILayoutOption[]
                {
                    GUILayout.Width(width)
                });
                bmaction = (BMAction)j;
                GUILayout.Label(bmaction.ToString(), Array.Empty<GUILayoutOption>());
                GUILayout.Label(weights[j].ToString("0.00"), Array.Empty<GUILayoutOption>());
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }

        [Command("Battle", "BattlePanelShow", "显示战场数据面板", false)]
        public void BattlePanelShow()
        {
            base.enabled = true;
        }

        [Command("Battle", "BattlePanelHide", "停止显示战场数据面板", false)]
        public void BattlePanelHide()
        {
            base.enabled = false;
        }

        public BattleDebug()
        {
        }

        public Rect rect = new Rect(5f, 100f, 600f, 1200f);
    }
}
