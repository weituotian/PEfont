using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace PEFont
{
    public partial class Class1 : PEPlugin.IPEPlugin, PEPlugin.IPEPluginOption
    {

        /// <summary>
        /// 保存pe运行参数
        /// </summary>
        private PEPlugin.IPERunArgs peArgs;

        private string path;
        private string fontxml = "";

        private Settings settings;//当前设置

        /// <summary>
        /// 获取对这个插件的描述
        /// </summary>
        public string Description
        {
            get { return "您是否想改变pmxeditor的字体？"; }
        }

        /// <summary>
        /// 获取这个插件的名称
        /// </summary>
        public string Name
        {
            get { return "pmxeditor字体修改器"; }
        }

        /// <summary>
        /// 获取启动的选项
        /// </summary>
        public PEPlugin.IPEPluginOption Option
        {
            get { return this; }
        }



        /// <summary>
        /// 这个方法在插件启动时被调用。
        /// </summary>
        /// <param name="args"></param>
        public void Run(PEPlugin.IPERunArgs args)
        {
            Console.WriteLine("pe插件开始运行！");

            peArgs = args;//保存这个IPERunArgs


            System.Windows.Forms.Form mainForm = peArgs.Host.Connector.Form as System.Windows.Forms.Form;//主窗口
            addNewMenus(mainForm); //添加菜单

            //文件夹检测，没有该文件夹就创建
            path = new FileInfo(peArgs.Host.Connector.System.HostApplicationPath).DirectoryName + @"\_data\weituotian\";
            DirectoryInfo dir = new DirectoryInfo(path);
            if (!dir.Exists)
            {
                dir.Create();
            }
            //初始化文件路径
            fontxml = path + "font.xml";
            

            //读取保存的字体
            settings = Utils.loadSettings(fontxml);

            if (settings == null)//配置文件读取失败
            {
                //第一次启动
                settings = new Settings(mainForm.Font);//当前字体设置为pe窗口的默认字体
                menuitem1_3.Text = str_bootUpNo;
                //font = new Font("MS UI Gothic", 9f);//当前字体设置为默认字体
                Utils.saveSettings(fontxml, settings);
            }
            else
            {
                if (settings.bootup)//默认启动修改字体
                {
                    menuitem1_3.Text = str_bootUpYes;//设置提示信息
                    changeAllFormsFont(settings.font);
                }
                else
                {
                    menuitem1_3.Text = str_bootUpNo;
                }
            }

        }


        /// <summary>
        /// 获取对这个插件版本的描述
        /// </summary>
        public string Version
        {
            get { return "1.0 by韦驮天"; }
        }

        /// <summary>
        /// 这个插件被销毁时被调用
        /// </summary>
        public void Dispose()
        {

        }

        /// <summary>
        /// 获取一个布尔值，表示插件是否随PE一起启动。取值为true或false
        /// </summary>
        public bool Bootup
        {
            get { return true; }
        }

        /// <summary>
        /// 获取一个布尔值，表示插件是否应该有菜单项。取值为true或false
        /// </summary>
        public bool RegisterMenu
        {
            get { return false; }
        }

        /// <summary>
        /// 获取一个字符串，表示插件菜单项上的文本。
        /// </summary>
        public string RegisterMenuText
        {
            get { return "修改字体 By 韦驮天"; }
        }
    }
}
