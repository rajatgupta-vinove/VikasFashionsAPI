using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization;

namespace VikasFashionsAPI.Common
{
    public static class CommonVars
    {
        public static DateTime CurrentDateTime
        {
            get { return DateTime.UtcNow; }
        }

        public enum MessageResults
        {
            [Display(Name = "Successfully saved!")]
            [Description("Successfully saved!")]
            SuccessSave,
            [Display(Name = "Successfully updated!")]
            [Description("Successfully updated!")]
            SuccessUpdate,
            [Display(Name = "Successfully get!")]
            [Description("Successfully get!")]
            SuccessGet,
            [Display(Name = "Error while saving!")]
            [Description("Error while saving!")]
            ErrorSave,
            [Display(Name = "Successfully deleted!")]
            [Description("Successfully deleted!")]
            SuccessDelete,
            [Display(Name = "Error while deleting!")]
            [Description("Error while deleting!")]
            ErrorDelete,
            [Display(Name = "Error while geting data!")]
            [Description("Error while geting data!")]
            ErrorGet,
            [Display(Name = "User with this email already exists!")]
            [Description("User with this email already exists!")]
            UserDuplicateEmail,
            [Display(Name = "User with this code already exists!")]
            [Description("User with this code already exists!")]
            UserDuplicateCode,
            [Display(Name = "Invalid login details!")]
            [Description("Invalid login details!")]
            InvalidLogin
        }

    }
}
