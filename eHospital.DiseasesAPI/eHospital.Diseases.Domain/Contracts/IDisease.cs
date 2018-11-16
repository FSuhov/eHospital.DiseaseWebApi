using eHospital.Diseases.DA.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace eHospital.Diseases.Domain.Contracts
{
    public interface IDisease
    {
        IQueryable<Disease> GetAllDiseases();

        IQueryable<Disease> GetDiseasedByCategory(int categoryId);

        Disease GetDiseaseById(int diseaseId);

        Task<Disease> AddDiseaseAsync(Disease disease);

        Task<Disease> DeleteDiseaseAsync(int id);
    }
}
