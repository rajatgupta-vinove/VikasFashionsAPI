using VikasFashionsAPI.Data;
using Microsoft.EntityFrameworkCore;
namespace VikasFashionsAPI.APIServices.ShadeService
{
    public class ShadeService : IShadeService
    {
        private readonly ILogger<ShadeService> _log;
        private readonly IConfiguration _config;
        private readonly VikasFashionsAPI.Data.DataContextVikasFashion _context;

        public ShadeService(IConfiguration config, ILogger<ShadeService> log, DataContextVikasFashion context)
        {
            _config = config;
            _log = log;
            _context = context;
        }
        public async Task<Shade> AddShadeAsync(Shade shade)
        {
            try
            {
                if (shade == null)
                    throw new ArgumentNullException("AddShade");
                _context.Shades.Add(shade);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while adding country", ex);
            }
            return shade;
        }

        public async Task<bool> DeleteShadeAsync(int shadeId)
        {
            bool isDeleted = false;
            try
            {
                if (shadeId == 0)
                    return isDeleted;
                var shade = await _context.Shades.FirstOrDefaultAsync(m => m.ShadeId == shadeId);
                if (shade == null)
                    return isDeleted;
                _context.Shades.Remove(shade);
                await _context.SaveChangesAsync();
                isDeleted = true;
            }
            catch (Exception ex)
            {
                _log.LogError("Error while deleting shade", ex);
            }
            return isDeleted;
        }

        public async Task<IEnumerable<Shade>> GetAllAsync()
        {
            _log.LogInformation("Shade GetAll Called!");
            return await _context.Shades.ToListAsync();
        }

        public async Task<Shade?> GetByIdAsync(int shadeId)
        {
            Shade? shade = null;
            try
            {
                if (shadeId == 0)
                    return shade;
                shade = await _context.Shades.FirstOrDefaultAsync(m => m.ShadeId == shadeId);
            }
            catch (Exception ex)
            {

                _log.LogError("Error while getting shade", ex);
            }
            return shade;
        }

        public async Task<Shade?> UpdateShadeAsync(Shade shade)
        {
            try
            {
                var exisingShade = await _context.Shades.FirstOrDefaultAsync(m => m.ShadeId == shade.ShadeId);
                if (exisingShade == null)
                    return null;
                exisingShade.CreatedBy = shade.CreatedBy;
                exisingShade.ShadeId = shade.ShadeId;
                exisingShade.ShadeName = shade.ShadeName;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while updating shade", ex);
            }
            return shade;
        }
    }
}
