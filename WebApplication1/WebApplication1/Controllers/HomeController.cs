using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _db;

        public HomeController(ILogger<HomeController> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            var studetns = _db.Students;
            return View(studetns);
        }

        [HttpGet]
        public IActionResult SelectDisciplines(int id)
        {
            var student = _db.Students.Include(s => s.Disciplines).SingleOrDefault(s => s.Id == id);
            var disciplines = _db.Disciplines.ToList();
            List<SelectedDisciplineViewModel> selectedDisciplines = new List<SelectedDisciplineViewModel>();
            foreach (var disc in disciplines)
            {
                selectedDisciplines.Add(new SelectedDisciplineViewModel 
                { Discipline = disc, IsSelected = student.Disciplines.Contains(disc) });
            }
            StudentDisciplinesViewModel studentDisciplines = new StudentDisciplinesViewModel
            {
                Disciplines = selectedDisciplines,
                Student = student,
            };
            return View(studentDisciplines);
        }

        [HttpPost]
        public IActionResult SelectDisciplines(StudentDisciplinesViewModel studentDisciplines)
        {
            var student = _db.Students
                .Include(s => s.Disciplines)
                .SingleOrDefault(s => s.Id == studentDisciplines.Student.Id);
            student.Disciplines.Clear();
            foreach(var disc in studentDisciplines.Disciplines)
            {
                if (disc.IsSelected)
                {
                    var discipline = _db.Disciplines.SingleOrDefault(d => d.Id == disc.Discipline.Id);
                    student.Disciplines.Add(discipline);
                }
            }
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
