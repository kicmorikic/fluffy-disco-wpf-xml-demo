using wpfApp.Model;

namespace wpfApp_Tests;

public class TestData
{
    public enum Xml
    {
        FileWithNoEntries,
        FileWithPerson1Only,
        FileWith2People
    }
    public static Dictionary<Xml, string> XmlSampleData = new Dictionary<Xml, string>()
    {
        {
            Xml.FileWithNoEntries,
            @"<?xml version=""1.0"" encoding=""utf-8""?><PeopleList xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema""><People /></PeopleList>"
        },
        {
            Xml.FileWithPerson1Only,
            @"<?xml version=""1.0"" encoding=""utf-8""?><PeopleList xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema""><People><Person><FirstName>fname</FirstName><LastName>lname</LastName><StreetName>street</StreetName><HouseNumber>1</HouseNumber><ApartmentNumber>1</ApartmentNumber><PostalCode>12345</PostalCode><Town>town</Town><PhoneNumber>123456789</PhoneNumber><DateOfBirth>2000-01-01T00:00:00</DateOfBirth></Person></People></PeopleList>"
        },
        {
            Xml.FileWith2People,
            @"<?xml version=""1.0"" encoding=""utf-8""?><PeopleList xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema""><People><Person><FirstName>fname</FirstName><LastName>lname</LastName><StreetName>street</StreetName><HouseNumber>1</HouseNumber><ApartmentNumber>1</ApartmentNumber><PostalCode>12345</PostalCode><Town>town</Town><PhoneNumber>123456789</PhoneNumber><DateOfBirth>2000-01-01T00:00:00</DateOfBirth></Person><Person><FirstName>fname2</FirstName><LastName>lname2</LastName><StreetName>street2</StreetName><HouseNumber>12</HouseNumber><ApartmentNumber>12</ApartmentNumber><PostalCode>123452</PostalCode><Town>town2</Town><PhoneNumber>1234567892</PhoneNumber><DateOfBirth>2000-01-02T00:00:00</DateOfBirth></Person></People></PeopleList>"
        }
    };

    public enum persEnum
    {
        person1,
        person2_nullApt,
        person2_setApt,
        person3

    }
    public static Dictionary<persEnum, Person> testPeople = new Dictionary<persEnum, Person>()
    {
        {
            persEnum.person1, new Person()
            {
                FirstName = "fname", LastName = "lname", StreetName = "street", HouseNumber = "1",
                ApartmentNumber = "1", Town = "town", DateOfBirth = DateTime.Parse("2000-01-01"),
                PhoneNumber = "123456789", PostalCode = "12345"
            }
        },
        {
            persEnum.person2_nullApt, new Person()
            {
                FirstName = "Stefan", LastName = "Nafets", StreetName = "Oak avenue", HouseNumber = "15A",
                ApartmentNumber = null, Town = "Rounding Error", DateOfBirth = DateTime.Parse("2000-05-01"),
                PhoneNumber = "555058542", PostalCode = "88888"
            }
        },
        {
            persEnum.person2_setApt, new Person()
            {
                FirstName = "Stefan", LastName = "Nafets", StreetName = "Oak avenue", HouseNumber = "15A",
                ApartmentNumber = "A", Town = "Rounding Error", DateOfBirth = DateTime.Parse("2000-05-01"),
                PhoneNumber = "555058542", PostalCode = "88888"
            }
        },
        {
            persEnum.person3, new Person()
            {
                FirstName = "Lucy", LastName = "Ycul", StreetName = "street", HouseNumber = "1",
                ApartmentNumber = "1", Town = "town", DateOfBirth = DateTime.Parse("2000-01-01"),
                PhoneNumber = "123456789", PostalCode = "12345"
            }
        }
    };

}