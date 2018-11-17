using EHospital.Diseases.Model;
using EHospital.Diseases.BusinessLogic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHospital.Diseases.BusinessLogic.Services
{
    public class DiseaseCategoryService : IDiseaseCategory
    {
        IUniteOfWork _unitOfWork;

        public DiseaseCategoryService(IUniteOfWork uow)
        {
            _unitOfWork = uow;
        }

        public IQueryable<DiseaseCategory> GetDiseaseCategories()
        {
            var result = _unitOfWork.DiseaseCategories.GetAll().Where(d => d.IsDeleted != true);

            if (result.Count() == 0)
            {
                throw new NullReferenceException("No records in db.");
            }

            return result.OrderBy(d => d.Name);
        }

        public DiseaseCategory GetDiseaseCategoryById(int id)
        {
            var result = _unitOfWork.DiseaseCategories.Get(id);

            return result as DiseaseCategory;
        }

        public async Task<DiseaseCategory> AddDiseaseCategoryAsync(DiseaseCategory diseaseCategory)
        {
            if (_unitOfWork.Diseases.GetAll().Any(dc => dc.Name == diseaseCategory.Name))
            {
                throw new ArgumentException("Category already exists.");
            }

            DiseaseCategory result = _unitOfWork.DiseaseCategories.Insert(diseaseCategory);
            await _unitOfWork.Save();
            return result;
        }
    }
}
