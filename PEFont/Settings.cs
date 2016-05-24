using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace PEFont
{
    /// <summary>
    /// 记录设置的类
    /// </summary>
    [Serializable]
    public class Settings
    {
        /// <summary>
        /// 是否随pe启动开启功能
        /// </summary>
        public bool bootup
        {
            get;
            set;
        }

        /// <summary>
        /// 上一次保存的字体
        /// </summary>
        public System.Drawing.Font font
        {
            get;
            set;
        }

        public Settings()
        {
            this.bootup = false;
            this.font = new System.Drawing.Font("MS UI Gothic", 9f);
        }

        public Settings(System.Drawing.Font font)
        {
            this.bootup = false;
            this.font = font;
        }
    }
}
