namespace VikasFashionsAPI.APIServices.CompanyFiscalYearService
{
    public interface ICompanyFiscalYearService
    {
        Task<CompanyControl?> GetCompanyControlByUserIdAsync(int userId);

        Task<List<CompanyFiscalYear>> GetCompanyFiscalYearByUserIdAsync(int userId);

    }
}
