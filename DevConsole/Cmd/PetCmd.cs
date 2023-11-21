using System;
using System.Collections.Generic;
using Commonder;
using HarmonyLib;
using Pathea;
using Pathea.FrameworkNs;
using Pathea.InfoTip;
using Pathea.PetNs;
using UnityEngine;

namespace DevConsole
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

        [Command("Npc", "PetRecruitForce", "Recruit Npc pet by ID", false)]
        public void RecruitPet(int id)
        {
            var origins = (List<PetOrigin>)AccessTools.Field(typeof(PetMgr), "origins").GetValue(Module<PetMgr>.Self);
            var pets = (List<Pet>)AccessTools.Field(typeof(PetMgr), "pets").GetValue(Module<PetMgr>.Self);
            var datas = (List<PetData>)AccessTools.Field(typeof(PetMgr), "datas").GetValue(Module<PetMgr>.Self);
            var uiDatas = (List<PetUIData>)AccessTools.Field(typeof(PetMgr), "uiDatas").GetValue(Module<PetMgr>.Self);
            var asset = (PetAsset)AccessTools.Field(typeof(PetMgr), "asset").GetValue(Module<PetMgr>.Self);
            foreach (var o in origins)
            {
                if (o.OriginType == PetOriginType.Npc && o.protoId == id)
                {
                    Pet pet = o.StartRecruit(8000, Module<PetMgr>.Self);
                    if (pet != null && !Module<PetMgr>.Self.HasPet(pet.PetType, pet.instId))
                    {
                        pets.Add(pet);
                        datas.Add(pet.Data);
                        uiDatas.Add(pet.UIData);
                        pet.ReAllocatingHouse();
                        pet.OnRecruitEnter(true);
                        o.OnRecruitEnter(pet.instId, true);
                        string iconPath = (pet != null) ? pet.iconPath1 : "";
                        string info = TextMgr.GetStr(asset.tipIdRecruit);
                        Module<InfoTipMgr>.Self.SendCustomImageTip(info, iconPath, OffsetMode.Head, TipCustomBg.None, "");
                        return;
                    }
                }
            }
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
