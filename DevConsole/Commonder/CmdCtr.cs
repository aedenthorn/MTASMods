using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using DevConsole;
using Pathea;
using Pathea.FrameworkNs;
using Pathea.UISystemV2.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Commonder
{
    public class CmdCtr : MonoBehaviour
    {
        public static CmdCtr Instance { get; set; }

        public void Start()
        {
            CmdCtr.Instance = this;
            Cmd.Instance.AddCallBack(new Action<string, Cmd.CallBackType>(ShowLabel));
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            graphicRaycaster.enabled = false;
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
            if (historyChanged)
            {
                if (canvasGroup.interactable)
                {
                    text.text = stringBuilder.ToString();
                }
                historyChanged = false;
            }
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                Commit(inputField.text);
            }
            if (Input.anyKeyDown)
            {

                bool flag = false;
                foreach (KeyCode keyCode in toggleHotKey)
                {
                    if (keyCode == toggleHotKey[0])
                    {
                        flag = Input.GetKey(keyCode);
                    }
                    else
                    {
                        flag = (flag && Input.GetKey(keyCode));
                    }
                }
                if (!activeCmder && flag)
                {
                    BepInExPlugin.Dbgl("activating");

                    Module<InputModule>.Self.Mgr.AddDisableInputIfNotExist(this);
                    UISystemMgr.SetInputModuleEnable(true);
                    UISystemMgr.SetRaycasterEnable(true);
                    activeCmder = true;
                    inputField.interactable = true;
                    inputField.Select();
                    inputField.text = string.Empty;
                    text.text = stringBuilder.ToString();
                    MouseStateAccessor instance = Singleton<MouseStateAccessor>.Instance;
                    if (instance != null)
                    {
                        instance.AddShow(this);
                    }

                }
                else if (activeCmder && flag)
                {
                    BepInExPlugin.Dbgl("deactivating");
                    Input.imeCompositionMode = IMECompositionMode.Auto;
                    Module<InputModule>.Self.Mgr.RemoveDisableInput(this);
                    activeCmder = false;
                    inputField.interactable = false;
                    MouseStateAccessor instance2 = Singleton<MouseStateAccessor>.Instance;
                    if (instance2 != null)
                    {
                        instance2.RemoveShow(this);
                    }
                }
                canvasGroup.interactable = activeCmder;
                canvasGroup.alpha = (float)(activeCmder ? 1 : 0);
                graphicRaycaster.enabled = activeCmder;
                if (activeCmder)
                {
                    inputField.Select();
                    inputField.ActivateInputField();
                }
            }
            if (lastCheckString != inputField.text || lastSelectingPastCmd != selector.selectingPastCmd)
            {
                lastCheckString = inputField.text;
                lastSelectingPastCmd = selector.selectingPastCmd;
                FreshTips(inputField.text);
            }
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
                inputField.text = string.Empty;
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
            commandHistories.Add(str);
            if (commandHistories.Count >= 100)
            {
                commandHistories.RemoveRange(0, 10);
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
            historyChanged = true;
        }

        public void FreshTips(string newStr)
        {
            Dictionary<int, MethodTarget>.ValueCollection values = Cmd.Instance.Dics.Values;

            string[] array = newStr.Split(' ');
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

        public CmdCtr()
        {
        }


        public List<string> commandHistories = new List<string>();

        [SerializeField]
        public KeyCode[] toggleHotKey = { KeyCode.F1 };

        [SerializeField]
        public CanvasGroup canvasGroup;

        [SerializeField]
        public GraphicRaycaster graphicRaycaster;

        [SerializeField]
        public bool activeCmder;

        [SerializeField]
        public TMP_InputField inputField;

        [SerializeField]
        public SelectorCtr selector;

        [SerializeField]
        public TextMeshProUGUI text;

        public static bool clone;

        public string lastCheckString;

        public bool lastSelectingPastCmd = true;

        public bool historyChanged;

        public StringBuilder stringBuilder = new StringBuilder();
    }
}
