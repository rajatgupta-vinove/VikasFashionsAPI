using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.APIServices.ChartService
{
    public interface IChartService
    {
        Task<IEnumerable<Chart>> GetAllAsync();
        Task<Chart?> GetByIdAsync(int chartId);
        Task<Chart> AddChartAsync(Chart Chart);
        Task<Chart?> UpdateChartAsync(Chart ChartChart);
        Task<bool> DeleteChartAsync(int chartId);
    }
}
