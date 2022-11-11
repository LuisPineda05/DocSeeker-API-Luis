using DocSeeker.API.DocSeeker.Domain.Models;
using DocSeeker.API.DocSeeker.Domain.Repositories;
using DocSeeker.API.DocSeeker.Domain.Services;
using DocSeeker.API.DocSeeker.Domain.Services.Communication;
using DocSeeker.API.Shared.Domain.Repositories;
using DocSeeker.API.Security.Services;
using DocSeeker.API.Security.Persistence.Repositories;


namespace DocSeeker.API.DocSeeker.Services
{
    public class HourAvailableService : IHourAvailableService
    {
        private readonly IHourAvailableRepository _hourAvailableRepository;
        private readonly IUnitOfWork _unitOfWork;

        public HourAvailableService(IHourAvailableRepository HourAvailableRepository, IUnitOfWork unitOfWork)
        {
            _hourAvailableRepository = HourAvailableRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<HourAvailable>> ListAsync()
        {
            return await _hourAvailableRepository.ListAsync();
        }

        public async Task<HourAvailableResponse> SaveAsync(HourAvailable hourAvailable)
        {
            try
            {
                await _hourAvailableRepository.AddAsync(hourAvailable);
                await _unitOfWork.CompleteAsync();
                return new HourAvailableResponse(hourAvailable);
            }
            catch (Exception e)
            {
                return new HourAvailableResponse($"An error occurred while saving the hour available: {e.Message}");
            }
        }

        public async Task<HourAvailableResponse> UpdateAsync(int id, HourAvailable hourAvailable)
        {
            var existingHourAvailable = await _hourAvailableRepository.FindById(id);

            if (existingHourAvailable == null)
                return new HourAvailableResponse("Category not found.");

            existingHourAvailable.Hours = hourAvailable.Hours;
            existingHourAvailable.Booked = hourAvailable.Booked;


            try
            {
                _hourAvailableRepository.Update(existingHourAvailable);
                await _unitOfWork.CompleteAsync();

                return new HourAvailableResponse(existingHourAvailable);
            }
            catch (Exception e)
            {
                return new HourAvailableResponse($"An error occurred while updating the hour available: {e.Message}");
            }
        }

        public async Task<HourAvailableResponse> DeleteAsync(int id)
        {
            var existingHourAvailable = await _hourAvailableRepository.FindById(id);

            if (existingHourAvailable == null)
                return new HourAvailableResponse("Hour available not found.");

            try
            {
                _hourAvailableRepository.Remove(existingHourAvailable);
                await _unitOfWork.CompleteAsync();

                return new HourAvailableResponse(existingHourAvailable);
            }
            catch (Exception e)
            {
                // Do some logging stuff
                return new HourAvailableResponse($"An error occurred while deleting the hour available: {e.Message}");
            }
        }
    }
}

