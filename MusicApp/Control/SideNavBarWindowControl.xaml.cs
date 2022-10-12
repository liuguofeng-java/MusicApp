using MusicApp.ViewModels;
using System.Windows.Controls;

namespace MusicApp.Control
{

    /// <summary>
    /// 主窗体侧边栏选择菜单按钮 的交互逻辑
    /// </summary>
    public partial class SideNavBarWindowControl : UserControl
    {
        
        public SideNavBarWindowControl()
        {
            InitializeComponent();
            ControlBean.getInstance().sideNavBarWindowControl = this;

            DataContext = new SideNavBarWindowControlViewModel();

            
        }

    }
}
