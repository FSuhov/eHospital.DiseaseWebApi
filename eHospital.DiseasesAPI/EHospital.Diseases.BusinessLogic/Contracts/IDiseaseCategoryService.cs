using System.Collections.Generic;
using EHospital.Diseases.Model;
using System.Linq;
using System.Threading.Tasks;

namespace EHospital.Diseases.BusinessLogic.Contracts
{
    public interface IDiseaseCategoryService
    {
        Task<IEnumerable<DiseaseCategory>> GetDiseaseCategories();

        Task<DiseaseCategory> GetDiseaseCategoryById(int id);

        Task<DiseaseCategory> AddDiseaseCategoryAsync(DiseaseCategory diseaseCategory);
    }
}
