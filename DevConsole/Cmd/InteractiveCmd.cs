using System;
using Commonder;
using Pathea.FrameworkNs;
using Pathea.InteractiveNs;
using UnityEngine;

namespace DevConsole
{
    public class InteractiveCmd : MonoBehaviour, ICmd
    {
        public void Awake()
        {
            Cmd.Instance.Register(this);
        }

        public void OnDestroy()
        {
            Cmd.Instance.ClearAll();
        }

        [Command("Interactive", "InteractiveUnlimitedCount", "互动次数无限", false)]
        public void InteractiveUnlimitedCount(int npcId, int optionId)
        {
            InteractiveConst.Number_PerDay = 99999;
        }

        [Command("Interactive", "InteractiveStart", "开启互动", false)]
        public void InteractiveStart(int npcId, int optionId)
        {
            Module<InteractiveMgr>.Self.StartInteractive(InteractiveActorType.Npc, npcId, npcId, optionId, InteractiveFlag.None, null);
        }

        [Command("Interactive", "InteractiveStop", "停止互动", false)]
        public void InteractiveStop()
        {
            Module<InteractiveMgr>.Self.StopInteractive(InteractiveStopType.Interrupt, false);
        }

        public InteractiveCmd()
        {
        }
    }
}
