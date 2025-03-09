using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DemoNotebook.Shared.Parameters
{
    public class MyQueryParameter
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string? Search { get; set; }
    }
}
