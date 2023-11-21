using System;
using Commonder;
using Pathea.DonateNs;
using Pathea.FrameworkNs;
using UnityEngine;

namespace DevConsole
{
    public class DonateCmd : MonoBehaviour, ICmd
    {
        public void Awake()
        {
            Cmd.Instance.Register(this);
        }

        public void OnDestroy()
        {
            Cmd.Instance.ClearAll();
        }

        [Command("Donate", "AddDonate", "捐赠", false)]
        public void AddDonate(int donateId, int actorId, int worth)
        {
            Module<DonateModule>.Self.AddDonate(donateId, actorId, worth);
        }

        [Command("Donate", "RemoveDonate", "删除捐赠", false)]
        public void RemoveDonate(int donateId, int actorId)
        {
            Module<DonateModule>.Self.RemoveDonate(donateId, actorId);
        }

        [Command("Donate", "AddDonateBox", "添加捐赠箱", false)]
        public void AddDonateBox(int donateId, int donateDays, string donatePoint)
        {
            Module<DonateModule>.Self.AddDonateBox(donateId, donateDays, donatePoint);
        }

        [Command("Donate", "RemoveDonateBox", "删除捐赠箱", false)]
        public void RemoveDonateBox(int donateId)
        {
            Module<DonateModule>.Self.RemoveDonateBox(donateId);
        }

        [Command("Donate", "OpenDonateUI", "打开捐赠UI", false)]
        public void OpenDonateUI(int donateId)
        {
            DonateBox.OpenDonateUI(donateId);
        }

        public DonateCmd()
        {
        }
    }
}
