using System;
using System.ComponentModel.DataAnnotations;

/* 
 * This class usess DataAnnotations for validation purpose because they are very flexible.
 * I prefer this appraoch because it might be used by Entity Framework and ASP MVC 
 */

namespace Models
{
    public class Person
    {
        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }
       
        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }
       
        [Required]
        public Address Address { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        public string PhoneNumber { get; set; }        
        
        // We could use Range attribute here, but it accepts only static values. 
        // The solution might be a new attribute derived from RangeAttribute that has a years offset argument
        [Required]        
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Validates the Person class instance
        /// </summary>
        /// <returns>Bollean validation result</returns>
        public bool IsValid()
        {
            return Validator.TryValidateObject(this, new ValidationContext(this), null) 
                && Validator.TryValidateObject(this.Address, new ValidationContext(this.Address), null);
        }
    }
}
