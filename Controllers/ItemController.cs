using Microsoft.AspNetCore.Mvc;
using eCommerce.Models;
using eCommerce.Data;

namespace eCommerce.Controllers
{
    public class ItemController : Controller
    {
        private BakeryItemContext _context;

        public ItemController(BakeryItemContext context)
        {
           _context = context;
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Item i) 
        {
            if (ModelState.IsValid) 
            {
                _context.Items.Add(i);
                _context.SaveChanges();

                ViewData["Message"] = $"{i.Title} was added successfully!";

                return View();
            }

            return View();
        }
    }
}
