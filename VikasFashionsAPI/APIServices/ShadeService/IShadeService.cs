namespace VikasFashionsAPI.APIServices.ShadeService
{
    public interface IShadeService
    {
        Task<IEnumerable<Shade>> GetAllAsync();
        Task<Shade?> GetByIdAsync(int shadeId);
        Task<Shade> AddShadeAsync(Shade shade);
        Task<Shade?> UpdateShadeAsync(Shade shade);
        Task<bool> DeleteShadeAsync(int shadeId);
        Task<Shade?> ChangeShadeStatusAsync(int shadeId, int updatedBy, DateTime updatedOn);


    }
}
