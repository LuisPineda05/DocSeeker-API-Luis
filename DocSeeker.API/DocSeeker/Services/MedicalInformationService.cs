using DocSeeker.API.DocSeeker.Domain.Models;
using DocSeeker.API.DocSeeker.Domain.Repositories;
using DocSeeker.API.DocSeeker.Domain.Services;
using DocSeeker.API.DocSeeker.Domain.Services.Communication;
using DocSeeker.API.Shared.Domain.Repositories;
using DocSeeker.API.Security.Services;
using DocSeeker.API.Security.Persistence.Repositories;


namespace DocSeeker.API.DocSeeker.Services
{
    public class MedicalInformationService : IMedicalInformationService
    {
        private readonly IMedicalInformationRepository _medicalInformationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MedicalInformationService(IMedicalInformationRepository MedicalInformationRepository, IUnitOfWork unitOfWork)
        {
            _medicalInformationRepository = MedicalInformationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<MedicalInformation>> ListAsync()
        {
            return await _medicalInformationRepository.ListAsync();
        }

        public async Task<MedicalInformationResponse> SaveAsync(MedicalInformation medicalInformation)
        {
            try
            {
                await _medicalInformationRepository.AddAsync(medicalInformation);
                await _unitOfWork.CompleteAsync();
                return new MedicalInformationResponse(medicalInformation);
            }
            catch (Exception e)
            {
                return new MedicalInformationResponse($"An error occurred while saving the hour available: {e.Message}");
            }
        }

        public async Task<MedicalInformationResponse> UpdateAsync(int id, MedicalInformation medicalInformation)
        {
            var existingMedicalInformation = await _medicalInformationRepository.FindById(id);

            if (existingMedicalInformation == null)
                return new MedicalInformationResponse("Category not found.");

            existingMedicalInformation.weight = medicalInformation.weight;
            existingMedicalInformation.height = medicalInformation.height;
            existingMedicalInformation.allergy = medicalInformation.allergy;
            existingMedicalInformation.bodyMass = medicalInformation.bodyMass;
            existingMedicalInformation.pathological = medicalInformation.pathological;

            try
            {
                _medicalInformationRepository.Update(existingMedicalInformation);
                await _unitOfWork.CompleteAsync();

                return new MedicalInformationResponse(existingMedicalInformation);
            }
            catch (Exception e)
            {
                return new MedicalInformationResponse($"An error occurred while updating the hour available: {e.Message}");
            }
        }

        public async Task<MedicalInformationResponse> DeleteAsync(int id)
        {
            var existingMedicalInformation = await _medicalInformationRepository.FindById(id);

            if (existingMedicalInformation == null)
                return new MedicalInformationResponse("Hour available not found.");

            try
            {
                _medicalInformationRepository.Remove(existingMedicalInformation);
                await _unitOfWork.CompleteAsync();

                return new MedicalInformationResponse(existingMedicalInformation);
            }
            catch (Exception e)
            {
                // Do some logging stuff
                return new MedicalInformationResponse($"An error occurred while deleting the hour available: {e.Message}");
            }
        }
    }
}



