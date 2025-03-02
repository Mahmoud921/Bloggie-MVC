using Microsoft.AspNetCore.Mvc;
using Bloggie.Web.Data;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> SubmitTag(AddReqestTag _add)
        {
            var tag = new Tag {
                Name = _add.Name,
                DisplayName = _add.DisplayName,
            };
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List() {
            var tag = await _context.Tags.ToListAsync();
            return View(tag);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id) {
            var tag = await _context.Tags.FirstOrDefaultAsync(e => e.Id == id);
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
        public async Task<IActionResult> Edit(EditTagRequest _editTagRequest) {

            var tag = new Tag {
                Id = _editTagRequest.Id,
                Name = _editTagRequest.Name,
                DisplayName= _editTagRequest.DisplayName,
            };
            var existtag = await _context.Tags.FirstOrDefaultAsync(e=> e.Id == tag.Id);

            if (existtag != null) {

                existtag.Name = tag.Name;
                existtag.DisplayName = tag.DisplayName;

                // save changes
                await _context.SaveChangesAsync();
                //show success notify
                return RedirectToAction("Edit", new { id = _editTagRequest.Id });
            }
            // show error notify
            return RedirectToAction("Edit", new { id = _editTagRequest.Id });
        }
        
        [HttpPost]

        public async Task<IActionResult> Delete(EditTagRequest _editTagRequest) {

            var tag = await _context.Tags.FindAsync(_editTagRequest.Id);
            if (tag != null) {
                _context.Tags.Remove(tag);
                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new { id = _editTagRequest.Id });
        }

    }
}
