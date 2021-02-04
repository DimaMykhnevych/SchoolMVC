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
            var disciplines = _db.Disciplines;
            StudentDisciplinesViewModel studentDisciplines = new StudentDisciplinesViewModel
            {
                Disciplines = disciplines,
                Student = student,
            };
            return View(studentDisciplines);
        }

        [HttpPost]
        public IActionResult SelectDisciplines(int id, IFormCollection collection)
        {
            var selectedDisciplines = collection.Keys.ToList();
            selectedDisciplines.RemoveAt(selectedDisciplines.Count - 1);

            var allDisciplinesIds = _db.Disciplines.Select(d => d.Id.ToString()).ToList();
            var uncheckedDisciplines = allDisciplinesIds.Except(selectedDisciplines).ToList();

            var currentStudent = _db.Students.Include(s => s.Disciplines).SingleOrDefault(s => s.Id == id);

            if (selectedDisciplines.Any())
            {
                AddStudentDiscipline(selectedDisciplines, currentStudent);
            }

            if (uncheckedDisciplines.Any())
            {
                DeleteStudentDiscipline(uncheckedDisciplines, currentStudent);
            }

            return RedirectToAction("Index");
        }

        private void AddStudentDiscipline(List<string> selectedDisciplines, Student currentStudent)
        {
            for (int i = 0; i < selectedDisciplines.Count; i++)
            {
                var disc = _db.Disciplines.SingleOrDefault(d =>
                d.Id == Convert.ToInt32(selectedDisciplines[i]));
                currentStudent.Disciplines.Add(disc);
            }
            _db.SaveChanges();
        }

        private void DeleteStudentDiscipline(List<string> uncheckedDisciplines, Student currentStudent)
        {
            for (int i = 0; i < uncheckedDisciplines.Count; i++)
            {
                var disc = _db.Disciplines.SingleOrDefault(d =>
                d.Id == Convert.ToInt32(uncheckedDisciplines[i]));
                currentStudent.Disciplines.Remove(disc);
            }
            _db.SaveChanges();
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
