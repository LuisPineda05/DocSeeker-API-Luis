using DocSeeker.API.DocSeeker.Domain.Models;
using DocSeeker.API.DocSeeker.Domain.Repositories;
using DocSeeker.API.DocSeeker.Domain.Services;
using DocSeeker.API.DocSeeker.Domain.Services.Communication;
using DocSeeker.API.Shared.Domain.Repositories;
using DocSeeker.API.Security.Services;
using DocSeeker.API.Security.Persistence.Repositories;

namespace DocSeeker.API.DocSeeker.Services;

public class DateService : IDateService
{
    private readonly IDateRepository _dateRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DateService(IDateRepository dateRepository, IUnitOfWork unitOfWork)
    {
        _dateRepository = dateRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Date>> ListAsync()
    {
        return await _dateRepository.ListAsync();
    }

    public async Task<DateResponse> SaveAsync(Date date)
    {
        try
        {
            await _dateRepository.AddAsync(date);
            await _unitOfWork.CompleteAsync();
            return new DateResponse(date);
        }
        catch (Exception e)
        {
            return new DateResponse($"An error occurred while saving the date: {e.Message}");
        }
    }

    public async Task<DateResponse> UpdateAsync(int id, Date date)
    {
        var existingDate = await _dateRepository.FindById(id);

        if (existingDate == null)
            return new DateResponse("Category not found.");

        existingDate.CDate = date.CDate;

        try
        {
            _dateRepository.Update(existingDate);
            await _unitOfWork.CompleteAsync();

            return new DateResponse(existingDate);
        }
        catch (Exception e)
        {
            return new DateResponse($"An error occurred while updating the date: {e.Message}");
        }
    }

    public async Task<DateResponse> DeleteAsync(int id)
    {
        var existingDate = await _dateRepository.FindById(id);

        if (existingDate == null)
            return new DateResponse("Date not found.");

        try
        {
            _dateRepository.Remove(existingDate);
            await _unitOfWork.CompleteAsync();

            return new DateResponse(existingDate);
        }
        catch (Exception e)
        {
            // Do some logging stuff
            return new DateResponse($"An error occurred while deleting the date: {e.Message}");
        }
    }
}
