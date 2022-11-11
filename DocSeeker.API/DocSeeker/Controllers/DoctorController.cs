using AutoMapper;
using DocSeeker.API.DocSeeker.Domain.Models;
using DocSeeker.API.DocSeeker.Domain.Services;
using DocSeeker.API.DocSeeker.Resources;
using DocSeeker.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DocSeeker.API.DocSeeker.Controllers;

[Route("/api/v1/[controller]")]
public class DoctorController : ControllerBase
{
    private readonly IDoctorService _doctorService;
    private readonly IMapper _mapper;


    public DoctorController(IDoctorService doctorService, IMapper mapper)
    {
        _doctorService = doctorService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<DoctorResource>> GetAllAsync()
    {
        var doctors = await _doctorService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Doctor>, IEnumerable<DoctorResource>>(doctors);

        return resources;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveDoctorResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var doctor = _mapper.Map<SaveDoctorResource, Doctor>(resource);

        var result = await _doctorService.SaveAsync(doctor);

        if (!result.Success)
            return BadRequest(result.Message);

        var doctorResource = _mapper.Map<Doctor, DoctorResource>(result.Resource);

        return Ok(doctorResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveDoctorResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var doctor = _mapper.Map<SaveDoctorResource, Doctor>(resource);
        var result = await _doctorService.UpdateAsync(id, doctor);

        if (!result.Success)
            return BadRequest(result.Message);

        var doctorResource = _mapper.Map<Doctor, DoctorResource>(result.Resource);

        return Ok(doctorResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _doctorService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var doctorResource = _mapper.Map<Doctor, DoctorResource>(result.Resource);

        return Ok(doctorResource);
    }

}


