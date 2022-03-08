namespace VikasFashionsAPI.APIServices.BusinessPartnerTypeService
{
    public interface IBusinessPartnerTypeService
    {
        Task<IEnumerable<BusinessPartnerType>> GetAllAsync();
        Task<BusinessPartnerType?> GetByIdAsync(int businessPartnerTypeId);
        Task<BusinessPartnerType> AddBusinessPartnerTypeAsync(BusinessPartnerType bussinessPartnerType);
        Task<BusinessPartnerType?> UpdateBusinessPartnerTypeAsync(BusinessPartnerType bussinessPartnerType);
        Task<bool> DeleteBusinessPartnerTypeAsync(int businessPartnerTypeId);
        Task<BusinessPartnerType?> ChangeBusinessPartnerTypeStatusAsync(int businessPartnerTypeId, int updatedBy, DateTime updatedOn);


    }
}
