namespace VikasFashionsAPI.APIServices.CountryService
{
    public class CountryService : ICountryService
    {
        private readonly ILogger<CountryService> _log;
        private readonly IConfiguration _config;
        private readonly VikasFashionsAPI.Data.DataContextVikasFashion _context;
        public CountryService(IConfiguration config, ILogger<CountryService> log, DataContextVikasFashion context)
        {
            _config = config;
            _log = log;
            _context = context;
        }

        public async Task<Country> AddCountryAsync(Country country)
        {
            try
            {
                if (country == null)
                    throw new ArgumentNullException("AddCountry");
                _context.Countries.Add(country);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while adding country", ex);
            }
            return country;
        }

        public async Task<bool> DeleteCountryAsync(int countryId)
        {
            bool isDeleted = false;
            try
            {
                if (countryId == 0)
                    return isDeleted;
                var country = await _context.Countries.FirstOrDefaultAsync(m => m.CountryId == countryId);
                if (country == null)
                    return isDeleted;
                _context.Countries.Remove(country);
                await _context.SaveChangesAsync();
                isDeleted = true;
            }
            catch (Exception ex)
            {
                _log.LogError("Error while deleting country", ex);
            }
            return isDeleted;
        }

        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            _log.LogInformation("Country GetAll Called!");
            return await _context.Countries.ToListAsync();
        }

        public async Task<IEnumerable<Country>> GetAllByStatusAsync(bool? isActive)
        {
            if (isActive == null) return await GetAllAsync();
            return await _context.Countries.Where(m => m.IsActive == isActive).ToListAsync();
        }

        public async Task<Country?> GetByIdAsync(int countryId)
        {
            Country? country = null;
            try
            {
                if (countryId == 0)
                    return country;
                country = await _context.Countries.FirstOrDefaultAsync(m => m.CountryId == countryId);
            }
            catch (Exception ex)
            {

                _log.LogError("Error while getting country", ex);
            }
            return country;
        }

        public async Task<Country?> UpdateCountryAsync(Country country)
        {
            try
            {
                var exisingCountry = await _context.Countries.FirstOrDefaultAsync(m => m.CountryId == country.CountryId);
                if (exisingCountry == null)
                    return null;
                exisingCountry.CountryCode = country.CountryCode;
                exisingCountry.CountryId = country.CountryId;
                exisingCountry.CountryName = country.CountryName;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while updating country", ex);
            }
            return country;
        }
    }
}
