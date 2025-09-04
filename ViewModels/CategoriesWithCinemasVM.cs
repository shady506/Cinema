using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Cinema.ViewModels
{
    public class CategoriesWithCinemasVM
    {
        [ValidateNever]
        public List<Categories> Categories { get; set; } = null!;
        [ValidateNever]
        public List<Cinemas> Cinemas{ get; set; } = null!;

        public Movies Movie { get; set; } = null!;
    }
}
