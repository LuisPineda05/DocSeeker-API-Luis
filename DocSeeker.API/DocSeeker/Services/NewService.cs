using DocSeeker.API.DocSeeker.Domain.Models;
using DocSeeker.API.DocSeeker.Domain.Repositories;
using DocSeeker.API.DocSeeker.Domain.Services;
using DocSeeker.API.DocSeeker.Domain.Services.Communication;
using DocSeeker.API.Shared.Domain.Repositories;

namespace DocSeeker.API.Docseeker.Services;

public class NewService : INewService
{
    private readonly INewRepository _newRepository;
    private readonly IUnitOfWork _unitOfWork;

    public NewService(INewRepository newRepository, IUnitOfWork unitOfWork)
    {
        _newRepository = newRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<New>> ListAsync()
    {
        return await _newRepository.ListAsync();
    }

    public async Task<NewResponse> SaveAsync(New _new)
    {
        try
        {
            await _newRepository.AddAsync(_new);
            await _unitOfWork.CompleteAsync();
            return new NewResponse(_new);
        }
        catch (Exception e)
        {
            return new NewResponse($"An error occurred while saving the new: {e.Message}");
        }
    }

    public async Task<NewResponse> UpdateAsync(int id, New _new)
    {
        var existingNew = await _newRepository.FindById(id);

        if (existingNew == null)
            return new NewResponse("New not found.");

        existingNew.Title = _new.Title;
        existingNew.Views = _new.Views;

        try
        {
            _newRepository.Update(existingNew);
            await _unitOfWork.CompleteAsync();

            return new NewResponse(existingNew);
        }
        catch (Exception e)
        {
            return new NewResponse($"An error occurred while updating the new: {e.Message}");
        }
    }

    public async Task<NewResponse> DeleteAsync(int id)
    {
        var existingNew = await _newRepository.FindById(id);

        if (existingNew == null)
            return new NewResponse("New not found.");

        try
        {
            _newRepository.Remove(existingNew);
            await _unitOfWork.CompleteAsync();

            return new NewResponse(existingNew);
        }
        catch (Exception e)
        {
            // Do some logging stuff
            return new NewResponse($"An error occurred while deleting the new: {e.Message}");
        }
    }
}





