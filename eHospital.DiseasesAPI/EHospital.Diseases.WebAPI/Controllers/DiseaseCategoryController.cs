﻿using System;
using System.Collections.Generic;
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
    public class DiseaseCategoryController : ControllerBase
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IDiseaseCategoryService _service;

        /// <summary>
        /// Initializes new instance of DiseaseCategoryController
        /// </summary>
        /// <param name="service">Instance of Business logic class</param>
        public DiseaseCategoryController(IDiseaseCategoryService service)
        {
            _service = service;
        }

        /// <summary>
        /// Handles request GET /api/diseasecategory/.
        /// Retrieves all entries of Categories from database
        /// </summary>
        /// <returns>Ok with Collection of DiseaseCategoryView objects</returns>
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            log.Info("DiseaseCategoryController::GetAllCategories. Retrieving all entries of DiseaseCategories.");

            var diseaseCateories = _service.GetDiseaseCategories();

            return Ok(Mapper.Map<IEnumerable<DiseaseCategoryView>>(diseaseCateories));
        }

        /// <summary>
        /// Handles request GET /api/diseasecategory/2
        /// Retrieves entry of Category with specified ID from database
        /// </summary>
        /// <param name="categoryId">An ID to look for</param>
        /// <returns>Ok with instance of DiseaseCategory or NotFound</returns>
        [HttpGet("{categoryId}")]
        public IActionResult GetDiseaseCategory(int categoryId)
        {
            log.Info($"DiseaseCategoryController::GetDiseaseCategory(). Retrieving DiseaseCategory with ID {categoryId}.");

            var diseaseCategory = _service.GetDiseaseCategoryById(categoryId);

            if (diseaseCategory == null)
            {
                log.Warn($"DiseaseCategoryController::GetDiseaseCategory(). No records found with ID {categoryId}.");

                return NotFound();
            }

            return Ok(diseaseCategory);
        }

        /// <summary>
        /// Handles request POST /api/diseasecategory/[FROM BODY] DiseaseCategory.
        /// Tries to add new DiseaseCategory entry to Database
        /// </summary>
        /// <param name="diseaseCategory">A new instance of DiseaseCategory</param>
        /// <returns>Created (with Id) or BadRequest</returns>
        [HttpPost]
        public IActionResult AddDeseaseCategory([FromBody]DiseaseCategory diseaseCategory)
        {
            log.Info($"DiseaseCategoryController::AddDeseaseCategory().Adding DiseaseCategory {diseaseCategory.Name}.");

            if (!ModelState.IsValid)
            {
                log.Error("DiseaseCategoryController::AddDeseaseCategory().Invalid model");

                return BadRequest("Invalid data submitted");
            }

            try
            {
                _service.AddDiseaseCategoryAsync(diseaseCategory);

                log.Info($"DiseaseCategoryController::AddDeseaseCategory(). DiseaseCategory {diseaseCategory.Name} has been added.");

                return Created("diseasecategory/", diseaseCategory.Id);
            }
            catch (ArgumentException ex)
            {
                log.Error($"DiseaseCategoryController::AddDeseaseCategory().Failed to add DiseaseCategory {diseaseCategory.Name}");

                return BadRequest(ex.Message);
            };
        }
    }
}