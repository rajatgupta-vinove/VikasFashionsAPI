namespace VikasFashionsAPI.APIServices.CompanyFiscalYearService
{
    public interface ICompanyFiscalYearService
    {
        Task<List<CompanyFiscalYear>> GetCompanyFiscalYearByUserIdAsync(int userId);

    }
}
