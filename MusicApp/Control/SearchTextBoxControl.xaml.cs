using MusicApp.ViewModels;
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

        public SearchTextBoxControl()
        {
            InitializeComponent();

            var model = SearchTextBoxViewModel.GetInstance();

            DataContext = model;
        }
    }
}
