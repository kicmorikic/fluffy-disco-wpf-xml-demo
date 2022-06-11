using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using wpfApp.Model;

namespace wpfApp.Repository;

public interface IPeopleRepository
{
    void Load();
    void Save();
    void Update(Person updatedPerson);
    void Delete(Person personToDelete);
    void Insert(Person personToInsert);
    Person GetPersonById(int personId);
    IEnumerable<Person> GetAllPeople();
    

}