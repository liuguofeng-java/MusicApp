using MusicApp.Common;
using MusicApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MusicApp.Control
{
    /// <summary>
    /// 点击'皮肤'弹出选择主题弹框,主窗体在头部 的交互逻辑
    /// </summary>
    public partial class ThemeColorControl : UserControl
    {
        private List<ResourceDictionary> dictionaryList;
        private ControlBean bean = ControlBean.getInstance();
        public ThemeColorControl()
        {
            InitializeComponent();

            DataContext = new ThemeColorControlViewModel(this);

            //找到系统所有资源
            dictionaryList = new List<ResourceDictionary>();
            foreach (ResourceDictionary dictionary in Application.Current.Resources.MergedDictionaries)
            {
                dictionaryList.Add(dictionary);
            }
            int? themeColor = bean.jsonData.themeColor;
            if (themeColor != null)
            {
                ((RadioButton)RadioButContrainer.Children[Convert.ToInt32(themeColor) - 1]).IsChecked = true;
            }
            GetDictionary(themeColor);
        }

        public void GetDictionary(int? num = null)
        {
            var val = string.Empty;
            switch (num)
            {
                case 1:
                    val  = "/Themes/Color/BlackColor.xaml";
                    break;
                case 2:
                    val = "/Themes/Color/DefaultColor.xaml";
                    break;
                default:
                    val = "/Themes/Color/DefaultColor.xaml";
                    break;
            }

            bean.jsonData.themeColor = num;
            ResourceDictionary dictionary = dictionaryList.FirstOrDefault(d => d.Source.OriginalString.Equals(val));

            Application.Current.Resources.MergedDictionaries.Add(dictionary);
        }



    }
}
