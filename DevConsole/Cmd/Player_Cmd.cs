using System;
using Pathea.CameraNs;
using Pathea.FrameworkNs;
using UnityEngine;

namespace Pathea.ActorNs
{
    public class Player_Cmd : MonoBehaviour
    {
        public void Update()
        {
            if (null == playerCC)
            {
                if (Module<Player>.Self == null || Module<Player>.Self.actor == null)
                {
                    return;
                }
                Transform actorTransform = Module<Player>.Self.actor.GetActorTransform();
                if (null == actorTransform)
                {
                    return;
                }
                playerCC = actorTransform.GetComponentInChildren<CharacterController>();
                if (null == playerCC)
                {
                    return;
                }
            }
            if (null == cameraTrans)
            {
                if (Module<CameraModule>.Self == null)
                {
                    return;
                }
                cameraTrans = Module<CameraModule>.Self.cameraTrans;
                if (null == cameraTrans)
                {
                    return;
                }
            }
            if (Input.GetKey(KeyCode.LeftControl))
            {
                float z = (Input.GetKey(KeyCode.W) ? 1f : 0f) - (Input.GetKey(KeyCode.S) ? 1f : 0f);
                float x = (Input.GetKey(KeyCode.D) ? 1f : 0f) - (Input.GetKey(KeyCode.A) ? 1f : 0f);
                Vector3 a = Quaternion.Euler(0f, cameraTrans.eulerAngles.y, 0f) * new Vector3(x, 0f, z);
                playerCC.Move(a * moveSpeed * Time.deltaTime);
            }
            if (Input.GetKeyDown(KeyCode.F4))
            {
                Module<Player>.Self.ToggleJetPack(true);
            }
        }

        public Player_Cmd()
        {
        }

        public float moveSpeed = 50f;

        public CharacterController playerCC;

        public Transform cameraTrans;
    }
}
