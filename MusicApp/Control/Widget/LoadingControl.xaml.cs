using MusicApp.OverwriteControl;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MusicApp.Control.Widget
{
    /// <summary>
    /// LoadingControl.xaml 的交互逻辑
    /// </summary>
    public partial class LoadingControl : UserControl
    {
        public LoadingControl()
        {
            InitializeComponent();
        }

        //是否显示控件
        public static readonly DependencyProperty IsVisibilityProperty =
            DependencyProperty.RegisterAttached("IsVisibility", typeof(Visibility), typeof(LoadingControl), new PropertyMetadata(null));

        public Visibility IsVisibility
        {
            get { return (Visibility)GetValue(IsVisibilityProperty); }
            set { SetValue(IsVisibilityProperty, value); }
        }

        //容器Margin
        public static readonly DependencyProperty ContrainerMarginProperty =
            DependencyProperty.RegisterAttached("ContrainerMargin", typeof(string), typeof(LoadingControl), new PropertyMetadata(null));

        public string ContrainerMargin
        {
            get { return (string)GetValue(ContrainerMarginProperty); }
            set { SetValue(ContrainerMarginProperty, value); }
        }

        //加载中Margin
        public static readonly DependencyProperty LoadMarginProperty =
            DependencyProperty.RegisterAttached("LoadMargin", typeof(string), typeof(LoadingControl), new PropertyMetadata(null));

        public string LoadMargin
        {
            get { return (string)GetValue(LoadMarginProperty); }
            set { SetValue(LoadMarginProperty, value);}
        }
        //是否显示加载中心
        public static readonly DependencyProperty IsLoadVisibilityProperty =
            DependencyProperty.RegisterAttached("IsLoadVisibility", typeof(Visibility), typeof(LoadingControl), new PropertyMetadata(null));

        public Visibility IsLoadVisibility
        {
            get { return (Visibility)GetValue(IsLoadVisibilityProperty); }
            set { SetValue(IsLoadVisibilityProperty, value); }
        }

        //是否显示error信息
        public static readonly DependencyProperty IsErrorVisibilityProperty =
            DependencyProperty.RegisterAttached("IsErrorVisibility", typeof(Visibility), typeof(LoadingControl), new PropertyMetadata(null));

        public Visibility IsErrorVisibility
        {
            get { return (Visibility)GetValue(IsErrorVisibilityProperty); }
            set {
                var res = IsErrorVisibilityProperty;
                if (res.Equals(Visibility.Visible))
                {
                    IsLoadVisibility = Visibility.Collapsed;
                }
                SetValue(IsErrorVisibilityProperty, value); }
        }

        
        //ErrorMargin
        public static readonly DependencyProperty ErrorMarginProperty =
            DependencyProperty.RegisterAttached("ErrorMargin", typeof(string), typeof(LoadingControl), new PropertyMetadata(null));

        public string ErrorMargin
        {
            get { return (string)GetValue(ErrorMarginProperty); }
            set { SetValue(ErrorMarginProperty, value); }
        }
    }
}
