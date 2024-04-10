using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopChart_DLL.Entities
{
    public class Messages
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public virtual Users? User { get; set; }
        public int UserId { get; set; }
        public string? Date { get; set; }
    }
}
