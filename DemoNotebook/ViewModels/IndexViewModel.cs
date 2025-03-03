using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoNotebook.Models;

namespace DemoNotebook.ViewModels
{
    class IndexViewModel : BindableBase
    {
        public IndexViewModel()
        {
            TaskBars = new ObservableCollection<TaskBar>();
            createTaskBar();
            createTestData();
        }
        private ObservableCollection<TaskBar> taskBars;
        private ObservableCollection<ToDoDTO> toDoDTOs;
        private ObservableCollection<MemoDTO> memoDTOs;

        public ObservableCollection<TaskBar> TaskBars
        {
            get { return taskBars; }
            set { taskBars = value; RaisePropertyChanged(); }
        }
        public ObservableCollection<ToDoDTO> ToDoDTOs
        {
            get { return toDoDTOs; }
            set { toDoDTOs = value; RaisePropertyChanged(); }
        }
        public ObservableCollection<MemoDTO> MemoDTOs
        {
            get { return memoDTOs; }
            set { memoDTOs = value; RaisePropertyChanged(); }
        }
        public void createTaskBar()
        {
            TaskBars.Add(new TaskBar() { Icon = "ClockFast", TaskName = "汇总", Content = 9, Color = "#FF0CA0FF", Target = "target" });
            TaskBars.Add(new TaskBar() { Icon = "ClockCheckOutline", TaskName = "已完成", Content = 9, Color = "#10B136", Target = "target" });
            TaskBars.Add(new TaskBar() { Icon = "ChartLineVariant", TaskName = "完成率", Content = 100, Color = "#00B5D9", Target = "target" });
            TaskBars.Add(new TaskBar() { Icon = "PlaylistStar", TaskName = "备忘录", Content = 19, Color = "#FF9F00", Target = "target" });
        }
        void createTestData()
        {
            ToDoDTOs = new ObservableCollection<ToDoDTO>();
            MemoDTOs = new ObservableCollection<MemoDTO>();
            for (int i = 0; i < 10; i++)
            {
                ToDoDTOs.Add(new ToDoDTO() { Title = "代办" + i, Content = "正在处理" + i, Status = 0 });
                MemoDTOs.Add(new MemoDTO() { Title = "备忘" + i, Content = "我的密码", Status = 0 });
            }
        }
    }
}
