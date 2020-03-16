using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Services;

namespace Services.Tests
{
    [TestClass]
    public class PersonServiceTests
    {
        // The test is rather primitive. It only checks the good case.
        // In real life we should check each method of the service separatly and validate them against predefined xml files.
        [TestMethod]
        public void TestAll()
        {
            var persons = new List<Person> {
                new Person
                {
                    FirstName = "John",
                    LastName = "Doe",
                    BirthDate = DateTime.Now,
                    PhoneNumber = "123-456-789",
                    Address = new Address
                    {
                        StreetName = "Long",
                        HouseNumber = "1A",
                        ApartmentNumer = 1,
                        Town = "Poznan",
                        PostalCode = "61000"
                    }
                },
                new Person
                {
                    FirstName = "Jane",
                    LastName = "Doe",
                    BirthDate = DateTime.Now,
                    PhoneNumber = "123-123-123",
                    Address = new Address
                    {
                        StreetName = "Short",
                        HouseNumber = "1",
                        ApartmentNumer = null,
                        Town = "Poznan",
                        PostalCode = "61000"
                    }
                }
            };

            var service = new PersonService();
            service.UpdatePersons(persons);
            var result = service.GetAllPersons().ToList();

            Assert.AreEqual(persons.Count, result.Count);
            Assert.AreEqual(persons.First().FirstName, result.First().FirstName);
            Assert.AreEqual(persons.First().LastName, result.First().LastName);
        }


    }
}
