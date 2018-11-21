using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EHospital.Diseases.BusinessLogic.Contracts;
using EHospital.Diseases.Model;
using EHospital.Diseases.WebAPI.ViewModels;
using Microsoft.AspNetCore.Http;
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
        /// <returns></returns>
        [HttpGet("names/{patientId}")]
        public IActionResult GetDiseasesOfPatient(int patientId)
        {
            var diseases = _service.GetDiseaseByPatient(patientId);

            return Ok(Mapper.Map<IEnumerable<DiseaseView>>(diseases));           
        }

        /// <summary>
        /// Handles request GET /api/patientdisease/2
        /// Returns list of disease of selected patient in detaled view
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        [HttpGet("patientid={patientId}")]
        public IActionResult GetDiseasesOfPatientDetailed(int patientId)
        {
            var patientDiseases = _service.GetPatientDiseasesInfos(patientId);           

            return Ok(patientDiseases);
        }

        /// <summary>
        /// Handles request GET /api/patientdisease/2
        /// Returns list of disease of selected patient in detaled view
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        [HttpGet("patientDiseaseId={patientDiseaseId}")]
        public IActionResult GetDiseaseOfPatientDetailed(int patientDiseaseId)
        {
            var patientDisease = _service.GetPatientDiseaseDetailes(patientDiseaseId);

            if (patientDisease == null)
            {
                return NotFound();
            }

            return Ok(patientDisease);
        }

        /// <summary>
        /// Handles request POST /api/patientdisease/[FROM BODY patientDisease]
        /// Adds new PatientDisease to the database
        /// </summary>
        /// <param name="patientDisease">Instance of PatientDisease to be added</param>
        /// <returns>Updated instance of PatientDisease or BadRequest </returns>
        [HttpPost]
        public async Task<IActionResult> AddPatientDisease([FromBody]PatientDisease patientDisease)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var result = await _service.AddPatientDiseaseAsync(patientDisease);
                return Created("patientdisease", result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var result = await _service.UpdatePatientDiseaseAsync(id, patientDiseaseDetails);
                return Ok(result);
            }
            catch(ArgumentException ex)
            {
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
            try
            {
                await _service.DeletePatientDiseaseAsync(id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}