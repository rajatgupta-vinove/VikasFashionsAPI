namespace VikasFashionsAPI.APIServices.DesignService
{
    public class DesignService : IDesignService
    {
        private readonly ILogger<DesignService> _log;
        private readonly IConfiguration _config;
        private readonly VikasFashionsAPI.Data.DataContextVikasFashion _context;
        public DesignService(IConfiguration config, ILogger<DesignService> log, DataContextVikasFashion context)
        {
            _config = config;
            _log = log;
            _context = context;
        }

        public async Task<Design> AddDesignAsync(Design design)
        {
            try
            {
                if (design == null)
                    throw new ArgumentNullException("AddDesign");
                _context.Designs.Add(design);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while adding design", ex);
            }
            return design;
        }

        public async Task<bool> DeleteDesignAsync(int designId)
        {
            bool isDeleted = false;
            try
            {
                if (designId == 0)
                    return isDeleted;
                var design = await _context.Designs.FirstOrDefaultAsync(m => m.DesignId == designId);
                if (design == null)
                    return isDeleted;
                _context.Designs.Remove(design);
                await _context.SaveChangesAsync();
                isDeleted = true;
            }
            catch (Exception ex)
            {
                _log.LogError("Error while deleting design", ex);
            }
            return isDeleted;
        }

        public async Task<IEnumerable<Design>> GetAllAsync()
        {
            _log.LogInformation("Design GetAll Called!");
            return await _context.Designs.ToListAsync();
        }

        public async Task<Design?> GetByIdAsync(int designId)
        {
            Design? design = null;
            try
            {
                if (designId == 0)
                    return design;
                design = await _context.Designs.FirstOrDefaultAsync(m => m.DesignId == designId);
            }
            catch (Exception ex)
            {

                _log.LogError("Error while getting design", ex);
            }
            return design;
        }

        public async Task<Design?> UpdateDesignAsync(Design design)
        {
            try
            {
                var exisingDesign = await _context.Designs.FirstOrDefaultAsync(m => m.DesignId == design.DesignId);
                if (exisingDesign == null)
                    return null;
                exisingDesign.DesignNumber = design.DesignNumber;
                exisingDesign.DesignId = design.DesignId;
                exisingDesign.DesignName = design.DesignName;
                exisingDesign.UpdatedOn = design.UpdatedOn;
                exisingDesign.UpdatedBy = design.UpdatedBy;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while updating design", ex);
            }
            return design;
        }

        public async Task<Design?> ChangeDesignStatusAsync(int designId, int updatedBy, DateTime updatedOn)
        {
            Design? exisingDesign = null;
            try
            {
                exisingDesign = await _context.Designs.FirstOrDefaultAsync(m => m.DesignId == designId);
                if (exisingDesign == null)
                    return null;
                exisingDesign.IsActive = !exisingDesign.IsActive;
                exisingDesign.UpdatedBy = updatedBy;
                exisingDesign.UpdatedOn = updatedOn;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                exisingDesign = null;
                _log.LogError("Error while updating Design", ex);
            }
            return exisingDesign;
        }
    }
}
