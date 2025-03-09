using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using DemoNotebook.Models;
using DemoNotebook.Service;
using DemoNotebook.Shared.DTO;
using Microsoft.Extensions.Caching.Memory;

namespace DemoNotebook.ViewModels
{
    class MemoViewModel:BindableBase
    {
        public DelegateCommand GoBackCommand { get; set; }
        public DelegateCommand GoForwardCommand { get; set; }

        private readonly IMemoService _service;
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
        public MemoViewModel(IMemoService service)
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
            this._service= service;
            createMemoData();
        }

        private void Add()
        {
            IsRightDrawerOpen = true;
        }

        private async void createMemoData()
        {
            try
            {
                var Result = await _service.GetAllFilterAsync(new Shared.Parameters.MyQueryParameter()
                {
                    PageIndex = 0,
                    PageSize = 100,

                });

                if (Result.IsSuccess)
                {
                    memoDTOs.Clear();
                    foreach (var item in Result.Result.Items)
                    {
                        memoDTOs.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("请求todo报错了++++++++++++++++++++++++++" + ex.Message);
            }


        }
    }
}
