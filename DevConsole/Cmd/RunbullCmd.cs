using System;
using System.Collections.Generic;
using Commonder;
using Pathea.FrameworkNs;
using Pathea.RunbullNs;
using UnityEngine;

namespace DevConsole
{
    public class RunbullCmd : MonoBehaviour, ICmd
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
            RunbullUIData runbullUIData = (Module<RunbullModule>.Self != null) ? Module<RunbullModule>.Self.GetUIData() : null;
            if (runbullUIData == null)
            {
                return;
            }
            GUILayout.BeginArea(rect);
            if (runbullUIData.recipes != null)
            {
                for (int i = 0; i < runbullUIData.recipes.Count; i++)
                {
                    DrawCookbook(runbullUIData.recipes[i]);
                }
            }
            if (runbullUIData.characters != null)
            {
                for (int j = 0; j < runbullUIData.characters.Count; j++)
                {
                    DrawScore(runbullUIData.characters[j]);
                }
            }
            GUILayout.EndArea();
        }

        public void DrawCookbook(RecipeUIData data)
        {
            if (data.items == null || data.items.Count == 0)
            {
                return;
            }
            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            for (int i = 0; i < data.items.Count; i++)
            {
                GUILayout.Label(data.items[i].x.ToString(), new GUILayoutOption[]
                {
                    GUILayout.Width(60f)
                });
                if (i < data.items.Count - 1)
                {
                    GUILayout.Label("+", new GUILayoutOption[]
                    {
                        GUILayout.Width(10f)
                    });
                }
                else
                {
                    GUILayout.Label("=", new GUILayoutOption[]
                    {
                        GUILayout.Width(10f)
                    });
                }
            }
            GUILayout.Label(data.recipeId.ToString(), new GUILayoutOption[]
            {
                GUILayout.Width(60f)
            });
            GUILayout.EndHorizontal();
        }

        public void DrawScore(CharacterUIData data)
        {
            if (data.scores == null || data.scores.Length == 0)
            {
                return;
            }
            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            GUILayout.Label(data.id.ToString(), Array.Empty<GUILayoutOption>());
            for (int i = 0; i < data.scores.Length; i++)
            {
                GUILayout.Label(data.scores[i].ToString(), Array.Empty<GUILayoutOption>());
            }
            GUILayout.EndHorizontal();
        }

        [Command("Runbull", "RunbullStart", "启动奔牛小游戏", false)]
        public void RunbullStart()
        {
            Module<RunbullModule>.Self.Runbull(new RunbullContext
            {
                playerIds = new List<int>
                {
                    8001,
                    8002,
                    8003,
                    8004,
                    8005
                },
                audienceIds = new List<int>
                {
                    8020,
                    8021,
                    8022,
                    8023,
                    8024
                }
            }, null);
        }

        [Command("Runbull", "RunbullGiveup", "玩家放弃逗牛小游戏", false)]
        public void RunbullGiveup()
        {
            Module<RunbullModule>.Self.PlayerGiveup();
        }

        [Command("Runbull", "RunbullDropItem", "爆出道具", false)]
        public void RunbullDropItem(int instId, float dropRate)
        {
            Module<RunbullModule>.Self.DropItem(instId, dropRate);
        }

        [Command("Runbull", "RunbullStartSprint", "启动奔牛冲刺", false)]
        public void RunbullStartSprint(int instId)
        {
            Module<RunbullModule>.Self.StartSprint(instId);
        }

        [Command("Runbull", "RunbullActiveSprintEffect", "开启&关闭奔牛冲刺特效", false)]
        public void RunbullActiveSprintEffect(bool isActive)
        {
            Module<RunbullModule>.Self.ActiveSprintEffect(isActive);
        }

        [Command("Runbull", "RunbullShowDebug", "显示&隐藏奔牛小游戏数据面板", false)]
        public void RunbullShowDebug(bool isShow)
        {
            base.enabled = isShow;
        }

        public RunbullCmd()
        {
        }

        public Rect rect;
    }
}
