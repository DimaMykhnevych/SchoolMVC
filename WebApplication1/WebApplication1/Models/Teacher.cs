using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Teacher
    {
        public int Id { set; get; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { set; get; }
        public string Dept { set; get; }
        
        public List<Discipline> Disciplines { set; get; }
    }
}
