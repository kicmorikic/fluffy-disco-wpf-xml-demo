using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using wpfApp.Annotations;
using wpfApp.Model;

namespace wpfApp.ViewModel
{
    public class PersonVM : IDataErrorInfo,INotifyPropertyChanged
    {
        internal Person _person;
        
        public int? Id
        {
            get => _person.Id;
        }

        public string FirstName
        {
            get => _person.FirstName;
            set
            {
                if (_person.FirstName.Equals(value)) return;
                _person.FirstName = value;
                OnPropertyChanged(nameof(FirstName));
                ;
            }
        }

        public string LastName
        {
            get => _person.LastName;
            set
            {
                if (_person.LastName.Equals(value)) return;
                _person.LastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string StreetName { get=>_person.StreetName;
            set
            {
                if (_person.StreetName.Equals(value))
                    return;
                _person.StreetName = value;
                OnPropertyChanged(nameof(StreetName));
            }
        } 
        public string HouseNumber
        {
            get => _person.HouseNumber;
            set
            {if (_person.HouseNumber.Equals(value))
                    return;
                _person.HouseNumber = value;
                OnPropertyChanged(nameof(HouseNumber));
            }
        }  //string to accomodate letters in address "number"
        public string? ApartmentNumber
        {
            get => _person.ApartmentNumber;
            set
            {if (_person.ApartmentNumber.Equals(value))
                    return;
                _person.ApartmentNumber = value;
                OnPropertyChanged(nameof(ApartmentNumber));
            }
        } //string to accomodate letters in address "number"
        public string PostalCode
        {
            get => _person.PostalCode;
            set
            {if (_person.PostalCode.Equals(value))
                    return;
                _person.PostalCode = value;
                OnPropertyChanged(nameof(PostalCode));
            }
        }
        public string Town
        {
            get => _person.Town;
            set
            {if (_person.Town.Equals(value))
                    return;
                _person.Town = value;
                OnPropertyChanged(nameof(Town));
            }
        }
        public string PhoneNumber { 
            get=>_person.PhoneNumber;
            set
            {if (_person.PhoneNumber.Equals(value))
                    return;
                _person.PhoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        public DateTime DateOfBirth
        {
            get => _person.DateOfBirth;
            set
            {if (_person.DateOfBirth.Equals(value))
                    return;
                _person.DateOfBirth = value;
                OnPropertyChanged(nameof(DateOfBirth));
                OnPropertyChanged(nameof(Age));
            }
        }

        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - DateOfBirth.Year;
                return DateOfBirth.Date > today.AddYears(age) ? age - 1 : age;
            }
        }

        public PersonVM()
        {
            _person = new Person();
            _person.FirstName = "";
            _person.LastName = "";
            _person.StreetName = "";
            _person.HouseNumber = "";
            _person.ApartmentNumber = "";
            _person.PostalCode = "";
            _person.Town = "";
            _person.PhoneNumber = "";
            _person.DateOfBirth = DateTime.MinValue;
        }
        public PersonVM(Person person)
        {
            this._person = person;


        }

        public string Error { get => $"First name: {FirstName}, LastName: {LastName} has an error"; }

        public string this[string columnName]
        {
            get
            {
                return columnName switch
                {
                    nameof(FirstName) => string.IsNullOrWhiteSpace(FirstName)
                        ? $"{nameof(FirstName)} cannot be empty"
                        : string.Empty,
                    nameof(LastName) => string.IsNullOrWhiteSpace(FirstName)
                        ? $"{nameof(LastName)} cannot be empty"
                        : string.Empty,
                    nameof(StreetName) => string.IsNullOrWhiteSpace(StreetName)
                        ? $"{nameof(StreetName)} cannot be empty"
                        : string.Empty,
                    nameof(HouseNumber) => string.IsNullOrWhiteSpace(HouseNumber)
                        ? $"{nameof(HouseNumber)} cannot be empty"
                        : string.Empty,
                    nameof(PostalCode) => string.IsNullOrWhiteSpace(PostalCode)
                        ? $"{nameof(PostalCode)} cannot be empty"
                        : string.Empty,
                    nameof(Town) => string.IsNullOrWhiteSpace(Town)
                        ? $"{nameof(Town)} cannot be empty"
                        : string.Empty,
                    nameof(PhoneNumber) => string.IsNullOrWhiteSpace(PhoneNumber)
                        ? $"{nameof(PhoneNumber)} cannot be empty"
                        : string.Empty,
                    _ => string.Empty

                };
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
