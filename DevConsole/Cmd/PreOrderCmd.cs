using System;
using System.Collections.Generic;
using Commonder;
using Pathea.ActorNs;
using Pathea.FrameworkNs;
using Pathea.PreOrderNs;
using UnityEngine;

namespace Pathea
{
    public class PreOrderCmd : MonoBehaviour, ICmd
    {
        public void Start()
        {
            Cmd.Instance.Register(this);
        }

        public void OnDestroy()
        {
            Cmd.Instance.ClearAll();
        }

        [Command("PreOrder", "PreOrderSignAgreement", "签定订购协议", false)]
        public void PreOrderSignAgreement(int pointId)
        {
            int num;
            if (Module<PreOrderModule>.Self.CanSignAgreement(pointId, out num))
            {
                if (Module<Player>.Self.bag.Gold < num)
                {
                    global::Debug.LogError("金币不足以签定订购协议");
                    return;
                }
                Module<PreOrderModule>.Self.SignAgreement(pointId);
                global::Debug.LogError(string.Format("成功签定协议[{0}]， 花费[{1}]", pointId, num));
            }
        }

        [Command("PreOrder", "PreOrderUpgradeAgreement", "升级订购协议", false)]
        public void PreOrderUpgradeAgreement(int pointId)
        {
            int num;
            if (Module<PreOrderModule>.Self.CanUpgradeAgreement(pointId, out num))
            {
                if (Module<Player>.Self.bag.Gold < num)
                {
                    global::Debug.LogError("金币不足以升级订购协议");
                    return;
                }
                Module<PreOrderModule>.Self.UpgradeAgreement(pointId);
                global::Debug.LogError(string.Format("成功升级订购协议[{0}]， 花费[{1}]", pointId, num));
            }
        }

        [Command("PreOrder", "PreOrderFetchItems", "领取订购货品", false)]
        public void PreOrderFetchItems(int pointId)
        {
            List<Vector3Int> items;
            if (Module<PreOrderModule>.Self.FetchItems(pointId, out items))
            {
                Module<Player>.Self.bag.Add(PreOrderUtil.GenItemInstances(items), true);
            }
        }

        public PreOrderCmd()
        {
        }

        public int pointId;
    }
}
