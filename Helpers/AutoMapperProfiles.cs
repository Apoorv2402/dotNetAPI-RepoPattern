
using AutoMapper;


namespace StudyMash.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Models.City, DTOS.CityDTO>().ReverseMap();
        }
    }
}
