﻿using eHospital.Diseases.DA.Contracts;
using eHospital.Diseases.DA.Entities;
using eHospital.Diseases.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eHospital.Diseases.Domain.Services
{
    public class DiseaseService : IDisease
    {
        IUniteOfWork _unitOfWork;

        public DiseaseService(IUniteOfWork uow)
        {
            _unitOfWork = uow;
        }
        public IQueryable<Disease> GetAllDiseases()
        {
            var result = _unitOfWork.Diseases.GetAll().Where(d => d.IsDeleted != true);

            if (result.Count() == 0)
            {
                throw new NullReferenceException("No records in db.");
            }

            return result.OrderBy(d => d.Name);
        }

        public IQueryable<Disease> GetDiseasedByCategory(int categoryId)
        {
            var result = _unitOfWork.Diseases.GetAll().Where(d => d.IsDeleted != true && d.CategoryId == categoryId);

            if (result.Count() == 0)
            {
                throw new NullReferenceException("No records for such category.");
            }

            return result.OrderBy(d => d.Name);
        }

        public Disease GetDiseaseById(int diseaseId)
        {
            var result = _unitOfWork.Diseases.GetAll().Where(d => d.DiseaseId == diseaseId);

            return result as Disease;
        }

        public async Task<Disease> AddDiseaseAsync(Disease disease)
        {
            if (_unitOfWork.Diseases.GetAll().Any(d => d.Name == disease.Name))
            {
                throw new ArgumentException("Disease already exists.");
            }

            Disease result = _unitOfWork.Diseases.Insert(disease);
            await _unitOfWork.Save();
            return result;
        }

        public async Task<Disease> DeleteDiseaseAsync(int id)
        {
            var result = _unitOfWork.Diseases.Get(id);

            if (result == null)
            {
                throw new NullReferenceException("No disease found.");
            }

            if (_unitOfWork.PatientDiseases.GetAll().Count() != 0)
            {
                if (_unitOfWork.PatientDiseases.GetAll().Any(d => d.DiseaseId == result.DiseaseId))
                {
                    throw new ArgumentException("There are existing records containing this Disease.");
                }
            }

            result.IsDeleted = true;
            _unitOfWork.Diseases.Delete(result);
            await _unitOfWork.Save();
            return result;
        }
    }
}
