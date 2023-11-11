using System;
using System.Collections.Generic;
using Commonder;
using Pathea.Cluster;
using Pathea.FrameworkNs;
using Pathea.NpcNs;
using UnityEngine;

namespace Pathea
{
    public class ClusterCmd : MonoBehaviour, ICmd
    {
        public void Awake()
        {
            Cmd.Instance.Register(this);
        }

        public void OnDestroy()
        {
            Cmd.Instance.ClearAll();
        }

        [Command("Cluster", "ClusterAddNpcGroup", "创建群组NPC", false)]
        public void ClusterAddNpcGroup(string name, string behavior, int leaderId, string npcIdStr)
        {
            List<Npc> list = new List<Npc>();
            string[] array = npcIdStr.Split(',');
            for (int i = 0; i < array.Length; i++)
            {
                Npc npc = Module<NpcMgr>.Self.GetNpc(Convert.ToInt32(array[i]));
                if (npc != null)
                {
                    list.Add(npc);
                }
            }
            Module<ClusterModule>.Self.GenNpcCluster(-1, name, ClusterFlag.None, behavior, null, Module<NpcMgr>.Self.GetNpc(leaderId), list);
        }

        public ClusterCmd()
        {
        }
    }
}
