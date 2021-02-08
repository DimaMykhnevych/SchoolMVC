using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Discipline
    {
        public int Id { set; get; }

        [Required]
        public string Title { set; get; }

        [Required]
        public string Annotation { set; get; }

        [Required]
        public int TeacherId { set; get; }   
        
        public Teacher Teacher { set; get; }
        public List<Student> Students { set; get; }
    }
}
