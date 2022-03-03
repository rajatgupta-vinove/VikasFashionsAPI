namespace VikasFashionsAPI.APIServices.BusinessPartnersBankDetailService
{ 
    public interface IBusinessPartnersBankDetailService
    {        
           Task<IEnumerable<BusinessPartnersBankDetail>> GetAllBusinessPartnersBankDetailAsync();
        Task<BusinessPartnersBankDetail?> GetByBusinessPartnersBankDetailIdAsync(int BusinessPartnersBankDetailId);
        Task<BusinessPartnersBankDetail> AddBusinessPartnersBankDetailAsync(BusinessPartnersBankDetail BusinessPartnersBankDetail);
        Task<BusinessPartnersBankDetail?> UpdateBusinessPartnersBankDetailAsync(BusinessPartnersBankDetail BusinessPartnersBankDetail);
        Task<bool> DeleteBusinessPartnersBankDetailAsync(int BusinessPartnersBankDetailId);
    }
}
