namespace VikasFashionsAPI.APIServices.CompanyGroupService
{
    public class CompanyGroupService : ICompanyGroupService
    {
        private readonly ILogger<CompanyGroupService> _log;
        private readonly IConfiguration _config;
        private readonly VikasFashionsAPI.Data.DataContextVikasFashion _context;
        public CompanyGroupService(IConfiguration config, ILogger<CompanyGroupService> log, DataContextVikasFashion context)
        {
            _config = config;
            _log = log;
            _context = context;
        }

        public async Task<CompanyGroup> AddCompanyGroupAsync(CompanyGroup companyGroup)
        {
            try
            {
                if (companyGroup == null)
                    throw new ArgumentNullException("Add Company Group");
                _context.CompanyGroups.Add(companyGroup);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while adding Company Group", ex);
            }
            return companyGroup;
        }

        public async Task<bool> DeleteCompanyGroupAsync(int companyGroupId)
        {
            bool isDeleted = false;
            try
            {
                if (companyGroupId == 0)
                    return isDeleted;
                var companyGroup = await _context.CompanyGroups.FirstOrDefaultAsync(m => m.CompanyGroupId == companyGroupId);
                if (companyGroup == null)
                    return isDeleted;
                _context.CompanyGroups.Remove(companyGroup);
                await _context.SaveChangesAsync();
                isDeleted = true;
            }
            catch (Exception ex)
            {
                _log.LogError("Error while deleting Company Group", ex);
            }
            return isDeleted;
        }

        public async Task<IEnumerable<CompanyGroup>> GetAllAsync()
        {
            _log.LogInformation("Company Group GetAll Called!");
            return await _context.CompanyGroups.ToListAsync();
        }

        public async Task<CompanyGroup?> GetByIdAsync(int companyGroupId)
        {
            CompanyGroup? companyGroup = null;
            try
            {
                if (companyGroupId == 0)
                    return companyGroup;
                companyGroup = await _context.CompanyGroups.FirstOrDefaultAsync(m => m.CompanyGroupId == companyGroupId);
            }
            catch (Exception ex)
            {

                _log.LogError("Error while getting Company Group", ex);
            }
            return companyGroup;
        }

        public async Task<CompanyGroup?> UpdateCompanyGroupAsync(CompanyGroup companyGroup)
        {
            try
            {
                var existingCompanyGroup = await _context.CompanyGroups.FirstOrDefaultAsync(m => m.CompanyGroupId == companyGroup.CompanyGroupId);
                if (existingCompanyGroup == null)
                    return null;
                existingCompanyGroup.CompanyGroupId = companyGroup.CompanyGroupId;
                existingCompanyGroup.CompanyGroupName = companyGroup.CompanyGroupName;
                existingCompanyGroup.Remark = companyGroup.Remark;
                existingCompanyGroup.UpdatedOn = companyGroup.UpdatedOn;
                existingCompanyGroup.UpdatedBy = companyGroup.UpdatedBy;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while updating Company Group", ex);
            }
            return companyGroup;
        }

        public async Task<CompanyGroup?> ChangeCompanyGroupStatusAsync(int companyGroupId, int updatedBy, DateTime updatedOn)
        {
            CompanyGroup? exisingCompanyGroup = null;
            try
            {
                exisingCompanyGroup = await _context.CompanyGroups.FirstOrDefaultAsync(m => m.CompanyGroupId == companyGroupId);
                if (exisingCompanyGroup == null)
                    return null;
                exisingCompanyGroup.IsActive = !exisingCompanyGroup.IsActive;
                exisingCompanyGroup.UpdatedBy = updatedBy;
                exisingCompanyGroup.UpdatedOn = updatedOn;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                exisingCompanyGroup = null;
                _log.LogError("Error while updating Company Group", ex);
            }
            return exisingCompanyGroup;
        }
    }
}
