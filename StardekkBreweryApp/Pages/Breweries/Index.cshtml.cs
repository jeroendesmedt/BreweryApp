using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StardekkBreweryApp.Models;

namespace StardekkBreweryApp.Pages.Breweries
{
    public class IndexModel : PageModel
    {
        private readonly BreweriesDbContext _db;

        [TempData]
        public string Message { get; set; }

        public void OnGet()
        {

        }
    }
}