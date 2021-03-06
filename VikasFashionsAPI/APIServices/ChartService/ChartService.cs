namespace VikasFashionsAPI.APIServices.ChartService
{
    public class ChartService : IChartService
    {
        private readonly ILogger<ChartService> _log;
        private readonly IConfiguration _config;
        private readonly VikasFashionsAPI.Data.DataContextVikasFashion _context;

        public ChartService(IConfiguration config, ILogger<ChartService> log, DataContextVikasFashion context)
        {
            _config = config;
            _log = log;
            _context = context;
        }

        /// <summary>
        /// Save Chart Details
        /// </summary>
        /// <param name="chart">Object of type chart</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<Chart> AddChartAsync(Chart chart)
        {
            try
            {
                if (chart == null)
                    throw new ArgumentException("AddChart");
                _context.Charts.Add(chart);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while adding Chart", ex);
            }
            return chart;
        }
        public async Task<bool> DeleteChartAsync(int chartId)
        {
            bool isDeleted = false;
            try
            {
                if (chartId == 0)
                    return isDeleted;
                var chart = await _context.Charts.FirstOrDefaultAsync(m => m.ChartId == chartId);
                if (chart == null)
                    return isDeleted;
                _context.Charts.Remove(chart);
                await _context.SaveChangesAsync();
                isDeleted = true;
            }
            catch (Exception ex)
            {
                _log.LogError("Error while deleting country", ex);
            }
            return isDeleted;
        }
        public async Task<IEnumerable<Chart>> GetAllAsync()
        {
            _log.LogInformation("Chart GetAll Called!");
            return await _context.Charts.ToListAsync();
        }

        public async Task<Chart?> GetByIdAsync(int chartId)
        {
            Chart? chart = null;
            try
            {
                if (chartId == 0)
                    return chart;
                chart = await _context.Charts.FirstOrDefaultAsync(m => m.ChartId == chartId);
            }
            catch (Exception ex)
            {

                _log.LogError("Error while getting country", ex);
            }
            return chart;
        }
        public async Task<Chart?> UpdateChartAsync(Chart chart)
        {
            try
            {
                var exisingChart = await _context.Charts.FirstOrDefaultAsync(m => m.ChartId == chart.ChartId);
                if (exisingChart == null)
                    return null;
                exisingChart.ChartCode = chart.ChartCode;
                exisingChart.ChartId = chart.ChartId;
                exisingChart.ChartName = chart.ChartName;
                exisingChart.UpdatedOn = chart.UpdatedOn;
                exisingChart.UpdatedBy = chart.UpdatedBy;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while updating country", ex);
            }
            return chart;
        }

        public async Task<Chart?> ChangeChartStatusAsync(int chartId, int updatedBy, DateTime updatedOn)
        {
            Chart? existingChart = null;
            try
            {
                existingChart = await _context.Charts.FirstOrDefaultAsync(m => m.ChartId == chartId);
                if (existingChart == null)
                    return null;
                existingChart.IsActive = !existingChart.IsActive;
                existingChart.UpdatedBy = updatedBy;
                existingChart.UpdatedOn = updatedOn;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                existingChart = null;
                _log.LogError("Error while updating chart", ex);
            }
            return existingChart;
        }
    }
}
