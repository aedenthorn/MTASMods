using DevConsole;
using HarmonyLib;
using Pathea.DesignerConfig;
using Pathea.FrameworkNs;
using Pathea.ItemNs;
using Pathea.MonsterNs;
using Pathea.Mtas;
using Pathea.NpcNs;
using Pathea.RideNs;
using Pathea;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UtilNs;
using System.IO;

namespace Commonder
{
    public class Cmd
    {
        public static Cmd Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Cmd();
                    instance.Init();
                }
                return instance;
            }
        }

        public Dictionary<int, MethodTarget> Dics
        {
            get
            {
                return methodDic;
            }
        }

        public void Register(object obj)
        {
            foreach (MethodInfo methodInfo in (from item in obj.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                                               where item.IsDefined(typeof(CommandAttribute), true)
                                               select item).ToArray<MethodInfo>())
            {
                CommandAttribute commandAttribute = methodInfo.GetCustomAttributes(typeof(CommandAttribute), true)[0] as CommandAttribute;
                if (commandAttribute != null)
                {
                    MethodTarget value = new MethodTarget(methodInfo, obj, commandAttribute);
                    if (commandAttribute.CMD.Contains(" "))
                    {
                        Debug.LogError("指令不能存在空格:" + commandAttribute.CMD);
                        CallBack("指令不能存在空格:" + commandAttribute.CMD, CallBackType.Error);
                    }
                    else if (methodDic.ContainsKey(commandAttribute.CMD.GetHashCode()))
                    {
                        string str = "Url 已经存在 ";
                        Type type = obj.GetType();
                        CallBack(str + ((type != null) ? type.ToString() : null) + "." + methodInfo.Name, CallBackType.Warning);
                        methodDic[commandAttribute.CMD.GetHashCode()] = value;
                    }
                    else
                    {
                        methodDic.Add(commandAttribute.CMD.GetHashCode(), value);
                    }
                }
            }
        }

        public void ClearAll()
        {
            Dics.Clear();
            methodDic.Clear();
        }

        public void Init()
        {
            methodDic = new Dictionary<int, MethodTarget>();
        }

        public void Commit(string str)
        {
            var array = str.Split(' ');
            if (!methodDic.ContainsKey(array[0].GetHashCode()))
            {
                BepInExPlugin.Dbgl($"Commit command: {str}, not in dic");
                foreach (var m in methodDic.Values)
                {
                    if (m.attr.CMD.ToLower() == array[0].ToLower() && methodDic.ContainsKey(m.attr.CMD.GetHashCode()))
                    {
                        array[0] = m.attr.CMD;
                        str = string.Join(" ", array);
                        BepInExPlugin.Dbgl($"Found command: {str}");
                        break;
                    }
                }
            }

            if (methodDic.ContainsKey(array[0].GetHashCode()))
            {
                MethodTarget methodTarget = methodDic[array[0].GetHashCode()];
                if (methodTarget == null)
                {
                    return;
                }
                List<object> list = new List<object>();
                ParameterInfo[] parameters = methodTarget.methodInfo.GetParameters();
                if (array.Length - 1 < parameters.Length)
                {
                    CallBack("Need " + parameters.Length.ToString() + " parameters found only " + (array.Length - 1).ToString(), CallBackType.Warning);
                    return;
                }
                for (int i = 0; i < parameters.Length; i++)
                {
                    list.Add(TryValue(array[i + 1], parameters[i].ParameterType));
                }
                object obj = methodTarget.methodInfo.Invoke(methodTarget.obj, list.ToArray());
                if (methodTarget.attr.ShowReturn)
                {
                    CallBack(obj.ToString(), CallBackType.Normal);
                }
            }
        }

        public object TryValue(string data, Type type)
        {
            object result = null;
            if (UtilParse.TryParse(type, data, out result))
            {
                return result;
            }
            return null;
        }

        public void AddCallBack(Action<string, Cmd.CallBackType> callback)
        {
            callBack = (Action<string, Cmd.CallBackType>)Delegate.Combine(callBack, callback);
        }

        public void CallBack(string str, Cmd.CallBackType type)
        {
            if (callBack != null)
            {
                callBack(str, type);
            }
            if (type == CallBackType.Error)
            {
                Debug.LogError(str);
            }
        }

        public void Log(string str, Cmd.CallBackType type = CallBackType.Normal)
        {
            if (callBack != null)
            {
                callBack(str, type);
            }
        }

        public Cmd()
        {
        }

        public static Cmd instance;

        public Action<string, Cmd.CallBackType> callBack;

        public Dictionary<int, MethodTarget> methodDic;

        public enum CallBackType
        {
            Normal,
            Warning,
            Error
        }

    }
}
