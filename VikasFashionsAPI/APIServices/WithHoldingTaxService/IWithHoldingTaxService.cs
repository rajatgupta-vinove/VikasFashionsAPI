namespace VikasFashionsAPI.APIServices.WithHoldingTaxService
{
    public interface IWithHoldingTaxService
    {
        Task<IEnumerable<WithHoldingTax>> GetAllAsync();
        Task<IEnumerable<WithHoldingTax>> GetAllByStatusAsync(bool? isActive);
        Task<WithHoldingTax?> GetByIdAsync(int withHoldingTaxId);
        Task<WithHoldingTax> AddWithHoldingTaxAsync(WithHoldingTax withHoldingTax);
        Task<WithHoldingTax?> UpdateWithHoldingTaxAsync(WithHoldingTax withHoldingTax);
        Task<bool> DeleteWithHoldingTaxAsync(int withHoldingTaxId);
        Task<WithHoldingTax?> ChangeWithHoldingTaxStatusAsync(int withHoldingTaxId, int updatedBy, DateTime updatedOn);

    }
}
