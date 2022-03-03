namespace VikasFashionsAPI.APIServices.CompanyService
{
    public interface ICompanyService
    {
        Task<IEnumerable<Company>> GetAllCompanyAsync();
        Task<Company?> GetByCompanyIdAsync(int companyId);
        Task<Company> AddCompanyAsync(Company company);
        Task<Company?> UpdateCompanyAsync(Company company);
        Task<bool> DeleteCompanyAsync(int companyId);
    }
}
