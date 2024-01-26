using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Models
{
    public class TaskModel
    {
        [Key]
        public int ID { get; set; }
        public int Title { get; set; }
        public string Description { get; set; }
        public string Assignee { get; set; }
        public DateTime? DueDate { get; set; }
       
    }
}
