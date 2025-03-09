using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Automation.Peers;
using DemoNotebook.Service;
using DemoNotebook.ViewModels;
using DemoNotebook.Views;
using Example;
using Microsoft.Extensions.Options;
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
            var restClientOptions = new RestClientOptions("https://localhost:44367")
            {
                Timeout = TimeSpan.FromSeconds(30), // 30秒超时
                //ThrowOnAnyError = true
            };
        
            containerRegistry.RegisterSingleton<RestClient>(() => new RestClient(
                options: restClientOptions,
                configureSerialization: s => s.UseSystemTextJson() // 使用 System.Text.Json
            ));

            containerRegistry.Register<IApiClient, RestSharpApiClient>();
            // 注册服务层

            containerRegistry.Register<IToDoService, ToDoService>();
            containerRegistry.Register<IMemoService, MemoService>();
            containerRegistry.RegisterForNavigation<MainView, MainViewModel>();
            // 显式指定导航名称（与 MenuBar.TargetView 匹配）
            containerRegistry.RegisterForNavigation<IndexView>("index");
            containerRegistry.RegisterForNavigation<SettingsSkinView>("settingsSkin");
            containerRegistry.RegisterForNavigation<ToDoView>("todo");
            containerRegistry.RegisterForNavigation<MemoView>("memo");
            containerRegistry.RegisterForNavigation<SettingsView>("settings");

       
        }

    }

}
