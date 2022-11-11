using DocSeeker.API.DocSeeker.Domain.Models;
using DocSeeker.API.DocSeeker.Domain.Repositories;
using DocSeeker.API.DocSeeker.Domain.Services;
using DocSeeker.API.DocSeeker.Domain.Services.Communication;
using DocSeeker.API.Shared.Domain.Repositories;

namespace DocSeeker.API.DocSeeker.Services;

public class DoctorService : IDoctorService
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DoctorService(IDoctorRepository doctorRepository, IUnitOfWork unitOfWork)
    {
        _doctorRepository = doctorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Doctor>> ListAsync()
    {
        return await _doctorRepository.ListAsync();
    }

    public async Task<DoctorResponse> SaveAsync(Doctor doctor)
    {
        try
        {
            await _doctorRepository.AddAsync(doctor);
            await _unitOfWork.CompleteAsync();
            return new DoctorResponse(doctor);
        }
        catch (Exception e)
        {
            return new DoctorResponse($"An error occurred while saving the doctor: {e.Message}");
        }
    }

    public async Task<DoctorResponse> UpdateAsync(int id, Doctor doctor)
    {
        var existingDoctor = await _doctorRepository.FindById(id);

        if (existingDoctor == null)
            return new DoctorResponse("Category not found.");

        existingDoctor.FirstName = doctor.FirstName;

        try
        {
            _doctorRepository.Update(existingDoctor);
            await _unitOfWork.CompleteAsync();

            return new DoctorResponse(existingDoctor);
        }
        catch (Exception e)
        {
            return new DoctorResponse($"An error occurred while updating the doctor: {e.Message}");
        }
    }

    public async Task<DoctorResponse> DeleteAsync(int id)
    {
        var existingDoctor = await _doctorRepository.FindById(id);

        if (existingDoctor == null)
            return new DoctorResponse("Doctor not found.");

        try
        {
            _doctorRepository.Remove(existingDoctor);
            await _unitOfWork.CompleteAsync();

            return new DoctorResponse(existingDoctor);
        }
        catch (Exception e)
        {
            // Do some logging stuff
            return new DoctorResponse($"An error occurred while deleting the doctor: {e.Message}");
        }
    }
}


