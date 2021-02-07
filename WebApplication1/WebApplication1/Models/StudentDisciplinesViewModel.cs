using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class StudentDisciplinesViewModel
    {
        public Student Student { get; set; }
        public List<SelectedDisciplineViewModel> Disciplines { get; set; }
    }
}
