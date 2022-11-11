using DocSeeker.API.DocSeeker.Domain.Models;
using DocSeeker.API.Shared.Domain.Services.Communication;

namespace DocSeeker.API.DocSeeker.Domain.Services.Communication;

public class HourAvailableResponse : BaseResponse<HourAvailable>
{
    public HourAvailableResponse(string message) : base(message)
    {
    }

    public HourAvailableResponse(HourAvailable resource) : base(resource)
    {
    }
}
