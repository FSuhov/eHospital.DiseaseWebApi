using EHospital.Diseases.BusinessLogic.Services;
using EHospital.Diseases.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EHospital.Diseases.Tests
{
    [TestClass]
    public class DiseasesTests
    {
        private static Mock<IRepository<Disease>> _mockRepo;
        private static Mock<IUniteOfWork> _mockData;
        private List<Disease> _diseasesList;
        private List<DiseaseCategory> _categoriesList;

        [TestInitialize]
        public void Initialize()
        {
            _mockRepo = new Mock<IRepository<Disease>>();
            _mockData = new Mock<IUniteOfWork>();
            _mockData.Setup(s => s.Diseases).Returns(_mockRepo.Object);
            _diseasesList = new List<Disease>()
            {
                new Disease(){Id = 1, CategoryId = 1, Name = "Test Disease Name 1", Description = "Test Disease Name 1 Description Text", IsDeleted = false},
                new Disease(){Id = 2, CategoryId = 1, Name = "Test Disease Name 2", Description = "Test Disease Name 2 Description Text", IsDeleted = false},
                new Disease(){Id = 3, CategoryId = 2, Name = "Test Disease Name 3", Description = "Test Disease Name 3 Description Text", IsDeleted = false}
            };

            _categoriesList = new List<DiseaseCategory>
            {
                new DiseaseCategory(){Id = 1, Name = "Test Category 1", IsDeleted = false},
                new DiseaseCategory(){Id = 2, Name = "Test Category 2", IsDeleted = false}
            };
        }

        [TestMethod]
        public void GetAllDiseasesTest()
        {
            // Arrange
            _mockData.Setup(q => q.Diseases.GetAll()).Returns(_diseasesList.AsQueryable);

            // Act
            var actual = new DiseaseService(_mockData.Object).GetAllDiseases().Result.ToList();

            // Assert
            Assert.AreEqual(_diseasesList.Count, actual.Count);
            Assert.AreEqual(_diseasesList[0].Name, actual[0].Name);
            Assert.AreEqual(_diseasesList[1].Name, actual[1].Name);
            Assert.AreEqual(_diseasesList[2].Name, actual[2].Name);
        }

        [TestMethod]
        [DataRow(1)]
        public void GetDiseasesByCategoryTest(int categoryId)
        {
            // Arrange
            _mockData.Setup(q => q.Diseases.GetAll()).Returns(_diseasesList.AsQueryable);
            _mockData.Setup(q => q.Categories.GetAll()).Returns(_categoriesList.AsQueryable);

            // Act
            var actual = new DiseaseService(_mockData.Object).GetDiseasedByCategory(categoryId).Result.ToList();

            // Assert
            Assert.AreEqual(2, actual.Count());
            Assert.AreEqual(_diseasesList[0].Name, actual[0].Name);
            Assert.AreEqual(_diseasesList[1].Name, actual[1].Name);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(3)]
        public void GetDiseaseById_ValidIdTest(int diseaseId)
        {
            // Arrange
            _mockData.Setup(q => q.Diseases.Get(diseaseId)).ReturnsAsync(_diseasesList[diseaseId-1]);

            // Act
            var actual = new DiseaseService(_mockData.Object).GetDiseaseById(diseaseId).Result;

            // Assert
            Assert.AreEqual(_diseasesList[diseaseId - 1], actual);
        }

        [TestMethod]
        [DataRow(5)]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetDiseaseById_InValidIdTest(int diseaseId)
        {
            // Arrange
            _mockData.Setup(q => q.Diseases.Get(diseaseId)).ReturnsAsync(default(Disease));

            // Act
            var actual = await new DiseaseService(_mockData.Object).GetDiseaseById(diseaseId);
        }

        [TestMethod]
        public void AddDiseaseAsync_ValidDisease_Test()
        {
            // Arrange
            _mockData.Setup(q => q.Diseases.GetAll()).Returns(_diseasesList.AsQueryable);
            Disease testDisease = new Disease() { Id = 4, CategoryId = 2, Name = "Test Disease Name 4", Description = "Test Disease Name 4 Description Text", IsDeleted = false };
            _mockData.Setup(q => q.Diseases.Insert(testDisease)).Returns(testDisease);

            // Act
            var actual = new DiseaseService(_mockData.Object).AddDiseaseAsync(testDisease).Result;

            // Assert
            Assert.AreEqual(testDisease, actual);
            _mockData.Verify(d => d.Save(),Times.Once);
        }

        [TestMethod]        
        public void AddDiseaseAsync_InValidDisease_Test()
        {
            // Arrange 
            _mockData.Setup(q => q.Diseases.GetAll()).Returns(_diseasesList.AsQueryable);
            Disease testDisease = new Disease() { Id = 3, CategoryId = 2, Name = "Test Disease Name 3", Description = "Test Disease Name 3 Description Text", IsDeleted = false };
            _mockData.Setup(q => q.Diseases.Insert(testDisease)).Returns(testDisease);            

            // Assert
            Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                new DiseaseService(_mockData.Object).AddDiseaseAsync(testDisease));
        }

        [TestMethod]
        [DataRow(1)]
        public void DeleteDiseaseAsync_ValidDiseaseId(int diseaseId)
        {
            // Arrange
            _mockData.Setup(q => q.Diseases.Get(diseaseId)).ReturnsAsync(_diseasesList[diseaseId - 1]);
            _mockData.Setup(q => q.PatientDiseases.GetAll()).Returns(new List<PatientDisease>().AsQueryable);

            // Act
            var actual = new DiseaseService(_mockData.Object).DeleteDiseaseAsync(diseaseId).Result;

            // Assert
            Assert.IsTrue(actual.IsDeleted);
            _mockData.Verify(m => m.Save(), Times.Once);
        }

        [TestMethod]
        [DataRow(7)]
        public void DeleteDiseaseAsync_InvalidDiseaseId_ThrowsArgumentException(int diseaseId)
        {
            // Arrange
            _mockData.Setup(q => q.Diseases.Get(diseaseId)).ReturnsAsync(default(Disease));
            _mockData.Setup(q => q.PatientDiseases.GetAll()).Returns(new List<PatientDisease>().AsQueryable);

            // Act
            
            // Assert
            Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                new DiseaseService(_mockData.Object).DeleteDiseaseAsync(diseaseId));
        }
    }
}
