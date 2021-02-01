using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class Discipline
    {
        public int Id { set; get; }
        public string Title { set; get; }
        public string Annotation { set; get; }
        public int TeacherId { set; get; }   
        
        public Teacher Teacher { set; get; }
        public List<Student> Students { set; get; }
    }
}
