using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EHospital.Diseases.BusinessLogic.Contracts;
using EHospital.Diseases.Model;
using EHospital.Diseases.WebAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EHospital.Diseases.WebAPI.Controllers
{
    /// <summary>
    /// Represents class containing methods for handling user requests
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DiseaseController : ControllerBase
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IDiseaseService _service;

        /// <summary>
        /// Initializes new instance of PatientDiseaseController
        /// </summary>
        /// <param name="service">Instance of Business logic class</param>
        public DiseaseController(IDiseaseService service)
        {
            _service = service;
        }

        /// <summary>
        /// Handles request GET /api/disease/.
        /// Retrieves all entries of diseases from database
        /// </summary>
        /// <returns>Ok with Collection of DiseaseView objects</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllDiseases()
        {
            log.Info("DiseaseController::GetAllDiseases. Retrieving all entries of diseases.");

            var diseases = await _service.GetAllDiseases();
            var diseasesViews = Mapper.Map<IEnumerable<DiseaseView>>(diseases);

            return Ok(diseasesViews);
        }

        /// <summary>
        /// Handles request GET /api/disease/categoryid=2.
        /// Retrieves all entries of diseases belonging to specified category from database
        /// </summary>
        /// <returns>OK with Collection of DiseaseView objects</returns>
        [HttpGet("categoryid={categoryId}")]
        public async Task<IActionResult> GetDiseasesByCategory(int categoryId)
        {
            log.Info($"DiseaseController::GetDiseasesByCategory. Retrieving all entries of diseases of Category {categoryId}.");

            var diseases = await _service.GetDiseasedByCategory(categoryId);
            var diseasesViews = Mapper.Map<IEnumerable<DiseaseView>>(diseases);

            return Ok(diseasesViews);
        }

        /// <summary>
        /// Handles request GET /api/disease/2.
        /// Retrieves diseases entry with specified ID database
        /// </summary>
        /// <returns>OK with Disease object or NotFound</returns>
        [HttpGet("{diseaseId}")]
        public async Task<IActionResult> GetDisease(int diseaseId)
        {
            log.Info($"DiseaseController::GetDisease. Retrieving Disease with ID {diseaseId}.");

            var disease = await _service.GetDiseaseById(diseaseId);

            if (disease == null)
            {
                log.Warn($"DiseaseController::GetDisease. No records found with ID {diseaseId}.");

                return NotFound();
            }

            return Ok(disease);
        }

        /// <summary>
        /// Handles request POST /api/disease/[FROM BODY] Disease.
        /// Tries to add new Disease entry to Database
        /// </summary>
        /// <param name="disease">A new instance of Disease</param>
        /// <returns>Created (with Id) or BadRequest</returns>
        [HttpPost]
        public async Task<IActionResult> AddDesease([FromBody]Disease disease)
        {
            log.Info($"DiseaseController::AddDisease.Adding Disease {disease.Name}.");

            if (!ModelState.IsValid)
            {
                log.Error($"DiseaseController::AddDisease. Invalid Model of Disease submitted.");

                return BadRequest("Invalid data submitted");
            }

            try
            {
                var addedDisease = await _service.AddDiseaseAsync(disease);

                log.Info($"DiseaseController::AddDisease. Disease {disease.Name} added.");

                return Created("disease/", addedDisease.Id);
            }
            catch (ArgumentException ex)
            {
                log.Error($"DiseaseController::AddDisease. Disease {disease.Name} not added due to duplicated name.");

                return BadRequest(ex.Message);
            };
        }

        /// <summary>
        /// Handles request DELETE /api/disease/2.
        /// Tries to delete Disease with specified id (set IsDeleted to true).
        /// </summary>
        /// <param name="id">Id of Disease to delete</param>
        /// <returns>Ok or NotFound</returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteDisease(int id)
        {
            log.Info($"DiseaseController::DeleteDisease. Deleting Disease with ID {id}.");

            try
            {
                var diseaseToDelete = await _service.DeleteDiseaseAsync(id);

                log.Info($"DiseaseController::DeleteDisease. Deleted Disease with ID {id}.");

                return Ok(diseaseToDelete.Id);
            }
            catch (ArgumentException ex)
            {
                log.Error($"DiseaseController::DeleteDisease.Failed to delete because no disease with {id} found.");

                return NotFound(ex.Message);
            }
        }
    }
}