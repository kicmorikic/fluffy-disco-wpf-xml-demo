using wpfApp.Model;
using wpfApp.Repository;

namespace wpfApp_Tests
{
    public class XMLManipulationTests
    {
        private const string TestFilenameSuffix = "_testdata.xml";
        private readonly string TestFilenamePrefix = $".{Path.DirectorySeparatorChar}";
        private TestData data = new TestData();
        
        [Fact]
        public void OpenNonExisting_GetAllPeople_ReturnsEmptyList()
        {
            //given
            string filename= GetPath(nameof(OpenNonExisting_GetAllPeople_ReturnsEmptyList));
            EnsureFileIsDeleted(filename);

            //when
            var SUT = new PeopleRepository(filename);
            var resultList = SUT.GetAllPeople();
            //then
            Assert.Empty(resultList);
        }

        [Fact]
        public void OpenNonExistingRepository_Save_SavesXmlWithNoPeople()
        {
            //given
            string filename = GetPath(nameof(OpenNonExistingRepository_Save_SavesXmlWithNoPeople));
            string expectedXmlContent = data.XmlSampleData[TestData.Xml.FileWithNoEntries];
            EnsureFileIsDeleted(filename);
            PeopleRepository SUT = new PeopleRepository(filename);
            
            //when
            SUT.Load();
            SUT.Save();
            //then
            var actualContent = File.ReadAllText(filename);
            Assert.Equal(expectedXmlContent,actualContent);
        }
        [Fact]
        public void OpenNonExistingRepository_AddThenSave_SavesXmlWithSinglePerson()
        {
            //given
            string filename = GetPath(nameof(OpenNonExistingRepository_AddThenSave_SavesXmlWithSinglePerson));
            string expectedXmlContent = data.XmlSampleData[TestData.Xml.FileWithPerson1Only];
            EnsureFileIsDeleted(filename);
            PeopleRepository SUT = new PeopleRepository(filename);

            //when
            SUT.Load();
            SUT.Insert(data.testPeople[TestData.persEnum.person1]);
            SUT.Save();
            //then
            var actualContent = File.ReadAllText(filename);
            Assert.Equal(expectedXmlContent, actualContent);
        }

