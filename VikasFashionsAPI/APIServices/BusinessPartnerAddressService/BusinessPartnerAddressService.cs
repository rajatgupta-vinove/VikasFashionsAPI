using VikasFashionsAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace VikasFashionsAPI.APIServices.BusinessPartnerAddressService
{
    public class BusinessPartnerAddressService : IBusinessPartnerAddressService
    {
        private readonly ILogger<BusinessPartnerAddressService> _log;
        private readonly IConfiguration _config;
        private readonly VikasFashionsAPI.Data.DataContextVikasFashion _context;

        /// <summary>
        /// Default Business Partner Address Service class constructor
        /// </summary>
        /// <param name="config"></param>
        /// <param name="log"></param>
        /// <param name="context"></param>
        public BusinessPartnerAddressService(IConfiguration config, ILogger<BusinessPartnerAddressService> log, DataContextVikasFashion context)
        {
            _config = config;
            _log = log;
            _context = context;
        }

        /// <summary>
        /// Add New Business Partner Address
        /// </summary>
        /// <param name="businessPartnerAddress"></param>
        /// <returns>BusinessPartnerAddress</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<BusinessPartnerAddress> AddBusinessPartnerAddressAsync(BusinessPartnerAddress businessPartnerAddress)
        {
            try
            {
                if (businessPartnerAddress == null)
                    throw new ArgumentNullException("AddBusinessPartnerAddress");
                _context.BusinessPartnerAddress.Add(businessPartnerAddress);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while adding BusinessPartnerAddress", ex);
            }
            return businessPartnerAddress;
        }

        /// <summary>
        /// Delete Business Partner Address based on BusinessPartnerAddress Id
        /// </summary>
        /// <param name="BusinessPartnerAddressId"></param>
        /// <returns>isDeleted</returns>
        public async Task<bool> DeleteBusinessPartnerAddressAsync(int businessPartnerAddressId)
        {
            bool isDeleted = false;
            try
            {
                if (businessPartnerAddressId == 0)
                    return isDeleted;
                var businessPartnerAddress = await _context.BusinessPartnerAddress.FirstOrDefaultAsync(m => m.BusinessPartnerAddressId == businessPartnerAddressId);
                if (businessPartnerAddress == null)
                    return isDeleted;
                _context.BusinessPartnerAddress.Remove(businessPartnerAddress);
                await _context.SaveChangesAsync();
                isDeleted = true;
            }
            catch (Exception ex)
            {
                _log.LogError("Error while deleting BusinessPartnerAddress", ex);
            }
            return isDeleted;
        }

        /// <summary>
        /// Get all Business Partner Address Details.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<BusinessPartnerAddress>> GetAllBusinessPartnerAddressAsync()
        {
            _log.LogInformation("BusinessPartnerAddress GetAll Called!");
            return await _context.BusinessPartnerAddress.ToListAsync();
        }

        /// <summary>
        /// Get Business Partner Address details based on BusinessPartnerAddress Id.
        /// </summary>
        /// <param name="BusinessPartnerAddressId"></param>
        /// <returns>BusinessPartnerAddress</returns>
        public async Task<BusinessPartnerAddress?> GetByBusinessPartnerAddressIdAsync(int businessPartnerAddressId)
        {
            BusinessPartnerAddress? businessPartnerAddress = null;
            try
            {
                if (businessPartnerAddressId == 0)
                    return businessPartnerAddress;
                businessPartnerAddress = await _context.BusinessPartnerAddress.FirstOrDefaultAsync(m => m.BusinessPartnerAddressId == businessPartnerAddressId);
            }
            catch (Exception ex)
            {

                _log.LogError("Error while getting BusinessPartnerAddress", ex);
            }
            return businessPartnerAddress;
        }

        /// <summary>
        /// Update existing Business Partner Address details based on BusinessPartnerAddress ID.
        /// </summary>
        /// <param name="BusinessPartnerAddress"></param>
        /// <returns>BusinessPartnerAddress</returns>
        public async Task<BusinessPartnerAddress?> UpdateBusinessPartnerAddressAsync(BusinessPartnerAddress businessPartnerAddress)
        {
            try
            {
                var exisingBusinessPartnerAddress = await _context.BusinessPartnerAddress.FirstOrDefaultAsync(m => m.BusinessPartnerAddressId == businessPartnerAddress.BusinessPartnerAddressId);               
                if (exisingBusinessPartnerAddress == null)
                    return null;
                exisingBusinessPartnerAddress.BusinessPartnerAddressId = businessPartnerAddress.BusinessPartnerAddressId;
                exisingBusinessPartnerAddress.BusinessPartnerId = businessPartnerAddress.BusinessPartnerId;
                exisingBusinessPartnerAddress.BPAddress = businessPartnerAddress.BPAddress;
                exisingBusinessPartnerAddress.BPAddressType = businessPartnerAddress.BPAddressType;
                exisingBusinessPartnerAddress.BPAddressLine1 = businessPartnerAddress.BPAddressLine1;
                exisingBusinessPartnerAddress.BPAddressLine2 = businessPartnerAddress.BPAddressLine2;
                exisingBusinessPartnerAddress.CityID = businessPartnerAddress.CityID;
                exisingBusinessPartnerAddress.StateID = businessPartnerAddress.StateID;
                exisingBusinessPartnerAddress.CountryID = businessPartnerAddress.CountryID;
                exisingBusinessPartnerAddress.PinCode = businessPartnerAddress.PinCode;
                exisingBusinessPartnerAddress.GSTIN = businessPartnerAddress.GSTIN;
                exisingBusinessPartnerAddress.GSTType = businessPartnerAddress.GSTType;
                exisingBusinessPartnerAddress.IsDefault = businessPartnerAddress.IsDefault;
               
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while updating Business Partner Address", ex);
            }
            return businessPartnerAddress;
        }

     
    }
}
