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
    public partial class PaymentTerm
    {
        [Key]
        public int PaymentTermId { get; set; }
        public string PaymentTermName { get; set; }
        public int Months { get; set; }
        public int Days { get; set; }
        public string Remark { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public System.DateTime UpdatedOn { get; set; }
    }
}
