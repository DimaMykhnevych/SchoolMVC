using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DisciplinesController : Controller
    {
        private readonly AppDbContext _db;

        public DisciplinesController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var disc = _db.Disciplines.Include(d => d.Teacher);
            return View(disc);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var teachers = _db.Teachers.AsEnumerable();
            return View(new DisciplineViewModel
            {
                Teachers = teachers,
                Discipline = null
            });
        }

        [HttpPost]
        public IActionResult Create(Discipline discipline)
        {
            var disc = discipline;
            _db.Disciplines.Add(disc);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var teachers = _db.Teachers.AsEnumerable();
            var discipline = _db.Disciplines.Include(t => t.Teacher).ToList().Find(d => d.Id == id);
            return View(new DisciplineViewModel
            {
                Teachers = teachers,
                Discipline = discipline
            });
        }

        [HttpPost]
        public IActionResult Edit(Discipline d)
        {
            _db.Update(d);
            _db.SaveChanges();

            return RedirectToAction("Edit", new { id = d.Id});
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var discipline = _db.Disciplines.Include(t => t.Teacher).ToList().Find(d => d.Id == id);
            return View(discipline);
        }

        [HttpPost]
        public IActionResult Delete(Discipline d)
        {
            var discipline = _db.Disciplines.Find(d.Id);
            _db.Remove(discipline);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
