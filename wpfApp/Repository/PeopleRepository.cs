using System;
using System.Collections.Generic;
using System.IO;
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
        
        var writer = File.OpenWrite(_xmlPath);
        _serializer.Serialize(writer, _peopleList);
        writer.Close();
        
    }

    public void Update(Person updatedPerson)
    {
        throw new System.NotImplementedException();
    }

    public void Delete(Person personToDelete)
    {
        throw new System.NotImplementedException();
    }

    public void Insert(Person personToInsert)
    {
        _peopleList.People.Add(personToInsert);
    }

    public Person GetPersonById(int personId)
    {
        throw new System.NotImplementedException();
    }

    public IEnumerable<Person> GetAllPeople()
    {
        return _peopleList.People;
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