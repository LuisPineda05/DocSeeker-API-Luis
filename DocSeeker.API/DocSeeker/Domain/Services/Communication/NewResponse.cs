using DocSeeker.API.DocSeeker.Domain.Models;
using DocSeeker.API.Shared.Domain.Services.Communication;


namespace DocSeeker.API.DocSeeker.Domain.Services.Communication;

public class NewResponse :  BaseResponse<New>
{
    public NewResponse(string message) : base(message)
    {
    }

    public NewResponse(New resource) : base(resource)
    {
    }
}
