using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Commonder
{
    public class CommondInputCtr : InputField
    {
        public override void OnUpdateSelected(BaseEventData eventData)
        {
            if (!base.isFocused)
            {
                return;
            }
            bool flag = false;
            Event @event = new Event();
            while (Event.PopEvent(@event))
            {
                if (@event.rawType == EventType.KeyDown)
                {
                    flag = true;
                    KeyCode keyCode = @event.keyCode;
                    if (keyCode - KeyCode.UpArrow > 1)
                    {
                        InputField.EditState editState = base.KeyPressed(@event);
                        if (@event.keyCode == KeyCode.Escape)
                        {
                            editState = InputField.EditState.Continue;
                        }
                        if (editState == InputField.EditState.Finish)
                        {
                            base.onEndEdit.Invoke(base.text);
                            Select();
                            break;
                        }
                    }
                }
            }
            if (flag)
            {
                base.UpdateLabel();
            }
            eventData.Use();
        }

        public CommondInputCtr()
        {
        }

        [SerializeField]
        public SelectorCtr select;
    }
}
