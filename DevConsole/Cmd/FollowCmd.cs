using System;
using Commonder;
using Pathea.BehaviorNs;
using Pathea.FollowNs;
using Pathea.FrameworkNs;
using UnityEngine;

namespace DevConsole
{
    public class FollowCmd : MonoBehaviour, ICmd
    {
        public void Start()
        {
            Cmd.Instance.Register(this);
        }

        public void OnDestroy()
        {
            Cmd.Instance.ClearAll();
        }

        [Command("Follow", "FollowAdd", "添加跟随", false)]
        public void FollowAdd(int id, int targetId, int state, int origin, string flagStr = "")
        {
            FollowFlag followFlag = FollowFlag.None;
            if (!string.IsNullOrEmpty(flagStr))
            {
                string[] array = flagStr.Split('|');
                for (int i = 0; i < array.Length; i++)
                {
                    FollowFlag followFlag2;
                    if (Enum.TryParse<FollowFlag>(array[i], out followFlag2))
                    {
                        followFlag |= followFlag2;
                    }
                }
            }
            Module<FollowModule>.Self.AddFollower(id, targetId, (FollowState)state, (FollowOrigin)origin, followFlag, "", null);
        }

        [Command("Follow", "FollowAddBehavior", "添加跟随并自定义行为树", false)]
        public void FollowAddBehavior(int id, int targetId, int state, int origin, string flagStr = "", string behavior = "", string paras = "")
        {
            FollowFlag followFlag = FollowFlag.None;
            if (!string.IsNullOrEmpty(flagStr))
            {
                string[] array = flagStr.Split('|');
                for (int i = 0; i < array.Length; i++)
                {
                    FollowFlag followFlag2;
                    if (Enum.TryParse<FollowFlag>(array[i], out followFlag2))
                    {
                        followFlag |= followFlag2;
                    }
                }
            }
            Module<FollowModule>.Self.AddFollower(id, targetId, (FollowState)state, (FollowOrigin)origin, followFlag, behavior, BehaviorUtil.GenerateHashtable(paras, ';', '='));
        }

        [Command("Follow", "FollowRemove", "删除跟随", false)]
        public void FollowRemove(int id, int targetId)
        {
            Module<FollowModule>.Self.RemoveFollower(id, targetId);
        }

        public FollowCmd()
        {
        }
    }
}
