using MusicApp.Common;
using MusicApp.Models.Vo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MusicApp.Control
{
    /// <summary>
    /// 发现音乐->个性推荐 的轮播图 的交互逻辑
    /// </summary>
    public partial class CarouselControl : UserControl
    {
        //数据源
        public List<BannersItem> list { get; set; }

        //记录当前索引
        private int[] indexs;

        public CarouselControl()
        {
            InitializeComponent();

            //接收数据
            string result = HttpUtil.HttpRequset(HttpUtil.serveUrl + "/banner?type=0");
            if (result == null)
            {
                return;
            }
            BannerResultModel data = JsonConvert.DeserializeObject<BannerResultModel>(result);
            list = data.banners;


            list.ForEach(item =>
            {
                item.image = new BitmapImage(new Uri(item.imageUrl));
            });

            //初始化
            InitBanner(list);

            //后退
            Back.Click += (s, e) =>
            {
                SwitchBanner(indexs[0] - 1, indexs[1] - 1, indexs[2] - 1, false);
            };

            //前进
            Forward.Click += (s, e) =>
            {
                SwitchBanner(indexs[0] + 1, indexs[1] + 1, indexs[2] + 1);
            };
            
            //点击图片时
            CenterBordrr.MouseDown += (s, e) =>
            {
                BitmapImage image = (BitmapImage)CenterImage.ImageSource;
                string url = image.UriSource.ToString();
                BannersItem bannersItem = list.Find(t => t.imageUrl.Equals(url));

                switch (bannersItem.targetType)
                {
                    case 1:  //直接播放 --新歌首发--热歌推荐
                        List<string> idList = new List<string>();
                        idList.Add(bannersItem.targetId.ToString());
                        ControlBean.getInstance().songPlayListControl.GetSongPlayList(idList);
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
            };
        }

        /// <summary>
        /// 初始化轮播图
        /// </summary>
        /// <param name="list">图片集合</param>
        public void InitBanner(List<BannersItem> list)
        {
            //生成按钮
            list.ForEach(item =>
            {
                RadioButton rb = new RadioButton();
                rb.SetResourceReference(StyleProperty, "RadioButStyle");
                ButContrainer.Children.Add(rb);
            });
            //初始化图片
            SwitchBanner(list.Count - 1, 0, 1);
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
                if (leftIndex > list.Count - 1)
                {
                    leftIndex = 0;
                    centerIndex = 1;
                    rightIndex = 2;
                }
                else if (centerIndex > list.Count - 1)
                {
                    centerIndex = 0;
                    rightIndex = 1;
                }
                else if (rightIndex > list.Count - 1)
                {
                    rightIndex = 0;
                }
            }
            else
            {
                if (leftIndex < 0)
                {
                    leftIndex = list.Count - 1;
                }
                else if (centerIndex < 0)
                {
                    centerIndex = list.Count - 1;
                }
                else if (rightIndex < 0)
                {
                    rightIndex = list.Count - 1;
                }
            }

            ((RadioButton)ButContrainer.Children[centerIndex]).IsChecked = true;
            indexs = new int[]{ leftIndex, centerIndex, rightIndex };
            LeftImage.ImageSource = list[leftIndex].image;


            CenterImage.ImageSource = list[centerIndex].image;
            CenterText.Text = list[centerIndex].typeTitle;

            BrushConverter brushConverter = new BrushConverter();
            ((Border)CenterText.Parent).Background = (Brush)brushConverter.ConvertFromString(list[centerIndex].titleColor);

            RightImage.ImageSource = list[rightIndex].image;
        }

    }
}
