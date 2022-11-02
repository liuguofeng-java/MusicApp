using MusicApp.Models;
using System;
using System.IO;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MusicApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //获取缓存
            InitJsonData.GetJsonFile();
            InitJsonData.isInit = true;

            this.App_Startup();
        }


        void App_Startup()
        {
            //UI线程未捕获异常处理事件
            this.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler((s ,e) =>
            {
                try
                {
                    e.Handled = true;      
                    MessageBox.Show("程序出现异常!");
                    var datetime = DateTime.Now;
                    var iniPath = @"./error/"+ datetime.ToString("yyyy-MM-dd") + "error.txt";

                    var errorStr = datetime.ToString("yyyy-MM-dd HH:mm:ss") + ": " + e.Exception + "\n";
                    
                    //不存在就创建
                    if (!Directory.Exists(Path.GetDirectoryName(iniPath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(iniPath));
                        if (!File.Exists(iniPath))
                        {
                            File.Create(iniPath).Close();
                        }
                    }
                    using (FileStream fs = new FileStream(iniPath, FileMode.Append))
                    {
                        using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                        {
                            sw.WriteLine(errorStr);
                        }
                    }
                }
                catch (Exception ex)
                {
                    //此时程序出现严重异常，将强制结束退出
                    MessageBox.Show("程序发生致命错误，将终止，请联系运营商！");
                }
            });
        }
    }
}
