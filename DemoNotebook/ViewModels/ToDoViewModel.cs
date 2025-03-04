using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoNotebook.Models;
using DemoNotebook.Shared.DTO;
using DemoNotebook.Views;

namespace DemoNotebook.ViewModels
{
    class ToDoViewModel:BindableBase
    {
        public DelegateCommand GoBackCommand { get; set; }
        public DelegateCommand GoForwardCommand { get; set; }
        private IRegionNavigationJournal journal;

        private bool isRightDrawerOpen;
        /// <summary>
        /// 右侧编辑窗口是否展开
        /// </summary>
        public bool IsRightDrawerOpen
        {
            get { return isRightDrawerOpen; }
            set { isRightDrawerOpen = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<ToDoDTO> toDoDTOs;

		public ObservableCollection<ToDoDTO> ToDoDTOs
        {
			get { return toDoDTOs; }
			set { toDoDTOs = value;RaisePropertyChanged(); }
		}
        public DelegateCommand AddCommand { get; set; }
        public ToDoViewModel()
        {
            ToDoDTOs = new ObservableCollection<ToDoDTO>();
            AddCommand = new DelegateCommand(Add);
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
            createToDoData();
        }

        private void Add()
        {
            IsRightDrawerOpen = true;
        }

        private void createToDoData()
        {
            for (int i = 0; i < 20; i++)
            {
              ToDoDTOs.Add(new ToDoDTO() { Title = "代办" + i, Content = "测试数据" + i, Status = 0 });
            }
           
        }
    }
}
