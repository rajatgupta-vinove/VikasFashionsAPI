namespace VikasFashionsAPI.APIServices.CatalogMasterService
{
    public interface ICatalogMasterService
    {
        Task<IEnumerable<Catalog>> GetAllAsync();
        Task<Catalog?> GetByIdAsync(int catalogId);
        Task<Catalog> AddCatalogAsync(Catalog catalog);
        Task<Catalog?> UpdateCatalogAsync(Catalog catalog);
        Task<bool> DeleteCatalogAsync(int catalogId);
        Task<Catalog?> ChangeCatalogStatusAsync(int catalogId, int updatedBy, DateTime updatedOn);
    }
}
