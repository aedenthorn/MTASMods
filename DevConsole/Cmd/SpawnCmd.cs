using System;
using System.Collections.Generic;
using Commonder;
using Pathea.FrameworkNs;
using Pathea.SpawnNs;
using UnityEngine;

namespace Pathea
{
    public class SpawnCmd : MonoBehaviour, ICmd
    {
        public void Awake()
        {
            base.enabled = false;
            tags = new List<string>();
            Cmd.Instance.Register(this);
        }

        public void OnDestroy()
        {
            Cmd.Instance.ClearAll();
        }

        public void OnGUI()
        {
            Module<SpawnModule>.Self.GetAllTags(ref tags);
            GUILayout.BeginArea(rect, GUI.skin.box);
            for (int i = 0; i < tags.Count; i++)
            {
                GUILayout.Label(tags[i], GUI.skin.box, Array.Empty<GUILayoutOption>());
            }
            GUILayout.EndArea();
        }

        [Command("Spawn", "SpawnShowTag", "显示生成系统标签", false)]
        public void SpawnShowTag()
        {
            base.enabled = true;
        }

        [Command("Spawn", "SpawnHideTag", "隐藏生成系统标签", false)]
        public void SpawnHideTag()
        {
            base.enabled = false;
        }

        [Command("Spawn", "SpawnAddTag", "添加生成系统标签", false)]
        public void SpawnAddTag(string tag)
        {
            Module<SpawnModule>.Self.AddTag(tag);
        }

        [Command("Spawn", "SpawnRemoveTag", "删除生成系统标签", false)]
        public void SpawnRemoveTag(string tag)
        {
            Module<SpawnModule>.Self.RemoveTag(tag);
        }

        [Command("Spawn", "SpawnAddModifier", "添加怪物属性修改", false)]
        public void SpawnAddModifier(string name, string tag, ModifierType type, string data)
        {
            Module<SpawnModule>.Self.AddModifier(name, tag, type, data);
        }

        [Command("Spawn", "SpawnRemoveModifier", "删除怪物属性修改", false)]
        public void SpawnRemoveModifier(string name)
        {
            Module<SpawnModule>.Self.RemoveModifier(name);
        }

        public SpawnCmd()
        {
        }

        public Rect rect;

        public List<string> tags;
    }
}
