using wpfApp.Model;
using wpfApp.Repository;

namespace wpfApp_Tests
{
    public class XMLManipulationTests
    {
        private const string TestFilenameSuffix = "_testdata.xml";
        private readonly string TestFilenamePrefix = $".{Path.DirectorySeparatorChar}";
        
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
            string expectedXmlContent = TestData.XmlSampleData[TestData.Xml.FileWithNoEntries];
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
            string expectedXmlContent = TestData.XmlSampleData[TestData.Xml.FileWithPerson1Only];
            EnsureFileIsDeleted(filename);
            PeopleRepository SUT = new PeopleRepository(filename);

            //when
            SUT.Load();
            SUT.Insert(TestData.testPeople[TestData.persEnum.person1]);
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
                , TestData.XmlSampleData[TestData.Xml.FileWithPerson1Only]);
            var expectedPerson = TestData.testPeople[TestData.persEnum.person1];


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
                , TestData.XmlSampleData[TestData.Xml.FileWith2People]);
            
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
                , TestData.XmlSampleData[TestData.Xml.FileWith2People]);
            PeopleRepository SUT = new PeopleRepository(filename);
            string expectedname = "fnameUpdated";
            DateTime expectedDateOfBirth = DateTime.MinValue;
            

            //when
            var personToUpdate = SUT.GetPersonById(1);
            personToUpdate.FirstName = expectedname;
            personToUpdate.DateOfBirth = expectedDateOfBirth;
            SUT.Update(personToUpdate);
            SUT.Save();
            //then
            var actualContent = File.ReadAllText(filename);
            Assert.Equal(TestData.XmlSampleData[TestData.Xml.FileWith2PeopleFirstUpdated]
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