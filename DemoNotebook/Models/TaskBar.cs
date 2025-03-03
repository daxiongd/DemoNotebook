using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DemoNotebook.Models
{
    internal class TaskBar:BindableBase
    {
		private string icon;

		public string Icon
		{
			get { return icon; }
			set { icon = value; RaisePropertyChanged(); }
		}
		private string taskName;

		public string TaskName
		{
			get { return taskName; }
			set { taskName = value; RaisePropertyChanged(); }
		}
		private int content;

		public int Content
		{
			get { return content; }
			set { content = value; RaisePropertyChanged(); }
		}
		private string color;
				
		public string Color
		{
			get { return color; }
			set { color = value; RaisePropertyChanged(); }
		}
		private string target;

		public string Target
		{
			get { return target; }
			set { target = value; RaisePropertyChanged(); }
		}

	}
}
