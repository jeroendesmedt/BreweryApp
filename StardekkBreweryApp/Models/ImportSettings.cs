using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StardekkBreweryApp.Models
{
    public class ImportSettings
    {
        public bool ClearDatabase { get; set; }

        [Required]
        public string ApiKey { get; set; }

        [Required]
        public string Language { get; set; }

        public int NbOfFields { get; set; } = 0;
    }
}
