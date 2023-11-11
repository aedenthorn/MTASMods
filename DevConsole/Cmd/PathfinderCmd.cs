using System;
using System.Runtime.CompilerServices;
using Commonder;
using Pathea.FrameworkNs;
using Pathea.PathfinderNs;
using Pathfinding;
using UnityEngine;

namespace Pathea
{
    public class PathfinderCmd : MonoBehaviour, ICmd
    {
        public void Awake()
        {
            base.enabled = false;
            Cmd.Instance.Register(this);
        }

        public void OnDestroy()
        {
            Cmd.Instance.ClearAll();
        }

        [Command("Pathfinder", "PathfinderShowGUI", "显示&关闭寻路系统面板", false)]
        public void PathfinderShowGUI(bool isShow)
        {
            base.enabled = isShow;
        }

        [Command("Pathfinder", "PathfinderShowPath", "开启&关闭寻路显示", false)]
        public void PathfinderShowPath(bool isShow)
        {
            Module<PathfinderModule>.Self.ShowSearchPath(isShow);
        }

        public static int count;

        [Command("Pathfinder", "PathfinderShowNodeCount", "显示寻路节点数量", true)]
        public int PathfinderShowNodeCount()
        {
            if (AstarPath.active != null)
            {
                count = 0;
                AstarPath.active.data.GetNodes(delegate (GraphNode node)
                {
                    count++;
                });
                return count;
            }
            return 0;
        }

        public PathfinderCmd()
        {
        }

        public Rect rect;

        public NavGraph[] graphs;

    }
}
