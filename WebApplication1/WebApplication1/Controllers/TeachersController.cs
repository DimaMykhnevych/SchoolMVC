using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TeachersController : Controller
    {
        private readonly AppDbContext _db;

        public TeachersController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Teachers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Teacher t)
        {
            if (ModelState.IsValid)
            {
                var teacher = new Teacher { Name = t.Name };
                _db.Teachers.Add(teacher);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var teacher = _db.Teachers.Find(id);
            return View(teacher);
        }

        [HttpPost]
        public IActionResult Edit(Teacher t)
        {
            if (ModelState.IsValid)
            {
                var teacher = _db.Teachers.Find(t.Id);
                teacher.Name = t.Name;
                _db.SaveChanges();
                return View(teacher);
            }
            return View(t);

        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var teacher = _db.Teachers.Find(id);
            return View(teacher);
        }

        [HttpPost]
        public IActionResult Delete(Teacher t)
        {
            var teacher = _db.Teachers.Find(t.Id);
            _db.Remove(teacher);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
