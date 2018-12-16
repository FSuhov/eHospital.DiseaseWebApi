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
            CreateMap<Disease, DiseaseView>()
                .ForMember(dest => dest.Id, src => src.MapFrom(opts => opts.Id));

            CreateMap<DiseaseCategory, DiseaseCategoryView>();
        }
    }
}
