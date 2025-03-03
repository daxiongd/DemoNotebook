using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DemoNotebook.Api.Context
{
    public class BaseEntity
    {
        [Key]
        public  int Id { get; set; } 
        public DateTime  CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
