using System;
using Pathea.ActorNs;
using Pathea.FrameworkNs;
using UnityEngine;

namespace DevConsole.ActorNs
{
    public class ActorDebug : MonoBehaviour
    {
        public void Start()
        {
        }

        public void OnDrawGizmos()
        {
            Module<ActorMgr>.Self.OnDrawGizmos();
        }

        public ActorDebug()
        {
        }
    }
}
