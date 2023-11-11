using System;
using Commonder;
using Pathea.FrameworkNs;
using Pathea.PetNs;
using UnityEngine;

namespace Pathea
{
    public class PetCmd : MonoBehaviour, ICmd
    {
        public void Awake()
        {
            Cmd.Instance.Register(this);
        }

        public void OnDestroy()
        {
            Cmd.Instance.ClearAll();
        }

        [Command("Pet", "PetUnlock", "解锁宠物系统", false)]
        public void PetUnlock()
        {
            Module<PetMgr>.Self.Unlock();
        }

        [Command("Pet", "PetStartRecruit", "招募宠物", false)]
        public void PetStartRecruit(string origin, int protoId)
        {
            PetOriginType originType;
            if (Enum.TryParse<PetOriginType>(origin, out originType))
            {
                Module<PetMgr>.Self.StartRecruit(originType, protoId, 8000);
            }
        }

        [Command("Pet", "PetSetRecruitState", "设置宠物状态", false)]
        public void PetSetRecruitState(string petTypeStr, int instId, string petStateStr)
        {
            PetType petType;
            PetState state;
            if (Enum.TryParse<PetType>(petTypeStr, out petType) && Enum.TryParse<PetState>(petStateStr, out state))
            {
                Module<PetMgr>.Self.SetRecruitState(petType, instId, state);
            }
        }

        [Command("Pet", "PetSetRecruitDispatch", "设置宠物派遣行为", false)]
        public void PetSetRecruitDispatch(string petTypeStr, int instId, int dispatchId)
        {
            PetType petType;
            if (Enum.TryParse<PetType>(petTypeStr, out petType))
            {
                Module<PetMgr>.Self.SetRecruitDispatch(petType, instId, dispatchId);
            }
        }

        [Command("Pet", "PetStopRecruit", "取消招募宠物", false)]
        public void PetStopRecruit(string petTypeStr, int protoId)
        {
            PetType petType;
            if (Enum.TryParse<PetType>(petTypeStr, out petType))
            {
                Module<PetMgr>.Self.StopRecruit(petType, protoId);
            }
        }

        [Command("Pet", "PetCall", "召唤宠物", false)]
        public void PetCall()
        {
            Module<PetMgr>.Self.Call();
        }

        public PetCmd()
        {
        }
    }
}
