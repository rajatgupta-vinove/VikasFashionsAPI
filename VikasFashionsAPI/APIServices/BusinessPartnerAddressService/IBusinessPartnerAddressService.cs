namespace VikasFashionsAPI.APIServices.BusinessPartnerAddressService
{ 
    public interface IBusinessPartnerAddressService
    {        
           Task<IEnumerable<BusinessPartnerAddress>> GetAllBusinessPartnerAddressAsync();
        Task<BusinessPartnerAddress?> GetByBusinessPartnerAddressIdAsync(int businessPartnerAddressId);
        Task<BusinessPartnerAddress> AddBusinessPartnerAddressAsync(BusinessPartnerAddress businessPartnerAddress);
        Task<BusinessPartnerAddress?> UpdateBusinessPartnerAddressAsync(BusinessPartnerAddress businessPartnerAddress);
        Task<bool> DeleteBusinessPartnerAddressAsync(int businessPartnerAddressId);
    }
}
