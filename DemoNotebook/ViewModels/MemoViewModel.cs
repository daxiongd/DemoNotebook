using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using DemoNotebook.Models;
using DemoNotebook.Shared.DTO;

namespace DemoNotebook.ViewModels
{
    class MemoViewModel:BindableBase
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

        private ObservableCollection<MemoDTO> memoDTOs;

        public ObservableCollection<MemoDTO> MemoDTOs
        {
            get { return memoDTOs; }
            set { memoDTOs = value; RaisePropertyChanged(); }
        }
        public DelegateCommand AddCommand { get; set; }
        public MemoViewModel()
        {
            MemoDTOs = new ObservableCollection<MemoDTO>();
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
            createMemoData();
        }

        private void Add()
        {
            IsRightDrawerOpen = true;
        }

        private void createMemoData()
        {
            for (int i = 0; i < 20; i++)
            {
                MemoDTOs.Add(new MemoDTO() { Title = "备忘" + i, Content = "备忘录测试数据" + i, Status = 0 });
            }

        }
    }
}
