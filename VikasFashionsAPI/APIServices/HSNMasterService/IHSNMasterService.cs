using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.APIServices.HSNMasterService
{
    public interface IHSNMasterService
    {
        Task<IEnumerable<HSN>> GetAllAsync();
        Task<HSN?> GetByIdAsync(int hsnId);
        Task<HSN> AddHsnAsync(HSN hsn);
        Task<HSN?> UpdateHsnAsync(HSN hsn);
        Task<bool> DeleteHsnAsync(int hsnId);
    }
}
