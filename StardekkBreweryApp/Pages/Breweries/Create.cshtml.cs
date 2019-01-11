using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StardekkBreweryApp.Models;

namespace StardekkBreweryApp.Pages.Breweries
{
    public class CreateModel : PageModel
    {
        private readonly BreweriesDbContext _db;

        [TempData]
        public string Message { get; set; }

        [BindProperty]
        public Brewery Brewery { get; set; }

        public CreateModel(BreweriesDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Brewery.Add(Brewery);

            await _db.SaveChangesAsync();
            Message = "Brewery has been created succesfully";

            return RedirectToPage("Index");
        }
    }
}