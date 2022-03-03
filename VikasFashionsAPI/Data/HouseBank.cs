//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VikasFashionsAPI.Data
{
    public partial class HouseBank
    {
        [Key]
        public int HouseBankId { get; set; }
        public string HouseBankName { get; set; }
        public string HouseBankCode { get; set; }
        public int CityId { get; set; }
        public string PinCode { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string AccountType { get; set; }
        public string GLCode { get; set; }
        public string SWIFTCode { get; set; }
        public string IFSCCode { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public bool IsActive { get; set; }
        public string Remark { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public System.DateTime UpdatedOn { get; set; }
        public int CompanyId { get; set; }
    }
}
