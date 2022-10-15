using Hardcodet.Wpf.TaskbarNotification;
using MusicApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
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
                    e.Handled = true; //把 Handled 属性设为true，表示此异常已处理，程序可以继续运行，不会强制退出      
                    MessageBox.Show("捕获未处理异常:" + e.Exception.Message);
                }
                catch (Exception ex)
                {
                    //此时程序出现严重异常，将强制结束退出
                    MessageBox.Show("程序发生致命错误，将终止，请联系运营商！");
                }
            });
            //Task线程内未捕获异常处理事件
            TaskScheduler.UnobservedTaskException += (s ,e) =>
            {
            };

            //非UI线程未捕获异常处理事件
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler((s, e) =>
            {

            });
        }
    }
}
