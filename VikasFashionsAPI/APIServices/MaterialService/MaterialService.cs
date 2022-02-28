using VikasFashionsAPI.Data;
using Microsoft.EntityFrameworkCore;
namespace VikasFashionsAPI.APIServices.MaterialService
{
    public class MaterialService : IMaterialService
    {
        private readonly ILogger<MaterialService> _log;
        private readonly IConfiguration _config;
        private readonly VikasFashionsAPI.Data.DataContextVikasFashion _context;
        public MaterialService(IConfiguration config, ILogger<MaterialService> log, DataContextVikasFashion context)
        {
            _config = config;
            _log = log;
            _context = context;
        }

        public async Task<Material> AddMaterialAsync(Material material)
        {
            try
            {
                if (material == null)
                    throw new ArgumentNullException("AddMaterial");
                _context.Materials.Add(material);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while adding material", ex);
            }
            return material;
        }

        public async Task<bool> DeleteMaterialAsync(int materialId)
        {
            bool isDeleted = false;
            try
            {
                if (materialId == 0)
                    return isDeleted;
                var material = await _context.Materials.FirstOrDefaultAsync(m => m.MaterialId == materialId);
                if (material == null)
                    return isDeleted;
                _context.Materials.Remove(material);
                await _context.SaveChangesAsync();
                isDeleted = true;
            }
            catch (Exception ex)
            {
                _log.LogError("Error while deleting material", ex);
            }
            return isDeleted;
        }

        public async Task<IEnumerable<Material>> GetAllAsync()
        {
            _log.LogInformation("Material GetAll Called!");
            return await _context.Materials.ToListAsync();
        }

        public async Task<Material?> GetByIdAsync(int materialId)
        {
            Material? material = null;
            try
            {
                if (materialId == 0)
                    return material;
                material = await _context.Materials.FirstOrDefaultAsync(m => m.MaterialId == materialId);
            }
            catch (Exception ex)
            {

                _log.LogError("Error while getting country", ex);
            }
            return material;
        }
        /*To Confirm*/
        public async Task<Material?> UpdateMaterialAsync(Material material)
        {
            try
            {
                var exisingMaterial = await _context.Materials.FirstOrDefaultAsync(m => m.MaterialId == material.MaterialId);
                if (exisingMaterial == null)
                    return null;
                exisingMaterial.CreatedBy = material.CreatedBy;
                exisingMaterial.MaterialId = material.MaterialId;
                exisingMaterial.MaterialName = material.MaterialName;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while updating material", ex);
            }
            return material;
        }
    }
}
