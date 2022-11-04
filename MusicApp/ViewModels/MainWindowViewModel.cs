using MusicApp.Common;
using MusicApp.Models;
using MusicApp.Models.Vo;
using MusicApp.Views;
using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MusicApp.ViewModels
{
    /// <summary>
    /// 主窗体
    /// </summary>
    public class MainWindowViewModel
    {
        private static MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
        public static MainWindowViewModel GetInstance()
        {
            return mainWindowViewModel;
        }
        //事件委托
        public Action<object> BaseBorderMouseDownDelegate { get; set; }

        public MainWindowModel Model { get; set; }

        public MainWindow MainWindow { get; set; }

        //缩小
        public CommandBase ZoomOutWindowButCommand { get; set; }
        //最大化
        public CommandBase ZoomWindowButCommand { get; set; }
        //关闭
        public CommandBase CloseWindowButCommand { get; set; }
        //点击图标
        public CommandBase LogoClickButCommand { get; set; }
        //点击最底层border
        public CommandBase BaseBorderMouseDownCommand { get; set; }
        //Frme内容改变
        public CommandBase FrmeContentRenderedCommand { get; set; }
        //Frme加载
        public CommandBase FrmePageLoadedCommand { get; set; }
        //点击带播放列表
        public CommandBase SongPlayListClickCommand { get; set; }
        //点击播放或暂停
        public CommandBase PlayClickCommand { get; set; }
        //上一首
        public CommandBase LastClickCommand { get; set; }
        //下一首
        public CommandBase NextClickCommand { get; set; }
        private MainWindowViewModel()
        {
            MainWindow = MainWindow.mainWindow;
            Model = new MainWindowModel();
            //初始化第一页
            Model.MenusChecked = MenusChecked.FoundMusicPage;

            //缩小
            ZoomOutWindowButCommand = new CommandBase();
            ZoomOutWindowButCommand.DoExecute = new Action<object>((o) =>
            {
                MainWindow.WindowState = WindowState.Minimized;
            });
            ZoomOutWindowButCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            //最大化
            ZoomWindowButCommand = new CommandBase();
            ZoomWindowButCommand.DoExecute = new Action<object>((o) => ZoomWindow());
            ZoomWindowButCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            //关闭应用
            CloseWindowButCommand = new CommandBase();
            CloseWindowButCommand.DoExecute = new Action<object>((o) =>
            {
                MainWindow.Visibility = Visibility.Collapsed;
            });
            CloseWindowButCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            //点击图标时
            LogoClickButCommand = new CommandBase();
            LogoClickButCommand.DoExecute = new Action<object>((o) =>
            {
                MainWindowViewModel.GetInstance().Model.MenusChecked = MenusChecked.FoundMusicPage;
            });
            LogoClickButCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            //点击最底层border
            BaseBorderMouseDownCommand = new CommandBase();
            BaseBorderMouseDownCommand.DoExecute = new Action<object>((o) => 
            {
                BaseBorderMouseDownDelegate.Invoke(o);
            });
            BaseBorderMouseDownCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            //Frme内容改变
            FrmeContentRenderedCommand = new CommandBase();
            FrmeContentRenderedCommand.DoExecute = new Action<object>((o) =>
            {
                var package = o.ToString();
                Model.MenusChecked = (MenusChecked)Enum.Parse(typeof(MenusChecked), package.Substring(package.LastIndexOf(".") + 1));
            });
            FrmeContentRenderedCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            //播放
            PlayClickCommand = new CommandBase();
            PlayClickCommand.DoExecute = new Action<object>((o) => PlayerViewModel.This.PlayButClick());
            PlayClickCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            //上一首
            LastClickCommand = new CommandBase();
            LastClickCommand.DoExecute = new Action<object>((o) => PlayerViewModel.This.PlayLastClick());
            LastClickCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            //下一首
            NextClickCommand = new CommandBase();
            NextClickCommand.DoExecute = new Action<object>((o) => PlayerViewModel.This.PlayNextClick());
            NextClickCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            //点击带播放列表
            SongPlayListClickCommand = new CommandBase();
            SongPlayListClickCommand.DoExecute = new Action<object>((o) =>
            {
                var songPlayListView = SongPlayListViewModel.This;
                if (songPlayListView.Model.PlayListVisibility == Visibility.Visible)
                {
                    songPlayListView.Model.PlayListVisibility = Visibility.Collapsed;
                }
                else
                {
                    songPlayListView.Model.PlayListVisibility = Visibility.Visible;
                }
            });
            SongPlayListClickCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });


           
            //游客登录
            //Anonimous();
        }

        /// <summary>
        /// 最大化\还原
        /// </summary>
        public void ZoomWindow()
        {
            //判断是否以及最大化，最大化就还原窗口，否则最大化
            if (MainWindow.WindowState == WindowState.Maximized)
            {
                MainWindow.WindowState = WindowState.Normal;
                Model.ZoomWindowButContent = "\xe65d";
            }
            else
            {
                MainWindow.WindowState = WindowState.Maximized;
                Model.ZoomWindowButContent = "\xe653";
            }
        }

        /// <summary>
        /// 游客登录
        /// </summary>
        public void Anonimous()
        {
            if (InitJsonData.jsonDataModel.AnonCookie != null) return;
            string anonimousRes = HttpUtil.HttpRequset(HttpUtil.serveUrl + "/register/anonimous");
            if (anonimousRes == null)
                return;
            AnonimousRedultModel anonRedultModel = JsonConvert.DeserializeObject<AnonimousRedultModel>(anonimousRes);
            if(anonRedultModel.code == 200)
            {

                InitJsonData.jsonDataModel.AnonCookie = anonRedultModel;
            }
        }


        /// <summary>
        /// 设置任务栏
        /// </summary>
        /// <param name="title">名称</param>
        /// <param name="isPlay">是否播放</param>
        /// <param name="overlayImage">任务栏缩略图</param>
        public void SetTaskbarStat(string title, bool isPlay, BitmapImage overlayImage)
        {
            Model.TitleName = title;
            Model.OverlayImage = overlayImage;
            if (isPlay)
            {
                Model.PlayButImage = "/Assets/Images/TaskbarItemInfo/start.png";
            }
            else
            {
                Model.PlayButImage = "/Assets/Images/TaskbarItemInfo/stop.png";
            }
        }
    }
}
