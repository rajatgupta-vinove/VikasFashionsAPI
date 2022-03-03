namespace VikasFashionsAPI.APIServices.ChartService
{
    public interface IChartService
    {
        Task<IEnumerable<Chart>> GetAllAsync();
        Task<Chart?> GetByIdAsync(int chartId);        

        /// <summary>
        /// Save Chart Details
        /// </summary>
        /// <param name="chart">Object of type chart</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        Task<Chart> AddChartAsync(Chart Chart);
        Task<Chart?> UpdateChartAsync(Chart ChartChart);
        Task<bool> DeleteChartAsync(int chartId);
    }
}
