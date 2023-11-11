using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using TMPro;
using UnityEngine;

namespace Commonder
{
    public class DebugCmd : MonoBehaviour
    {
        public static DebugCmd Instance { get; set; }
        public void Start()
        {
            DebugCmd.Instance = this;
            Cmd.Instance.AddCallBack(new Action<string, Cmd.CallBackType>(ShowLabel));
        }

        public void HandleLog(string condition, string stackTrace, LogType type)
        {
            Cmd.CallBackType type2 = Cmd.CallBackType.Normal;
            switch (type)
            {
                case LogType.Error:
                    type2 = Cmd.CallBackType.Error;
                    break;
                case LogType.Warning:
                    type2 = Cmd.CallBackType.Warning;
                    break;
                case LogType.Exception:
                    type2 = Cmd.CallBackType.Error;
                    break;
            }
            ShowLabel(condition, type2);
        }

        public void OnDestroy()
        {
        }

        public void LateUpdate()
        {
            if (histroyChanged)
            {
                if (canvasGroup.interactable)
                {
                    text.text = stringBuilder.ToString();
                }
                histroyChanged = false;
            }
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                Commit(inputFiled.text);
            }
            if (lastCheckString != inputFiled.text || lastSelectingPastCmd != selector.selectingPastCmd)
            {
                lastCheckString = inputFiled.text;
                lastSelectingPastCmd = selector.selectingPastCmd;
                FreshTips(inputFiled.text);
            }
        }

        public void CommitCmd()
        {
            Commit(inputFiled.text);
        }

        public void Commit(string str)
        {
            if (string.IsNullOrEmpty(str) || selector.Index >= 0)
            {
                return;
            }
            ShowCommand(str);
            selector.ResetSelectingPast(selector.selectingPastCmd);
            try
            {
                Cmd.Instance.Commit(str);
                inputFiled.text = string.Empty;
            }
            catch (Exception ex)
            {
                ShowLabel(ex.Message, Cmd.CallBackType.Error);
                global::Debug.LogError(ex.ToString());
            }
        }

        public void ShowCommand(string str)
        {
            AddHistory("[Command]" + str);
            commandHistoryes.Add(str);
            if (commandHistoryes.Count >= 100)
            {
                commandHistoryes.RemoveRange(0, 10);
            }
        }

        public void ShowLabel(string str, Cmd.CallBackType type)
        {
            if (type != Cmd.CallBackType.Warning)
            {
                if (type == Cmd.CallBackType.Error)
                {
                    str = "<color=red>" + str + "</color>";
                }
            }
            else
            {
                str = "<color=yellow>" + str + "</color>";
            }
            AddHistory(str);
        }

        public void AddHistory(string history)
        {
            stringBuilder.AppendLine();
            stringBuilder.Append(history);
            if (stringBuilder.Length > 10000)
            {
                stringBuilder.Remove(0, 1000);
            }
            histroyChanged = true;
        }

        public void FreshTips(string newStr)
        {
            string[] array = newStr.Split(' ');
            Dictionary<int, MethodTarget>.ValueCollection values = Cmd.Instance.Dics.Values;
            string value = array[0].ToLower();
            List<MethodTarget> list = new List<MethodTarget>();
            foreach (MethodTarget methodTarget in values)
            {
                if (methodTarget.attr.CMD.ToLower().Contains(value))
                {
                    list.Add(methodTarget);
                }
            }
            string str = "";
            foreach (MethodTarget methodTarget2 in list)
            {
                str = str + "\n" + methodTarget2.attr.CMD;
            }
            selector.FreshList(list.ToArray());
        }

        public DebugCmd()
        {
        }

        public List<string> commandHistoryes = new List<string>();

        [SerializeField]
        public KeyCode[] toggleHotKey;

        [SerializeField]
        public CanvasGroup canvasGroup;

        [SerializeField]
        public bool activeCmder;

        [SerializeField]
        public TMP_InputField inputFiled;

        [SerializeField]
        public SelectorCtr selector;

        [SerializeField]
        public TextMeshProUGUI text;

        public static bool clone;

        public string lastCheckString;

        public bool lastSelectingPastCmd;

        public bool histroyChanged;

        public StringBuilder stringBuilder = new StringBuilder();
    }
}
