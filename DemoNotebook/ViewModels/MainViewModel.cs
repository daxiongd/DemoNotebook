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
            MenuBars.Add(new MenuBar { Icon = "Home", Title = "首页", TargetView = "index" });
            MenuBars.Add(new MenuBar { Icon = "Notebook", Title = "备忘录", TargetView = "memo" });
            MenuBars.Add(new MenuBar { Icon = "NotebookEditOutline", Title = "待办事项", TargetView = "todo" });
            MenuBars.Add(new MenuBar { Icon = "Cog", Title = "设置", TargetView = "settings" });
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
         
        }
    }
}
