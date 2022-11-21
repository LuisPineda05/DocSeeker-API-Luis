using DocSeeker.API.DocSeeker.Domain.Models;
using DocSeeker.API.Shared.Domain.Services.Communication;

namespace DocSeeker.API.DocSeeker.Domain.Services.Communication;

public class PrescriptionResponse : BaseResponse<Prescription>
{
    public PrescriptionResponse(string message) : base(message)
    {
    }

    public PrescriptionResponse(Prescription resource) : base(resource)
    {
    }
}

