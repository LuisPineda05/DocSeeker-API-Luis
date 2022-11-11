using AutoMapper;
using DocSeeker.API.DocSeeker.Domain.Models;
using DocSeeker.API.DocSeeker.Domain.Services;
using DocSeeker.API.DocSeeker.Resources;
using DocSeeker.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DocSeeker.API.DocSeeker.Controllers;

[Route("/api/v1/[controller]")]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;
    private readonly IMapper _mapper;


    public PatientController(IPatientService patientService, IMapper mapper)
    {
        _patientService = patientService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<PatientResource>> GetAllAsync()
    {
        var patients = await _patientService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Patient>, IEnumerable<PatientResource>>(patients);

        return resources;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SavePatientResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var patient = _mapper.Map<SavePatientResource, Patient>(resource);

        var result = await _patientService.SaveAsync(patient);

        if (!result.Success)
            return BadRequest(result.Message);

        var patientResource = _mapper.Map<Patient, PatientResource>(result.Resource);

        return Ok(patientResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SavePatientResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var patient = _mapper.Map<SavePatientResource, Patient>(resource);
        var result = await _patientService.UpdateAsync(id, patient);

        if (!result.Success)
            return BadRequest(result.Message);

        var patientResource = _mapper.Map<Patient, PatientResource>(result.Resource);

        return Ok(patientResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _patientService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var patientResource = _mapper.Map<Patient, PatientResource>(result.Resource);

        return Ok(patientResource);
    }

}


