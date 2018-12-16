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
    public class PatientDiseaseController : ControllerBase
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IPatientDiseaseService _service;

        /// <summary>
        /// Initializes new instance of PatientDiseaseController
        /// </summary>
        /// <param name="service">Instance of Business logic class</param>
        public PatientDiseaseController(IPatientDiseaseService service)
        {
            _service = service;
        }

        /// <summary>
        /// Handles request GET /api/patientdisease/names/2
        /// Returns list of diseases of this patient in simple view: id of disease + name of disease
        /// </summary>
        /// <param name="patientId">Id of patient to look for</param>
        /// <returns>Ok with Collection of DiseaseViews</returns>
        [HttpGet("names/{patientId}")]
        public async Task<IActionResult> GetDiseasesByPatient(int patientId)
        {
            log.Info($"PatiendDiseaseController::GetDiseasesOfPatient(). Retrieving entries for patient {patientId}.");

            var diseases = await _service.GetDiseaseByPatient(patientId);

            return Ok(Mapper.Map<IEnumerable<DiseaseView>>(diseases));
        }

        /// <summary>
        /// Handles request GET /api/patientdisease/2
        /// Returns list of disease of selected patient in detaled view
        /// </summary>
        /// <param name="patientId">ID of patient to look for</param>
        /// <returns>Ok with collection of PatientDiseaseInfo</returns>
        [HttpGet("patientid={patientId}")]
        public async Task <IActionResult> GetDiseasesByPatientDetailed(int patientId)
        {
            log.Info($"PatiendDiseaseController::GetDiseasesOfPatientDetailed(). Retrieving entries for patient {patientId}.");

            var patientDiseases = await _service.GetPatientDiseasesInfos(patientId);

            return Ok(patientDiseases);
        }

        /// <summary>
        /// Handles request GET /api/patientdisease/patientdisease=2
        /// Returns detailed view of specific PatientDisease
        /// </summary>
        /// <param name="patientDiseaseId">Id of PatientDisease entry</param>
        /// <returns>Ok with PatientDiseaseDetails or NotFound</returns>
        [HttpGet("patientDiseaseId={patientDiseaseId}")]
        public async Task<IActionResult> GetPatientDisease(int patientDiseaseId)
        {
            log.Info($"PatiendDiseaseController::GetPatientDisease(). Retrieving entries for patient {patientDiseaseId}.");

            var patientDisease = await _service.GetPatientDiseaseDetails(patientDiseaseId);

            if (patientDisease == null)
            {
                log.Warn($"Entry with Id {patientDiseaseId} has not been found.");

                return NotFound();
            }

            return Ok(patientDisease);
        }

        /// <summary>
        /// Handles request POST /api/patientdisease/[FROM BODY patientDisease]
        /// Adds new PatientDisease to the database
        /// </summary>
        /// <param name="patientDisease">Instance of PatientDisease to be added</param>
        /// <returns>Created with Added instance of PatientDisease or BadRequest </returns>
        [HttpPost]
        public async Task<IActionResult> AddPatientDisease([FromBody]PatientDisease patientDisease)
        {
            log.Info("PatiendDiseaseController::AddPatientDisease(). Adding new PatietDisease.");

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _service.AddPatientDiseaseAsync(patientDisease);

            log.Info($"PatiendDiseaseController::AddPatientDisease(). Disease with ID {patientDisease.Id} added.");

            return Created("patientdisease", result);
        }

        /// <summary>
        /// Handles request PUT /api/patientdisease/id?={}id + [FROM BODY patientDiseaseDetails]
        /// Updates EndDate, Note and UserId (Doctor) in patientDisease instance with specified Id 
        /// </summary>
        /// <param name="id">Id of parientDisease that has to be udpated </param>
        /// <param name="patientDiseaseDetails">PatientDiseaseDetails instance data from UI</param>
        /// <returns> Updated instance of PatientDisease or BadRequest </returns>
        [HttpPut("id={id}", Name = "UpdatePatientDisease")]
        public async Task<IActionResult> UpdatePatientDisease(int id, [FromBody]PatientDiseaseDetails patientDiseaseDetails)
        {
            log.Info($"PatiendDiseaseController::UpdatePatientDisease(). Updating PatientDisease with ID {id}.");

            if (!ModelState.IsValid)
            {
                log.Error("PatiendDiseaseController::UpdatePatientDisease(). Model is not valid");

                return BadRequest();
            }

            try
            {
                var result = await _service.UpdatePatientDiseaseAsync(id, patientDiseaseDetails);

                log.Info($"PatiendDiseaseController::UpdatePatientDisease(). Updated PatientDisease with ID {id}.");

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                log.Error($"PatiendDiseaseController::UpdatePatientDisease(). No records found with ID {id}");

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Handles request DELETE /api/patientdisease/2
        /// Sets IsDeleted property of PatientDisease instance with specified ID to TRUE
        /// </summary>
        /// <param name="id">Id of PatientDisease instance to look for </param>
        /// <returns>Ok if deleted, NotFound if no entry with such ID found </returns>
        [HttpDelete]
        public async Task<IActionResult> DeletePatientDisease(int id)
        {
            log.Info($"PatiendDiseaseController::UpdatePatientDisease().Deleting PatientDisease with ID {id}.");

            try
            {
                await _service.DeletePatientDiseaseAsync(id);

                log.Info($"PatiendDiseaseController::UpdatePatientDisease(). PatientDisease with ID {id} has been deleted.");

                return Ok();
            }
            catch (ArgumentException ex)
            {
                log.Error($"PatiendDiseaseController::UpdatePatientDisease(). No records found with ID {id}");

                return NotFound(ex.Message);
            }
        }
    }
}