namespace VikasFashionsAPI.APIServices.PlantBranchService
{ 
    public interface IPlantBranchService
{
        Task<IEnumerable<PlantBranch>> GetAllPlantBranchAsync();
        Task<PlantBranch?> GetByPlantBranchIdAsync(int plantId);
        Task<PlantBranch> AddPlantBranchAsync(PlantBranch plantBranch);
        Task<PlantBranch?> UpdatePlantBranchAsync(PlantBranch plantBranch);
        Task<bool> DeletePlantBranchAsync(int plantId);
        Task<bool> CheckPlantBranchStatusAsync(int plantId , string plantCode);
        Task<PlantBranch?> ChangePlantBranchStatusAsync(int plantId, int updatedBy, DateTime updatedOn);


    }
}
