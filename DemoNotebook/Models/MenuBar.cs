using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DemoNotebook.Models
{
    internal class MenuBar: BindableBase
    {
        /// <summary>
        /// 惨淡图标
        /// </summary>
        private string icon;

        public string Icon
        {
            get { return icon; }
            set { icon = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 标题
        /// </summary>
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 命名空间
        /// </summary>
        private string targetView;

        public string TargetView
        {
            get { return targetView; }
            set { targetView = value; RaisePropertyChanged(); }
        }

    }
}
