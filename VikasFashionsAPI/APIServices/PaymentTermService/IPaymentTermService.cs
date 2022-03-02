using VikasFashionsAPI.Data;
namespace VikasFashionsAPI.APIServices.PaymentTermService
{
    public interface IPaymentTermService
    {
        Task<IEnumerable<PaymentTerm>> GetAllAsync();
        Task<IEnumerable<PaymentTerm>> GetAllByStatusAsync(bool? isActive);
        Task<PaymentTerm?> GetByIdAsync(int paymentTermId);
        Task<PaymentTerm> AddPaymentTermAsync(PaymentTerm paymentTerm);
        Task<PaymentTerm?> UpdatePaymentTermAsync(PaymentTerm paymentTerm);
        Task<bool> DeletePaymentTermAsync(int paymentTermId);
    }
}
