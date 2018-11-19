using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EHospital.Diseases.Model;
using EHospital.Diseases.WebAPI.ViewModels;

namespace EHospital.Diseases.WebAPI
{
    public class AutomapperProfileConfig : Profile
    {
        public AutomapperProfileConfig()
        {
            CreateMap<Disease, DiseaseView>();
            CreateMap<DiseaseCategory, DiseaseCategoryView>();


            // PatientDisease View Mapping
            /*
            CreateMap<PatientDisease, PatientDiseaseView>()
                .ForMember(dest => dest.Id, src => src.MapFrom(opts => opts.PatientDiseaseId))
                .ForMember(dest => dest.IsCurrent, src => src.MapFrom(opts => opts.EndDate < DateTime.Now));

            CreateMap<DiseaseCategory, PatientDiseaseView>()
                .ForMember(dest => dest.CategoryName, src => src.MapFrom(opts => opts.Name));

            CreateMap<Disease, PatientDiseaseView>()
                .ForMember(dest => dest.Name, src => src.MapFrom(opts => opts.Name));

            CreateMap<UsersData, PatientDiseaseView>()
                .ForMember(dest => dest.Doctor, src => src.MapFrom(opts => opts.LastName));
             */
            // End patientDisease View

        }
    }
}
