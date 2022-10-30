using MusicApp.Common;
using MusicApp.Models;
using MusicApp.Models.Vo;
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
        //事件委托
        public Action<object> BaseBorderMouseDownDelegate { get; set; }
        public static MainWindowViewModel This { get; set; }
        public MainWindowModel Model { get; set; }

        //点击最底层border
        public CommandBase BaseBorderMouseDownCommand { get; set; }
        //Frme内容改变
        public CommandBase FrmeContentRenderedCommand { get; set; }
        //点击带播放列表
        public CommandBase SongPlayListClickCommand { get; set; }
        //点击播放或暂停
        public CommandBase PlayClickCommand { get; set; }
        //上一首
        public CommandBase LastClickCommand { get; set; }
        //下一首
        public CommandBase NextClickCommand { get; set; }
        public MainWindowViewModel()
        {
            This = this;
            Model = new MainWindowModel();
            //初始化第一页
            Model.MenusChecked = MenusChecked.FoundMusicPage;

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
