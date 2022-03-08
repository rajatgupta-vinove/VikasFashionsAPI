namespace VikasFashionsAPI.APIServices.BusinessPartnerService
{
    public interface IBusinessPartnerService
    {
        Task<IEnumerable<BusinessPartner>> GetAllAsync();
        Task<BusinessPartner?> GetByIdAsync(int businessPartnerId);
        Task<BusinessPartner> AddBusinessPartnerAsync(BusinessPartner businessPartner);
        Task<BusinessPartner?> UpdateBusinessPartnerAsync(BusinessPartner businessPartner);
        Task<bool> DeleteBusinessPartnerAsync(int businessPartnerId);
        Task<BusinessPartner?> ChangeBusinessPartnerStatusAsync(int businessPartnerId, int updatedBy, DateTime updatedOn);
    }
}
