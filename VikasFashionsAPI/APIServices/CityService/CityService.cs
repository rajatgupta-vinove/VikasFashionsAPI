namespace VikasFashionsAPI.APIServices.CityService
{
    public class CityService : ICityService
    {
        private readonly ILogger<CityService> _log;
        private readonly IConfiguration _config;
        private readonly VikasFashionsAPI.Data.DataContextVikasFashion _context;
        public CityService(IConfiguration config, ILogger<CityService> log, DataContextVikasFashion context)
        {
            _config = config;
            _log = log;
            _context = context;
        }

        public async Task<City> AddCityAsync(City City)
        {
            try
            {
                if (City == null)
                    throw new ArgumentNullException("AddCity");
                _context.Cities.Add(City);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while adding City", ex);
            }
            return City;
        }

        public async Task<bool> DeleteCityAsync(int CityId)
        {
            bool isDeleted = false;
            try
            {
                if (CityId == 0)
                    return isDeleted;
                var City = await _context.Cities.FirstOrDefaultAsync(m => m.CityId == CityId);
                if (City == null)
                    return isDeleted;
                _context.Cities.Remove(City);
                await _context.SaveChangesAsync();
                isDeleted = true;
            }
            catch (Exception ex)
            {
                _log.LogError("Error while deleting City", ex);
            }
            return isDeleted;
        }

        public async Task<IEnumerable<City>> GetAllAsync()
        {
            _log.LogInformation("City GetAll Called!");
            return await _context.Cities.ToListAsync();
        }

        public async Task<IEnumerable<City>> GetAllByStatusAsync(bool? isActive)
        {
            if (isActive == null) return await GetAllAsync();
            return await _context.Cities.Where(m => m.IsActive == isActive).ToListAsync();
        }
        public async Task<IEnumerable<City>> GetAllByStateIdAsync(int stateId)
        {
            _log.LogInformation("City GetAllByStateIdAsync Called!");
            return await _context.Cities.Where(m => m.StateId == stateId).ToListAsync();
        }

        public async Task<IEnumerable<City>> GetAllByStateAndStatusAsync(int stateId, bool? isActive)
        {
            if (isActive == null) return await GetAllByStateIdAsync(stateId);
            return await _context.Cities.Where(m => m.IsActive == isActive && m.StateId == stateId).ToListAsync();
        }

        public async Task<City?> GetByIdAsync(int CityId)
        {
            City? City = null;
            try
            {
                if (CityId == 0)
                    return City;
                City = await _context.Cities.FirstOrDefaultAsync(m => m.CityId == CityId);
            }
            catch (Exception ex)
            {

                _log.LogError("Error while getting City", ex);
            }
            return City;
        }

        public async Task<City?> UpdateCityAsync(City City)
        {
            try
            {
                var exisingCity = await _context.Cities.FirstOrDefaultAsync(m => m.CityId == City.CityId);
                if (exisingCity == null)
                    return null;
                exisingCity.CityCode = City.CityCode;
                exisingCity.CityId = City.CityId;
                exisingCity.CityName = City.CityName;
                exisingCity.UpdatedOn = City.UpdatedOn;
                exisingCity.UpdatedBy = City.UpdatedBy;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while updating City", ex);
            }
            return City;
        }

        public async Task<City?> ChangeCityStatusAsync(int cityId, int updatedBy, DateTime updatedOn)
        {
            City? exisingCity = null;
            try
            {
                exisingCity = await _context.Cities.FirstOrDefaultAsync(m => m.CityId == cityId);
                if (exisingCity == null)
                    return null;
                exisingCity.IsActive = !exisingCity.IsActive;
                exisingCity.UpdatedBy = updatedBy;
                exisingCity.UpdatedOn = updatedOn;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                exisingCity = null;
                _log.LogError("Error while updating city", ex);
            }
            return exisingCity;
        }
    }
}
