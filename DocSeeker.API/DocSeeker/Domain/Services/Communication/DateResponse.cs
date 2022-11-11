using DocSeeker.API.DocSeeker.Domain.Models;
using DocSeeker.API.Shared.Domain.Services.Communication;

namespace DocSeeker.API.DocSeeker.Domain.Services.Communication;



public class DateResponse: BaseResponse<Date>
{
    public DateResponse(string message) : base(message)
    {
    }

    public DateResponse(Date resource) : base(resource)
    {
    }
}
