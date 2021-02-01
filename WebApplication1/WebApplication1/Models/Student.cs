using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class Student
    {
        public int Id { set; get; }
        public string Name { set; get; }

        public List<Discipline> Disciplines { set; get; }
    }
}
