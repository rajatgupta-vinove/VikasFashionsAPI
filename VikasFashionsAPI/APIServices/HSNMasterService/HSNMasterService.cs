namespace VikasFashionsAPI.APIServices.HSNMasterService
{
    public class HSNMasterService : IHSNMasterService
    {
        private readonly ILogger<HSNMasterService> _log;
        private readonly IConfiguration _config;
        private readonly VikasFashionsAPI.Data.DataContextVikasFashion _context;
        public HSNMasterService(IConfiguration config, ILogger<HSNMasterService> log, DataContextVikasFashion context)
        {
            _config = config;
            _log = log;
            _context = context;
        }
        public async Task<HSN> AddHsnAsync(HSN hsn)
        {
            try
            {
                if (hsn == null)
                    throw new ArgumentNullException("Add HSN");
                _context.HSNs.Add(hsn);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while adding HSN", ex);
            }
            return hsn;
        }

        public async Task<bool> DeleteHsnAsync(int hsnId)
        {
            bool isDeleted = false;
            try
            {
                if (hsnId == 0)
                    return isDeleted;
                var hsn = await _context.HSNs.FirstOrDefaultAsync(m => m.HSNId == hsnId);
                if (hsn == null)
                    return isDeleted;
                _context.HSNs.Remove(hsn);
                await _context.SaveChangesAsync();
                isDeleted = true;
            }
            catch (Exception ex)
            {
                _log.LogError("Error while deleting HSN", ex);
            }
            return isDeleted;
        }

        public async Task<IEnumerable<HSN>> GetAllAsync()
        {
            _log.LogInformation("HSN GetAll Called!");
            return await _context.HSNs.ToListAsync();
        }

        public async Task<HSN?> GetByIdAsync(int hsnId)
        {
            HSN? hsn = null;
            try
            {
                if (hsnId == 0)
                    return hsn;
                hsn = await _context.HSNs.FirstOrDefaultAsync(m => m.HSNId == hsnId);
            }
            catch (Exception ex)
            {

                _log.LogError("Error while getting HSN", ex);
            }
            return hsn;
        }

        public async Task<HSN?> UpdateHsnAsync(HSN hsn)
        {
            try
            {
                var existingHsn = await _context.HSNs.FirstOrDefaultAsync(m => m.HSNId == hsn.HSNId);
                if (existingHsn == null)
                    return null;
                existingHsn.HSNId = hsn.HSNId;
                existingHsn.HSNName = hsn.HSNName;
                existingHsn.HSNCode = hsn.HSNCode;
                existingHsn.Remark = hsn.Remark;


                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while updating HSN", ex);
            }
            return hsn;
        }
    }
}
