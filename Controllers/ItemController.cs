﻿using Microsoft.AspNetCore.Mvc;
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

                ViewData["Message"] = $"{i.Title} was added successfully!";

                return View();
            }

            return View();
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
                return RedirectToAction("Index");
            }

            return View(i);
        }
    }
}
