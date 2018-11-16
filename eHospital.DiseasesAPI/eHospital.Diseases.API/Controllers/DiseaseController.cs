using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eHospital.Diseases.Domain.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eHospital.Diseases.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiseaseController : ControllerBase
    {
        IDisease _service;

        public DiseaseController(IDisease service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllDiseases()
        {
            try
            {
                var diseases = _service.GetAllDiseases();
                return Ok(diseases);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }
               
        [HttpGet("categoryid={categoryId}")]
        public IActionResult GetDiseasesByCategory(int categoryId)
        {
            try
            {
                var diseases = _service.GetDiseasedByCategory(categoryId);
                return Ok(diseases);
            }

            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{diseaseId}")]
        public IActionResult GetDisease(int diseaseId)
        {
            var disease = _service.GetDiseaseById(diseaseId);
            
            if (disease == null)
            {
                return NotFound();
            }

            return Ok(disease);
        }
    }
}