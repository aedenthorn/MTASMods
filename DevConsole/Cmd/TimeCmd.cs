using System;
using Pathea;
using Pathea.FrameworkNs;
using Pathea.SleepNs;
using Pathea.TimeNs;
using UnityEngine;

public class TimeCmd : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F10))
        {
            Module<TimeModule>.Self.JumpTime(new GameTimeSpan(1, 0, 0));
            return;
        }
        if (Input.GetKeyDown(KeyCode.F12))
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
