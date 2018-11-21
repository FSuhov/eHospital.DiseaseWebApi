using System;
using EHospital.Diseases.BusinessLogic.Services;
using EHospital.Diseases.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace EHospital.Diseases.Tests
{
    [TestClass]
    public class PatientDiseaseTests
    {
        private static Mock<IRepository<Disease>> _mockRepo;        
        private static Mock<IUniteOfWork> _mockData;
        private List<Disease> _diseasesList;
        private List<DiseaseCategory> _categoriesList;
        private List<PatientDisease> _patientDiseasesList;
        private List<PatientInfo> _patientsList;
        private List<UsersData> _usersList;

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
                new Disease(){DiseaseId = 3, CategoryId = 2, Name = "Test Disease Name 3", Description = "Test Disease Name 3 Description Text", IsDeleted = false}
            };

            _categoriesList = new List<DiseaseCategory>
            {
                new DiseaseCategory(){CategoryId = 1, Name = "Test Category 1", IsDeleted = false},
                new DiseaseCategory(){CategoryId = 2, Name = "Test Category 2", IsDeleted = false}
            };

            _patientsList = new List<PatientInfo>
            {
                new PatientInfo(){PatientId = 1, BirthDate = new DateTime(1970,12,1), FirstName = "Vasya", LastName = "Pupkin", Gender = 1, Email = "test@gmail.com", IsDeleted = false},
                new PatientInfo(){PatientId = 2, BirthDate = new DateTime(1960,12,1), FirstName = "James", LastName = "Bond", Gender = 1, Email = "test1@gmail.com", IsDeleted = false},
                new PatientInfo(){PatientId = 3, BirthDate = new DateTime(1990,12,1), FirstName = "John", LastName = "Snown", Gender = 1, Email = "test2@gmail.com", IsDeleted = false},
            };

            _usersList = new List<UsersData>
            {
                new UsersData(){UserId = 1, FirstName = "Sarah", LastName = "Connor", Email = "email@me.ru", Gender = 2, BirthDate = new DateTime(1970,12,1), IsDeleted = false},
                new UsersData(){UserId = 2, FirstName = "Piter", LastName = "Ustinov", Email = "email1@me.ru", Gender = 1, BirthDate = new DateTime(1970,12,1), IsDeleted = false},
            };

            _patientDiseasesList = new List<PatientDisease>()
            {
                new PatientDisease(){PatientDiseaseId = 1, DiseaseId = 1, UserId = 1, StartDate = new DateTime(2018,11,1), EndDate = null, Note = "Expecting analysis results", PatientId = 1, IsDeleted = false},
                new PatientDisease(){PatientDiseaseId = 2, DiseaseId = 2, UserId = 2, StartDate = new DateTime(2018,11,10), EndDate = new DateTime(2018,11,14), Note = "Strong headache", PatientId = 2, IsDeleted = false},
                new PatientDisease(){PatientDiseaseId = 3, DiseaseId = 3, UserId = 2, StartDate = new DateTime(2018,11,2), EndDate = null, Note = "Passing medication course", PatientId = 2, IsDeleted = false},
            };
        }

        [TestMethod]
        [DataRow(2)]
        public void GetDiseaseByPatientTest_ValidPatientId(int patientId)
        {
            // Arrange
            _mockData.Setup(q => q.PatientDiseases.GetAll()).Returns(_patientDiseasesList.AsQueryable);
            _mockData.Setup(q => q.Diseases.GetAll()).Returns(_diseasesList.AsQueryable);

            // Act
            var actual = new PatientDiseaseService(_mockData.Object).GetDiseaseByPatient(patientId).ToList();

            // Assert
            Assert.AreEqual(2, actual.Count());
            Assert.AreEqual(_diseasesList[1], actual[0]);
            Assert.AreEqual(_diseasesList[2], actual[1]);
        }

        [TestMethod]
        [DataRow(2)]
        public void GetPatientDiseaseTest_ValidPatientDiseaseId(int patientDiseaseId)
        {
            // Arrange
            _mockData.Setup(q => q.PatientDiseases.Get(patientDiseaseId)).Returns(_patientDiseasesList[patientDiseaseId - 1]);

            // Act
            var actual = new PatientDiseaseService(_mockData.Object).GetPatientDisease(patientDiseaseId);

            // Assert
            Assert.AreEqual(_patientDiseasesList[patientDiseaseId - 1], actual);
        }

        [TestMethod]
        [DataRow(5)]
        public void GetPatientDiseaseTest_InValidPatientDiseaseId(int patientDiseaseId)
        {
            // Arrange
            _mockData.Setup(q => q.PatientDiseases.Get(patientDiseaseId)).Returns(default(PatientDisease));

            // Act
            var actual = new PatientDiseaseService(_mockData.Object).GetPatientDisease(patientDiseaseId);

            // Assert
            Assert.AreEqual(null, actual);
        }

        [TestMethod]
        [DataRow(2)]
        public void GetPatientDiseasesInfosTest(int patientId)
        {
            // Arrange
            _mockData.Setup(q => q.PatientDiseases.GetAll()).Returns(_patientDiseasesList.AsQueryable);
            _mockData.Setup(q => q.Diseases.GetAll()).Returns(_diseasesList.AsQueryable);
            _mockData.Setup(q => q.Categories.GetAll()).Returns(_categoriesList.AsQueryable);
            _mockData.Setup(q => q.Patients.GetAll()).Returns(_patientsList.AsQueryable);
            _mockData.Setup(q => q.Users.GetAll()).Returns(_usersList.AsQueryable);

            IEnumerable<PatientDiseaseInfo> expected = new List<PatientDiseaseInfo>()
            {
                new PatientDiseaseInfo(){Id = 2, CategoryName = "Test Category 1", IsCurrent = false, Name = "Test Disease Name 2", Doctor = "Ustinov", StartDate = new DateTime(2018,11,10)},
                new PatientDiseaseInfo(){Id = 3, CategoryName = "Test Category 2", IsCurrent = true, Name = "Test Disease Name 3", Doctor = "Ustinov", StartDate = new DateTime(2018,11,2)}
            };

            // Act
            var actual = new PatientDiseaseService(_mockData.Object).GetPatientDiseasesInfos(patientId);

            // Assert
            Assert.AreEqual(expected.Count(), actual.Count());
            Assert.AreEqual(expected.ElementAt(0).Id, actual.ElementAt(0).Id);
            Assert.AreEqual(expected.ElementAt(1).Id, actual.ElementAt(1).Id);
            Assert.AreEqual(expected.ElementAt(0).CategoryName, actual.ElementAt(0).CategoryName);
            Assert.AreEqual(expected.ElementAt(1).CategoryName, actual.ElementAt(1).CategoryName);
            Assert.AreEqual(expected.ElementAt(0).IsCurrent, actual.ElementAt(0).IsCurrent);
            Assert.AreEqual(expected.ElementAt(1).IsCurrent, actual.ElementAt(1).IsCurrent);
            Assert.AreEqual(expected.ElementAt(0).Name, actual.ElementAt(0).Name);
            Assert.AreEqual(expected.ElementAt(1).Name, actual.ElementAt(1).Name);
            Assert.AreEqual(expected.ElementAt(0).Doctor, actual.ElementAt(0).Doctor);
            Assert.AreEqual(expected.ElementAt(1).Doctor, actual.ElementAt(1).Doctor);
        }
    }
}
