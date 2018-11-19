using EHospital.Diseases.Model;
using System.Linq;
using System.Threading.Tasks;

namespace EHospital.Diseases.BusinessLogic.Contracts
{
    public interface IDiseaseService
    {
        IQueryable<Disease> GetAllDiseases();

        IQueryable<Disease> GetDiseasedByCategory(int categoryId);

        Disease GetDiseaseById(int diseaseId);

        Task<Disease> AddDiseaseAsync(Disease disease);

        Task<Disease> DeleteDiseaseAsync(int id);
    }
}
