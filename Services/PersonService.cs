using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using IServices;

namespace Services
{
    /// <summary>
    /// Service used to acquire and modify Person data  
    /// </summary>
    public class PersonService : IPersonService
    {
        //The target file path should be stored in configuration files or selected directly by the user        
        private readonly string _filename = "persons.xml";
        public IEnumerable<Person> GetAllPersons()
        {
            return ReadFromXml();
        }

        public void UpdatePersons(IEnumerable<Person> persons)
        {
            if (persons.Any(p => p.IsValid() == false))
            {
                throw new Exception("Cannot save invalid data");
            }
            WriteToXml(persons);
        }

        //In real life this code should be moved to separate repository class, so we could easily change the storeage type, eg. to a database        
        #region XML serialization
        protected IEnumerable<Person> ReadFromXml()
        {
            List<Person> result;

            XmlSerializer serializer = new XmlSerializer(typeof(List<Person>));
            using (FileStream fs = new FileStream(GetFilePath(), FileMode.OpenOrCreate))
            {
                if (fs.Length == 0)
                {
                    // New file or all elements has been deleted
                    result = new List<Person>();
                }
                else
                {
                    result = (List<Person>)serializer.Deserialize(fs);
                    fs.Close();
                }
            }

            return result;
        }

        protected void WriteToXml(IEnumerable<Person> persons)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Person>));
            //the argument 'append' is set to false to make sure we overrite the date insed of appending them
            using (TextWriter writer = new StreamWriter(GetFilePath(), false))
            {
                serializer.Serialize(writer, persons.ToList());
                writer.Close();
            }
        }
        #endregion

        #region File

        protected string GetFilePath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), _filename);
        }
        #endregion
    }
}
