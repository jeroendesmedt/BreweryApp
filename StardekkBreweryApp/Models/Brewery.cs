using System;
using System.Collections.Generic;

namespace StardekkBreweryApp.Models
{
    public partial class Brewery
    {
        public Guid Id { get; set; }
        public Guid BreweryId { get; set; }
        public string Title { get; set; }
        public string Logo { get; set; }
        public string Address { get; set; }
        public string ContactCell { get; set; }
        public int NbOfActivities { get; set; }
    }
}
