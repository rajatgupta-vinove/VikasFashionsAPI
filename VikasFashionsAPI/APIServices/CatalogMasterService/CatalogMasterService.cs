namespace VikasFashionsAPI.APIServices.CatalogMasterService
{
    public class CatalogMasterService : ICatalogMasterService
    {
        private readonly ILogger<CatalogMasterService> _log;
        private readonly IConfiguration _config;
        private readonly VikasFashionsAPI.Data.DataContextVikasFashion _context;
        public CatalogMasterService(IConfiguration config, ILogger<CatalogMasterService> log, DataContextVikasFashion context)
        {
            _config = config;
            _log = log;
            _context = context;
        }
        public async Task<Catalog> AddCatalogAsync(Catalog catalog)
        {
            try
            {
                if (catalog == null)
                    throw new ArgumentNullException("Add Catalog");
                _context.Catalogs.Add(catalog);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while adding Catalog", ex);
            }
            return catalog;
        }

        public async Task<bool> DeleteCatalogAsync(int catalogId)
        {
            bool isDeleted = false;
            try
            {
                if (catalogId == 0)
                    return isDeleted;
                var catalog = await _context.Catalogs.FirstOrDefaultAsync(m => m.CatalogId == catalogId);
                if (catalog == null)
                    return isDeleted;
                _context.Catalogs.Remove(catalog);
                await _context.SaveChangesAsync();
                isDeleted = true;
            }
            catch (Exception ex)
            {
                _log.LogError("Error while deleting Catalog", ex);
            }
            return isDeleted;
        }

        public async Task<IEnumerable<Catalog>> GetAllAsync()
        {
            _log.LogInformation("Catalog GetAll Called!");
            return await _context.Catalogs.ToListAsync();
        }

        public async Task<Catalog?> GetByIdAsync(int catalogId)
        {
            Catalog? catalog = null;
            try
            {
                if (catalogId == 0)
                    return catalog;
                catalog = await _context.Catalogs.FirstOrDefaultAsync(m => m.CatalogId == catalogId);
            }
            catch (Exception ex)
            {

                _log.LogError("Error while getting Catalog", ex);
            }
            return catalog;
        }

        public async Task<Catalog?> UpdateCatalogAsync(Catalog catalog)
        {
            try
            {
                var existingCatalog = await _context.Catalogs.FirstOrDefaultAsync(m => m.CatalogId == catalog.CatalogId);
                if (existingCatalog == null)
                    return null;
                existingCatalog.CatalogId = catalog.CatalogId;
                existingCatalog.CatalogName = catalog.CatalogName;
                existingCatalog.CatalogIndex = catalog.CatalogIndex;
                existingCatalog.Remark = catalog.Remark;
                existingCatalog.UpdatedBy = catalog.UpdatedBy;
                existingCatalog.UpdatedOn = catalog.UpdatedOn;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while updating Catalog", ex);
            }
            return catalog;
        }

        public async Task<Catalog?> ChangeCatalogStatusAsync(int catalogId, int updatedBy, DateTime updatedOn)
        {
            Catalog? existingCatalog = null;
            try
            {
                existingCatalog = await _context.Catalogs.FirstOrDefaultAsync(m => m.CatalogId == catalogId);
                if (existingCatalog == null)
                    return null;
                existingCatalog.IsActive = !existingCatalog.IsActive;
                existingCatalog.UpdatedBy = updatedBy;
                existingCatalog.UpdatedOn = updatedOn;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                existingCatalog = null;
                _log.LogError("Error while updating catalog", ex);
            }
            return existingCatalog;
        }
    }
}
