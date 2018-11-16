using eHospital.Diseases.DA.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace eHospital.Diseases.Domain.Contracts
{
    public interface IDiseaseCategory
    {
        IQueryable<DiseaseCategory> GetDiseaseCategories();

        DiseaseCategory GetDiseaseCategoryById(int id);

        Task<DiseaseCategory> AddDiseaseCategoryAsync(DiseaseCategory diseaseCategory);
    }
}
