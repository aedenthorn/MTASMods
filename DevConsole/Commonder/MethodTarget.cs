using System;
using System.Reflection;

namespace Commonder
{
    public class MethodTarget
    {
        public MethodTarget(MethodInfo methodInfo, object obj, CommandAttribute attr)
        {
            this.methodInfo = methodInfo;
            this.obj = obj;
            this.attr = attr;
            ParameterInfo[] parameters = methodInfo.GetParameters();
            parms = "( )";
            for (int i = parameters.Length - 1; i >= 0; i--)
            {
                string text = parms;
                int startIndex = 2;
                Type parameterType = parameters[i].ParameterType;
                parms = text.Insert(startIndex, ((parameterType != null) ? parameterType.ToString() : null) + " , ");
            }
            parms = parms.Replace(", )", ")");
            if (parameters.Length == 0)
            {
                parms = "( void )";
            }
        }

        public object Invoke(object[] parameters)
        {
            return methodInfo.Invoke(obj, parameters);
        }

        public MethodInfo methodInfo;

        public object obj;

        public CommandAttribute attr;

        public string parms;
    }
}
