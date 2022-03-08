namespace VikasFashionsAPI.APIServices.WithHoldingTaxService
{
    public class WithHoldingTaxService : IWithHoldingTaxService
    {
        private readonly ILogger<WithHoldingTaxService> _log;
        private readonly IConfiguration _config;
        private readonly VikasFashionsAPI.Data.DataContextVikasFashion _context;
        public WithHoldingTaxService(IConfiguration config, ILogger<WithHoldingTaxService> log, DataContextVikasFashion context)
        {
            _config = config;
            _log = log;
            _context = context;
        }

        public async Task<WithHoldingTax> AddWithHoldingTaxAsync(WithHoldingTax withHoldingTax)
        {
            try
            {
                if (withHoldingTax == null)
                    throw new ArgumentNullException("AddWithHoldingTax");
                _context.WithHoldingTaxes.Add(withHoldingTax);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while adding withHoldingTax", ex);
            }
            return withHoldingTax;
        }

        public async Task<bool> DeleteWithHoldingTaxAsync(int withHoldingTaxId)
        {
            bool isDeleted = false;
            try
            {
                if (withHoldingTaxId == 0)
                    return isDeleted;
                var withHoldingTax = await _context.WithHoldingTaxes.FirstOrDefaultAsync(m => m.WithHoldingTaxId == withHoldingTaxId);
                if (withHoldingTax == null)
                    return isDeleted;
                _context.WithHoldingTaxes.Remove(withHoldingTax);
                await _context.SaveChangesAsync();
                isDeleted = true;
            }
            catch (Exception ex)
            {
                _log.LogError("Error while deleting withHoldingTax", ex);
            }
            return isDeleted;
        }

        public async Task<IEnumerable<WithHoldingTax>> GetAllAsync()
        {
            _log.LogInformation("WithHoldingTax GetAll Called!");
            return await _context.WithHoldingTaxes.ToListAsync();
        }

        public async Task<IEnumerable<WithHoldingTax>> GetAllByStatusAsync(bool? isActive)
        {
            if (isActive == null) return await GetAllAsync();
            return await _context.WithHoldingTaxes.Where(m => m.IsActive == isActive).ToListAsync();
        }

        public async Task<WithHoldingTax?> GetByIdAsync(int withHoldingTaxId)
        {
            WithHoldingTax? withHoldingTax = null;
            try
            {
                if (withHoldingTaxId == 0)
                    return withHoldingTax;
                withHoldingTax = await _context.WithHoldingTaxes.FirstOrDefaultAsync(m => m.WithHoldingTaxId == withHoldingTaxId);
            }
            catch (Exception ex)
            {

                _log.LogError("Error while getting withHoldingTax", ex);
            }
            return withHoldingTax;
        }

        public async Task<WithHoldingTax?> UpdateWithHoldingTaxAsync(WithHoldingTax withHoldingTax)
        {
            try
            {
                var exisingWithHoldingTax = await _context.WithHoldingTaxes.FirstOrDefaultAsync(m => m.WithHoldingTaxId == withHoldingTax.WithHoldingTaxId);
                if (exisingWithHoldingTax == null)
                    return null;
                exisingWithHoldingTax.WithHoldingTaxId = withHoldingTax.WithHoldingTaxId;
                exisingWithHoldingTax.UpdatedBy = withHoldingTax.UpdatedBy;
                exisingWithHoldingTax.UpdatedOn = withHoldingTax.UpdatedOn;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while updating withHoldingTax", ex);
            }
            return withHoldingTax;
        }

        public async Task<WithHoldingTax?> ChangeWithHoldingTaxStatusAsync(int withHoldingTaxId, int updatedBy, DateTime updatedOn)
        {
            WithHoldingTax? exisingWithHoldingTax = null;
            try
            {
                exisingWithHoldingTax = await _context.WithHoldingTaxes.FirstOrDefaultAsync(m => m.WithHoldingTaxId == withHoldingTaxId);
                if (exisingWithHoldingTax == null)
                    return null;
                exisingWithHoldingTax.IsActive = !exisingWithHoldingTax.IsActive;
                exisingWithHoldingTax.UpdatedBy = updatedBy;
                exisingWithHoldingTax.UpdatedOn = updatedOn;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                exisingWithHoldingTax = null;
                _log.LogError("Error while updating WithHoldingTax", ex);
            }
            return exisingWithHoldingTax;
        }
    }
}
