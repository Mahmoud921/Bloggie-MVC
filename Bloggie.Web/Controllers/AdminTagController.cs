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

        [HttpGet]
        public IActionResult List() {
            var tag = _context.Tags.ToList();
            return View(tag);
        }

        [HttpGet]
        public IActionResult Edit(Guid id) {
            var tag = _context.Tags.FirstOrDefault(e => e.Id == id);
            if (tag != null)
            {
                var edittag = new EditTagRequest{
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName,
                 };
                return View(edittag);
            }
            return View(null);
        }

        [HttpPost]
        public IActionResult Edit(EditTagRequest _editTagRequest) {

            var tag = new Tag {
                Id = _editTagRequest.Id,
                Name = _editTagRequest.Name,
                DisplayName= _editTagRequest.DisplayName,
            };
            var existtag = _context.Tags.FirstOrDefault(e=> e.Id == tag.Id);

            if (existtag != null) {

                existtag.Name = tag.Name;
                existtag.DisplayName = tag.DisplayName;

                // save changes
                _context.SaveChanges();
                //show success notify
                return RedirectToAction("Edit", new { id = _editTagRequest.Id });
            }
            // show error notify
            return RedirectToAction("Edit", new { id = _editTagRequest.Id });
        }
        
        [HttpPost]

        public IActionResult Delete(EditTagRequest _editTagRequest) {

            var tag = _context.Tags.Find(_editTagRequest.Id);
            if (tag != null) {
                _context.Tags.Remove(tag);
                _context.SaveChanges();
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new { id = _editTagRequest.Id });
        }




    }
}
