using Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;
using IServices;
using Services;
using System.Collections.Generic;

namespace ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        /// <summary>
        /// The service required to fetch and update data
        /// </summary>
        private IPersonService _service;

        /// <summary>
        /// The collection of PersonViewModels
        /// </summary>
        public ObservableCollection<PersonViewModel> Persons { get; set; }

        /// <summary>
        /// Indicates if data has been modified
        /// </summary>
        public bool IsModified { get; set; }

        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        // Injecting service instance. In real life it should be injected by some sort of IoC container, e.g. SimpleInjector
        public MainViewModel() : this(new PersonService())
        {
        }

        public MainViewModel(IPersonService service)
        {
            _service = service;
            SaveCommand = new RelayCommand(p => SaveData());
            CancelCommand = new RelayCommand(p => LoadData());
            LoadData();
        }

        public void SaveData()
        {
            // Preparing data to be saved
            var dataToSave = new List<Person>();
            foreach (var p in Persons)
            {
                var person = new Person
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    BirthDate = p.BirthDate,
                    PhoneNumber = p.PhoneNumber,
                    Address = new Address
                    {
                        HouseNumber = p.HouseNumber,
                        StreetName = p.StreetName,
                        PostalCode = p.PostalCode,
                        Town = p.Town,
                        ApartmentNumer = p.ApartmentNumer
                    }
                };
          
                dataToSave.Add(person);
            }

            _service.UpdatePersons(dataToSave);
            IsModified = false;
        }

        public void LoadData()
        {
            var loadedData = _service.GetAllPersons();

            // Creating new collection to dispose of old values
            Persons = new ObservableCollection<PersonViewModel>();

            // Filling collection with view models
            foreach (var person in loadedData)
            {
                var personViewModel = new PersonViewModel
                {
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    PhoneNumber = person.PhoneNumber,
                    BirthDate = person.BirthDate,
                    StreetName = person.Address.StreetName,
                    HouseNumber = person.Address.HouseNumber,
                    ApartmentNumer = person.Address.ApartmentNumer,
                    PostalCode = person.Address.PostalCode,
                    Town = person.Address.Town,
                };

                personViewModel.PropertyChanged += OnItemPropertyChanged;
                Persons.Add(personViewModel);
            }

            Persons.CollectionChanged += OnCollectionChanged;
            IsModified = false;
        }


        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            IsModified = true;
        }

        void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            IsModified = true;
        }
    }
}
