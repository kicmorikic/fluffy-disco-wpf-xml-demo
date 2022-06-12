using System.Collections.Generic;
using System.Windows.Documents;
using System.Xml.Serialization;

namespace wpfApp.Model;

[XmlRoot("PeopleList")]
[XmlInclude(typeof(Person))]
public class PeopleList
{
    [XmlArray("People")]
    [XmlArrayItem("Person")]
    public List<Person> People { get; private set; } = new List<Person>();


}