        [Fact]
        public void OpenExistingRepository_GetAll_ReturnsSinglePerson()
        {
            //given
            string filename = GetPath(nameof(OpenExistingRepository_GetAll_ReturnsSinglePerson));
            EnsureFileExistsWithSpecificContent(filename
                , data.XmlSampleData[TestData.Xml.FileWithPerson1Only]);
            var expectedPerson = data.testPeople[TestData.persEnum.person1];


            //when
            PeopleRepository SUT = new PeopleRepository(filename);
            //then
            var result = SUT.GetAllPeople();
            Assert.Collection(result, item =>
            {
                Assert2PeopleEqual(expectedPerson, item);
            });
        }
        [Fact]
        public void ExistingRepositoryWithMultiplePeople_GetAll_ReturnsEveryPersonWithId()
        {
            //given
            string filename = GetPath(nameof(ExistingRepositoryWithMultiplePeople_GetAll_ReturnsEveryPersonWithId));
            EnsureFileExistsWithSpecificContent(filename
                , data.XmlSampleData[TestData.Xml.FileWith2People]);
            
            //when
            PeopleRepository SUT = new PeopleRepository(filename);
            //then
            var result = SUT.GetAllPeople();
            Assert.Collection(result, 
                    item => Assert.Equal(1,item.Id),
                    item => Assert.Equal(2,item.Id)
                    );
        }
        [Fact]
        public void ExistingRepositoryWithMultiplePeople_UpdateSave_SavesUpdatedPerson()
        {
            //given
            string filename = GetPath(nameof(ExistingRepositoryWithMultiplePeople_UpdateSave_SavesUpdatedPerson));
            EnsureFileExistsWithSpecificContent(filename
                , data.XmlSampleData[TestData.Xml.FileWith2People]);
            PeopleRepository SUT = new PeopleRepository(filename);
            string newName = "fnameUpdated";
            DateTime newDateOfBirth = DateTime.MinValue;
            

            //when
            var personToUpdate = SUT.GetPersonById(1);
            personToUpdate.FirstName = newName;
            personToUpdate.DateOfBirth = newDateOfBirth;
            SUT.Save();
            //then
            var actualContent = File.ReadAllText(filename);
            Assert.Equal(data.XmlSampleData[TestData.Xml.FileWith2PeopleFirstUpdated]
                , actualContent);
        }
        [Fact]
        public void EmptyRepository_InsertUpdateSave_SavesUpdatedPerson()
        {
            //given
            string filename = GetPath(nameof(EmptyRepository_InsertUpdateSave_SavesUpdatedPerson));
            EnsureFileIsDeleted(filename);
            string newName = "fnameUpdated";
            var SUT = new PeopleRepository(filename);
            var personToAddAndUpdate = data.testPeople[TestData.persEnum.person1];


            //when
            SUT.Insert(personToAddAndUpdate);
            personToAddAndUpdate.FirstName = newName;
            
            SUT.Save();
            //then
            var actualContent = File.ReadAllText(filename);
            Assert.Equal(data.XmlSampleData[TestData.Xml.FileWithPerson1OnlyUpdated]
                , actualContent);
        }
        [Theory]
        [InlineData(TestData.Xml.FileWithNoEntries, 1)]
        [InlineData(TestData.Xml.FileWith2People, 3)]
        public void ExistingRepository_Insert_ReturnsCorrectId(TestData.Xml repoIdXml, int expectedId)
        {
            //given
            string filename = GetPath(nameof(ExistingRepository_Insert_ReturnsCorrectId)+ repoIdXml);
            EnsureFileExistsWithSpecificContent(filename, data.XmlSampleData[repoIdXml]);
            
            var SUT = new PeopleRepository(filename);
            var personToInsert = data.testPeople[TestData.persEnum.person1];


            //when
            var result = SUT.Insert(personToInsert);

            
            //then
            Assert.Equal(expectedId, result);
        }
        [Fact]
        public void OpenExistingRepository_Delete_SavesEmptyXml()
        {
            //given
            string filename = GetPath(nameof(OpenExistingRepository_Delete_SavesEmptyXml));
            EnsureFileExistsWithSpecificContent(filename
                , data.XmlSampleData[TestData.Xml.FileWith2People]);


            //when
            PeopleRepository SUT = new PeopleRepository(filename);
            foreach (var person in SUT.GetAllPeople())
            {
                SUT.Delete(person.Id);
            }
            SUT.Save();
            //then
            var actualContent = File.ReadAllText(filename);
            Assert.Equal(data.XmlSampleData[TestData.Xml.FileWithNoEntries]
                , actualContent);
        }

        private static void EnsureFileIsDeleted(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private static void EnsureFileExistsWithSpecificContent(string path, string content)
        {
            EnsureFileIsDeleted(path);
            TextWriter writer  = File.CreateText(path);
            writer.Write(content.ToCharArray());
            writer.Flush();
            writer.Close();
            
        }

        private string GetPath(string functionName)
        {
            return $"{TestFilenamePrefix}{functionName}{TestFilenameSuffix}";
        }

        private static void Assert2PeopleEqual(Person expectedPerson, Person ActualPerson)
        {
            Assert.Equal(expectedPerson.FirstName, ActualPerson.FirstName);
            Assert.Equal(expectedPerson.LastName, ActualPerson.LastName);
            Assert.Equal(expectedPerson.StreetName, ActualPerson.StreetName);
            Assert.Equal(expectedPerson.PostalCode, ActualPerson.PostalCode);
            Assert.Equal(expectedPerson.PhoneNumber, ActualPerson.PhoneNumber);
            Assert.Equal(expectedPerson.Town, ActualPerson.Town);
            Assert.Equal(expectedPerson.HouseNumber, ActualPerson.HouseNumber);
            Assert.Equal(expectedPerson.ApartmentNumber, ActualPerson.ApartmentNumber);
            Assert.Equal(expectedPerson.DateOfBirth, ActualPerson.DateOfBirth);
        }
    }
}