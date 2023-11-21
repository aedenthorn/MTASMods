using System;
using Commonder;
using Pathea.FrameworkNs;
using Pathea.MessageNs;
using UnityEngine;

namespace DevConsole
{
    public class MessageCmd : MonoBehaviour, ICmd
    {
        public void Awake()
        {
            Cmd.Instance.Register(this);
        }

        public void OnDestroy()
        {
            Cmd.Instance.ClearAll();
        }

        [Command("Message", "Message", "发送指定事件", false)]
        public void MessageInstID(string msgName, int id)
        {
            Module<MessageModule>.Self.Broadcast<int>(msgName, id);
        }

        public MessageCmd()
        {
        }
    }
}
