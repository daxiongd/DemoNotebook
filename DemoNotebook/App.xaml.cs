using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Automation.Peers;
using DemoNotebook.ViewModels;
using DemoNotebook.Views;
using Prism.Ioc;

namespace DemoNotebook
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainView>(); // 主窗口
        }

      
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainView, MainViewModel>();
            // 显式指定导航名称（与 MenuBar.TargetView 匹配）
            containerRegistry.RegisterForNavigation<IndexView>("IndexView");
            containerRegistry.RegisterForNavigation<SettingsSkinView>("SettingsSkinView");
            containerRegistry.RegisterForNavigation<ToDoView>("ToDoView");
            containerRegistry.RegisterForNavigation<MemoView>("MemoView");
            containerRegistry.RegisterForNavigation<SettingsView>("SettingsView");
        }

    }

}
