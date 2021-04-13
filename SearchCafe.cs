using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CafeFinder.Models
{
    public class SearchCafe
    {
        [Required]
        public string ZipCode { get; set; }
    }
}
