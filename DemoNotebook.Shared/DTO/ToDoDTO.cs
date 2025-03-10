﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNotebook.Shared.DTO
{
    public class ToDoDTO : BaseDTO
    {
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value;OnPropertyChanged(); }
        }


        private int status;

        public int Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged(); }
        }

        private string content;

        public string Content
        {
            get { return content; }
            set { content = value; OnPropertyChanged(); }
        }

    }
}
