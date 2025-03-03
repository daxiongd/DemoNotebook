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
using System.Windows.Shapes;
using DemoNotebook.ViewModels;
using MaterialDesignThemes.Wpf;

namespace DemoNotebook.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {

        public MainView()
        {
            InitializeComponent();
            RegionManager.SetRegionManager(this, ContainerLocator.Container.Resolve<IRegionManager>());
            //等效于xaml 中MouseMove=Yours Handler Name
            ColorZone.MouseMove += (s, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            };
            ColorZone.MouseDoubleClick += (s, e) =>
            {
                if (WindowState != WindowState.Maximized)
                {
                    this.WindowState = WindowState.Maximized;
                }
                else
                {
                    this.WindowState = WindowState.Normal;
                }
            };
            
           
            
           
        }

        private void MaxWindow(object sender, RoutedEventArgs e)
        {
            if (WindowState != WindowState.Maximized)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;

            }

        }

        private void MinWindow(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;

        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void HideLeftHost(object sender, SelectionChangedEventArgs e)
        {
            drawerHost.IsLeftDrawerOpen = false;
        }
    }
}
