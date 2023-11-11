using System;
using Pathea.FrameworkNs;
using UnityEngine;

namespace Pathea.ActorNs
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
