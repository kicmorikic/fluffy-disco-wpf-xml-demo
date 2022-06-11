using wpfApp.Model;

namespace wpfApp_Tests;

public class XMLManipulationTests_data
{
    public static Dictionary<string, string> XmlSampleData = new Dictionary<string, string>()
    {
        {
            "FileWithNoEntries",
            @"<?xml version=""1.0"" encoding=""utf-8""?><PeopleList xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema""><People /></PeopleList>"
        },
        {
            "FileWithPerson1Only",
            @"<?xml version=""1.0"" encoding=""utf-8""?><PeopleList xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema""><People><Person><FirstName>fname</FirstName><LastName>lname</LastName><StreetName>street</StreetName><HouseNumber>1</HouseNumber><ApartmentNumber>1</ApartmentNumber><PostalCode>12345</PostalCode><Town>town</Town><PhoneNumber>123456789</PhoneNumber><DateOfBirth>2000-01-01T00:00:00</DateOfBirth></Person></People></PeopleList>"
        }
    };
    public static Dictionary<string, Person> testPersons = new Dictionary<string, Person>()
    {
        {
            "person1", new Person()
            {
                FirstName = "fname", LastName = "lname", StreetName = "street", HouseNumber = "1",
                ApartmentNumber = "1", Town = "town", DateOfBirth = DateTime.Parse("2000-01-01"),
                PhoneNumber = "123456789", PostalCode = "12345"
            }
        },
        {
            "person2_nullApt", new Person()
            {
                FirstName = "Stefan", LastName = "Nafets", StreetName = "Oak avenue", HouseNumber = "15A",
                ApartmentNumber = null, Town = "Rounding Error", DateOfBirth = DateTime.Parse("2000-05-01"),
                PhoneNumber = "555058542", PostalCode = "88888"
            }
        },
        {
            "person2_setApt", new Person()
            {
                FirstName = "Stefan", LastName = "Nafets", StreetName = "Oak avenue", HouseNumber = "15A",
                ApartmentNumber = "A", Town = "Rounding Error", DateOfBirth = DateTime.Parse("2000-05-01"),
                PhoneNumber = "555058542", PostalCode = "88888"
            }
        },
        {
            "person3", new Person()
            {
                FirstName = "Lucy", LastName = "Ycul", StreetName = "street", HouseNumber = "1",
                ApartmentNumber = "1", Town = "town", DateOfBirth = DateTime.Parse("2000-01-01"),
                PhoneNumber = "123456789", PostalCode = "12345"
            }
        }
    };

}