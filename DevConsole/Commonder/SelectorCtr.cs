using DevConsole;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Commonder
{
    public class SelectorCtr : MonoBehaviour
    {
        public int Index
        {
            get
            {
                return index;
            }
        }

        public void LateUpdate()
        {
            if (clicked)
            {
                clicked = false;
                foreach (Image image in itemList)
                {
                    image.color = normalColor;
                }
            }
            if (index != -1)
            {
                prog = 1f - (float)index / (float)count;
                if (prog > 0.8f)
                {
                    prog = 1f;
                }
                else if (prog < 0.2f)
                {
                    prog = 0f;
                }
                scroll.verticalNormalizedPosition = Mathf.Lerp(scroll.verticalNormalizedPosition, prog, Time.deltaTime * 4f);
            }
            if (!Input.anyKey)
            {
                return;
            }
            if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) && index != -1)
            {
                BepInExPlugin.Dbgl($"selecting command {methods[index].attr.CMD}");
                index = Mathf.Clamp(index, 0, methods.Length - 1);
                inputText.text = methods[index].attr.CMD + " ";
                inputText.caretPosition = inputText.text.Length;
                index = -1;
                ResetSelectingPast(true);
            }
            if (string.IsNullOrEmpty(inputText.text) && !selectingPastCmd)
            {
                ResetSelectingPast(true);
            }
            if (selectingPastCmd && Input.anyKeyDown)
            {
                if (!Input.GetKeyDown(KeyCode.Return))
                {
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        if (selectingPastIndex > 1)
                        {
                            selectingPastIndex--;
                            inputText.text = cmdCtr.commandHistories[cmdCtr.commandHistories.Count - selectingPastIndex];
                            inputText.caretPosition = inputText.text.Length;
                            BepInExPlugin.Dbgl($"selecting command {inputText.text}");
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        if (selectingPastIndex < cmdCtr.commandHistories.Count)
                        {
                            selectingPastIndex++;
                            inputText.text = cmdCtr.commandHistories[cmdCtr.commandHistories.Count - selectingPastIndex];
                            inputText.caretPosition = inputText.text.Length;
                            BepInExPlugin.Dbgl($"selecting command {inputText.text}");
                        }
                    }
                    else if (lastFrameText != inputText.text)
                    {
                        ResetSelectingPast(false);
                    }
                }
                lastFrameText = inputText.text;
                return;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (index == -1)
                {
                    index = 0;
                }
                else
                {
                    index++;
                    if (index == itemList.Count)
                    {
                        index = 0;
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (index == -1)
                {
                    index = 0;
                }
                else
                {
                    index--;
                    if (index == -1)
                    {
                        index = itemList.Count - 1;
                    }
                }
            }
            if (index != -1)
            {
                index = Mathf.Clamp(index, 0, itemList.Count - 1);
            }
            if (count == 0)
            {
                index = -1;
            }
            foreach (Image image in itemList)
            {
                image.color = normalColor;
            }
            if (index != -1)
            {
                itemList[index].color = selectColor;
            }
            lastFrameText = inputText.text;
        }

        public void ResetSelectingPast(bool enable)
        {
            selectingPastCmd = enable;
            selectingPastIndex = 0;
        }

        public void FreshList(MethodTarget[] list)
        {
            ClearItems();
            count = 0;
            string[] array = inputText.text.Split(' ');
            if (selectingPastCmd)
            {
                BepInExPlugin.Dbgl($"selecting past cmd");

                return;
            }
            if (string.IsNullOrEmpty(inputText.text))
            {
                BepInExPlugin.Dbgl($"empty input field");
                ResetSelectingPast(true);
                return;
            }
            BepInExPlugin.Dbgl($"making fresh list: {list.Length}");
            count = list.Length;
            foreach (MethodTarget data in list)
            {
                GameObject gameObject = Instantiate<GameObject>(itemPrefab);
                gameObject.transform.SetParent(container);
                gameObject.transform.localScale = Vector3.one;
                gameObject.SetActive(true);
                itemList.Add(gameObject.GetComponent<Image>());
                gameObject.gameObject.name = (itemList.Count - 1).ToString();
                gameObject.GetComponent<TipsItemCtr>().SetData(array[0], data);
                gameObject.transform.Find("Image").GetComponent<Image>().color = normalColor;
                string text = gameObject.GetComponentInChildren<Text>()?.text.Split(' ')[0];
                if (text != null)
                {
                    ClickAction a = gameObject.GetComponent<ClickAction>();
                    if (a is null)
                        a = gameObject.AddComponent<ClickAction>();
                    a.text = data.attr.CMD;
                    a.input = inputText;
                }
            }
            methods = list;

        }

        public void ClearItems()
        {
            itemList.Clear();
            Transform[] componentsInChildren = container.GetComponentsInChildren<Transform>();
            for (int i = componentsInChildren.Length - 1; i >= 0; i--)
            {
                if (componentsInChildren[i] != container)
                {
                    UnityEngine.Object.Destroy(componentsInChildren[i].gameObject);
                }
            }
        }

        public SelectorCtr()
        {
        }

        [SerializeField]
        public GameObject itemPrefab;

        [SerializeField]
        public Transform container;

        [SerializeField]
        public TMP_InputField inputText;

        [SerializeField]
        public int index = -1;

        [SerializeField]
        public int count;

        public List<Image> itemList = new List<Image>();

        [SerializeField]
        public static Color normalColor = Color.black;

        [SerializeField]
        public Color selectColor = new Color(0, 0.7059f, 0.6183f, 1);

        public MethodTarget[] methods;

        [SerializeField]
        public ScrollRect scroll;

        [SerializeField]
        public float prog;

        [SerializeField]
        public CmdCtr cmdCtr;

        public int selectingPastIndex;

        public bool selectingPastCmd;

        public string lastFrameText = "";
        public static bool clicked;
    }
}
