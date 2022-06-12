using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using wpfApp.Model;

namespace wpfApp.Repository;

public class PeopleRepository : IPeopleRepository
{
    private string _xmlPath;
    private PeopleList _peopleList = new();
    private readonly XmlSerializer _serializer = new (typeof(PeopleList));

    public PeopleRepository(string xmlPath)
    {
        _xmlPath = xmlPath;
        Load();
    }

    public void Load()
    {
        if (File.Exists(_xmlPath))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(PeopleList));
            using (FileStream stream = File.OpenRead(_xmlPath))
            {
                _peopleList = (PeopleList)serializer.Deserialize(stream);
            }
            AddIds();
            

        }
        else
        {
            _peopleList = new PeopleList();
        }
    }
    
    public void Save()
    {
        var writer = File.Create(_xmlPath);
        _serializer.Serialize(writer, _peopleList);
        writer.Close();
        
    }

    

    public void Delete(int id)
    {
        _peopleList.People.RemoveAll(item => item.Id == id);
    }

    public int Insert(Person personToInsert)
    {
        int nextid = _peopleList.People.Count>0?_peopleList.People.Max(p => p.Id)+1:1;
        personToInsert.Id= nextid;
        _peopleList.People.Add(personToInsert);
        return nextid;
    }

    public Person GetPersonById(int personId)
    {
        return _peopleList.People.Single(p => p.Id == personId);
    }

    public IEnumerable<Person> GetAllPeople()
    {
        return _peopleList.People.ToList();
    }

    private void AddIds()
    {
        int id = 1;
        foreach (Person person in _peopleList.People)
        {
            person.Id=id;
            id++;
        }
    }

    
}