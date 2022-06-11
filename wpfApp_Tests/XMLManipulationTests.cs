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




        private void EnsureFileIsDeleted(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private string GetPath(string functionName)
        {
            return $"{TestFilenamePrefix}{functionName}{TestFilenameSuffix}";
        }

        
    }
}