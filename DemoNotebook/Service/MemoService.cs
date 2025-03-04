using DemoNotebook.Models;
using DemoNotebook.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNotebook.Service
{
    public class MemoService : BaseService<MemoDTO>, IMemoService
    {
        public MemoService(HttpRestClient client) : base(client, "Memo")
        {

        }
    }
}
