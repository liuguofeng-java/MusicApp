using MusicApp.Common;
using MusicApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Windows;
using System.Threading;
using MusicApp.Models.Vo;
using Newtonsoft.Json;
using MusicApp.Control;
using MusicApp.Views;

namespace MusicApp.ViewModels
{
    public class SearchTextBoxViewModel
    {

        public SearchTextBoxModel Model { get; set; }

        //输入框值改变
        public CommandBase TextChangedCommand { get; set; }

        //点击输入框事件
        public CommandBase TextMouseDownCommand { get; set; }

        //点击搜索按钮时触发
        public CommandBase TextClickSearchCommand { get; set; }
        public SearchTextBoxViewModel()
        {
            Model = new SearchTextBoxModel();

            //输入框值改变
            TextChangedCommand = new CommandBase();
            TextChangedCommand.DoExecute = new Action<object>(TextChangedDoExecute);
            TextChangedCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            //点击输入框事件
            TextMouseDownCommand = new CommandBase();
            TextMouseDownCommand.DoExecute = new Action<object>(TextMouseDownDoExecute);
            TextMouseDownCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            //点击搜索按钮时触发
            TextClickSearchCommand = new CommandBase();
            TextClickSearchCommand.DoExecute = new Action<object>(TextClickSearchDoExecute);
            TextClickSearchCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            //点击主窗体border,隐藏搜索列表控件
            MainWindowViewModel.GetInstance().BaseBorderMouseDownDelegate += new Action<object>((o) => {
                SearchListViewModel.This.Model.GridVisibility = Visibility.Collapsed;
            });
        }

        /// <summary>
        /// 输入框值改变
        /// </summary>
        /// <param name="o"></param>
        public void TextChangedDoExecute(object o)
        {
            var text = (string)o;

            //如果没有弹出搜索框就弹出
            if (SearchListViewModel.This.Model.GridVisibility != Visibility.Visible)
            {
                SearchListViewModel.This.Model.GridVisibility = Visibility.Visible;
            }

            // 重启延时器
            if (Model.Timer != null)
                Model.Timer.Stop();
            //开始计时
            Model.Timer = new System.Timers.Timer(500);
            Model.Timer.AutoReset = false;
            Model.Timer.Elapsed += new ElapsedEventHandler((o, ex) => {
                //text == "" 显示排行榜,否则显示搜索
                if (string.IsNullOrEmpty(text))
                {
                    SearchListViewModel.This.Model.SearchVisibility = Visibility.Collapsed;
                    SearchListViewModel.This.Model.RankingVisibility = Visibility.Visible;
                }
                else
                {

                    SearchListViewModel.This.Model.SearchVisibility = Visibility.Visible;
                    SearchListViewModel.This.Model.RankingVisibility = Visibility.Collapsed;
                }
                SearchListViewModel.This.GetSearchList(text);
                Model.Timer.Stop();//关闭延时器

            });

            Model.Timer.Start();
        }

        


        /// <summary>
        /// 点击搜索按钮时触发
        /// </summary>
        /// <param name="o"></param>
        private void TextClickSearchDoExecute(object o)
        {
            var text = (string)o;
        }

        /// <summary>
        /// 点击输入框事件
        /// </summary>
        /// <param name="o"></param>
        private void TextMouseDownDoExecute(object o)
        {
            //如果当前排行列表是空的,就重新获取
            if (SearchListViewModel.This.Model.RankingList.Count == 0)
            {
                SearchListViewModel.This.GetRankingList();
            }
            //点击输入框,显示搜索列表控件
            SearchListViewModel.This.Model.GridVisibility = Visibility.Visible;
        }
    }
}
