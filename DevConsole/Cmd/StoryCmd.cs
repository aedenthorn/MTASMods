using System;
using Commonder;
using Pathea.FrameworkNs;
using Pathea.StoryScript;
using UnityEngine;

namespace DevConsole
{
    public class StoryCmd : MonoBehaviour, ICmd
    {
        public void Start()
        {
            Cmd.Instance.Register(this);
        }

        public void OnDestroy()
        {
            Cmd.Instance.ClearAll();
        }

        [Command("StoryScript", "StoryVarShowDebug", "显示剧情脚本变量", false)]
        public void StoryVarShowDebug(bool isdebug)
        {
            if (isdebug && debug == null)
            {
                debug = new GameObject("CCVarDebug").AddComponent<CCVariableDebug>();
            }
            if (!isdebug && debug != null)
            {
                UnityEngine.Object.Destroy(debug.gameObject);
            }
        }

        [Command("StoryScript", "StoryVarAdd", "添加剧情脚本变量", false)]
        public void StoryVarAdd(string name, string typeStr)
        {
            CCVarType type;
            if (Enum.TryParse<CCVarType>(typeStr, true, out type))
            {
                Module<CCScriptAssistantMgr>.Self.AddVar(name, type);
            }
        }

        [Command("StoryScript", "StoryVarRemove", "删除剧情脚本变量", false)]
        public void StoryVarRemove(string name)
        {
            Module<CCScriptAssistantMgr>.Self.RemoveVar(name);
        }

        [Command("StoryScript", "StoryVarSet", "设置剧情脚本变量", false)]
        public void StoryVarSet(string name, string operaStr, string data)
        {
            CCVarOpera opera;
            if (Enum.TryParse<CCVarOpera>(operaStr, true, out opera))
            {
                Module<CCScriptAssistantMgr>.Self.SetVar(name, opera, data);
            }
        }

        public StoryCmd()
        {
        }

        public CCVariableDebug debug;
    }
}
