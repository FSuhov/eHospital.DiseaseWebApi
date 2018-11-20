using EHospital.Diseases.BusinessLogic.Services;
using EHospital.Diseases.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace EHospital.Diseases.Tests
{
    [TestClass]
    public class DiseasesTests
    {
        private static Mock<IRepository<Disease>> _mockRepo;
        private static Mock<IUniteOfWork> _mockData;
        private List<Disease> _diseasesList;

        [TestInitialize]
        public void Initialize()
        {
            _mockRepo = new Mock<IRepository<Disease>>();
            _mockData = new Mock<IUniteOfWork>();
            _mockData.Setup(s => s.Diseases).Returns(_mockRepo.Object);
            _diseasesList = new List<Disease>()
            {
                new Disease(){DiseaseId = 1, CategoryId = 1, Name = "Test Disease Name 1", Description = "Test Disease Name 1 Description Text", IsDeleted = false},
                new Disease(){DiseaseId = 2, CategoryId = 1, Name = "Test Disease Name 2", Description = "Test Disease Name 2 Description Text", IsDeleted = false},
                new Disease(){DiseaseId = 3, CategoryId = 1, Name = "Test Disease Name 3", Description = "Test Disease Name 3 Description Text", IsDeleted = false}
            };
        }

        [TestMethod]
        public void GetAllDiseasesTest()
        {
            // Arrange
            _mockData.Setup(q => q.Diseases.GetAll()).Returns(_diseasesList.AsQueryable);

            // Act
            var actual = new DiseaseService(_mockData.Object).GetAllDiseases().ToList();

            // Assert
            Assert.AreEqual(_diseasesList.Count, actual.Count);
            Assert.AreEqual(_diseasesList[0].Name, actual[0].Name);
            Assert.AreEqual(_diseasesList[1].Name, actual[1].Name);
            Assert.AreEqual(_diseasesList[2].Name, actual[2].Name);
        }
    }
}
