using Commonder;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DevConsole
{

    public class ClickAction : MonoBehaviour, IPointerClickHandler
    {
        public string text;
        public TMP_InputField input;
        public void OnPointerClick(PointerEventData eventData)
        {
            SelectorCtr.clicked = true;
            BepInExPlugin.Dbgl($"Clicked on {text}");
            input.text = text;
            input.caretPosition = text.Length;
        }
    }
}