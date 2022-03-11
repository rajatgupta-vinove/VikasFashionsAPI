namespace VikasFashionsAPI.APIServices.CompanyFiscalYearService
{
    public class CompanyFiscalYearService : ICompanyFiscalYearService
    {
        private readonly ILogger<CompanyFiscalYearService> _log;
        private readonly IConfiguration _config;
        private readonly VikasFashionsAPI.Data.DataContextVikasFashion _context;

        /// <summary>
        /// Default CompanyFiscalYear Service class constructor
        /// </summary>
        /// <param name="config"></param>
        /// <param name="log"></param>
        /// <param name="context"></param>
        public CompanyFiscalYearService(IConfiguration config, ILogger<CompanyFiscalYearService> log, DataContextVikasFashion context)
        {
            _config = config;
            _log = log;
            _context = context;
        }

        /// <summary>
        /// Get CompanyFiscalYear details based on userId Id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>CompanyFiscalYear</returns>
        public async Task<List<CompanyFiscalYear>> GetCompanyFiscalYearByUserIdAsync(int userId)
        {
            List<CompanyFiscalYear> companyFiscalYears = new List<CompanyFiscalYear>();
            try
            {
                if (userId == 0)
                    return companyFiscalYears;

                companyFiscalYears = await (from pp in _context.postingPeriods
                                            join cc in _context.CompanyControls on pp.CompanyId equals cc.CompanyId
                                            join c in _context.Companies on pp.CompanyId equals c.CompanyId
                                            where c.IsActive && cc.UserId == userId && pp.IsActive
                                            select new CompanyFiscalYear()
                                            {
                                                CompanyId = cc.CompanyId,
                                                CompanyName = c.CompanyName,
                                                CompanyCode = c.CompanyCode,
                                                FiscalYear = pp.FiscalYear,
                                                UserId = cc.UserId
                                            }).ToListAsync();

            }
            catch (Exception ex)
            {

                _log.LogError("Error while getting company fiscal year", ex);
            }
            return companyFiscalYears;
        }

    }
}
