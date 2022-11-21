using DocSeeker.API.DocSeeker.Domain.Models;
using DocSeeker.API.Shared.Domain.Services.Communication;

namespace DocSeeker.API.DocSeeker.Domain.Services.Communication;

public class MedicalInformationResponse : BaseResponse<MedicalInformation>
{
    public MedicalInformationResponse(string message) : base(message)
    {
    }

    public MedicalInformationResponse(MedicalInformation resource) : base(resource)
    {
    }
}


