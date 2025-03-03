using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoNotebook.Extension;
using DemoNotebook.Models;

namespace DemoNotebook.ViewModels
{
    class SettingsViewModel:BindableBase
    {
        private ObservableCollection<MenuBar> menuBars;

        private readonly IRegionManager regionManager;
        public DelegateCommand<MenuBar> NavigateCommand { get; set; }
        public ObservableCollection<MenuBar> MenuBars
        {
            get { return menuBars; }
            set { menuBars = value; RaisePropertyChanged(); }
        }
        private void Navigate(MenuBar menuBar)
        {
            if (menuBar == null || string.IsNullOrEmpty(menuBar.TargetView))
            {
                //System.Diagnostics.Debug.WriteLine("导航失败：菜单项或 TargetView 为空");
                return;
            }

            //System.Diagnostics.Debug.WriteLine($"尝试导航到 {menuBar.TargetView}");
            regionManager.RequestNavigate(PrismManager.SettingsViewRegion, menuBar.TargetView);

        }
        public SettingsViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            MenuBars = new ObservableCollection<MenuBar>();
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate); // This line is now correct
            CreateMenuBar();
           
        }

        private void CreateMenuBar()
        {
            menuBars.Add(new MenuBar { Icon = "Home", Title = "个性化", TargetView = "SettingsSkinView" });
            menuBars.Add(new MenuBar { Icon = "Home", Title = "首页", TargetView = "IndexView" });
            menuBars.Add(new MenuBar { Icon = "Home", Title = "首页", TargetView = "IndexView" });
        }
    }
     
    }
