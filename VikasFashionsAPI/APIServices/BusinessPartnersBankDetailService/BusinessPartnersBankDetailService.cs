namespace VikasFashionsAPI.APIServices.BusinessPartnersBankDetailService
{
    public class BusinessPartnersBankDetailService : IBusinessPartnersBankDetailService
    {
        private readonly ILogger<BusinessPartnersBankDetailService> _log;
        private readonly IConfiguration _config;
        private readonly VikasFashionsAPI.Data.DataContextVikasFashion _context;

        /// <summary>
        /// Default Business Partners Bank Detail Service class constructor
        /// </summary>
        /// <param name="config"></param>
        /// <param name="log"></param>
        /// <param name="context"></param>
        public BusinessPartnersBankDetailService(IConfiguration config, ILogger<BusinessPartnersBankDetailService> log, DataContextVikasFashion context)
        {
            _config = config;
            _log = log;
            _context = context;
        }

        /// <summary>
        /// Add New Business Partners Bank Detail 
        /// </summary>
        /// <param name="BusinessPartnersBankDetail"></param>
        /// <returns>BusinessPartnersBankDetail</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<BusinessPartnersBankDetail> AddBusinessPartnersBankDetailAsync(BusinessPartnersBankDetail businessPartnersBankDetail)
        {
            try
            {
                if (businessPartnersBankDetail == null)
                    throw new ArgumentNullException("AddBusinessPartnersBankDetail");
                _context.BusinessPartnersBankDetails.Add(businessPartnersBankDetail);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while adding BusinessPartnersBankDetail", ex);
            }
            return businessPartnersBankDetail;
        }

        /// <summary>
        /// Delete Business Partners Bank Detail  based on BusinessPartnersBankDetail Id
        /// </summary>
        /// <param name="BusinessPartnersBankDetailId"></param>
        /// <returns>isDeleted</returns>
        public async Task<bool> DeleteBusinessPartnersBankDetailAsync(int businessPartnersBankDetailId)
        {
            bool isDeleted = false;
            try
            {
                if (businessPartnersBankDetailId == 0)
                    return isDeleted;
                var businessPartnersBankDetail = await _context.BusinessPartnersBankDetails.FirstOrDefaultAsync(m => m.BPBankId == businessPartnersBankDetailId);
                if (businessPartnersBankDetail == null)
                    return isDeleted;
                _context.BusinessPartnersBankDetails.Remove(businessPartnersBankDetail);
                await _context.SaveChangesAsync();
                isDeleted = true;
            }
            catch (Exception ex)
            {
                _log.LogError("Error while deleting BusinessPartnersBankDetail", ex);
            }
            return isDeleted;
        }

        /// <summary>
        /// Get all Business Partners Bank Detail  Details.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<BusinessPartnersBankDetail>> GetAllBusinessPartnersBankDetailAsync()
        {
            _log.LogInformation("BusinessPartnersBankDetail GetAll Called!");
            return await _context.BusinessPartnersBankDetails.ToListAsync();
        }

        /// <summary>
        /// Get Business Partners Bank details based on BusinessPartnersBankDetail Id.
        /// </summary>
        /// <param name="BusinessPartnersBankDetailId"></param>
        /// <returns>BusinessPartnersBankDetail</returns>
        public async Task<BusinessPartnersBankDetail?> GetByBusinessPartnersBankDetailIdAsync(int businessPartnersBankDetailId)
        {
            BusinessPartnersBankDetail? businessPartnersBankDetail = null;
            try
            {
                if (businessPartnersBankDetailId == 0)
                    return businessPartnersBankDetail;
                businessPartnersBankDetail = await _context.BusinessPartnersBankDetails.FirstOrDefaultAsync(m => m.BPBankId  == businessPartnersBankDetailId);
            }
            catch (Exception ex)
            {

                _log.LogError("Error while getting BusinessPartnersBankDetail", ex);
            }
            return businessPartnersBankDetail;
        }

        /// <summary>
        /// Update existing Business Partners Bank Detail details based on BusinessPartnersBankDetail ID.
        /// </summary>
        /// <param name="BusinessPartnersBankDetail"></param>
        /// <returns>BusinessPartnersBankDetail</returns>
        public async Task<BusinessPartnersBankDetail?> UpdateBusinessPartnersBankDetailAsync(BusinessPartnersBankDetail businessPartnersBankDetail)
        {
            try
            {
                var exisingBusinessPartnersBankDetail = await _context.BusinessPartnersBankDetails.FirstOrDefaultAsync(m => m.BPBankId == businessPartnersBankDetail.BPBankId);               
                if (exisingBusinessPartnersBankDetail == null)
                    return null;
                exisingBusinessPartnersBankDetail.BPBankId = businessPartnersBankDetail.BPBankId;
                exisingBusinessPartnersBankDetail.BusinessPartnerId = businessPartnersBankDetail.BusinessPartnerId;
                exisingBusinessPartnersBankDetail.BankCode = businessPartnersBankDetail.BankCode;
                exisingBusinessPartnersBankDetail.BankName = businessPartnersBankDetail.BankName;
                exisingBusinessPartnersBankDetail.BankAccountNo = businessPartnersBankDetail.BankAccountNo;
                exisingBusinessPartnersBankDetail.BankAccountName = businessPartnersBankDetail.BankAccountName;
                exisingBusinessPartnersBankDetail.BankAccountType = businessPartnersBankDetail.BankAccountType;
                exisingBusinessPartnersBankDetail.BankSWIFTCode = businessPartnersBankDetail.BankSWIFTCode;
                exisingBusinessPartnersBankDetail.BankIFSCCode = businessPartnersBankDetail.BankIFSCCode;
                exisingBusinessPartnersBankDetail.BankBranchCode = businessPartnersBankDetail.BankBranchCode;
                exisingBusinessPartnersBankDetail.BankBranchName = businessPartnersBankDetail.BankBranchName;
                exisingBusinessPartnersBankDetail.BankAddressLine1 = businessPartnersBankDetail.BankAddressLine1;
                exisingBusinessPartnersBankDetail.BankAddressLine2 = businessPartnersBankDetail.BankAddressLine2;
                exisingBusinessPartnersBankDetail.BankCity = businessPartnersBankDetail.BankCity;
                exisingBusinessPartnersBankDetail.BankState = businessPartnersBankDetail.BankState;
                exisingBusinessPartnersBankDetail.BankCountry = businessPartnersBankDetail.BankCountry;
                exisingBusinessPartnersBankDetail.BankPinCode = businessPartnersBankDetail.BankPinCode;
                exisingBusinessPartnersBankDetail.IsDefault = businessPartnersBankDetail.IsDefault;
                exisingBusinessPartnersBankDetail.UpdatedOn = businessPartnersBankDetail.UpdatedOn;
                exisingBusinessPartnersBankDetail.UpdatedBy = businessPartnersBankDetail.UpdatedBy;




                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while updating Business Partners Bank Detail", ex);
            }
            return businessPartnersBankDetail;
        }

        public async Task<BusinessPartnersBankDetail?> ChangeBusinessPartnersBankDetailStatusAsync(int BusinessPartnersBankDetailId, int updatedBy, DateTime updatedOn)
        {
            BusinessPartnersBankDetail? existingBusinessPartnersBankDetail= null;
            try
            {
                existingBusinessPartnersBankDetail = await _context.BusinessPartnersBankDetails.FirstOrDefaultAsync(m => m.BPBankId == BusinessPartnersBankDetailId);
                if (existingBusinessPartnersBankDetail == null)
                    return null;
                existingBusinessPartnersBankDetail.IsActive = !existingBusinessPartnersBankDetail.IsActive;
                existingBusinessPartnersBankDetail.UpdatedBy = updatedBy;
                existingBusinessPartnersBankDetail.UpdatedOn = updatedOn;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                existingBusinessPartnersBankDetail = null;
                _log.LogError("Error while updating Business Partners BankDetail", ex);
            }
            return existingBusinessPartnersBankDetail;
        }



     
    }
}
