//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class BusinessPartnersBankDetail
    {
        [Key]
        public int BPBankId { get; set; }
        public int BusinessPartnerId { get; set; }
        public int BankCode { get; set; }
        public string BankName { get; set; }
        public string BankAccountNo { get; set; }
        public string BankAccountName { get; set; }
        public string BankAccountType { get; set; }
        public string BankSWIFTCode { get; set; }
        public string BankIFSCCode { get; set; }
        public string BankBranchCode { get; set; }
        public string BankBranchName { get; set; }
        public string BankAddressLine1 { get; set; }
        public string BankAddressLine2 { get; set; }
        public int BankCity { get; set; }
        public int BankState { get; set; }
        public int BankCountry { get; set; }
        public string BankPinCode { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public System.DateTime UpdatedOn { get; set; }
    }
}
