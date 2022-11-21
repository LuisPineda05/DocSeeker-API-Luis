using AutoMapper;
using DocSeeker.API.DocSeeker.Domain.Models;
using DocSeeker.API.DocSeeker.Resources;
using DocSeeker.API.Docseeker.Resources;

namespace DocSeeker.API.DocSeeker.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SavePatientResource, Patient>();
        CreateMap<SaveDoctorResource, Doctor>();
        CreateMap<SaveNewResource, New>();
        CreateMap<SaveDateResource, Date>();
        CreateMap<SaveHourAvailableResource, HourAvailable>();
        CreateMap<SaveReviewResource, Review>();
        CreateMap<SaveMedicalInformationResource, MedicalInformation>();
        CreateMap<SavePrescriptionResource, Prescription>();
    }
}


