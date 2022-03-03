namespace VikasFashionsAPI.APIServices.AreaService
{
    public class AreaService : IAreaService
    {
        private readonly ILogger<AreaService> _log;
        private readonly IConfiguration _config;
        private readonly VikasFashionsAPI.Data.DataContextVikasFashion _context;
        public AreaService(IConfiguration config, ILogger<AreaService> log, DataContextVikasFashion context)
        {
            _config = config;
            _log = log;
            _context = context;
        }
        public async Task<Area> AddAreaAsync(Area area)
        {
            try
            {
                if (area == null)
                    throw new ArgumentNullException("Add Area");
                _context.Areas.Add(area);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while adding Area", ex);
            }
            return area;
        }

        public async Task<bool> DeleteAreaAsync(int areaId)
        {
            bool isDeleted = false;
            try
            {
                if (areaId == 0)
                    return isDeleted;
                var area = await _context.Areas.FirstOrDefaultAsync(m => m.AreaId == areaId);
                if (area == null)
                    return isDeleted;
                _context.Areas.Remove(area);
                await _context.SaveChangesAsync();
                isDeleted = true;
            }
            catch (Exception ex)
            {
                _log.LogError("Error while deleting Area", ex);
            }
            return isDeleted;
        }

        public async Task<IEnumerable<Area>> GetAllAsync()
        {
            _log.LogInformation("Area GetAll Called!");
            return await _context.Areas.ToListAsync();
        }

        public async Task<Area?> GetByIdAsync(int areaId)
        {
            Area? area = null;
            try
            {
                if (areaId == 0)
                    return area;
                area = await _context.Areas.FirstOrDefaultAsync(m => m.AreaId == areaId);
            }
            catch (Exception ex)
            {

                _log.LogError("Error while getting Area", ex);
            }
            return area;
        }

        public async Task<Area?> UpdateAreaAsync(Area area)
        {
            try
            {
                var existingArea = await _context.Areas.FirstOrDefaultAsync(m => m.AreaId == area.AreaId);
                if (existingArea == null)
                    return null;
                existingArea.AreaId = area.AreaId;  
                existingArea.AreaName = area.AreaName;
                existingArea.AreaCode = area.AreaCode;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while updating Area", ex);
            }
            return area;
        }
    }
}
