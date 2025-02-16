using EUP.Data;
using EUP.Shared;
using AutoMapper;

namespace EUP.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Proffesor, ProffesorModel>().ForMember(dest => dest.UniversityName, opt => opt.MapFrom(src => src.university!.Name))
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.university!.Country));
            CreateMap<ProffesorModel, Proffesor>();
            CreateMap<Proffesor, ProffesorEditModel>().ReverseMap();

        }
    }
}
