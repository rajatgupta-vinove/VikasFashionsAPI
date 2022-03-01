using Microsoft.EntityFrameworkCore;
using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.APIServices.MaterialGroupService
{
    public class MaterialGroupService : IMaterialGroupService
    {
        private readonly ILogger<MaterialGroupService> _log;
        private readonly IConfiguration _config;
        private readonly VikasFashionsAPI.Data.DataContextVikasFashion _context;
        public MaterialGroupService(IConfiguration config, ILogger<MaterialGroupService> log, DataContextVikasFashion context)
        {
            _config = config;
            _log = log;
            _context = context;
        }
        public async Task<MaterialGroup> AddMaterialGroupAsync(MaterialGroup materialGroup)
        {
            try
            {
                if (materialGroup == null)
                    throw new ArgumentNullException("Add Material Group");
                _context.MaterialGroups.Add(materialGroup);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while adding Material Group", ex);
            }
            return materialGroup;
        }

        public async Task<bool> DeleteMaterialGroupAsync(int materialGroupId)
        {
            bool isDeleted = false;
            try
            {
                if (materialGroupId == 0)
                    return isDeleted;
                var materialGroup = await _context.MaterialGroups.FirstOrDefaultAsync(m => m.MaterialGroupId == materialGroupId);
                if (materialGroup == null)
                    return isDeleted;
                _context.MaterialGroups.Remove(materialGroup);
                await _context.SaveChangesAsync();
                isDeleted = true;
            }
            catch (Exception ex)
            {
                _log.LogError("Error while deleting Material Group", ex);
            }
            return isDeleted;
        }

        public async Task<IEnumerable<MaterialGroup>> GetAllAsync()
        {
            _log.LogInformation("MaterialGroup GetAll Called!");
            return await _context.MaterialGroups.ToListAsync();
        }

        public async Task<MaterialGroup?> GetByIdAsync(int materialGroupId)
        {
            MaterialGroup? materialGroup = null;
            try
            {
                if (materialGroupId == 0)
                    return materialGroup;
                materialGroup = await _context.MaterialGroups.FirstOrDefaultAsync(m => m.MaterialGroupId == materialGroupId);
            }
            catch (Exception ex)
            {

                _log.LogError("Error while getting MaterialGroup", ex);
            }
            return materialGroup;
        }

        public async Task<MaterialGroup?> UpdateMaterialGroupAsync(MaterialGroup materialGroup)
        {
            try
            {
                var exisingMaterialGroup = await _context.MaterialGroups.FirstOrDefaultAsync(m => m.MaterialGroupId == materialGroup.MaterialGroupId);
                if (exisingMaterialGroup == null)
                    return null;
                exisingMaterialGroup.MaterialGroupId = materialGroup.MaterialGroupId;
                exisingMaterialGroup.MaterialGroupName = materialGroup.MaterialGroupName;
                exisingMaterialGroup.MaterialGroupCode = materialGroup.MaterialGroupCode;
                exisingMaterialGroup.UnitsOfMeasureId = materialGroup.UnitsOfMeasureId;
                exisingMaterialGroup.Remark = materialGroup.Remark;


                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while updating MaterialGroup", ex);
            }
            return materialGroup;
        }
    }
}
