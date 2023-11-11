using System;

namespace Commonder
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class CommandAttribute : Attribute
    {
        public string CMD
        {
            get
            {
                return cmd;
            }
        }

        public string Tips
        {
            get
            {
                return tips;
            }
        }

        public bool ShowReturn
        {
            get
            {
                return showReturn;
            }
        }

        public string Page
        {
            get
            {
                return page;
            }
        }

        public CommandAttribute(string page, string cmd, string tips = "", bool showReturn = false)
        {
            this.page = page;
            this.cmd = cmd;
            this.tips = tips;
            this.showReturn = showReturn;
        }

        public string page;

        public string cmd;

        public string tips;

        public bool showReturn;
    }
}
