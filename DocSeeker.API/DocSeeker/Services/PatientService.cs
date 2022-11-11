using DocSeeker.API.DocSeeker.Domain.Models;
using DocSeeker.API.DocSeeker.Domain.Repositories;
using DocSeeker.API.DocSeeker.Domain.Services;
using DocSeeker.API.DocSeeker.Domain.Services.Communication;
using DocSeeker.API.Shared.Domain.Repositories;

namespace DocSeeker.API.DocSeeker.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PatientService(IPatientRepository patientRepository, IUnitOfWork unitOfWork)
    {
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Patient>> ListAsync()
    {
        return await _patientRepository.ListAsync();
    }

    public async Task<PatientResponse> SaveAsync(Patient patient)
    {
        try
        {
            await _patientRepository.AddAsync(patient);
            await _unitOfWork.CompleteAsync();
            return new PatientResponse(patient);
        }
        catch (Exception e)
        {
            return new PatientResponse($"An error occurred while saving the patient: {e.Message}");
        }
    }

    public async Task<PatientResponse> UpdateAsync(int id, Patient patient)
    {
        var existingPatient = await _patientRepository.FindById(id);

        if (existingPatient == null)
            return new PatientResponse("Category not found.");

        existingPatient.FirstName = patient.FirstName;

        try
        {
            _patientRepository.Update(existingPatient);
            await _unitOfWork.CompleteAsync();

            return new PatientResponse(existingPatient);
        }
        catch (Exception e)
        {
            return new PatientResponse($"An error occurred while updating the patient: {e.Message}");
        }
    }

    public async Task<PatientResponse> DeleteAsync(int id)
    {
        var existingPatient = await _patientRepository.FindById(id);

        if (existingPatient == null)
            return new PatientResponse("Doctor not found.");

        try
        {
            _patientRepository.Remove(existingPatient);
            await _unitOfWork.CompleteAsync();

            return new PatientResponse(existingPatient);
        }
        catch (Exception e)
        {
            // Do some logging stuff
            return new PatientResponse($"An error occurred while deleting the patient: {e.Message}");
        }
    }
}



