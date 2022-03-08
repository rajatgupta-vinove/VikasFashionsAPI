namespace VikasFashionsAPI.APIServices.AreaService
{
    public interface IAreaService
    {
        Task<IEnumerable<Area>> GetAllAsync();
        Task<Area?> GetByIdAsync(int areaId);
        Task<Area> AddAreaAsync(Area area);
        Task<Area?> UpdateAreaAsync(Area area);
        Task<bool> DeleteAreaAsync(int areaId);
        Task<Area?> ChangeAreaStatusAsync(int areaId, int updatedBy, DateTime updatedOn);

    }
}
