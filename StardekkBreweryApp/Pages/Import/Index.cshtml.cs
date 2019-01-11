﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using StardekkBreweryApp.Models;

namespace StardekkBreweryApp.Pages.Import
{
    public class IndexModel : PageModel
    {
        private readonly BreweriesDbContext _db;

        private readonly IHttpClientFactory _clientFactory;

        [TempData]
        public string Message { get; set; }

        [BindProperty]
        public ImportSettings Settings { get; set; }

        public IndexModel(IHttpClientFactory cf, BreweriesDbContext db)
        {
            _db = db;
            _clientFactory = cf;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                Message = "Not all settings are valid.";

                return Page();
            }

            var success = await ImportBreweries(Settings);

            Message = success ? "Database imported successfully" : "Could not import database.";

            return RedirectToPage("/Breweries/Index");

        }

        private async Task<bool> ImportBreweries(ImportSettings settings)
        {
            if (settings.ClearDatabase)
            {
                var clearSuccesfull = ClearDatabase();

                if (!clearSuccesfull)
                {
                    Message = "Could not clear the database.";
                    return false;
                }
            }

            var response = await GetResponseFromUnibooker(settings.ApiKey, settings.Language);

            if (!response.IsSuccessStatusCode)
            {
                Message = "Could not retrieve breweries from Unibooker";
                return false;
            }

            var result = await response.Content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(result);

            var jsonResponses = jsonObject["Response"];

            IList<Brewery> importedBreweries = jsonResponses.Select(r => new Brewery
            {
                Id = Guid.NewGuid(), // TODO should it be auto-generated by EF ?
                BreweryId = (Guid)r["ID"],
                Title = (string)r["Title"],
                Logo = (string)r["Logo"],
                Address = (string)r["AddressCard"]["Street"] + " " + r["AddressCard"]["Number"] + " " + r["AddressCard"]["Box"] + ", " +
                    r["AddressCard"]["Postal"] + " " + r["AddressCard"]["City"] + ", " + r["AddressCard"]["Country"], // TODO: create address builder
                ContactCell = (string)r["ContactCard"]["Cellphone"],
                NbOfActivities = r["Activities"].Count()
            }).ToList();


            await _db.Brewery.AddRangeAsync(importedBreweries);
            await _db.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Clears the entire database
        /// </summary>
        /// <returns>True if it works</returns>
        private bool ClearDatabase()
        {
            _db.Brewery.RemoveRange(_db.Brewery); // TODO: might be better way to do this.

            return true;
        }

        /// <summary>
        /// Calls the Unibooker API and returns the breweries
        /// </summary>
        /// <param name="apiKey">Api Key for authentorization</param>
        /// <param name="language">ISO code language</param>
        /// <returns></returns>
        private async Task<HttpResponseMessage> GetResponseFromUnibooker(string apiKey, string language)
        {
            // TODO: put base api uri in appsettings
            // TODO: create uri builder that builds uri from settings
            var requestUri = "packages/filter?apiKey=" + apiKey + "&language=" + language;
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var client = _clientFactory.CreateClient("Unibooker");

            var response = await client.SendAsync(request);
            return response;
        }
    }
}