using System;
using System.Collections.Generic;
using Commonder;
using Pathea.BehaviorNs;
using Pathea.FrameworkNs;
using UnityEngine;

namespace DevConsole
{
    public class BehaviorDebug : MonoBehaviour, ICmd
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
            if (instId <= 0)
            {
                Module<BehaviorModule>.Self.GetAllTags(ref tags);
            }
            else
            {
                Module<BehaviorModule>.Self.GetAllTags(instId, ref tags);
            }
            GUILayout.BeginArea(rect, GUI.skin.box);
            for (int i = 0; i < tags.Count; i++)
            {
                GUILayout.Label(tags[i], GUI.skin.box, Array.Empty<GUILayoutOption>());
            }
            GUILayout.EndArea();
        }

        [Command("Behavior", "BehaviorHide", "隐藏行为系统面板", false)]
        public void BehaviorHide()
        {
            base.enabled = false;
        }

        [Command("Behavior", "BehaviorShowTagAll", "显示所有全局标签", false)]
        public void BehaviorShowTagAll()
        {
            base.enabled = true;
            instId = 0;
        }

        [Command("Behavior", "BehaviorShowTagInst", "显示所有个体标签", false)]
        public void BehaviorShowTagInst(int instId)
        {
            base.enabled = true;
            this.instId = instId;
        }

        [Command("Behavior", "BehaviorAddTag", "添加AI全局标签", false)]
        public void BehaviorAddTag(string tag)
        {
            Module<BehaviorModule>.Self.AddTag(tag);
        }

        [Command("Behavior", "BehaviorRemoveTag", "删除AI全局标签", false)]
        public void BehaviorRemoveTag(string tag)
        {
            Module<BehaviorModule>.Self.RemoveTag(tag);
        }

        [Command("Behavior", "BehaviorAddTagInst", "添加AI个体标签", false)]
        public void BehaviorAddTagInst(int instId, string tag)
        {
            Module<BehaviorModule>.Self.AddTag(instId, tag);
        }

        [Command("Behavior", "BehaviorRemoveTagInst", "删除AI个体标签", false)]
        public void BehaviorRemoveTagInst(int instId, string tag)
        {
            Module<BehaviorModule>.Self.RemoveTag(instId, tag);
        }

        public BehaviorDebug()
        {
        }

        public Rect rect;

        public int instId;

        public List<string> tags;
    }
}
