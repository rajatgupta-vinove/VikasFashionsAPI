namespace VikasFashionsAPI.APIServices.CompanyGroupService
{
    public interface ICompanyGroupService
    {
        Task<IEnumerable<CompanyGroup>> GetAllAsync();
        Task<CompanyGroup?> GetByIdAsync(int companyGroupId);
        Task<CompanyGroup> AddCompanyGroupAsync(CompanyGroup companyGroup);
        Task<CompanyGroup?> UpdateCompanyGroupAsync(CompanyGroup companyGroup);
        Task<bool> DeleteCompanyGroupAsync(int companyGroupId);
    }
}
