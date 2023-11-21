using System;
using Commonder;
using HarmonyLib;
using Pathea;
using Pathea.ActorNs;
using Pathea.CameraNs;
using Pathea.CustomPlayer;
using Pathea.FrameworkNs;
using Pathea.MissionNs;
using Pathea.TimeNs;
using Pathea.UISystemV2.UI;
using Pathea.UISystemV2.UIControl;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DevConsole.ActorNs
{
    public class Player_Cmd : MonoBehaviour, ICmd
    {
        public void Start()
        {
            Cmd.Instance.Register(this);
        }

        public void OnDestroy()
        {
            Cmd.Instance.ClearAll();
        }
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
            if (Input.GetKey(BepInExPlugin.zoomKey.Value))
            {
                float z = (Input.GetKey(KeyCode.W) ? 1f : 0f) - (Input.GetKey(KeyCode.S) ? 1f : 0f);
                float x = (Input.GetKey(KeyCode.D) ? 1f : 0f) - (Input.GetKey(KeyCode.A) ? 1f : 0f);
                Vector3 a = Quaternion.Euler(0f, cameraTrans.eulerAngles.y, 0f) * new Vector3(x, 0f, z);
                playerCC.Move(a * moveSpeed * Time.deltaTime);
            }
            if (Input.GetKeyDown(BepInExPlugin.jetPackKey.Value))
            {
                Module<Player>.Self.ToggleJetPack(true);
            }
        }
        [Command("Player", "SetPlayerName", "Set player name", false)]
        public void SetPlayerName(string playerName)
        {
            //Module<CustomPlayerModule>.Self.PlayerData.playerName = name;
            AccessTools.Field(typeof(Player), "playerName").SetValue(Module<Player>.Self, playerName);
            Module<Player>.Self.actor.ActorName = playerName;
        }
        
        [Command("Player", "SetPlayerBirthday", "Set player birthday", false)]
        public void SetPlayerBirthday(int month, int day)
        {
            //Module<CustomPlayerModule>.Self.PlayerData.playerName = name;
            AccessTools.Property(typeof(CustomPlayerModule), nameof(CustomPlayerModule.PlayerBirthDay)).SetValue(Module<CustomPlayerModule>.Self, new GameDateTime(Module<TimeModule>.Self.CurrentTime.Year, month, day));
        }


        public Player_Cmd()
        {
        }

        public float moveSpeed = 50f;

        public CharacterController playerCC;

        public Transform cameraTrans;
    }
}
