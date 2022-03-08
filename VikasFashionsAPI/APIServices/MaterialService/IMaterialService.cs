namespace VikasFashionsAPI.APIServices.MaterialService
{
    public interface IMaterialService
    {
        Task<IEnumerable<Material>> GetAllAsync();
        Task<Material?> GetByIdAsync(int materialId);
        Task<Material> AddMaterialAsync(Material material);
        Task<Material?> UpdateMaterialAsync(Material material);
        Task<bool> DeleteMaterialAsync(int materialId);
        Task<Material?> ChangeMaterialStatusAsync(int materialId, int updatedBy, DateTime updatedOn);

    }
}
