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
    public class DiseaseCategoryController : ControllerBase
    {
        IDiseaseCategoryService _service;

        public DiseaseCategoryController(IDiseaseCategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var diseaseCateories = _service.GetDiseaseCategories();

            return Ok(Mapper.Map<IEnumerable<DiseaseCategoryView>>(diseaseCateories));
        }

        [HttpGet("{categoryId}")]
        public IActionResult GetDiseaseCategory(int categoryId)
        {
            var diseaseCategory = _service.GetDiseaseCategoryById(categoryId);

            if (diseaseCategory == null)
            {
                return NotFound();
            }

            return Ok(diseaseCategory);
        }

        [HttpPost]
        public IActionResult AddDeseaseCategory([FromBody]DiseaseCategory diseaseCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data submitted");
            }

            try
            {
                _service.AddDiseaseCategoryAsync(diseaseCategory);
                return Created("diseasecategory/", diseaseCategory.Id);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            };
        }
    }
}