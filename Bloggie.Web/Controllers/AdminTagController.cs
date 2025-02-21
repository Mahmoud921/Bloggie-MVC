using Microsoft.AspNetCore.Mvc;
using Bloggie.Web.Data;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Models.Domain;
namespace Bloggie.Web.Controllers
{
    public class AdminTagController : Controller
    {
        private readonly BloggieDbContext _context;



        public AdminTagController(BloggieDbContext context) 
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]

        public IActionResult SubmitTag(AddReqestTag _add)
        {
            var tag = new Tag {
                Name = _add.Name,
                DisplayName = _add.DisplayName,
            };
            _context.Tags.Add(tag);
            _context.SaveChanges();
            return RedirectToAction("List");
        }
        public IActionResult List() {
            var tag = _context.Tags.ToList();
            return View(tag);
        }
    }
}
