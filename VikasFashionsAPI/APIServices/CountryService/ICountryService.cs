using VikasFashionsAPI.Data;
namespace VikasFashionsAPI.APIServices.CountryService
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetAllAsync();
        Task<IEnumerable<Country>> GetAllByStatusAsync(bool? isActive);
        Task<Country?> GetByIdAsync(int countryId);
        Task<Country> AddCountryAsync(Country country);
        Task<Country?> UpdateCountryAsync(Country country);
        Task<bool> DeleteCountryAsync(int countryId);
        Task<Country?> ChangeCountryStatusAsync(int countryId);
    }
}
