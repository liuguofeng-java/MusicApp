using MusicApp.Views;
using System;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MusicApp.Control
{
    /// <summary>
    /// 搜索框 的交互逻辑
    /// </summary>
    public partial class SearchTextBoxControl : UserControl
    {
        private MainWindow mainWindow = ControlBean.getInstance().mainWindow;
        //延时执行器
        private System.Timers.Timer timer;
        public SearchTextBoxControl()
        {
            InitializeComponent();

            //点击主窗体border,隐藏搜索列表控件
            mainWindow.BaseBorder.MouseDown += (s, e) =>
            {
                ControlBean.getInstance().searchListControl.GridContrainer.Visibility = Visibility.Collapsed;
            };

            //文本值改变触发,搜索
            SearchBox.TextChanged += (s, e) =>
            {
                var control = ControlBean.getInstance().searchListControl;
                var text = SearchBox.Text;

                //如果没有弹出搜索框就弹出
                if (control.GridContrainer.Visibility != Visibility.Visible)
                {
                    control.GridContrainer.Visibility = Visibility.Visible;
                }

                // 重启延时器
                if (timer != null)
                    timer.Stop();
                //开始计时
                timer = new System.Timers.Timer(500);
                timer.AutoReset = false;
                timer.Elapsed += new ElapsedEventHandler((o, ex) => {
                    //text == "" 显示排行榜,否则显示搜索
                    this.Dispatcher.Invoke(new Action(delegate
                    {
                        if (string.IsNullOrEmpty(text))
                        {
                            control.SearchContrainer.Visibility = Visibility.Collapsed;
                            control.RankingContrainer.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            control.SearchContrainer.Visibility = Visibility.Visible;
                            control.RankingContrainer.Visibility = Visibility.Collapsed;
                        }

                    }));
                        
                    control.GetSearchList(text);
                    timer.Stop();//关闭延时器

                });

                timer.Start();                
            };


        }

        /// <summary>
        /// 点击搜索按钮时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        /// <summary>
        /// 点击输入框触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputScrollViewer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var control = ControlBean.getInstance().searchListControl;
            //如果当前排行列表是空的,就重新获取
            if (control.RankingListBox.Items.Count == 0)
            {
                control.GetRankingList();
            }
            //点击输入框,显示搜索列表控件
            control.GridContrainer.Visibility = Visibility.Visible;
        }
    }
}
