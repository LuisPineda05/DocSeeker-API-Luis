using AutoMapper;
using DocSeeker.API.DocSeeker.Domain.Models;
using DocSeeker.API.DocSeeker.Domain.Services;
using DocSeeker.API.DocSeeker.Resources;
using DocSeeker.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DocSeeker.API.DocSeeker.Controllers;

[Route("/api/v1/[controller]")]
public class MedicalInformationController : ControllerBase
{
    private readonly IMedicalInformationService _medicalInformationService;
    private readonly IMapper _mapper;


    public MedicalInformationController(IMedicalInformationService medicalInformationService, IMapper mapper)
    {
        _medicalInformationService = medicalInformationService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<MedicalInformationResource>> GetAllAsync()
    {
        var medicalInformations = await _medicalInformationService.ListAsync();
        var resources = _mapper.Map<IEnumerable<MedicalInformation>, IEnumerable<MedicalInformationResource>>(medicalInformations);

        return resources;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveMedicalInformationResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var medicalInformation = _mapper.Map<SaveMedicalInformationResource, MedicalInformation>(resource);

        var result = await _medicalInformationService.SaveAsync(medicalInformation);

        if (!result.Success)
            return BadRequest(result.Message);

        var medicalInformationResource = _mapper.Map<MedicalInformation, MedicalInformationResource>(result.Resource);

        return Ok(medicalInformationResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveMedicalInformationResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var medicalInformation = _mapper.Map<SaveMedicalInformationResource, MedicalInformation>(resource);
        var result = await _medicalInformationService.UpdateAsync(id, medicalInformation);

        if (!result.Success)
            return BadRequest(result.Message);

        var medicalInformationResource = _mapper.Map<MedicalInformation, MedicalInformationResource>(result.Resource);

        return Ok(medicalInformationResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _medicalInformationService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var medicalInformationResource = _mapper.Map<MedicalInformation, MedicalInformationResource>(result.Resource);

        return Ok(medicalInformationResource);
    }

}

