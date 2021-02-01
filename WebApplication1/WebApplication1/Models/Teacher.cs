using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class Teacher
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Dept { set; get; }
        
        public List<Discipline> Disciplines { set; get; }
    }
}
