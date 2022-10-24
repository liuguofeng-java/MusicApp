using MusicApp.Common;
using MusicApp.Control;
using MusicApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MusicApp.ViewModels
{
    /// <summary>
    /// 点击'皮肤'弹出选择主题弹框的ViewModel
    /// </summary>
    public class ThemeColorControlViewModel
    {
        public ThemeColorModel Model { get; set; }

        public CommandBase SelectButCommand { get; set; }
        public ThemeColorControlViewModel()
        {
            Model = new ThemeColorModel();

            //选择主题
            SelectButCommand = new CommandBase();
            SelectButCommand.DoExecute = new Action<object>((o) =>
            {
                GetDictionary(o.ToString());
            });
            SelectButCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            InitResource();
        }

        /// <summary>
        /// 初始化资源
        /// </summary>
        public void InitResource()
        {
            //初始化所有资源
            Model.DictionaryList = new List<ResourceDictionary>();
            foreach (ResourceDictionary dictionary in Application.Current.Resources.MergedDictionaries)
            {
                Model.DictionaryList.Add(dictionary);
            }
            //获取缓存
            var themeColor = InitJsonData.jsonDataModel.ThemeColor;
            if (themeColor == null)
            {
                themeColor = ThemeSelect.DefaultColor.ToString();
            }
            //初始化主题
            GetDictionary(themeColor);
            Model.ThemeSelect = (ThemeSelect)Enum.Parse(typeof(ThemeSelect), themeColor);

        }

        /// <summary>
        /// 选择主题
        /// </summary>
        /// <param name="num"></param>
        public void GetDictionary(string themeColor = "DefaultColor")
        {
            var val = string.Empty;
            switch (themeColor)
            {
                case "BlackColor":
                    val = "/Themes/Color/BlackColor.xaml";
                    break;
                case "DefaultColor":
                    val = "/Themes/Color/DefaultColor.xaml";
                    break;
            }
            InitJsonData.jsonDataModel.ThemeColor = themeColor;
            ResourceDictionary dictionary = Model.DictionaryList.FirstOrDefault(d => d.Source.OriginalString.Equals(val));
            Application.Current.Resources.MergedDictionaries.Add(dictionary);
        }
    }
}
