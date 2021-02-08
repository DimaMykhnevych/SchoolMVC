using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            //var teachers = _db.Teachers.AsEnumerable();
            ViewData["TeacherId"] = new SelectList(_db.Teachers, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Discipline discipline)
        {
            if (ModelState.IsValid)
            {
                _db.Disciplines.Add(discipline);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(discipline);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var discipline = _db.Disciplines.ToList().Find(d => d.Id == id);
            ViewData["TeacherId"] = new SelectList(_db.Teachers, "Id", "Name", discipline.TeacherId);
            return View(discipline);
        }

        [HttpPost]
        public IActionResult Edit(Discipline d)
        {
            ViewData["TeacherId"] = new SelectList(_db.Teachers, "Id", "Name", d.TeacherId);
            if (ModelState.IsValid)
            {
                _db.Update(d);
                _db.SaveChanges();
                return RedirectToAction("Edit", new { id = d.Id });
            }
            return View(d);
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
