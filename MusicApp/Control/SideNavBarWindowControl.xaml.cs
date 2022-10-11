using MusicApp.ViewModels;
using System.Windows.Controls;

namespace MusicApp.Control
{

    /// <summary>
    /// SideNavBarWindowControl.xaml 的交互逻辑
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
