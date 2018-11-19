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
    public class DiseaseController : ControllerBase
    {
        IDiseaseService _service;

        public DiseaseController(IDiseaseService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllDiseases()
        {
            var diseases = _service.GetAllDiseases();

            return Ok(Mapper.Map<IEnumerable<DiseaseView>>(diseases));
        }

        [HttpGet("categoryid={categoryId}")]
        public IActionResult GetDiseasesByCategory(int categoryId)
        {
            var diseases = _service.GetDiseasedByCategory(categoryId);

            return Ok(Mapper.Map<IEnumerable<DiseaseView>>(diseases));
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

        [HttpPost]
        public IActionResult AddDesease([FromBody]Disease disease)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data submitted");
            }

            try
            {
                _service.AddDiseaseAsync(disease);
                return Created("disease/", disease.DiseaseId);
            }
            catch (ArgumentException ex)
            {
                return BadRequest("Disease already exists");
            };
        }
    }
}