using System;
using DevConsole;
using Pathea;
using Pathea.FrameworkNs;
using Pathea.SleepNs;
using Pathea.TimeNs;
using UnityEngine;

namespace Commonder
{
    public class TimeCmd : MonoBehaviour
    {
        public void Update()
        {
            if (Input.GetKeyDown(BepInExPlugin.jumpTimeKey1.Value))
            {
                Module<TimeModule>.Self.JumpTime(new GameTimeSpan(1, 0, 0));
                return;
            }
            if (Input.GetKeyDown(BepInExPlugin.jumpTimeKey2.Value))
            {
                Module<SleepModule>.Self.disableForceSleep = true;
                Module<TimeModule>.Self.JumpTime(new GameTimeSpan(1, 0, 0, 0));
                Module<SleepModule>.Self.disableForceSleep = false;
            }
        }

        public TimeCmd()
        {
        }
    }

}
