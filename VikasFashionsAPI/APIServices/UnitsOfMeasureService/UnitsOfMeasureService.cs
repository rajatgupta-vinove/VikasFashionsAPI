namespace VikasFashionsAPI.APIServices.UnitsOfMeasureService
{
    public class UnitsOfMeasureService : IUnitsOfMeasureService
    {
        private readonly ILogger<UnitsOfMeasureService> _log;
        private readonly IConfiguration _config;
        private readonly VikasFashionsAPI.Data.DataContextVikasFashion _context;
        public UnitsOfMeasureService(IConfiguration config, ILogger<UnitsOfMeasureService> log, DataContextVikasFashion context)
        {
            _config = config;
            _log = log;
            _context = context;
        }

        public async Task<UnitsOfMeasure> AddUnitsOfMeasureAsync(UnitsOfMeasure unitsOfMeasure)
        {
            try
            {
                if (unitsOfMeasure == null)
                    throw new ArgumentNullException("AddUnitsOfMeasure");
                _context.UnitsOfMeasures.Add(unitsOfMeasure);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error  While Adding Units of Measure",ex);
            }
            return unitsOfMeasure;
        }

        public async Task<bool> DeleteUnitsOfMeasureAsync(int unitsOfMeasureId)
        {
            bool isDeleted = false;
            try
            {
                if (unitsOfMeasureId == 0)
                    return isDeleted;
                var unitsOfMeasure = await _context.UnitsOfMeasures.FirstOrDefaultAsync(m => m.UnitsOfMeasureId == unitsOfMeasureId);
                if(unitsOfMeasure == null)
                    return isDeleted;
                _context.UnitsOfMeasures.Remove(unitsOfMeasure);
                await _context.SaveChangesAsync();
                isDeleted = true;
            }
            catch (Exception ex)
            {
                _log.LogError("Error while deleting Units of Measure", ex);
            }
            return isDeleted;
        }

        public async Task<IEnumerable<UnitsOfMeasure>> GetAllAsync()
        {
            _log.LogInformation("Units of Measure GetAll Called");
            return await _context.UnitsOfMeasures.ToListAsync();
        }

        public async Task<UnitsOfMeasure?> GetByIdAsync(int unitsOfMeasureId)
        {
            UnitsOfMeasure? unitsOfMeasure = null;
            try
            {
                if (unitsOfMeasureId == 0)
                    return unitsOfMeasure;
                unitsOfMeasure = await _context.UnitsOfMeasures.FirstOrDefaultAsync(m=>m.UnitsOfMeasureId==unitsOfMeasureId);   
            }
            catch(Exception ex)
            {
                _log.LogError("Error while Getting Units of Measure",ex);
            }
            return unitsOfMeasure;

        }

        public async Task<UnitsOfMeasure?> UpdateUnitsOfMeasureAsync(UnitsOfMeasure unitsOfMeasure)
        {
            try
            {
                var existingUnitsOfMeasure = await _context.UnitsOfMeasures.FirstOrDefaultAsync(m=>m.UnitsOfMeasureId == unitsOfMeasure.UnitsOfMeasureId);
                if (existingUnitsOfMeasure == null)
                    return null;
                existingUnitsOfMeasure.UnitsOfMeasureId = unitsOfMeasure.UnitsOfMeasureId;
                existingUnitsOfMeasure.UnitsOfMeasureCode = unitsOfMeasure.UnitsOfMeasureCode;
                existingUnitsOfMeasure.UnitsOfMeasureName = unitsOfMeasure.UnitsOfMeasureName;
                existingUnitsOfMeasure.Remark = unitsOfMeasure.Remark;
                existingUnitsOfMeasure.UpdatedOn = unitsOfMeasure.UpdatedOn;    
                existingUnitsOfMeasure.UpdatedBy = unitsOfMeasure.UpdatedBy;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while Updating Units of Measure", ex);
            }
            return unitsOfMeasure;
        }

        public async Task<UnitsOfMeasure?> ChangeUnitsOfMeasureStatusAsync(int UnitsofMeasureId, int updatedBy, DateTime updatedOn)
        {
            UnitsOfMeasure? exisingUnitsOfMeasure = null;
            try
            {
                exisingUnitsOfMeasure = await _context.UnitsOfMeasures.FirstOrDefaultAsync(m => m.UnitsOfMeasureId == UnitsofMeasureId);
                if (exisingUnitsOfMeasure == null)
                    return null;
                exisingUnitsOfMeasure.IsActive = !exisingUnitsOfMeasure.IsActive;
                exisingUnitsOfMeasure.UpdatedBy = updatedBy;
                exisingUnitsOfMeasure.UpdatedOn = updatedOn;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                exisingUnitsOfMeasure = null;
                _log.LogError("Error while updating UnitsOfMeasure", ex);
            }
            return exisingUnitsOfMeasure;
        }
    }
}
