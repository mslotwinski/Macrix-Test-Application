using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Models.Tests
{
    [TestClass]
    public class PersonTest    
    {
        [TestMethod]
        public void IsValid_NullValues_ReturnsFalse()
        {
            var p = new Person();
            var result = p.IsValid();
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_EmptyStringValues_ReturnsFalse()
        {
            var p = new Person()
            {
                FirstName = "",
                LastName = "",
                PhoneNumber = "",
                BirthDate = DateTime.Now,
                Address = new Address
                {
                   HouseNumber = "",
                   PostalCode = "",
                   StreetName = "",
                   Town = ""
                }
            };
            var result = p.IsValid();
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_WhiteSpacesStringValues_ReturnsFalse()
        {
            var p = new Person()
            {
                FirstName = "  ",
                LastName = "  ",
                PhoneNumber = "  ",
                BirthDate = DateTime.Now,
                Address = new Address
                {
                    HouseNumber = "  ",
                    PostalCode = "  ",
                    StreetName = "  ",
                    Town = "  "
                }
            };
            var result = p.IsValid();
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_CorrectValues_ReturnsTrue()
        {
            var p = new Person()
            {
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "123-456-789",
                BirthDate = DateTime.Now,
                Address = new Address
                {
                    HouseNumber = "1A",
                    PostalCode = "00-000",
                    StreetName = "Test Street",
                    Town = "Test Town"
                }
            };
            var result = p.IsValid();
            Assert.IsTrue(result);
        }
    }
}
