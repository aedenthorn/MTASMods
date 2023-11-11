using DevConsole;
using Pathea.StoryScript;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Commonder
{
    public class TipsItemCtr : MonoBehaviour
    {
        public void SetData(string input, MethodTarget data)
        {
            string text = data.attr.CMD;
            string tips = data.attr.Tips;
            for(int i = 0; i < DebugToolModule.translatable.Length; i++)
            {
                if (DebugToolModule.translatable[i].Equals(tips))
                {
                    tips = DebugToolModule.translations[i];
                }

            }
            text = text.Replace(input, "<b><color=#C7FFDCFF>" + input + "</color></b>");
            label.text = string.Concat(new string[]
            {
                text,
                " ",
                data.parms.Replace("System.", ""),
                " \t",
                tips
            });
        }

        public TipsItemCtr()
        {
        }

        [SerializeField]
        public Text label;
    }
}
