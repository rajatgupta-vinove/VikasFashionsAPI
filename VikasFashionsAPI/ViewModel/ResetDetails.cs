namespace VikasFashionsAPI.ViewModel
{
    public class ResetDetails
    {
        [Required(ErrorMessage = "UserCode is required")]
        public string UserCode { get; set; }
        [Required(ErrorMessage = "Current Password is required")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Both passwords should match")]
        public string ConfirmPassword { get; set; }
    }
}
