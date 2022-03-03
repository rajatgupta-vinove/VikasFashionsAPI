namespace VikasFashionsAPI.APIServices.CompanyService
{
    public class CompanyService : ICompanyService
    {
        private readonly ILogger<CompanyService> _log;
        private readonly IConfiguration _config;
        private readonly VikasFashionsAPI.Data.DataContextVikasFashion _context;

        /// <summary>
        /// Default Company Service class constructor
        /// </summary>
        /// <param name="config"></param>
        /// <param name="log"></param>
        /// <param name="context"></param>
        public CompanyService(IConfiguration config, ILogger<CompanyService> log, DataContextVikasFashion context)
        {
            _config = config;
            _log = log;
            _context = context;
        }

        /// <summary>
        /// Add New Company
        /// </summary>
        /// <param name="company"></param>
        /// <returns>company</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<Company> AddCompanyAsync(Company company)
        {
            try
            {
                if (company == null)
                    throw new ArgumentNullException("AddCompany");
                _context.Companies.Add(company);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while adding company", ex);
            }
            return company;
        }

        /// <summary>
        /// Delete Company based on company Id
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns>isDeleted</returns>
        public async Task<bool> DeleteCompanyAsync(int companyId)
        {
            bool isDeleted = false;
            try
            {
                if (companyId == 0)
                    return isDeleted;
                var company = await _context.Companies.FirstOrDefaultAsync(m => m.CompanyId == companyId);
                if (company == null)
                    return isDeleted;
                _context.Companies.Remove(company);
                await _context.SaveChangesAsync();
                isDeleted = true;
            }
            catch (Exception ex)
            {
                _log.LogError("Error while deleting company", ex);
            }
            return isDeleted;
        }

        /// <summary>
        /// Get all Company Details.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Company>> GetAllCompanyAsync()
        {
            _log.LogInformation("Company GetAll Called!");
            return await _context.Companies.ToListAsync();
        }

        /// <summary>
        /// Get company details based on Company Id.
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns>company</returns>
        public async Task<Company?> GetByCompanyIdAsync(int companyId)
        {
            Company? company = null;
            try
            {
                if (companyId == 0)
                    return company;
                company = await _context.Companies.FirstOrDefaultAsync(m => m.CompanyId == companyId);
            }
            catch (Exception ex)
            {

                _log.LogError("Error while getting company", ex);
            }
            return company;
        }

        /// <summary>
        /// Update existing company details based on company ID.
        /// </summary>
        /// <param name="company"></param>
        /// <returns>company</returns>
        public async Task<Company?> UpdateCompanyAsync(Company company)
        {
            try
            {
                var exisingCompany = await _context.Companies.FirstOrDefaultAsync(m => m.CompanyId == company.CompanyId);
                if (exisingCompany == null)
                    return null;
                exisingCompany.CompanyId = company.CompanyId;
                exisingCompany.CompanyName = company.CompanyName;
                exisingCompany.CompanyShortName = company.CompanyShortName;
                exisingCompany.Address1 = company.Address1;
                exisingCompany.Address2 = company.Address2;
                exisingCompany.CityId=company.CityId;
                exisingCompany.PinCode=company.PinCode;
                exisingCompany.PaymentAddressLine1 = company.PaymentAddressLine1;
                exisingCompany.PaymentAddressLine2 = company.PaymentAddressLine2;
                exisingCompany.PaymentAddressCityId = company.PaymentAddressCityId;
                exisingCompany.PaymentAddressPinCode = company.PaymentAddressPinCode;
                exisingCompany.ActMgrName = company.ActMgrName;
                exisingCompany.PhoneNo1 = company.PhoneNo1;
                exisingCompany.PhoneNo2 = company.PhoneNo2;
                exisingCompany.EmailId = company.EmailId;
                exisingCompany.Fax = company.Fax;
                exisingCompany.PANNo = company.PANNo;
                exisingCompany.CSTNo = company.CSTNo;
                exisingCompany.Remark = company.Remark;
                exisingCompany.Logo = company.Logo;

        await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while updating company", ex);
            }
            return company;
        }
    }
}
