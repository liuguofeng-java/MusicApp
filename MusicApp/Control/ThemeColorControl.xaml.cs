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
            string val = INIFileUtil.readValue("sys", "themeColor");
            if (val != null)
            {
                ((RadioButton)RadioButContrainer.Children[Convert.ToInt32(val) - 1]).IsChecked = true;
            }
            GetDictionary(val);
        }

        public void GetDictionary(string num = null)
        {
            var val = string.Empty;
            switch (num)
            {
                case "1":
                    val  = "/Themes/Color/BlackColor.xaml";
                    break;
                case "2":
                    val = "/Themes/Color/DefaultColor.xaml";
                    break;
                default:
                    val = "/Themes/Color/DefaultColor.xaml";
                    break;
            }
            INIFileUtil.writeValue("sys", "themeColor", num);
            ResourceDictionary dictionary = dictionaryList.FirstOrDefault(d => d.Source.OriginalString.Equals(val));

            Application.Current.Resources.MergedDictionaries.Add(dictionary);
        }



    }
}
