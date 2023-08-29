using Microsoft.AspNetCore.Mvc;
using eCommerce.Models;
using eCommerce.Data;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Controllers
{
    public class ItemController : Controller
    {
        private BakeryItemContext _context;

        public ItemController(BakeryItemContext context)
        {
           _context = context;
        }



        public async Task<IActionResult> Index()
        {
            List<Item> items = await (from item in _context.Items select item).ToListAsync();
            return View(items);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Item i) 
        {
            if (ModelState.IsValid) 
            {
                _context.Items.Add(i);
                await _context.SaveChangesAsync();

                TempData["Message"] = $"{i.Title} was added successfully!";

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Edit(int id)
        {
            Item? itemToEdit = await _context.Items.FindAsync(id);

            if (itemToEdit == null)
            {
                return NotFound();
            }

            return View(itemToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Item i)
        {
            if (ModelState.IsValid)
            {
                _context.Items.Update(i);
                await _context.SaveChangesAsync();

                TempData["Message"] = $"{i.Title} was edited successfully!";

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int id)
        {
            Item? itemToDelete = await _context.Items.FindAsync(id);

            if(itemToDelete == null) 
            {
                return NotFound();
            }

            return View(itemToDelete);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Item? itemToDelete = await _context.Items.FindAsync(id);

            if (itemToDelete != null)
            {
                _context.Items.Remove(itemToDelete);
                await _context.SaveChangesAsync();

                TempData["Message"] = $"{itemToDelete.Title} was deleted successfully!";

                return RedirectToAction("Index");
            }

            TempData["Message"] = "This item was already deleted.";
            return RedirectToAction("Index");
        }
    }
}
