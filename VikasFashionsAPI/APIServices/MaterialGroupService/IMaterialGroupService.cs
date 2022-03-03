namespace VikasFashionsAPI.APIServices.MaterialGroupService
{
    public interface IMaterialGroupService
    {
        Task<IEnumerable<MaterialGroup>> GetAllAsync();
        Task<MaterialGroup?> GetByIdAsync(int materialGroupId);
        Task<MaterialGroup> AddMaterialGroupAsync(MaterialGroup materialGroup);
        Task<MaterialGroup?> UpdateMaterialGroupAsync(MaterialGroup materialGroup);
        Task<bool> DeleteMaterialGroupAsync(int materialGroupId);
    }
}
