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



        public async Task<IActionResult> Index(int? id)
        {
            // setup for pagination
            const int NumItemsDisplayed = 3;
            const int PageOffset = 1; // for using current page and deciding how many items to skip
            int currPage = id ?? 1; // sets to id value if exists, otherwise sets to 1

            // getting num of pages
            int totalItems = await _context.Items.CountAsync();
            double maxPages = Math.Ceiling((double)totalItems / NumItemsDisplayed);
            int lastPage = Convert.ToInt32(maxPages); // rounds up to next whole page

            // method syntax:
            //List<Item> items = _context.Items
            //                     .Skip(NumItemsDisplayed * (currPageId - PageOffset))
            //                     .Take(NumItemsDisplayed)
            //                     .ToList();

            // query syntax:
            List<Item> items = await (from item in _context.Items select item)
                                .Skip(NumItemsDisplayed * (currPage - PageOffset))
                                .Take(NumItemsDisplayed)
                                .ToListAsync();

            ItemCatalogViewModel catalogModel = new(items, lastPage, currPage);
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

        
        public async Task<IActionResult> Details(int id)
        {
            Item? itemDetails = await _context.Items.FindAsync(id);

            if (itemDetails == null )
            {
                return NotFound();
            }

            return View(itemDetails);
        }
    }
}
