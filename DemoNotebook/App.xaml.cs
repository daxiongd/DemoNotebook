using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Automation.Peers;
using DemoNotebook.Service;
using DemoNotebook.ViewModels;
using DemoNotebook.Views;
using Prism.Ioc;
using RestSharp;
using RestSharp.Serializers.Json;

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
            //containerRegistry.GetContainer().Register<HttpRestClient>(made:Parameters.Of.Type<string>(serviceKey:"webUrl"));
            //containerRegistry.GetContainer().RegisterInstance(@"http://localhost:5000",serviceKey:"webUrl");
            var restClientOptions = new RestClientOptions("http://localhost:5000")
            {
                Timeout = TimeSpan.FromSeconds(30), // 30秒超时
                ThrowOnAnyError = true
            };

            var restClient = new RestClient(
                options: restClientOptions,
                configureSerialization: s => s.UseSystemTextJson() // 使用 System.Text.Json
            );
            containerRegistry.RegisterInstance<IRestClient>(restClient); // 通过接口注册

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
