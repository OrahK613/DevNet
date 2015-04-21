using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using System.Web.WebPages.Html;
using System.Data.Entity;
using System.Linq;


namespace DevNet.Models
{
    public class ManageUserViewModels
    {
        public ManageUserViewModel ManageCurrentUser { get; set; }

        public ApplicationUser CurrentUser { get; set; }
    }
    
    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }




        //[Required]
        //[EmailAddress]
        //[Display(Name = "Email")]
        //public string Email { get; set; }

        //[Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        //[DataType(DataType.Text)]
        //[Display(Name = "First Name")]
        //public string FirstName { get; set; }

        //[Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        //[DataType(DataType.Text)]
        //[Display(Name = "Last Name")]
        //public string LastName { get; set; }

        //[Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        //[DataType(DataType.Text)]
        //[Display(Name = "Address")]
        //public string Address { get; set; }

        //[Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        //[DataType(DataType.Text)]
        //[Display(Name = "City")]
        //public string City { get; set; }

        //[Required]
        //[Display(Name = "Select State")]
        //public Int32 StateID { get; set; }

        //[Required]
        //[Display(Name = "Date Of Birth")]
        //public DateTime BirthDate { get; set; }

        //[Required]
        //[Display(Name = "Favorite IDE")]
        //public Int32 FavoriteIDEID { get; set; }

        //[Required]
        //[Display(Name = "Software Specialty")]
        //public Int32 SoftwareSpecialtyID { get; set; }

        //[Required]
        //[Display(Name = "Programming Language")]
        //public Int32 ProgrammingLanguageID { get; set; }

        //[Required]
        //[Display(Name = "Select Your State")]
        //public int SelectedStateID { get; set; }
        //public IEnumerable<System.Web.Mvc.SelectListItem> StateList { get; set; }

        //[Required]
        //[Display(Name = "Select Your Favorite IDE")]
        //public int SelectedIDEID { get; set; }
        //public IEnumerable<System.Web.Mvc.SelectListItem> FavoriteIDEList { get; set; }

        //[Required]
        //[Display(Name = "Select Your Software Specialty")]
        //public int SelectedSoftwareSpecialtyID { get; set; }
        //public IEnumerable<System.Web.Mvc.SelectListItem> SoftwareSpecialtyList { get; set; }

        //[Required]
        //[Display(Name = "Select Your Favorite Programming Language")]
        //public int SelectedProgrammingLanguageID { get; set; }
        //public IEnumerable<System.Web.Mvc.SelectListItem> ProgrammingLanguageList { get; set; }
    }

    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string Action { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Select State")]
        public Int32 StateID { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "Favorite IDE")]
        public Int32 FavoriteIDEID { get; set; }

        [Required]
        [Display(Name = "Software Specialty")]
        public Int32 SoftwareSpecialtyID { get; set; }

        [Required]
        [Display(Name = "Programming Language")]
        public Int32 ProgrammingLanguageID { get; set; }

        [Required]
        [Display(Name = "Select Your State")]
        public int SelectedStateID { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> StateList { get; set; }

        [Required]
        [Display(Name = "Select Your Favorite IDE")]
        public int SelectedIDEID { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> FavoriteIDEList { get; set; }

        [Required]
        [Display(Name = "Select Your Software Specialty")]
        public int SelectedSoftwareSpecialtyID { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> SoftwareSpecialtyList { get; set; }

        [Required]
        [Display(Name = "Select Your Favorite Programming Language")]
        public int SelectedProgrammingLanguageID { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> ProgrammingLanguageList { get; set; }

        public RSSFeedModel Feeds { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

}
