using System.Collections.Generic;
using EHospital.Diseases.Model;
using System.Linq;
using System.Threading.Tasks;

namespace EHospital.Diseases.BusinessLogic.Contracts
{
    public interface IDiseaseService
    {
        Task <IEnumerable<Disease>> GetAllDiseases();

        Task <IEnumerable<Disease>> GetDiseasedByCategory(int categoryId);

        Task <Disease> GetDiseaseById(int diseaseId);

        Task<Disease> AddDiseaseAsync(Disease disease);

        Task<Disease> DeleteDiseaseAsync(int id);
    }
}
