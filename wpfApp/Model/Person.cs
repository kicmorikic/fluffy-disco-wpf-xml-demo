using System;
using System.Xml.Serialization;

namespace wpfApp.Model;

[XmlType("Person")]
public class Person
{
    [XmlIgnore]
    public int Id { get; set; }

    [XmlElement] public string FirstName { get; set; } = "";
    [XmlElement]
    public string LastName { get; set; } = "";
    [XmlElement]
    public string StreetName { get; set; } = "";
    [XmlElement]
    public string HouseNumber { get; set; } = ""; //string to accomodate letters in address "number"
    [XmlElement(IsNullable = true)]
    public string? ApartmentNumber { get; set; } //string to accomodate letters in address "number"
    [XmlElement]
    public string PostalCode { get; set; } = "";
    [XmlElement]
    public string Town { get; set; } = "";
    [XmlElement]
    public string PhoneNumber { get; set; } = "";
    [XmlElement]
    public DateTime DateOfBirth { get; set; }= DateTime.MinValue;
    
}