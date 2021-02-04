using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class StudentDisciplinesViewModel
    {
        public Student Student { get; set; }
        public IEnumerable<Discipline> Disciplines { get; set; }
    }
}
