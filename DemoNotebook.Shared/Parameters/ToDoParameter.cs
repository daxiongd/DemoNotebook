﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNotebook.Shared.Parameters
{
    public class ToDoParameter : MyQueryParameter
    {
        public int? Status { get; set; }
    }
}
