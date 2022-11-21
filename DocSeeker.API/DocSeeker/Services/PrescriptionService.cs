using DocSeeker.API.DocSeeker.Domain.Models;
using DocSeeker.API.DocSeeker.Domain.Repositories;
using DocSeeker.API.DocSeeker.Domain.Services;
using DocSeeker.API.DocSeeker.Domain.Services.Communication;
using DocSeeker.API.Shared.Domain.Repositories;
using DocSeeker.API.Security.Services;
using DocSeeker.API.Security.Persistence.Repositories;


namespace DocSeeker.API.DocSeeker.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IPrescriptionRepository _prescriptionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PrescriptionService(IPrescriptionRepository prescriptionRepository, IUnitOfWork unitOfWork)
        {
            _prescriptionRepository = prescriptionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Prescription>> ListAsync()
        {
            return await _prescriptionRepository.ListAsync();
        }

        public async Task<PrescriptionResponse> SaveAsync(Prescription prescription)
        {
            try
            {
                await _prescriptionRepository.AddAsync(prescription);
                await _unitOfWork.CompleteAsync();
                return new PrescriptionResponse(prescription);
            }
            catch (Exception e)
            {
                return new PrescriptionResponse($"An error occurred while saving the hour available: {e.Message}");
            }
        }

        public async Task<PrescriptionResponse> UpdateAsync(int id, Prescription prescriptionInformation)
        {
            var existingPrescriptionInformation = await _prescriptionRepository.FindById(id);

            if (existingPrescriptionInformation == null)
                return new PrescriptionResponse("Category not found.");

            existingPrescriptionInformation.IdPatient = prescriptionInformation.IdPatient;
            existingPrescriptionInformation.Food = prescriptionInformation.Food;
            existingPrescriptionInformation.Rest = prescriptionInformation.Rest;
            existingPrescriptionInformation.NumberDose = prescriptionInformation.NumberDose;
            existingPrescriptionInformation.Hours = prescriptionInformation.Hours;
            existingPrescriptionInformation.DateIssue = prescriptionInformation.DateIssue;
            existingPrescriptionInformation.Drink = prescriptionInformation.Drink;
            existingPrescriptionInformation.Meals = prescriptionInformation.Meals;
            existingPrescriptionInformation.Condition = prescriptionInformation.Condition;
            existingPrescriptionInformation.MedicalSpeciality = prescriptionInformation.MedicalSpeciality;
            existingPrescriptionInformation.Medicines = prescriptionInformation.Medicines;
            existingPrescriptionInformation.DateExpiration = prescriptionInformation.DateExpiration;

            try
            {
                _prescriptionRepository.Update(existingPrescriptionInformation);
                await _unitOfWork.CompleteAsync();

                return new PrescriptionResponse(existingPrescriptionInformation);
            }
            catch (Exception e)
            {
                return new PrescriptionResponse($"An error occurred while updating the hour available: {e.Message}");
            }
        }

        public async Task<PrescriptionResponse> DeleteAsync(int id)
        {
            var existingPrescriptionInformation = await _prescriptionRepository.FindById(id);

            if (existingPrescriptionInformation == null)
                return new PrescriptionResponse("Hour available not found.");

            try
            {
                _prescriptionRepository.Remove(existingPrescriptionInformation);
                await _unitOfWork.CompleteAsync();

                return new PrescriptionResponse(existingPrescriptionInformation);
            }
            catch (Exception e)
            {
                // Do some logging stuff
                return new PrescriptionResponse($"An error occurred while deleting the hour available: {e.Message}");
            }
        }
    }
}



