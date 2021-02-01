using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class DisciplineViewModel
    {
        public IEnumerable<Teacher> Teachers { get; set; }
        public Discipline Discipline { get; set; }
    }
}
