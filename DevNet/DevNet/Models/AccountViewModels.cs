using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using System.Web.WebPages.Html;
using System.Data.Entity;


namespace DevNet.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
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

      
        public List<System.Web.Mvc.SelectListItem> StateList
        {
            get
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {

                    System.Data.Entity.DbSet<State> States = db.States;
                    List<System.Web.Mvc.SelectListItem> lstSelectedStates = new List<System.Web.Mvc.SelectListItem>();

                    foreach (DevNet.Models.State state in States)
                    {
                        lstSelectedStates.Add(new System.Web.Mvc.SelectListItem
                        {
                            Selected = (state.StateID == 1),
                            Text = state.StateName + "(" + state.StateAbbreviation + ")",
                            Value = state.StateID.ToString()
                        });
                    }

                  
                    return lstSelectedStates;
                }


            }
            set
            { ; }
        }


        public List<System.Web.Mvc.SelectListItem> FavoriteIDEList
        {
            get
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    System.Data.Entity.DbSet<FavoriteIDE> FavoriteIDEs = db.FavoriteIDEs;
                    List<System.Web.Mvc.SelectListItem> lstFavoriteIDEs = new List<System.Web.Mvc.SelectListItem>();

                    foreach (DevNet.Models.FavoriteIDE favoriteIDE in FavoriteIDEs)
                    {
                        lstFavoriteIDEs.Add(new System.Web.Mvc.SelectListItem
                        {
                            Selected = (favoriteIDE.FavoriteIDEID == 1),
                            Text = favoriteIDE.FavoriteIDEName,
                            Value = favoriteIDE.FavoriteIDEID.ToString()
                        });
                    }

                    return lstFavoriteIDEs;
                }


            }
            set
            { ; }
        }

        public List<System.Web.Mvc.SelectListItem> SoftwareSpecialtyList
        {
            get
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    System.Data.Entity.DbSet<SoftwareSpecialty> SoftwareSpecialties = db.SoftwareSpecialties;
                    List<System.Web.Mvc.SelectListItem> lstSoftwareSpecialties = new List<System.Web.Mvc.SelectListItem>();

                    foreach (DevNet.Models.SoftwareSpecialty softwareSpecialty in SoftwareSpecialties)
                    {
                        lstSoftwareSpecialties.Add(new System.Web.Mvc.SelectListItem
                        {
                            Selected = (softwareSpecialty.SoftwareSpecialtyID == 1),
                            Text = softwareSpecialty.SoftwareSpecialtyName,
                            Value = softwareSpecialty.SoftwareSpecialtyID.ToString()
                        });
                    }

                
                    return lstSoftwareSpecialties;
                }


            }
            set
            { ; }
        }

        public List<System.Web.Mvc.SelectListItem> ProgrammingLanguageList
        {
            get
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    System.Data.Entity.DbSet<ProgrammingLanguage> ProgrammingLanguages = db.ProgrammingLanguages;
                    List<System.Web.Mvc.SelectListItem> lstProgrammingLanguages = new List<System.Web.Mvc.SelectListItem>();

                    foreach (DevNet.Models.ProgrammingLanguage programmingLanguage in ProgrammingLanguages)
                    {
                        lstProgrammingLanguages.Add(new System.Web.Mvc.SelectListItem
                        {
                            Selected = (programmingLanguage.ProgrammingLanguageID == 1),
                            Text = programmingLanguage.ProgrammingLanguageName,
                            Value = programmingLanguage.ProgrammingLanguageID.ToString()
                        });
                    }

                    return lstProgrammingLanguages;
                }


            }
            set
            { ; }
        }

        //public static IEnumerable<SelectListItem> ToSelectListItems(
        //      this IEnumerable<State> states, int selectedId)
        //{
        //    return
        //        states.OrderBy(state => state.StateName)
        //              .Select(state =>
        //                  new SelectListItem
        //                  {
        //                      Selected = (state.StateID == selectedId),
        //                      Text = state.StateName + "(" + state.StateAbbreviation + ")",
        //                      Value = state.StateID.ToString()
        //                  });
        //}

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
