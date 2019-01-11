using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StardekkBreweryApp.Models;

namespace StardekkBreweryApp.Pages.Breweries
{
    public class IndexModel : PageModel
    {
        private readonly BreweriesDbContext _db;

        [TempData]
        public string Message { get; set; }

        public IEnumerable<Brewery> Breweries { get; set; }

        public IndexModel(BreweriesDbContext db)
        {
            _db = db;
        }

        public async Task OnGet()
        {
            Breweries = await _db.Brewery.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var brewery = await _db.Brewery.FindAsync(id);
            _db.Brewery.Remove(brewery);
            await _db.SaveChangesAsync();

            Message = "Brewery deleted successfully.";

            return RedirectToPage("Index");
        }
    }
}