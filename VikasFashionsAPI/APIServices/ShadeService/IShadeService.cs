using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.APIServices.ShadeService
{
    public interface IShadeService
    {
        Task<IEnumerable<Shade>> GetAllAsync();
        Task<Shade?> GetByIdAsync(int chartId);
        Task<Shade> AddShadeAsync(Shade Chart);
        Task<Shade?> UpdateShadeAsync(Shade ChartChart);
        Task<bool> DeleteShadeAsync(int chartId);
    }
}
