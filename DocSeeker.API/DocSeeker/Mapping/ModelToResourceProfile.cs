using AutoMapper;
using DocSeeker.API.DocSeeker.Domain.Models;
using DocSeeker.API.DocSeeker.Resources;
using DocSeeker.API.Docseeker.Resources;

namespace DocSeeker.API.DocSeeker.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Patient, PatientResource>();
        CreateMap<Doctor, DoctorResource>();
        CreateMap<New, NewResource>();
        CreateMap<Date, DateResource>();
        CreateMap<HourAvailable, HourAvailableResource>();
    }
}


