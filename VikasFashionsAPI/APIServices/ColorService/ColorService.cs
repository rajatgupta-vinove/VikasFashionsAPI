namespace VikasFashionsAPI.APIServices.ColorService
{
    public class ColorService : IColorService
    {

        private readonly ILogger<ColorService> _log;
        private readonly IConfiguration _config;
        private readonly VikasFashionsAPI.Data.DataContextVikasFashion _context;
        public ColorService(IConfiguration config, ILogger<ColorService> log, DataContextVikasFashion context)
        {
            _config = config;
            _log = log;
            _context = context;
        }

        public async Task<Color> AddColorAsync(Color color)
        {
            try
            {
                if (color == null)
                    throw new ArgumentNullException("AddCountry");
                _context.Colors.Add(color);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while adding color", ex);
            }
            return color;
        }

        public async Task<bool> DeleteColorAsync(int colorId)
        {
            bool isDeleted = false;
            try
            {
                if (colorId == 0)
                    return isDeleted;
                var color = await _context.Colors.FirstOrDefaultAsync(m => m.ColorId == colorId);
                if (color == null)
                    return isDeleted;
                _context.Colors.Remove(color);
                await _context.SaveChangesAsync();
                isDeleted = true;
            }
            catch (Exception ex)
            {
                _log.LogError("Error while deleting color", ex);
            }
            return isDeleted;
        }

        public async Task<IEnumerable<Color>> GetAllAsync()
        {
            _log.LogInformation("Color GetAll Called!");
            return await _context.Colors.ToListAsync();
        }

        public async Task<Color?> GetByIdAsync(int colorId)
        {
            Color? color = null;
            try
            {
                if (colorId == 0)
                    return color;
                color = await _context.Colors.FirstOrDefaultAsync(m => m.ColorId == colorId);
            }
            catch (Exception ex)
            {

                _log.LogError("Error while getting country", ex);
            }
            return color;
        }

        public async Task<Color?> UpdateColorAsync(Color color)
        {
            try
            {
                var exisingColor = await _context.Colors.FirstOrDefaultAsync(m => m.ColorId == color.ColorId);
                if (exisingColor == null)
                    return null;
                exisingColor.ColorNumber = color.ColorNumber;
                exisingColor.ColorId = color.ColorId;
                exisingColor.ColorName = color.ColorName;
                exisingColor.UpdatedBy = color.UpdatedBy;
                exisingColor.UpdatedOn = color.UpdatedOn;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while updating country", ex);
            }
            return color;
        }

        public async Task<Color?> ChangeColorStatusAsync(int colorId, int updatedBy, DateTime updatedOn)
        {
            Color? exisingColor = null;
            try
            {
                exisingColor = await _context.Colors.FirstOrDefaultAsync(m => m.ColorId == colorId);
                if (exisingColor == null)
                    return null;
                exisingColor.IsActive = !exisingColor.IsActive;
                exisingColor.UpdatedBy = updatedBy;
                exisingColor.UpdatedOn = updatedOn;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                exisingColor = null;
                _log.LogError("Error while updating color", ex);
            }
            return exisingColor;
        }
    }
}
