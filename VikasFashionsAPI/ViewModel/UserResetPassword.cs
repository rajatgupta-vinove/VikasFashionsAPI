//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VikasFashionsAPI.ViewModel
{
    public partial class UserResetPassword
    {
        [Required(ErrorMessage = "UserId is required")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string NewPassword { get; set; }

        [Compare("NewPassword",ErrorMessage ="Both passwords should match")]
        public string ConfirmPassword { get; set; }
    }
   
}
