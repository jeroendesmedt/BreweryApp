using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StardekkBreweryApp.Models
{
    public partial class Brewery
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public Guid BreweryId { get; set; }
        public string Title { get; set; }
        public string Logo { get; set; }
        public string Address { get; set; }
        public string ContactCell { get; set; }
        public int NbOfActivities { get; set; }


        private void OnDeserialized()
        {

        }
    }
}
