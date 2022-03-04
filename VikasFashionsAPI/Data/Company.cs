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
    public partial class Company
    {
        [Key]
        public int CompanyId { get; set; }

        [MaxLength(255)]
        [Column(TypeName = "varchar(255)")]
        [Required(ErrorMessage = "Company Name is required")]
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }

        [MaxLength(1000)]
        [Column(TypeName = "varchar(1000)")]
        [Required(ErrorMessage = "Address Line 1 is required")]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int CityId { get; set; }
        public string PinCode { get; set; }

        [MaxLength(1000)]
        [Column(TypeName = "varchar(1000)")]
        [Required(ErrorMessage = "Payment Address Line 1 is required")]
        public string PaymentAddressLine1 { get; set; }
        public string PaymentAddressLine2 { get; set; }
        public int PaymentAddressCityId { get; set; }
        public string PaymentAddressPinCode { get; set; }

        [MaxLength(255)]
        [Column(TypeName = "varchar(255)")]
        [Required(ErrorMessage = "Account Manager Name is required")]
        public string ActMgrName { get; set; }
        public string PhoneNo1 { get; set; }
        public string PhoneNo2 { get; set; }
        public string EmailId { get; set; }
        public string Fax { get; set; }
        public string WebSite { get; set; }
        public string PANNo { get; set; }
        public string CSTNo { get; set; }
        public string Remark { get; set; }
        public string Logo { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public System.DateTime UpdatedOn { get; set; }
        public int CompanyGroupId { get; set; }
    }
}
