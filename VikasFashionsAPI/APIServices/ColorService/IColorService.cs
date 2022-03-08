namespace VikasFashionsAPI.APIServices.ColorService
{
    public interface IColorService
    {
        Task<IEnumerable<Color>> GetAllAsync();
        Task<Color?> GetByIdAsync(int colorId);
        Task<Color> AddColorAsync(Color color);
        Task<Color?> UpdateColorAsync(Color color);
        Task<bool> DeleteColorAsync(int colorId);
        Task<Color?> ChangeColorStatusAsync(int colorId, int updatedBy, DateTime updatedOn);

    }
}
