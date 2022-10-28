using MusicApp.Models.PageView.ChildPage;
using MusicApp.ViewModels.PageView.ChildPage;
using System;
using System.Collections.Generic;
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

namespace MusicApp.PageView.ChildPage
{
    /// <summary>
    /// SongOfDay.xaml 的交互逻辑
    /// </summary>
    public partial class SongListOfDay : UserControl
    {
        public SongListOfDay()
        {
            InitializeComponent();
            var model = new SongListOfDayViewModel();
            DataContext = model;

            //解决ListBox不能滚动的问题
            SongList.PreviewMouseWheel += (s, e) =>
            {
                if (!e.Handled)
                {
                    // ListView拦截鼠标滚轮事件
                    e.Handled = true;
                    // 激发一个鼠标滚轮事件，冒泡给外层ListView接收到
                    var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
                    eventArg.RoutedEvent = UIElement.MouseWheelEvent;
                    eventArg.Source = s;
                    var parent = ((ListBox)s).Parent as UIElement;
                    parent.RaiseEvent(eventArg);
                }
            };
        }
    }
}
