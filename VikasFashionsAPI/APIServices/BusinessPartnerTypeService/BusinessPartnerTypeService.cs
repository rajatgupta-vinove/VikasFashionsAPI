namespace VikasFashionsAPI.APIServices.BusinessPartnerTypeService
{
    public class BusinessPartnerTypeService : IBusinessPartnerTypeService
    {



        private readonly ILogger<BusinessPartnerTypeService> _log;
        private readonly IConfiguration _config;
        private readonly VikasFashionsAPI.Data.DataContextVikasFashion _context;
        public BusinessPartnerTypeService(IConfiguration config, ILogger<BusinessPartnerTypeService> log, DataContextVikasFashion context)
        {
            _config = config;
            _log = log;
            _context = context;
        }
        public async Task<BusinessPartnerType> AddBusinessPartnerTypeAsync(BusinessPartnerType businessPartnerType)
        {
            try
            {
                if (businessPartnerType == null)
                    throw new ArgumentNullException("Add Business PartnerType");
                _context.BusinessPartnerTypes.Add(businessPartnerType);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while adding Business PartnerType", ex);
            }
            return businessPartnerType;
        }

        public async Task<bool> DeleteBusinessPartnerTypeAsync(int businessPartnerTypeId)
        {
            bool isDeleted = false;
            try
            {
                if (businessPartnerTypeId == 0)
                    return isDeleted;
                var businessPartnerType = await _context.BusinessPartnerTypes.FirstOrDefaultAsync(m => m.BusinessPartnerTypeId == businessPartnerTypeId);
                if (businessPartnerType == null)
                    return isDeleted;
                _context.BusinessPartnerTypes.Remove(businessPartnerType);
                await _context.SaveChangesAsync();
                isDeleted = true;
            }
            catch (Exception ex)
            {
                _log.LogError("Error while deleting Business PartnerType", ex);
            }
            return isDeleted;
        }

        public async Task<IEnumerable<BusinessPartnerType>> GetAllAsync()
        {
            _log.LogInformation("BusinessPartnerType GetAll Called!");
            return await _context.BusinessPartnerTypes.ToListAsync();
        }

        public async Task<BusinessPartnerType?> GetByIdAsync(int businessPartnerTypeId)
        {
            BusinessPartnerType? businessPartnerType = null;
            try
            {
                if (businessPartnerTypeId == 0)
                    return businessPartnerType;
                businessPartnerType = await _context.BusinessPartnerTypes.FirstOrDefaultAsync(m => m.BusinessPartnerTypeId == businessPartnerTypeId);
            }
            catch (Exception ex)
            {

                _log.LogError("Error while getting BusinessPartnerType", ex);
            }
            return businessPartnerType;
        }

        public async Task<BusinessPartnerType?> UpdateBusinessPartnerTypeAsync(BusinessPartnerType businessPartnerType)
        {
            try
            {
                var exisingBusinessPartnerType = await _context.BusinessPartnerTypes.FirstOrDefaultAsync(m => m.BusinessPartnerTypeId == businessPartnerType.BusinessPartnerTypeId);
                if (exisingBusinessPartnerType == null)
                    return null;
                exisingBusinessPartnerType.BusinessPartnerTypeId = businessPartnerType.BusinessPartnerTypeId;
                exisingBusinessPartnerType.BusinessPartnerTypeName = businessPartnerType.BusinessPartnerTypeName;
                exisingBusinessPartnerType.Remark = businessPartnerType.Remark;
                exisingBusinessPartnerType.CreatedBy = businessPartnerType.CreatedBy;


                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while updating BusinessPartnerType", ex);
            }
            return businessPartnerType;
        }
    }
}
