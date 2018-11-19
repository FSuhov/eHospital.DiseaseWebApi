using EHospital.Diseases.Model;
using System.Linq;
using System.Threading.Tasks;

namespace EHospital.Diseases.BusinessLogic.Contracts
{
    public interface IDiseaseCategoryService
    {
        IQueryable<DiseaseCategory> GetDiseaseCategories();

        DiseaseCategory GetDiseaseCategoryById(int id);

        Task<DiseaseCategory> AddDiseaseCategoryAsync(DiseaseCategory diseaseCategory);
    }
}
