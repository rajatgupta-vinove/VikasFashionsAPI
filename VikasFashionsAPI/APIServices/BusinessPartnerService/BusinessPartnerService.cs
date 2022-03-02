using Microsoft.EntityFrameworkCore;
using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.APIServices.BusinessPartnerService
{
    public class BusinessPartnerService : IBusinessPartnerService
    {

        private readonly ILogger<BusinessPartnerService> _log;
        private readonly IConfiguration _config;
        private readonly VikasFashionsAPI.Data.DataContextVikasFashion _context;
        public BusinessPartnerService(IConfiguration config, ILogger<BusinessPartnerService> log, DataContextVikasFashion context)
        {
            _config = config;
            _log = log;
            _context = context;
        }
        public async Task<BusinessPartner> AddBusinessPartnerAsync(BusinessPartner businessPartner)
        {
            try
            {
                if (businessPartner == null)
                    throw new ArgumentNullException("Add Business Partner");
                _context.BusinessPartners.Add(businessPartner);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while adding Business Partner", ex);
            }
            return businessPartner;
        }

        public async Task<bool> DeleteBusinessPartnerAsync(int businessPartnerId)
        {
            bool isDeleted = false;
            try
            {
                if (businessPartnerId == 0)
                    return isDeleted;
                var businessPartner = await _context.BusinessPartners.FirstOrDefaultAsync(m => m.BusinessPartnerId == businessPartnerId);
                if (businessPartner == null)
                    return isDeleted;
                _context.BusinessPartners.Remove(businessPartner);
                await _context.SaveChangesAsync();
                isDeleted = true;
            }
            catch (Exception ex)
            {
                _log.LogError("Error while deleting Business Partner", ex);
            }
            return isDeleted;
        }

        public async Task<IEnumerable<BusinessPartner>> GetAllAsync()
        {
            _log.LogInformation("BusinessPartner GetAll Called!");
            return await _context.BusinessPartners.ToListAsync();
        }

        public async Task<BusinessPartner?> GetByIdAsync(int businessPartnerId)
        {
            BusinessPartner? businessPartner = null;
            try
            {
                if (businessPartnerId == 0)
                    return businessPartner;
                businessPartner = await _context.BusinessPartners.FirstOrDefaultAsync(m => m.BusinessPartnerId == businessPartnerId);
            }
            catch (Exception ex)
            {

                _log.LogError("Error while getting BusinessPartner", ex);
            }
            return businessPartner;
        }

        public async Task<BusinessPartner?> UpdateBusinessPartnerAsync(BusinessPartner businessPartner)
        {
            try
            {
                var exisingBusinessPartner = await _context.BusinessPartners.FirstOrDefaultAsync(m => m.BusinessPartnerId == businessPartner.BusinessPartnerId);
                if (exisingBusinessPartner == null)
                    return null;
                exisingBusinessPartner.BusinessPartnerId = businessPartner.BusinessPartnerId;
                exisingBusinessPartner.BusinessPartnerName = businessPartner.BusinessPartnerName;
                exisingBusinessPartner.BPLegalName = businessPartner.BPLegalName;
                exisingBusinessPartner.PANNo = businessPartner.PANNo;


                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while updating BusinessPartner", ex);
            }
            return businessPartner;
        }
    }
}
