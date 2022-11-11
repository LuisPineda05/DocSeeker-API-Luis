using AutoMapper;
using DocSeeker.API.Security.Domain.Models;
using DocSeeker.API.Security.Domain.Services.Communication;
using DocSeeker.API.Security.Resources;

namespace DocSeeker.API.Security.Mapping;


public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        
        CreateMap<User, UserResource>();
    }
}
