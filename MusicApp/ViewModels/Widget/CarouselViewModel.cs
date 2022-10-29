using MusicApp.Common;
using MusicApp.Control;
using MusicApp.Control.Widget;
using MusicApp.Models;
using MusicApp.Models.Vo;
using MusicApp.Models.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace MusicApp.ViewModels.Widget
{
    public class CarouselViewModel : NotifyBase
    {
        public CarouselModel Model { get; set; }
        public CarouselControl Control { get; set; }
        //点击后退
        public CommandBase BackClickCommand { get; set; }
        //点击前进
        public CommandBase ForwardClickCommand { get; set; }
        //点击中间轮播图时
        public CommandBase CenterMouseDownCommand { get; set; }
        //记录当前索引
        public int[] indexs;
        public CarouselViewModel(CarouselControl control)
        {
            Control = control;
            Model = new CarouselModel();

            //点击后退
            BackClickCommand = new CommandBase();
            BackClickCommand.DoExecute = new Action<object>((o) => 
            {
                if (indexs == null || indexs.Length < 3) return;
                SwitchBanner(indexs[0] - 1, indexs[1] - 1, indexs[2] - 1, false);
            });
            BackClickCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            //点击前进
            ForwardClickCommand = new CommandBase();
            ForwardClickCommand.DoExecute = new Action<object>((o) =>
            {
                if (indexs == null || indexs.Length < 3) return;
                SwitchBanner(indexs[0] + 1, indexs[1] + 1, indexs[2] + 1);
            });
            ForwardClickCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });


            //点击中间轮播图时
            CenterMouseDownCommand = new CommandBase();
            CenterMouseDownCommand.DoExecute = new Action<object>((o) =>
            {
                BitmapImage image = Model.CenterImage;
                if (image == null) return;
                string url = image.UriSource.ToString();
                BannersItem bannersItem = Model.List.Find(t => t.imageUrl.Equals(url));

                switch (bannersItem.targetType)
                {
                    case 1:  //直接播放 --新歌首发--热歌推荐
                        List<string> idList = new List<string>();
                        idList.Add(bannersItem.targetId.ToString());
                        SongPlayListViewModel.This.GetSongPlayList(idList);
                        break;
                    case 10: //打开歌曲列表 --新碟首发
                        break;
                    case 1000: //打开歌曲列表 --歌单
                        break;
                    case 3000: //弹出浏览器 --独家策划--数字专辑
                        Process.Start(new ProcessStartInfo(bannersItem.url)
                        {
                            UseShellExecute = true,
                        });
                        break;
                }
            });
            CenterMouseDownCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });
            
            //初始化
            InitBanner();
        }

        /// <summary>
        /// 初始化轮播图
        /// </summary>
        public void InitBanner()
        {
            //接收数据
            string result = HttpUtil.HttpRequset(HttpUtil.serveUrl + "/banner?type=0");
            if (result == null)
            {
                return;
            }
            BannerResultModel data = JsonConvert.DeserializeObject<BannerResultModel>(result);
            Model.List = data.banners;

            Model.List.ForEach(item =>
            {
                item.image = new BitmapImage(new Uri(item.imageUrl));
            });

            //生成按钮
            for (int i = 0; i < Model.List.Count; i++)
            {
                RadioButton rb = new RadioButton();
                rb.Content = i;
                rb.SetResourceReference(FrameworkElement.StyleProperty, "RadioButStyle");
                Control.ButContrainer.Children.Add(rb);
                rb.MouseEnter += (s, e) =>
                {
                    int index = Convert.ToInt32(rb.Content);
                    SwitchBanner(index -1 < 0 ? Model.List.Count - 1 : index - 1, index, index + 1);
                };
            }
            //初始化图片
            SwitchBanner(Model.List.Count - 1, 0, 1);


            //轮播
            var time = new DispatcherTimer();
            time.Interval = TimeSpan.FromMilliseconds(8000);
            time.Tick += new EventHandler((s, e) =>
            {
                SwitchBanner(indexs[0] + 1, indexs[1] + 1, indexs[2] + 1);
            });
            time.Start();
        }


        /// <summary>
        /// 切换图片
        /// </summary>
        /// <param name="leftIndex">左侧图片下标</param>
        /// <param name="centerIndex">中心图片下标</param>
        /// <param name="rightIndex">右侧图片下标</param>
        /// <param name="isForward">是否前进</param>
        public void SwitchBanner(int leftIndex, int centerIndex, int rightIndex, bool isForward = true)
        {
            if (isForward)
            {
                if (leftIndex > Model.List.Count - 1)
                {
                    leftIndex = 0;
                    centerIndex = 1;
                    rightIndex = 2;
                }
                else if (centerIndex > Model.List.Count - 1)
                {
                    centerIndex = 0;
                    rightIndex = 1;
                }
                else if (rightIndex > Model.List.Count - 1)
                {
                    rightIndex = 0;
                }
            }
            else
            {
                if (leftIndex < 0)
                {
                    leftIndex = Model.List.Count - 1;
                }
                else if (centerIndex < 0)
                {
                    centerIndex = Model.List.Count - 1;
                }
                else if (rightIndex < 0)
                {
                    rightIndex = Model.List.Count - 1;
                }
            }

            ((RadioButton)Control.ButContrainer.Children[centerIndex]).IsChecked = true;
            indexs = new int[] { leftIndex, centerIndex, rightIndex };
            Model.RightImage = Model.List[leftIndex].image;


            Model.CenterImage = Model.List[centerIndex].image;
            Model.CenterText = Model.List[centerIndex].typeTitle;

            //字符串转颜色
            BrushConverter brushConverter = new BrushConverter();
            Model.CenterTextBack = brushConverter.ConvertFromString(Model.List[centerIndex].titleColor).ToString();

            Model.LeftImage = Model.List[rightIndex].image;
        }

    }
}
