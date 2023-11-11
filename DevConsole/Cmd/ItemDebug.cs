using System;
using System.Runtime.CompilerServices;
using Commonder;
using Pathea.FrameworkNs;
using Pathea.ItemNs;
using Pathea.NpcNs;
using UnityEngine;

namespace Pathea.SocialNs
{
    public class ItemDebug : MonoBehaviour, ICmd
    {
        public void Awake()
        {
            Cmd.Instance.Register(this);
        }

        public void OnDestroy()
        {
            Cmd.Instance.ClearAll();
        }

        [Command("ItemPanel", "ItemShowPanel", "Show items panel", false)]
        public void ItemPanelShow(bool isShow)
        {
            enabled = isShow;
        }
        public bool CanShow(int itemId)
        {
            return Module<ItemPrototypeModule>.Self.GetItemName(itemId).ToLower().Contains(filter);
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
                return;
            }
            GUILayout.EndHorizontal();

            scrollPos = GUILayout.BeginScrollView(scrollPos, GUI.skin.box);
            if(!string.IsNullOrEmpty(filter)){
                Module<ItemPrototypeModule>.Self.ForeachPrototype(delegate (ItemPrototype item)
                {
                    if (CanShow(item.id))
                    {
                        GUILayout.BeginHorizontal(GUI.skin.box, Array.Empty<GUILayoutOption>());
                        GUILayout.Label(Module<ItemPrototypeModule>.Self.GetItemName(item.id), new GUILayoutOption[]
                        {
                        GUILayout.Width((float)width)
                        });
                        if (GUILayout.Button(item.id.ToString(), new GUILayoutOption[]
                        {
                        GUILayout.Width((float)width)
                        }))
                        {
                            CmdCtr.Instance.inputField.text.Insert(CmdCtr.Instance.inputField.caretPosition, item.id.ToString());
                            CmdCtr.Instance.inputField.caretPosition += item.id.ToString().Length;
                        }
                        GUILayout.Label(item.sellPrice.ToString(), new GUILayoutOption[]
                        {
                        GUILayout.Width((float)width)
                        });
                        GUILayout.Label(item.buyPrice.ToString(), new GUILayoutOption[]
                        {
                        GUILayout.Width((float)width)
                        });
                        GUILayout.Label(item.stackNumber.ToString(), new GUILayoutOption[]
                        {
                        GUILayout.Width((float)width)
                        });
                        GUILayout.EndHorizontal();
                    }
                });

            }
            GUILayout.EndScrollView();
            GUILayout.EndArea();
        }

        public ItemDebug()
        {
        }

        public string filter;

        public Vector2 scrollPos = Vector2.zero;

    }
}
