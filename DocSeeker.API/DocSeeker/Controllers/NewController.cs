using AutoMapper;
using DocSeeker.API.DocSeeker.Domain.Models;
using DocSeeker.API.DocSeeker.Domain.Services;
using DocSeeker.API.DocSeeker.Resources;
using DocSeeker.API.Docseeker.Resources;
using DocSeeker.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DocSeeker.API.Docseeker.Controllers;

[Route("/api/v1/[controller]")]
public class NewController : ControllerBase
{
    private readonly INewService _newService;
    private readonly IMapper _mapper;


    public NewController(INewService newService, IMapper mapper)
    {
        _newService = newService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<NewResource>> GetAllAsync()
    {
        var news = await _newService.ListAsync();
        var resources = _mapper.Map<IEnumerable<New>, IEnumerable<NewResource>>(news);

        return resources;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveNewResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var _new = _mapper.Map<SaveNewResource, New>(resource);

        var result = await _newService.SaveAsync(_new);

        if (!result.Success)
            return BadRequest(result.Message);

        var newResource = _mapper.Map<New, NewResource>(result.Resource);

        return Ok(newResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveNewResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var _new = _mapper.Map<SaveNewResource, New>(resource);
        var result = await _newService.UpdateAsync(id, _new);

        if (!result.Success)
            return BadRequest(result.Message);

        var newResource = _mapper.Map<New, NewResource>(result.Resource);

        return Ok(newResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _newService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var newResource = _mapper.Map<New, NewResource>(result.Resource);

        return Ok(newResource);
    }

}



