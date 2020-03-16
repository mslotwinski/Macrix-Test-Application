using Models;
using System.Collections.Generic;

namespace IServices
{
    public interface IPersonService
    {
        IEnumerable<Person> GetAllPersons();
        void UpdatePersons(IEnumerable<Person> persons);
    }
}
    