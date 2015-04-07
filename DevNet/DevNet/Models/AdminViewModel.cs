using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DevNet.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "RoleName")]
        public string Name { get; set; }
    }

    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        
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
                    List<System.Web.Mvc.SelectListItem> lstStates = new List<System.Web.Mvc.SelectListItem>();

                    foreach (DevNet.Models.State state in States)
                    {
                        lstStates.Add(new System.Web.Mvc.SelectListItem
                        {
                            Selected = (state.StateID == 1),
                            Text = state.StateName + "(" + state.StateAbbreviation + ")",
                            Value = state.StateID.ToString()
                        });
                    }
                    return lstStates;
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


        public IEnumerable<SelectListItem> RolesList { get; set; }
    }
}