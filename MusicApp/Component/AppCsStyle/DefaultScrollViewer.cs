using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MusicApp.Component.AppCsStyle
{
    public class DefaultScrollViewer : ScrollViewer
    {
        public DefaultScrollViewer()
        {
            //解决ListBox不能滚动的问题-
            PreviewMouseWheel += (s, e) =>
            {
                if (!e.Handled)
                {
                    // ListView拦截鼠标滚轮事件
                    e.Handled = true;
                    // 激发一个鼠标滚轮事件，冒泡给外层ListView接收到
                    var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
                    eventArg.RoutedEvent = MouseWheelEvent;
                    eventArg.Source = s;
                    var parent = (DefaultScrollViewer)s as UIElement;
                    parent.RaiseEvent(eventArg);
                }
            };

            //解决事件传导问题
            AddHandler(ScrollViewer.MouseLeftButtonDownEvent, new MouseButtonEventHandler((s, e) => {
                e.Handled = false;
            }), true);
        }
    }
}
