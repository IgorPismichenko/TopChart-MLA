using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopChart_DLL.Entities;

namespace TopChart_BLL.DTO
{
    public class MessagesDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Add message before sending!")]
        public string? Message { get; set; }
        public virtual Users? User { get; set; }
        public int UserId { get; set; }
        public string? Date { get; set; }
    }
}
