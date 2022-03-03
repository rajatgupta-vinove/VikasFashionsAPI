namespace VikasFashionsAPI.APIServices.MaterialTypeService
{
    public class MaterialTypeService : IMaterialTypeService
    {
        private readonly ILogger<MaterialTypeService> _log;
        private readonly IConfiguration _config;
        private readonly VikasFashionsAPI.Data.DataContextVikasFashion _context;
        public MaterialTypeService(IConfiguration config, ILogger<MaterialTypeService> log, DataContextVikasFashion context)
        {
            _config = config;
            _log = log;
            _context = context;
        }
        public async Task<MaterialType> AddMaterialTypeAsync(MaterialType materialType)
        {
            try
            {
                if (materialType == null)
                    throw new ArgumentNullException("AddMaterialType");
                _context.materialTypes.Add(materialType);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while adding material type", ex);
            }
            return materialType;
        }

        public async Task<bool> DeleteMaterialTypeAsync(int materialTypeId)
        {
            bool isDeleted = false;
            try
            {
                if (materialTypeId == 0)
                    return isDeleted;
                var materialType = await _context.materialTypes.FirstOrDefaultAsync(m => m.MaterialTypeId == materialTypeId);
                if (materialType == null)
                    return isDeleted;
                _context.materialTypes.Remove(materialType);
                await _context.SaveChangesAsync();
                isDeleted = true;
            }
            catch (Exception ex)
            {
                _log.LogError("Error while deleting material type", ex);
            }
            return isDeleted;
        }

        public async Task<IEnumerable<MaterialType>> GetAllAsync()
        {
            _log.LogInformation("Material Type GetAll Called!");
            return await _context.materialTypes.ToListAsync();
        }

        public async Task<MaterialType?> GetByIdAsync(int materialTypeId)
        {
            MaterialType? materialType = null;
            try
            {
                if (materialTypeId == 0)
                    return materialType;
                materialType = await _context.materialTypes.FirstOrDefaultAsync(m => m.MaterialTypeId == materialTypeId);
            }
            catch (Exception ex)
            {

                _log.LogError("Error while getting material type", ex);
            }
            return materialType;
        }

        public async Task<MaterialType?> UpdateMaterialTypeAsync(MaterialType materialType)
        {
            try
            {
                var exisingMaterialType = await _context.materialTypes.FirstOrDefaultAsync(m => m.MaterialTypeId == materialType.MaterialTypeId);
                if (exisingMaterialType == null)
                    return null;
                exisingMaterialType.MaterialTypeId = materialType.MaterialTypeId;
                exisingMaterialType.MaterialName = materialType.MaterialName;
                exisingMaterialType.MaterialCode = materialType.MaterialCode;
                exisingMaterialType.Remark = materialType.Remark;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while updating material type", ex);
            }
            return materialType;
        }
    }
}
