namespace VikasFashionsAPI.APIServices.CityService
{
    public interface ICityService
    {
        Task<IEnumerable<City>> GetAllAsync();
        Task<IEnumerable<City>> GetAllByStatusAsync(bool? isActive);
        Task<IEnumerable<City>> GetAllByStateIdAsync(int stateId);
        Task<IEnumerable<City>> GetAllByStateAndStatusAsync(int stateId, bool? isActive);
        Task<City?> GetByIdAsync(int CityId);
        Task<City> AddCityAsync(City City);
        Task<City?> UpdateCityAsync(City City);
        Task<bool> DeleteCityAsync(int CityId);
    }
}
