using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PEFont
{
    partial class Class1
    {

        ToolStripMenuItem menuitem1;//一级菜单
        ToolStripMenuItem menuitem1_1;//下拉菜单项1
        ToolStripMenuItem menuitem1_2;//下拉菜单项2
        ToolStripMenuItem menuitem1_3;//下拉菜单项3[是否随pe启动]
        ToolStripMenuItem menuitem1_4;//下拉菜单项4

        private string str_bootUpYes = "是否随pe启动自动修改字体【是】";
        private string str_bootUpNo = "是否随pe启动自动修改字体【否】";

        /// <summary>
        /// 从一个窗口中获取它的菜单控件对象
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        private MenuStrip searchMenu(Form form)
        {
            foreach (System.Windows.Forms.Control control in form.Controls)
            {
                if (control is MenuStrip)
                {
                    return (MenuStrip)control;
                }
            }
            return null;
        }

        /// <summary>
        /// 动态创建新菜单
        /// </summary>
        private void addNewMenus(Form form)
        {

            MenuStrip menu = searchMenu(form);
            if (menu == null)
            {
                return;
            }

            //初始化菜单项
            menuitem1 = new ToolStripMenuItem();//一级菜单
            menuitem1_1 = new ToolStripMenuItem();//下拉菜单项1
            menuitem1_2 = new ToolStripMenuItem();//下拉菜单项2
            menuitem1_3 = new ToolStripMenuItem();//下拉菜单项3
            menuitem1_4 = new ToolStripMenuItem();//下拉菜单项4

            //pmxeditor原本的窗口添加这个菜单
            menu.Items.AddRange(new ToolStripItem[] { menuitem1 });

            // 
            // 一级菜单
            // 
            menuitem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { menuitem1_1, menuitem1_2, menuitem1_3, menuitem1_4 });
            menuitem1.Name = "Font_menu_fontchange";
            menuitem1.Size = new System.Drawing.Size(68, 21);
            menuitem1.Text = "[插件]修改字体";
            menuitem1.BackColor = System.Drawing.Color.Maroon;
            menuitem1.ForeColor = System.Drawing.Color.Coral;
            menuitem1.Image = Resource1.face;
            // 
            // 下拉菜单项1
            // 
            menuitem1_1.Name = "Font_menu_fontchange_2";
            menuitem1_1.Size = new System.Drawing.Size(152, 22);
            menuitem1_1.Text = "修改字体";
            menuitem1_1.BackColor = System.Drawing.Color.Maroon;
            menuitem1_1.ForeColor = System.Drawing.Color.Coral;
            menuitem1_1.Click += menuitem1_1Click;
            // 
            // 下拉菜单项2
            // 
            menuitem1_2.Name = "Font_menu_tutorial";
            menuitem1_2.Size = new System.Drawing.Size(152, 22);
            menuitem1_2.Text = "查看教程在bilibili";
            menuitem1_2.BackColor = System.Drawing.Color.Maroon;
            menuitem1_2.ForeColor = System.Drawing.Color.Coral;
            // 
            // 下拉菜单项3
            // 
            menuitem1_3.Name = "Font_menu_boolup";
            menuitem1_3.Size = new System.Drawing.Size(152, 22);
            menuitem1_3.Text = "是否随pe启动自动修改字体【是】";
            menuitem1_3.BackColor = System.Drawing.Color.Maroon;
            menuitem1_3.ForeColor = System.Drawing.Color.Coral;
            menuitem1_3.Click += menuitem1_3Click;
            // 
            // 下拉菜单项4
            // 
            menuitem1_4.Name = "Font_menu_ultraman";
            menuitem1_4.Size = new System.Drawing.Size(152, 22);
            menuitem1_4.Text = "顺便去看超人^_^";
            menuitem1_4.BackColor = System.Drawing.Color.Maroon;
            menuitem1_4.ForeColor = System.Drawing.Color.Coral;
            menuitem1_4.Click += menuitem1_4_Click;

        }


        /// <summary>
        /// 遍历一个窗口的控件，然后改变它的字体为提供的字体
        /// </summary>
        /// <param name="form"></param>
        /// <param name="font"></param>
        private void changeFont(System.Windows.Forms.Form form, System.Drawing.Font font)
        {
            foreach (System.Windows.Forms.Control control in form.Controls)
            {
                control.Font = font;

                control.Width = (int)(control.Width * 1.3);
                control.Height = (int)(control.Height * 1.3);
                //为了不使控件之间覆盖 位置也要按比例变化
                control.Left = (int)(control.Left * 1.3);
                control.Top = (int)(control.Top * 1.3);
            }
        }

        /// <summary>
        /// 遍历所有的form，然后改变它的字体
        /// </summary>
        /// <param name="font"></param>
        private void changeAllFormsFont(Font font)
        {
            #region 方法1
            //获得pmx editor的窗口
            //Form frm = peArgs.Host.Connector.Form as System.Windows.Forms.Form;
            //Form pmxView = peArgs.Host.Connector.View.PmxView as System.Windows.Forms.Form;
            //Form transformView = peArgs.Host.Connector.View.TransformView as System.Windows.Forms.Form;
            //changeFont(frm, font);
            //changeFont(pmxView, font);
            //changeFont(transformView, font);
            #endregion
            //主窗口改字体
            Form frm = peArgs.Host.Connector.Form as System.Windows.Forms.Form;
            changeFont(frm, font);

            //获得view这个接口下所有实例，然后如果它们是窗口就去汉化它
            //先获取view的type
            Type t = peArgs.Host.Connector.View.GetType();

            foreach (System.Reflection.PropertyInfo pi in t.GetProperties())
            {
                //用pi.GetValue获得对象，传入view表示获得这个view的对象
                object value1 = pi.GetValue(peArgs.Host.Connector.View, null);

                //获得属性的名字,后面就可以根据名字判断来进行些自己想要的操作
                string name = pi.Name;

                Console.WriteLine("b.type==" + value1.GetType());
                //获得属性的类型,判断是否form的子类
                if (value1 is Form)//value1.GetType().IsSubclassOf(typeof(Form)) 
                {
                    //进行你想要的操作
                    //Console.WriteLine("b.name1==" + name);
                    changeFont((Form)value1, font);
                }
            }

        }

        /// <summary>
        /// 改变pmx editor的多个窗口的字体
        /// <param name="defaultFont">对话框默认选择字体</param>
        /// </summary>
        private void selectChangeFont(Font defaultFont)
        {
            //主窗口
            Form mainForm = peArgs.Host.Connector.Form as System.Windows.Forms.Form;

            using (System.Windows.Forms.FontDialog dlg = new System.Windows.Forms.FontDialog())//新建字体对话框
            {
                dlg.Font = defaultFont;


                if (dlg.ShowDialog(mainForm) == System.Windows.Forms.DialogResult.OK)//确定按钮被点击
                {
                    //Console.WriteLine("asd");

                    //设置为选中的字体
                    changeAllFormsFont(dlg.Font);

                    //保存设置到文件
                    settings.font = dlg.Font;
                    Utils.saveSettings(fontxml, settings);
                    #region 反射的方法获取控件
                    //var ppts = pmxView.GetType().GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);
                    //foreach (var a in ppts)
                    //{
                    //    if (a.FieldType.IsSubclassOf(typeof(System.Windows.Forms.Form)))
                    //    {
                    //        System.Windows.Forms.Form f = (System.Windows.Forms.Form)a.GetValue(view);
                    //        if (f != null)
                    //        {
                    //            f.Font = dlg.Font;
                    //        }
                    //    }
                    //}
                    #endregion

                }
            }
        }

        /// <summary>
        /// 修改字体菜单被点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuitem1_1Click(object sender, EventArgs e)
        {
            selectChangeFont(settings.font);
        }

        private void menuitem1_3Click(object sender, EventArgs e)
        {
            if (menuitem1_3.Text == str_bootUpYes)
            {
                menuitem1_3.Text = str_bootUpNo;
                //保存设置到文件
                settings.bootup = false;
                Utils.saveSettings(fontxml, settings);
            }
            else
            {
                menuitem1_3.Text = str_bootUpYes;
                //保存设置到文件
                settings.bootup = true;
                Utils.saveSettings(fontxml, settings);
            }

        }

        void menuitem1_4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.bilibili.com/video/av4347649/");
        }

    }
}
