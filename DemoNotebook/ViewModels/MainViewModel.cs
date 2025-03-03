using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using DemoNotebook.Extension;
using DemoNotebook.Models;

namespace DemoNotebook.ViewModels
{
    internal class MainViewModel : BindableBase
    {
        private ObservableCollection<MenuBar> menuBars;

        private readonly IRegionManager regionManager;
        private IRegionNavigationJournal journal;
        public ObservableCollection<MenuBar> MenuBars
        {
            get { return menuBars; }
            set { menuBars = value; RaisePropertyChanged(); }
        }
        public DelegateCommand<MenuBar> NavigateCommand { get; set; } // Change the type to DelegateCommand<MenuBar>
        public DelegateCommand GoBackCommand { get; set; }
        public DelegateCommand GoForwardCommand { get; set; }


        private void Navigate(MenuBar menuBar)
        {
            if (menuBar == null || string.IsNullOrEmpty(menuBar.TargetView))
            {
                //System.Diagnostics.Debug.WriteLine("导航失败：菜单项或 TargetView 为空");
                return;
            }
         
            //System.Diagnostics.Debug.WriteLine($"尝试导航到 {menuBar.TargetView}");
            regionManager.RequestNavigate(PrismManager.MainViewRegion, menuBar.TargetView, callback =>
            {
                //if (callback.Result.HasError)
                //{
                //    // 处理导航错误（例如输出日志）
                //    System.Diagnostics.Debug.WriteLine($"导航失败: {callback.Error.Message}");
                //}
                //else
                //{
                //System.Diagnostics.Debug.WriteLine("导航成功");
                journal = callback.Context.NavigationService.Journal;
                //}
            });
        }

        void CreateMenuBar()
        {
            MenuBars.Add(new MenuBar { Icon = "Home", Title = "首页", TargetView = "IndexView" });
            MenuBars.Add(new MenuBar { Icon = "Notebook", Title = "备忘录", TargetView = "MemoView" });
            MenuBars.Add(new MenuBar { Icon = "NotebookEditOutline", Title = "待办事项", TargetView = "ToDoView" });
            MenuBars.Add(new MenuBar { Icon = "Cog", Title = "设置", TargetView = "SettingsView" });
        }

        public MainViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            MenuBars = new ObservableCollection<MenuBar>();
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate); // This line is now correct
            CreateMenuBar();
            GoBackCommand = new DelegateCommand(() =>
            {
                if (journal != null && journal.CanGoBack)
                {
                    journal.GoBack();
                }
            });
            GoForwardCommand = new DelegateCommand(() =>
            {
                if (journal != null && journal.CanGoForward)
                {
                    journal.GoForward();
                }
            });
            //System.Diagnostics.Debug.WriteLine("MainViewModel 已初始化，RegionManager 是否为 null: " + (regionManager == null));
            //var region = regionManager.Regions.ContainsRegionWithName(PrismManager.MainViewRegion);
            //object oj = regionManager.Regions.GetType();
            //System.Diagnostics.Debug.WriteLine($"区域 {PrismManager.MainViewRegion} 是否存在: {region}");
        }
    }
}
