using MusicApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// ThemeColor.xaml 的交互逻辑
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
            GetDictionary();
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
            ResourceDictionary dictionary = dictionaryList.FirstOrDefault(d => d.Source.OriginalString.Equals(val));

            Application.Current.Resources.MergedDictionaries.Add(dictionary);
        }



    }
}
