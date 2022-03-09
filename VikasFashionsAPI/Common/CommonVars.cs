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
            InvalidLogin,
            [Display(Name = "No such user found!")]
            [Description("No such user found!")]
            UserNotFound,
            [Display(Name = "No such country found!")]
            [Description("No such country found!")]
            CountryNotFound,
            [Display(Name = "No such area found!")]
            [Description("No such area found!")]
            AreaNotFound,
            [Display(Name = "No such Bin Location found!")]
            [Description("No such Bin Location found!")]
            BinLocationNotFound,
            [Display(Name = "No such Business Partner Address found!")]
            [Description("No such Business Partner Address found!")]
            BusinessPartnerAddressNotFound,
            [Display(Name = "No such Business Partner found!")]
            [Description("No such Business Partner found!")]
            BusinessPartnerNotFound,
            [Display(Name = "No such Business Partner Bank Details found!")]
            [Description("No such Business Partner Bank Details found!")]
            BusinessPartnerBankDetailsNotFound,
            [Display(Name = "No such Business Partner Bank Details found!")]
            [Description("No such Business Partner Bank Details found!")]
            BusinessPartnerTypeNotFound,
            [Display(Name = "No such Catalog found!")]
            [Description("No such Catalog found!")]
            CatalogNotFound,
            [Display(Name = "No such Chart found!")]
            [Description("No such Chart found!")]
            ChartNotFound,
            [Display(Name = "No such City found!")]
            [Description("No such City found!")]
            CityNotFound,
            [Display(Name = "No such Color found!")]
            [Description("No such Color found!")]
            ColorNotFound,
            [Display(Name = "No such Company found!")]
            [Description("No such Company found!")]
            CompanyNotFound,
            [Display(Name = "No such Cmpany Group found!")]
            [Description("No such Company Group found!")]
            CompanyGroupNotFound,
            [Display(Name = "No such Design found!")]
            [Description("No such Design found!")]
            DesignNotFound,
            [Display(Name = "No such HSN found!")]
            [Description("No such HSN found!")]
            HSNNotFound,
            [Display(Name = "No such Material found!")]
            [Description("No such Material found!")]
            MaterialNotFound,
            [Display(Name = "No such Material Group found!")]
            [Description("No such Material Group found!")]
            MaterialGroupNotFound,
            [Display(Name = "No such Material Type found!")]
            [Description("No such Material Type found!")]
            MaterialTypeNotFound,
            [Display(Name = "No such Payment Term found!")]
            [Description("No such Payment Term found!")]
            PaymentTermNotFound,
            [Display(Name = "No such Plant Branch found!")]
            [Description("No such Plant Branch found!")]
            PlantBranchNotFound,
            [Display(Name = "No such Shade found!")]
            [Description("No such Shade found!")]
            ShadeNotFound,
            [Display(Name = "No such State found!")]
            [Description("No such State found!")]
            StateNotFound,
            [Display(Name = "No such UnitsOfMeasure found!")]
            [Description("No such UnitsOfMeasure found!")]
            UnitsOfMeasureNotFound,
            [Display(Name = "No such Warehouse found!")]
            [Description("No such Warehouse found!")]
            WarehouseNotFound,
            [Display(Name = "No such With Holding Tax found!")]
            [Description("No such With Holding Tax found!")]
            WithHoldingTaxNotFound,
        }

    }
}
