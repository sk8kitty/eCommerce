using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    public class MemberController : Controller
    {
        private BakeryItemContext _context;

        public MemberController(BakeryItemContext context)
        {
            _context = context;
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel regModel)
        {
            if (ModelState.IsValid)
            {
                // mapping RegisterViewModel data to a Member object
                Member newMember = new()
                {
                    Email = regModel.Email,
                    Password = regModel.Password
                };

                _context.Members.Add(newMember);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}
