namespace VikasFashionsAPI.APIServices.DesignService
{
    public interface IDesignService
    {
        Task<IEnumerable<Design>> GetAllAsync();
        Task<Design?> GetByIdAsync(int designId);
        Task<Design> AddDesignAsync(Design design);
        Task<Design?> UpdateDesignAsync(Design design);
        Task<bool> DeleteDesignAsync(int designId);
        Task<Design?> ChangeDesignStatusAsync(int designId, int updatedBy, DateTime updatedOn);

    }
}
