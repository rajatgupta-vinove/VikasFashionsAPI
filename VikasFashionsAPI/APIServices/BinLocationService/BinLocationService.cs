namespace VikasFashionsAPI.APIServices.BinLocationService
{
    public class BinLocationService : IBinLocationService
    {
        private readonly ILogger<BinLocationService> _log;
        private readonly IConfiguration _config;
        private readonly VikasFashionsAPI.Data.DataContextVikasFashion _context;

        /// <summary>
        /// Default Bin Location Service class constructor
        /// </summary>
        /// <param name="config"></param>
        /// <param name="log"></param>
        /// <param name="context"></param>
        public BinLocationService(IConfiguration config, ILogger<BinLocationService> log, DataContextVikasFashion context)
        {
            _config = config;
            _log = log;
            _context = context;
        }

        /// <summary>
        /// Add New Bin Location
        /// </summary>
        /// <param name="BinLocation"></param>
        /// <returns>Bin Location</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<BinLocation> AddBinLocationAsync(BinLocation binLocation)
        {
            try
            {
                if (binLocation == null)
                    throw new ArgumentNullException("AddBinLocation");
                _context.BinLocations.Add(binLocation);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while adding bin location", ex);
            }
            return binLocation;
        }

        /// <summary>
        /// Delete Warehouse based on Bin Location Id
        /// </summary>
        /// <param name="BinLocId"></param>
        /// <returns>isDeleted</returns>
        public async Task<bool> DeleteBinLocationAsync(int binLocId)
        {
            bool isDeleted = false;
            try
            {
                if (binLocId == 0)
                    return isDeleted;
                var binLocation = await _context.BinLocations.FirstOrDefaultAsync(m => m.BinLocId == binLocId);
                if (binLocation == null)
                    return isDeleted;
                _context.BinLocations.Remove(binLocation);
                await _context.SaveChangesAsync();
                isDeleted = true;
            }
            catch (Exception ex)
            {
                _log.LogError("Error while deleting bin location", ex);
            }
            return isDeleted;
        }

        /// <summary>
        /// Get all Bin Location Details.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<BinLocation>> GetAllBinLocationAsync()
        {
            _log.LogInformation("Bin location GetAll Called!");
            return await _context.BinLocations.ToListAsync();
        }

        /// <summary>
        /// Get Plant Branch details based on Plant Branch Id.
        /// </summary>
        /// <param name="binLocId"></param>
        /// <returns>BinLocation</returns>
        public async Task<BinLocation?> GetByBinLocationIdAsync(int binLocId)
        {
            BinLocation? binLocation = null;
            try
            {
                if (binLocId == 0)
                    return binLocation;
                binLocation = await _context.BinLocations.FirstOrDefaultAsync(m => m.BinLocId == binLocId);
            }
            catch (Exception ex)
            {

                _log.LogError("Error while getting bin location", ex);
            }
            return binLocation;
        }

        /// <summary>
        /// Update existing bin location details based on bin location ID.
        /// </summary>
        /// <param name="binLocation"></param>
        /// <returns>BinLocation</returns>
        public async Task<BinLocation?> UpdateBinLocationAsync(BinLocation binLocation)
        {
            try
            {
                var exisingBinLocation = await _context.BinLocations.FirstOrDefaultAsync(m => m.BinLocId == binLocation.BinLocId);               
                if (exisingBinLocation == null)
                    return null;
                exisingBinLocation.BinLocId = binLocation.BinLocId;
                exisingBinLocation.BinLocationName = binLocation.BinLocationName;
                exisingBinLocation.WarehouseId = binLocation.WarehouseId;
                exisingBinLocation.SL1 = binLocation.SL1;
                exisingBinLocation.SL2 = binLocation.SL2;
                exisingBinLocation.SL3 = binLocation.SL3;
                exisingBinLocation.SL4 = binLocation.SL4;
                exisingBinLocation.Remark = binLocation.Remark;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while updating bin location", ex);
            }
            return binLocation;
        }

     
    }
}
