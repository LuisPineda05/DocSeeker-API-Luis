using AutoMapper;
using DocSeeker.API.DocSeeker.Domain.Models;
using DocSeeker.API.DocSeeker.Domain.Services;
using DocSeeker.API.DocSeeker.Resources;
using DocSeeker.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DocSeeker.API.DocSeeker.Controllers;

[Route("/api/v1/[controller]")]
public class HourAvailableController : ControllerBase
{
    private readonly IHourAvailableService _hourAvailableService;
    private readonly IMapper _mapper;


    public HourAvailableController(IHourAvailableService hourAvailableService, IMapper mapper)
    {
        _hourAvailableService = hourAvailableService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<HourAvailableResource>> GetAllAsync()
    {
        var hoursAvailable = await _hourAvailableService.ListAsync();
        var resources = _mapper.Map<IEnumerable<HourAvailable>, IEnumerable<HourAvailableResource>>(hoursAvailable);

        return resources;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveHourAvailableResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var hourAvailable = _mapper.Map<SaveHourAvailableResource, HourAvailable>(resource);

        var result = await _hourAvailableService.SaveAsync(hourAvailable);

        if (!result.Success)
            return BadRequest(result.Message);

        var hourAvailableResource = _mapper.Map<HourAvailable, HourAvailableResource>(result.Resource);

        return Ok(hourAvailableResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveHourAvailableResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var hourAvailable = _mapper.Map<SaveHourAvailableResource, HourAvailable>(resource);
        var result = await _hourAvailableService.UpdateAsync(id, hourAvailable);

        if (!result.Success)
            return BadRequest(result.Message);

        var hourAvaibleResource = _mapper.Map<HourAvailable, HourAvailableResource>(result.Resource);

        return Ok(hourAvaibleResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _hourAvailableService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var hourAvailableResource = _mapper.Map<HourAvailable, HourAvailableResource>(result.Resource);

        return Ok(hourAvailableResource);
    }

}

