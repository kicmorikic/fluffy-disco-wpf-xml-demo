using wpfApp.Model;
using wpfApp.Repository;

namespace wpfApp_Tests
{
    public class XMLManipulationTests
    {
        private const string TestFilenameSuffix = "_testdata.xml";
        private string TestFilenamePrefix = $".{Path.DirectorySeparatorChar}";
        
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
            string expectedXmlContent = XMLManipulationTests_data.XmlSampleData["FileWithNoEntries"];
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
            string expectedXmlContent = XMLManipulationTests_data.XmlSampleData["FileWithPerson1Only"];
            EnsureFileIsDeleted(filename);
            PeopleRepository SUT = new PeopleRepository(filename);

            //when
            SUT.Load();
            SUT.Insert(XMLManipulationTests_data.testPersons["person1"]);
            SUT.Save();
            //then
            var actualContent = File.ReadAllText(filename);
            Assert.Equal(expectedXmlContent, actualContent);
        }

        [Fact]
        public void OpenExistingRepository_GetAll_ReturnsSinglePerson()
        {
            //given
            string filename = GetPath(nameof(OpenNonExistingRepository_AddThenSave_SavesXmlWithSinglePerson));
            EnsureFileExistsWithSpecificContent(filename
                , XMLManipulationTests_data.XmlSampleData["FileWithPerson1Only"]);
            var expectedPerson = XMLManipulationTests_data.testPersons["person1"];
            PeopleRepository SUT = new PeopleRepository(filename);

            //when
            SUT.Load();
            //then
            var result = SUT.GetAllPeople();
            Assert.Collection(result, item =>
            {
                Assert2PeopleEqual(expectedPerson, item);
            });
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