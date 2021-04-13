using System.Collections.Generic;
namespace CafeFinder.Models
{
    public class HomeModelBindingViewModel
    {
        public SearchCafe Thing { get; set; }
        public bool HasErrors { get; set; }
        public IEnumerable<string>
        ValidationErrors
        { get; set; }
    }
}