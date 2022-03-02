using VikasFashionsAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace VikasFashionsAPI.APIServices.PaymentTermService
{
    public class PaymentTermService : IPaymentTermService
    {
        private readonly ILogger<PaymentTermService> _log;
        private readonly IConfiguration _config;
        private readonly VikasFashionsAPI.Data.DataContextVikasFashion _context;
        public PaymentTermService(IConfiguration config, ILogger<PaymentTermService> log, DataContextVikasFashion context)
        {
            _config = config;
            _log = log;
            _context = context;
        }

        public async Task<PaymentTerm> AddPaymentTermAsync(PaymentTerm paymentTerm)
        {
            try
            {
                if (paymentTerm == null)
                    throw new ArgumentNullException("AddPaymentTerm");
                _context.PaymentTerms.Add(paymentTerm);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while adding paymentTerm", ex);
            }
            return paymentTerm;
        }

        public async Task<bool> DeletePaymentTermAsync(int paymentTermId)
        {
            bool isDeleted = false;
            try
            {
                if (paymentTermId == 0)
                    return isDeleted;
                var paymentTerm = await _context.PaymentTerms.FirstOrDefaultAsync(m => m.PaymentTermId == paymentTermId);
                if (paymentTerm == null)
                    return isDeleted;
                _context.PaymentTerms.Remove(paymentTerm);
                await _context.SaveChangesAsync();
                isDeleted = true;
            }
            catch (Exception ex)
            {
                _log.LogError("Error while deleting paymentTerm", ex);
            }
            return isDeleted;
        }

        public async Task<IEnumerable<PaymentTerm>> GetAllAsync()
        {
            _log.LogInformation("PaymentTerm GetAll Called!");
            return await _context.PaymentTerms.ToListAsync();
        }

        public async Task<IEnumerable<PaymentTerm>> GetAllByStatusAsync(bool? isActive)
        {
            if (isActive == null) return await GetAllAsync();
            return await _context.PaymentTerms.Where(m => m.IsActive == isActive).ToListAsync();
        }

        public async Task<PaymentTerm?> GetByIdAsync(int paymentTermId)
        {
            PaymentTerm? paymentTerm = null;
            try
            {
                if (paymentTermId == 0)
                    return paymentTerm;
                paymentTerm = await _context.PaymentTerms.FirstOrDefaultAsync(m => m.PaymentTermId == paymentTermId);
            }
            catch (Exception ex)
            {

                _log.LogError("Error while getting paymentTerm", ex);
            }
            return paymentTerm;
        }

        public async Task<PaymentTerm?> UpdatePaymentTermAsync(PaymentTerm paymentTerm)
        {
            try
            {
                var exisingPaymentTerm = await _context.PaymentTerms.FirstOrDefaultAsync(m => m.PaymentTermId == paymentTerm.PaymentTermId);
                if (exisingPaymentTerm == null)
                    return null;
                exisingPaymentTerm.PaymentTermId = paymentTerm.PaymentTermId;
                exisingPaymentTerm.PaymentTermName = paymentTerm.PaymentTermName;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while updating paymentTerm", ex);
            }
            return paymentTerm;
        }
    }
}
