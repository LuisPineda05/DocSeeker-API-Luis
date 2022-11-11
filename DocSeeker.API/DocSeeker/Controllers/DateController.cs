using AutoMapper;
using DocSeeker.API.DocSeeker.Domain.Models;
using DocSeeker.API.DocSeeker.Domain.Services;
using DocSeeker.API.DocSeeker.Resources;
using DocSeeker.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DocSeeker.API.DocSeeker.Controllers;


[Route("/api/v1/[controller]")]
public class DateController : ControllerBase
{
    private readonly IDateService _dateService;
    private readonly IMapper _mapper;


    public DateController(IDateService dateService, IMapper mapper)
    {
        _dateService = dateService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<DateResource>> GetAllAsync()
    {
        var dates = await _dateService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Date>, IEnumerable<DateResource>>(dates);

        return resources;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveDateResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var date = _mapper.Map<SaveDateResource, Date>(resource);

        var result = await _dateService.SaveAsync(date);

        if (!result.Success)
            return BadRequest(result.Message);

        var dateResource = _mapper.Map<Date, DateResource>(result.Resource);

        return Ok(dateResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveDateResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var date = _mapper.Map<SaveDateResource, Date>(resource);
        var result = await _dateService.UpdateAsync(id, date);

        if (!result.Success)
            return BadRequest(result.Message);

        var dateResource = _mapper.Map<Date, DateResource>(result.Resource);

        return Ok(dateResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _dateService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var dateResource = _mapper.Map<Date, DateResource>(result.Resource);

        return Ok(dateResource);
    }

}
