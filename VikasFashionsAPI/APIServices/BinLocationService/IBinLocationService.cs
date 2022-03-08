
namespace VikasFashionsAPI.APIServices.BinLocationService
{ 
    public interface IBinLocationService
    {        
        Task<IEnumerable<BinLocation>> GetAllBinLocationAsync();
        Task<BinLocation?> GetByBinLocationIdAsync(int binLocId);
        Task<BinLocation> AddBinLocationAsync(BinLocation binLocation);
        Task<BinLocation?> UpdateBinLocationAsync(BinLocation binLocation);
        Task<bool> DeleteBinLocationAsync(int binLocId);
        Task<BinLocation?> ChangeBinlocationStatusAsync(int binLocId, int updatedBy, DateTime updatedOn);

    }
}
