using System;
using Commonder;
using Pathea.FestivalNs;
using Pathea.FrameworkNs;
using Pathea.TimeNs;
using UnityEngine;

namespace Pathea
{
    public class FestivalCmd : MonoBehaviour, ICmd
    {
        public void Awake()
        {
            Cmd.Instance.Register(this);
        }

        public void OnDestroy()
        {
            Cmd.Instance.ClearAll();
        }

        [Command("Festival", "FestivalAddModifier", "添加节日数据修改", false)]
        public void FestivalAddModifier(FestivalEnum festival, FestivalModifierType type, string data)
        {
            Module<FestivalModule>.Self.AddFestivalModifier(festival, type, data);
        }

        [Command("Festival", "FestivalRemoveModifier", "删除节日数据修改", false)]
        public void FestivalRemoveModifier(FestivalEnum festival, FestivalModifierType type)
        {
            Module<FestivalModule>.Self.RemoveFestivalModifier(festival, type);
        }

        [Command("Festival", "FestivalPlayerSignupHidenSeek", "躲猫猫活动玩家报名", false)]
        public void FestivalPlayerSignupHidenSeek()
        {
            Module<FestivalModule>.Self.GetActivity<Activity_HidenSeek>(ActivityEnum.HidenSeek).PlayerSignup();
        }

        [Command("Festival", "FestivalStartSandSkiiing", "开启滑沙节", false)]
        public void FestivalStartSandSkiiing()
        {
            Module<FestivalModule>.Self.GetActivity<Activity_SandSkiing>(ActivityEnum.SandSkiing).SetStartGameTime(Module<TimeModule>.Self.CurrentTime);
            Module<FestivalModule>.Self.GetActivity<Activity_SandSkiing>(ActivityEnum.SandSkiing2).SetStartGameTime(Module<TimeModule>.Self.CurrentTime);
        }

        [Command("Festival", "FestivalStartDancing", "开启跳舞活动", false)]
        public void FestivalStartDancing()
        {
            Module<FestivalModule>.Self.GetActivity<Activity_Dancing>(ActivityEnum.Dancing).SetStartGameTime(Module<TimeModule>.Self.CurrentTime);
            Module<FestivalModule>.Self.GetActivity<Activity_Dancing>(ActivityEnum.Dancing2).SetStartGameTime(Module<TimeModule>.Self.CurrentTime);
        }

        [Command("Festival", "FestivalStartDancingForce", "强制开启跳舞活动", false)]
        public void FestivalStartDancingForce(bool isForceEnable)
        {
            Module<FestivalModule>.Self.GetActivity<Activity_Dancing>(ActivityEnum.Dancing).SetForceEnbale(isForceEnable);
            Module<FestivalModule>.Self.GetActivity<Activity_Dancing>(ActivityEnum.Dancing2).SetForceEnbale(isForceEnable);
        }

        [Command("Festival", "FestivalActive", "开&关节日系统", false)]
        public void FestivalActive(bool isActive)
        {
            if (isActive)
            {
                Module<FestivalModule>.Self.RemoveStopFestival(this);
                return;
            }
            Module<FestivalModule>.Self.AddStopFestival(this);
        }

        [Command("Festival", "FestivalAddStop", "关闭节日系统", false)]
        public void FestivalAddStop(int id)
        {
            Module<FestivalModule>.Self.AddStopFestivalMission(id);
        }

        [Command("Festival", "FestivalRemoveStop", "打开节日系统", false)]
        public void FestivalRemoveStop(int id)
        {
            Module<FestivalModule>.Self.RemoveStopFestivalMission(id);
        }

        public FestivalCmd()
        {
        }
    }
}
