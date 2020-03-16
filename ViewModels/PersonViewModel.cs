using System;
using System.ComponentModel;

namespace ViewModels
{
    /* 
     * This class uses IDataErrorInfo for validation purpose, but we could use as well custom validation rules.
     * I choosed this approach because I am more familiar with it.
     */

    public class PersonViewModel : BaseViewModel, IDataErrorInfo
    {
        private const string EMPTY_FIELD = "Filed can not be empty";

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; } = DateTime.Today;
        public string StreetName { get; set; }
        //House number is type of string because we could have address like "Elm Street 1A"
        public string HouseNumber { get; set; }
        public int? ApartmentNumer { get; set; }
        public string PostalCode { get; set; }
        public string Town { get; set; }

        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - BirthDate.Year;
                // Go back to the year the person was born in case of a leap year
                if (BirthDate.Date > today.AddYears(-age)) age--;

                return age;
            }
        }

        public string Error => null;
        
        public string this[string name]
        {
            get
            {
                string result = null;

                // It might be more generic by using reflections, but this way should be efficient enough. 
                // Moreover, we could easily apply additional conditions later.
                switch (name)
                {
                    case nameof(FirstName):
                        if (string.IsNullOrWhiteSpace(FirstName)) result = EMPTY_FIELD;
                        break;
                    case nameof(LastName):
                        if (string.IsNullOrWhiteSpace(LastName)) result = EMPTY_FIELD;
                        break;
                    case nameof(PhoneNumber):
                        // we could also check if the value is correct by using regular expressions
                        if (string.IsNullOrWhiteSpace(PhoneNumber)) result = EMPTY_FIELD;
                        break;
                    case nameof(StreetName):
                        if (string.IsNullOrWhiteSpace(StreetName)) result = EMPTY_FIELD;
                        break;
                    case nameof(HouseNumber):
                        if (string.IsNullOrWhiteSpace(HouseNumber)) result = EMPTY_FIELD;
                        break;
                    case nameof(Town):
                        if (string.IsNullOrWhiteSpace(Town)) result = EMPTY_FIELD;
                        break;
                    case nameof(PostalCode):
                        if (string.IsNullOrWhiteSpace(PostalCode)) result = EMPTY_FIELD;
                        break;
                    case nameof(BirthDate):
                        // minimal date - no one lives more than 150 years (at least not yet)
                        var minDate = DateTime.Now.AddYears(-150).Date;
                        if (BirthDate < minDate || BirthDate > DateTime.Now.Date)
                        {
                            result = $"Invalid Date of birth. Please choose date between {minDate.ToShortDateString()} and today";
                        }
                        break;
                }

                return result;
            }
        }
    }
}