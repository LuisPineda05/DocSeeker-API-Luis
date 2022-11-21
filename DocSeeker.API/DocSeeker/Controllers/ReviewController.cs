using AutoMapper;
using DocSeeker.API.DocSeeker.Domain.Models;
using DocSeeker.API.DocSeeker.Domain.Services;
using DocSeeker.API.DocSeeker.Resources;
using DocSeeker.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DocSeeker.API.DocSeeker.Controllers;


[Route("/api/v1/[controller]")]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;
    private readonly IMapper _mapper;


    public ReviewController(IReviewService reviewService, IMapper mapper)
    {
        _reviewService = reviewService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<ReviewResource>> GetAllAsync()
    {
        var reviews = await _reviewService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Review>, IEnumerable<ReviewResource>>(reviews);

        return resources;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveReviewResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var review = _mapper.Map<SaveReviewResource, Review>(resource);

        var result = await _reviewService.SaveAsync(review);

        if (!result.Success)
            return BadRequest(result.Message);

        var reviewResource = _mapper.Map<Review, ReviewResource>(result.Resource);

        return Ok(reviewResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveReviewResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var review = _mapper.Map<SaveReviewResource, Review>(resource);
        var result = await _reviewService.UpdateAsync(id, review);

        if (!result.Success)
            return BadRequest(result.Message);

        var reviewResource = _mapper.Map<Review, ReviewResource>(result.Resource);

        return Ok(reviewResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _reviewService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var reviewResource = _mapper.Map<Review, ReviewResource>(result.Resource);

        return Ok(reviewResource);
    }

}
