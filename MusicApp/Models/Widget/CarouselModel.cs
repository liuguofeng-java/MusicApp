using MusicApp.Common;
using MusicApp.Models.Vo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace MusicApp.Models.Widget
{
    public class CarouselModel : NotifyBase
    {
        //数据源
        private List<BannersItem> _list;
        public List<BannersItem> List
        {
            get { return _list; }
            set
            {
                _list = value;
                DoNotify();
            }
        }

        //中心图片对象
        private BitmapImage _centerImage;
        public BitmapImage CenterImage
        {
            get { return _centerImage; }
            set
            {
                _centerImage = value;
                DoNotify();
            }
        }

        //左边图片对象
        private BitmapImage _leftImage;
        public BitmapImage LeftImage
        {
            get { return _leftImage; }
            set
            {
                _leftImage = value;
                DoNotify();
            }
        }

        //右边图片对象
        private BitmapImage _rightImage;
        public BitmapImage RightImage
        {
            get { return _rightImage; }
            set
            {
                _rightImage = value;
                DoNotify();
            }
        }

        //中心文字
        private string _centerText;
        public string CenterText
        {
            get { return _centerText; }
            set
            {
                _centerText = value;
                DoNotify();
            }
        }

        //中心文字背景
        private string _centerTextBack;
        public string CenterTextBack
        {
            get { return _centerTextBack; }
            set
            {
                _centerTextBack = value;
                DoNotify();
            }
        }

    }
}
