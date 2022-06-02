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
            input.text = text;
            input.caretPosition = text.Length;
        }
    }
}