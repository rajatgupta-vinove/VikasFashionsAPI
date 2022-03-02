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
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class BusinessPartnerAddress
    {
        [Key]
        public int BusinessPartnerAddressId { get; set; }
        public int BusinessPartnerId { get; set; }
        public string BPAddress { get; set; }
        public string BPAddressType { get; set; }
        public string BPAddressLine1 { get; set; }
        public string BPAddressLine2 { get; set; }
        public int CityID { get; set; }
        public int StateID { get; set; }
        public int CountryID { get; set; }
        public string PinCode { get; set; }
        public string GSTIN { get; set; }
        public string GSTType { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public System.DateTime UpdatedOn { get; set; }
    }
}
