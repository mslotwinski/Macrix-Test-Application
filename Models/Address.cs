using System.ComponentModel.DataAnnotations;

/* 
 * This class usess DataAnnotations for validation purpose because they are very flexible.
 * I prefer this appraoch because it might be used by Entity Framework and ASP MVC 
 */

namespace Models
{
    public class Address
    {
        [Required(AllowEmptyStrings = false)]
        public string StreetName { get; set; }

        //House number is type of string because we could have address like "Elm Street 1A"
        [Required(AllowEmptyStrings = false)]
        public string HouseNumber { get; set; }

        // we could additionally validate it with RegularExpression attribute
        [Required(AllowEmptyStrings = false)]
        public string PostalCode { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Town { get; set; }

        public int? ApartmentNumer { get; set; }
    }
}
