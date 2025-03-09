using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoNotebook.Models;
using DemoNotebook.Service;
using DemoNotebook.Shared.DTO;
using DemoNotebook.Shared.Parameters;
using DemoNotebook.Views;
using MaterialDesignColors;

namespace DemoNotebook.ViewModels
{
    class ToDoViewModel:BindableBase
    {
        public DelegateCommand GoBackCommand { get; set; }
        private readonly IToDoService _service;

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
        public ToDoViewModel(IToDoService service)
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
            this._service = service;
            createToDoData();
        }

        private void Add()
        {
            IsRightDrawerOpen = true;
        }

        async void createToDoData()
        {
            try
            {
                var todoResult = await _service.GetAllFilterAsync(new Shared.Parameters.ToDoParameter()
                {
                    PageIndex = 0,
                    PageSize = 100,

                });

                if (todoResult.IsSuccess)
                {
                    toDoDTOs.Clear();
                    foreach (var item in todoResult.Result.Items)
                    {
                        toDoDTOs.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("请求todo报错了++++++++++++++++++++++++++"+ ex.Message);
            }
       
        }
    
    }
}
