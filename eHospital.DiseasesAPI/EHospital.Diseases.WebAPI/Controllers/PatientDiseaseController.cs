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
    [Route("api/[controller]")]
    [ApiController]
    public class PatientDiseaseController : ControllerBase
    {
        IPatientDiseaseService _service;

        public PatientDiseaseController(IPatientDiseaseService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetDiseasesOfPatient(int patientId)
        {
            var diseases = _service.GetDiseaseByPatient(patientId);

            return Ok(Mapper.Map<IEnumerable<DiseaseView>>(diseases));           
        }
       
        [HttpGet("patient={patientId}")]
        public IActionResult GetDiseasesOfPatientDetailed(int patientId)
        {
            var patientDiseases = _service.GetPatientDiseaseInfos(patientId);
           

            return Ok(patientDiseases);
        }
    }
}