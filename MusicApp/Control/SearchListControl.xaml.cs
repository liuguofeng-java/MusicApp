﻿using MusicApp.Common;
using MusicApp.Models.Vo;
using MusicApp.Views;
using Newtonsoft.Json;
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

namespace MusicApp.Control
{
    /// <summary>
    /// 歌曲搜索框,输入弹出搜索结果 的交互逻辑
    /// </summary>
    public partial class SearchListControl : UserControl
    {
        public SearchListControl()
        {
            ControlBean.getInstance().searchListControl = this;
            InitializeComponent();

            //解决ListBox不能滚动的问题
            RankingListBox.PreviewMouseWheel += (s, e) =>
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

            //默认隐藏控件
            StackPanelContrainer.Visibility = Visibility.Collapsed;

            //获取排行
            GetRankingList();

        }

        /// <summary>
        /// 获取排行榜数据
        /// </summary>
        public void GetRankingList()
        {
            //接收数据
            string result = HttpUtil.HttpRequset(HttpUtil.serveUrl + "/search/hot/detail");
            RankingModel data = JsonConvert.DeserializeObject<RankingModel>(result);

            for (int i = 0; i < data.data.Count; i++)
            {
                data.data[i].num = i + 1;
                data.data[i].color = i >= 3 ? "#5b5b5b" : "#e63838";
            }

            RankingListBox.ItemsSource = data.data;
        }

        /// <summary>
        /// 去除历史按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine();
        }

        /// <summary>
        /// 点击历史清除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void historyBut_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine();
        }

        
    }
}
