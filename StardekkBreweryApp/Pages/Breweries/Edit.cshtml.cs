using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StardekkBreweryApp.Models;

namespace StardekkBreweryApp.Pages.Breweries
{
    public class EditModel : PageModel
    {
        private readonly BreweriesDbContext _db;

        [TempData]
        public string Message { get; set; }

        [BindProperty]
        public Brewery Brewery { get; set; }

        public EditModel(BreweriesDbContext db)
        {
            _db = db;
        }

        public void OnGet(int id)
        {
            Brewery = _db.Brewery.Find(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage();
            }

            var breweryToEdit = _db.Brewery.Find(Brewery.Id);
            breweryToEdit.Title = Brewery.Title;
            breweryToEdit.Logo = Brewery.Logo;
            breweryToEdit.Address = Brewery.Address;
            breweryToEdit.ContactCell = Brewery.ContactCell;
            breweryToEdit.NbOfActivities = Brewery.NbOfActivities;


            await _db.SaveChangesAsync();
            Message = "Brewery has been edited successfully.";

            return RedirectToPage("Index");
        }
    }
}