using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using DevNet.Models;
using System.Net.Http;
using System.Collections.Generic;
using System.Net.Http.Headers;


using System.Xml;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;

using System.Data.Entity;
using System.Net;

namespace DevNet.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        // Constructors
        public AccountController()
        {
        }


        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        #region Private Properties

        // Private Properties
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        #endregion

        #region Public Properties

        // Public Properties
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }


        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        #endregion

        #region Views

        #region Register - Needs to be refactored - badly

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();

            model.BirthDate = DateTime.Now;

            using (ApplicationDbContext db = new ApplicationDbContext())
            {

                // States
                IEnumerable<State> TStates = db.States;
                IList<State> lstStates = new List<State>();

                foreach (State state in TStates)
                {
                    lstStates.Add(state);
                }
                IEnumerable<SelectListItem> lstSelectedStates =
                     from state in lstStates
                     select new SelectListItem
                     {
                         Selected = false,
                         Text = state.StateName + "(" + state.StateAbbreviation + ")",
                         Value = state.StateID.ToString()
                     };

                // IDEs
                IEnumerable<FavoriteIDE> TFavoriteIDEs = db.FavoriteIDEs;
                IList<FavoriteIDE> FavoriteIDEs = new List<FavoriteIDE>();

                foreach (FavoriteIDE ide in TFavoriteIDEs)
                {
                    FavoriteIDEs.Add(ide);
                }

                IEnumerable<SelectListItem> lstSelectedFavoriteIDEs =
                     from ide in FavoriteIDEs
                     select new SelectListItem
                     {
                         Selected = false,
                         Text = ide.FavoriteIDEName,
                         Value = ide.FavoriteIDEID.ToString()
                     };


                // Software Specialties
                IEnumerable<SoftwareSpecialty> TSoftwareSpecialties = db.SoftwareSpecialties;
                IList<SoftwareSpecialty> SoftwareSpecialties = new List<SoftwareSpecialty>();

                foreach (SoftwareSpecialty ss in TSoftwareSpecialties)
                {
                    SoftwareSpecialties.Add(ss);
                }

                IEnumerable<SelectListItem> lstSelectedSoftwareSpecialties =
                     from ss in SoftwareSpecialties
                     select new SelectListItem
                     {
                         Selected = false,
                         Text = ss.SoftwareSpecialtyName,
                         Value = ss.SoftwareSpecialtyID.ToString()
                     };

                // Programming Languages
                IEnumerable<ProgrammingLanguage> TProgrammingLanguages = db.ProgrammingLanguages;
                IList<ProgrammingLanguage> ProgrammingLanguages = new List<ProgrammingLanguage>();

                foreach (ProgrammingLanguage pl in TProgrammingLanguages)
                {
                    ProgrammingLanguages.Add(pl);
                }

                IEnumerable<SelectListItem> lstSelectedProgrammingLanguages =
                     from pl in ProgrammingLanguages
                     select new SelectListItem
                     {
                         Selected = false,
                         Text = pl.ProgrammingLanguageName,
                         Value = pl.ProgrammingLanguageID.ToString()
                     };

                // We haven't registered yet so we will not want to bring up the confirm RSS Feed Dialog when we post back to the Register form
                // Stores a variable indicating that we don't want to bring up the dialog
                model.ShowDialog = false;

                model.SelectedStateID = -1;
                model.SelectedIDEID = -1;
                model.SelectedSoftwareSpecialtyID = -1;
                model.SelectedProgrammingLanguageID = -1;

                model.StateList = lstSelectedStates;
                model.FavoriteIDEList = lstSelectedFavoriteIDEs;
                model.SoftwareSpecialtyList = lstSelectedSoftwareSpecialties;
                model.ProgrammingLanguageList = lstSelectedProgrammingLanguages;
            }
            return View(model);
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                // Call Web Servic to get recommended RSS Feed Info
                InvokeRequestResponseService(model).Wait();

                // Get the JObject 
                Newtonsoft.Json.Linq.JObject RecommendedRSSFeed = new Newtonsoft.Json.Linq.JObject();

                // Get the actual feeds
                RecommendedRSSFeed = DevNetAnalyticsViewModel.RSSFeed;

                // Get the feed name
                string strRecommendedRSSFeed = RecommendedRSSFeed["Results"]["Recommended RSS Feed"]["value"]["Values"][0][12].ToString();

                // Since the registration was successful we will want to bring up the confirm RSS Feed Dialog when we post back to the Register form
                // Stores a variable indicating that we need to bring up the dialog
                ViewData["ShowDialog"] = "true";
                
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    City = model.City,
                    StateID = model.SelectedStateID,
                    DateOfBirth = model.BirthDate,
                    FavoriteIDEID = model.SelectedIDEID,
                    SoftwareSpecialtyID = model.SelectedSoftwareSpecialtyID,
                    ProgrammingLanguageID = model.SelectedProgrammingLanguageID,
                    RssFeedName = strRecommendedRSSFeed
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded) // Successful Registration
                {

                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    model.ShowDialog = true;
                    return RedirectToAction("Register", "Account");
                   // return RedirectToAction("Index", "Home");
                }
                else // Failed Registration
                {
                    using (ApplicationDbContext db = new ApplicationDbContext())
                    {
                        // States
                        IEnumerable<State> TStates = db.States;
                        IList<State> lstStates = new List<State>();

                        foreach (State state in TStates)
                        {
                            lstStates.Add(state);
                        }
                        IEnumerable<SelectListItem> lstSelectedStates =
                             from state in lstStates
                             select new SelectListItem
                             {
                                 Selected = false,
                                 Text = state.StateName + "(" + state.StateAbbreviation + ")",
                                 Value = state.StateID.ToString()
                             };

                        // IDEs
                        IEnumerable<FavoriteIDE> TFavoriteIDEs = db.FavoriteIDEs;
                        IList<FavoriteIDE> FavoriteIDEs = new List<FavoriteIDE>();

                        foreach (FavoriteIDE ide in TFavoriteIDEs)
                        {
                            FavoriteIDEs.Add(ide);
                        }

                        IEnumerable<SelectListItem> lstSelectedFavoriteIDEs =
                             from ide in FavoriteIDEs
                             select new SelectListItem
                             {
                                 Selected = false,
                                 Text = ide.FavoriteIDEName,
                                 Value = ide.FavoriteIDEID.ToString()
                             };


                        // Software Specialties
                        IEnumerable<SoftwareSpecialty> TSoftwareSpecialties = db.SoftwareSpecialties;
                        IList<SoftwareSpecialty> SoftwareSpecialties = new List<SoftwareSpecialty>();

                        foreach (SoftwareSpecialty ss in TSoftwareSpecialties)
                        {
                            SoftwareSpecialties.Add(ss);
                        }

                        IEnumerable<SelectListItem> lstSelectedSoftwareSpecialties =
                             from ss in SoftwareSpecialties
                             select new SelectListItem
                             {
                                 Selected = false,
                                 Text = ss.SoftwareSpecialtyName,
                                 Value = ss.SoftwareSpecialtyID.ToString()
                             };

                        // Programming Languages
                        IEnumerable<ProgrammingLanguage> TProgrammingLanguages = db.ProgrammingLanguages;
                        IList<ProgrammingLanguage> ProgrammingLanguages = new List<ProgrammingLanguage>();

                        foreach (ProgrammingLanguage pl in TProgrammingLanguages)
                        {
                            ProgrammingLanguages.Add(pl);
                        }

                        IEnumerable<SelectListItem> lstSelectedProgrammingLanguages =
                             from pl in ProgrammingLanguages
                             select new SelectListItem
                             {
                                 Selected = false,
                                 Text = pl.ProgrammingLanguageName,
                                 Value = pl.ProgrammingLanguageID.ToString()
                             };

                        // Since the registration failed we will not want to bring up the confirm RSS Feed Dialog when we post back to the Register form
                        // Stores a variable indicating that we don't want to bring up the dialog
                        model.ShowDialog = false;

                        model.StateList = lstSelectedStates;
                        model.FavoriteIDEList = lstSelectedFavoriteIDEs;
                        model.SoftwareSpecialtyList = lstSelectedSoftwareSpecialties;
                        model.ProgrammingLanguageList = lstSelectedProgrammingLanguages;
                    }
                }
                AddErrors(result);
            }
            else
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {

                    // States
                    IEnumerable<State> TStates = db.States;
                    IList<State> lstStates = new List<State>();

                    foreach (State state in TStates)
                    {
                        lstStates.Add(state);
                    }
                    IEnumerable<SelectListItem> lstSelectedStates =
                         from state in lstStates
                         select new SelectListItem
                         {
                             Selected = (model.StateID == model.SelectedStateID),
                             Text = state.StateName + "(" + state.StateAbbreviation + ")",
                             Value = state.StateID.ToString()
                         };

                    // IDEs
                    IEnumerable<FavoriteIDE> TFavoriteIDEs = db.FavoriteIDEs;
                    IList<FavoriteIDE> FavoriteIDEs = new List<FavoriteIDE>();

                    foreach (FavoriteIDE ide in TFavoriteIDEs)
                    {
                        FavoriteIDEs.Add(ide);
                    }

                    IEnumerable<SelectListItem> lstSelectedFavorieIDEs =
                         from ide in FavoriteIDEs
                         select new SelectListItem
                         {
                             Selected = (ide.FavoriteIDEID == model.FavoriteIDEID),
                             Text = ide.FavoriteIDEName,
                             Value = ide.FavoriteIDEID.ToString()
                         };

                    // Software Specialties
                    IEnumerable<SoftwareSpecialty> TSoftwareSpecialties = db.SoftwareSpecialties;
                    IList<SoftwareSpecialty> SoftwareSpecialties = new List<SoftwareSpecialty>();

                    foreach (SoftwareSpecialty ss in TSoftwareSpecialties)
                    {
                        SoftwareSpecialties.Add(ss);
                    }

                    IEnumerable<SelectListItem> lstSelectedSoftwareSpecialties =
                         from ss in SoftwareSpecialties
                         select new SelectListItem
                         {
                             Selected = (ss.SoftwareSpecialtyID == model.SoftwareSpecialtyID),
                             Text = ss.SoftwareSpecialtyName,
                             Value = ss.SoftwareSpecialtyID.ToString()
                         };

                    // Programming Languages
                    IEnumerable<ProgrammingLanguage> TProgrammingLanguages = db.ProgrammingLanguages;
                    IList<ProgrammingLanguage> ProgrammingLanguages = new List<ProgrammingLanguage>();

                    foreach (ProgrammingLanguage pl in TProgrammingLanguages)
                    {
                        ProgrammingLanguages.Add(pl);
                    }

                    IEnumerable<SelectListItem> lstSelectedProgrammingLanguages =
                         from pl in ProgrammingLanguages
                         select new SelectListItem
                         {
                             Selected = (pl.ProgrammingLanguageID == model.ProgrammingLanguageID),
                             Text = pl.ProgrammingLanguageName,
                             Value = pl.ProgrammingLanguageID.ToString()
                         };

                    model.StateList = lstSelectedStates;
                    model.FavoriteIDEList = lstSelectedFavorieIDEs;
                    model.SoftwareSpecialtyList = lstSelectedSoftwareSpecialties;
                    model.ProgrammingLanguageList = lstSelectedProgrammingLanguages;

                    model.ShowDialog = false;

                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        #endregion

        #region Login

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        #endregion

        #region Verify Code

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            var user = await UserManager.FindByIdAsync(await SignInManager.GetVerifiedUserIdAsync());
            if (user != null)
            {
                ViewBag.Status = "For DEMO purposes the current " + provider + " code is: " + await UserManager.GenerateTwoFactorTokenAsync(user.Id, provider);
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        #endregion

        #region Confirm Email

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        #endregion

        #region Password Stuff

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        #endregion

        #region Send Code

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        #endregion

        #region External Login

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        #endregion

        #region Manage Profile

        //
        // GET: /Account/Manage

        public async Task<ActionResult> Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");

            ManageUserViewModels muv = new ManageUserViewModels();
            string userId = "";
            var Users = await HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().Users.ToListAsync();

            if (Request.IsAuthenticated)
            {

                if (Users != null)
                {

                    foreach (var user in Users)
                    {

                        if (user.Id == User.Identity.GetUserId())
                        {
                            userId = User.Identity.GetUserId();
                            break;
                        }

                    }
                }
            }

            if (userId != "")
            {
                muv.CurrentUser = await UserManager.FindByIdAsync(userId);
            }
            return View(muv);
        }



       
         

        //public ActionResult Manage(ManageMessageId? message)
        //{
        //    ViewBag.StatusMessage =
        //        message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
        //        : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
        //        : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
        //        : message == ManageMessageId.Error ? "An error has occurred."
        //        : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
        //        : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
        //        : "";

        //    ViewBag.HasLocalPassword = HasPassword();
        //    ViewBag.ReturnUrl = Url.Action("Manage");

        //    ManageUserViewModels muv = new ManageUserViewModels();
        //    string userId = "";
        //    var Users = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().Users.ToList();
           
        //        if (Request.IsAuthenticated)
        //        {
                   
        //            if (Users != null)
        //            {

        //                foreach (var user in Users)
        //                {

        //                    if (user.Id == User.Identity.GetUserId())
        //                    {
        //                        userId = User.Identity.GetUserId();
        //                        break;
        //                    }

        //                }
        //            }
        //        }
            
        //    if(userId != "")
        //    {
        //        muv.CurrentUser = UserManager.FindById(userId);
        //    }

           
          //  return View(muv);
       // }
    
        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModels model)
        {
           
            //using (ApplicationDbContext db = new ApplicationDbContext())
            //{

                //// States
                //IEnumerable<State> TStates = db.States;
                //IList<State> lstStates = new List<State>();

                //foreach (State state in TStates)
                //{
                //    lstStates.Add(state);
                //}
                //IEnumerable<SelectListItem> lstSelectedStates =
                //     from state in lstStates
                //     select new SelectListItem
                //     {
                //         Selected = (model.SelectedStateID == model.StateID),
                //         Text = state.StateName + "(" + state.StateAbbreviation + ")",
                //         Value = state.StateID.ToString()
                //     };

                //// IDEs
                //IEnumerable<FavoriteIDE> TFavoriteIDEs = db.FavoriteIDEs;
                //IList<FavoriteIDE> FavoriteIDEs = new List<FavoriteIDE>();

                //foreach (FavoriteIDE ide in TFavoriteIDEs)
                //{
                //    FavoriteIDEs.Add(ide);
                //}

                //IEnumerable<SelectListItem> lstSelectedFavoriteIDEs =
                //     from ide in FavoriteIDEs
                //     select new SelectListItem
                //     {
                //         Selected = (model.SelectedIDEID == model.FavoriteIDEID),
                //         Text = ide.FavoriteIDEName,
                //         Value = ide.FavoriteIDEID.ToString()
                //     };


                //// Software Specialties
                //IEnumerable<SoftwareSpecialty> TSoftwareSpecialties = db.SoftwareSpecialties;
                //IList<SoftwareSpecialty> SoftwareSpecialties = new List<SoftwareSpecialty>();

                //foreach (SoftwareSpecialty ss in TSoftwareSpecialties)
                //{
                //    SoftwareSpecialties.Add(ss);
                //}

                //IEnumerable<SelectListItem> lstSelectedSoftwareSpecialties =
                //     from ss in SoftwareSpecialties
                //     select new SelectListItem
                //     {
                //         Selected = (model.SelectedSoftwareSpecialtyID == model.SoftwareSpecialtyID),
                //         Text = ss.SoftwareSpecialtyName,
                //         Value = ss.SoftwareSpecialtyID.ToString()
                //     };

                //// Programming Languages
                //IEnumerable<ProgrammingLanguage> TProgrammingLanguages = db.ProgrammingLanguages;
                //IList<ProgrammingLanguage> ProgrammingLanguages = new List<ProgrammingLanguage>();

                //foreach (ProgrammingLanguage pl in TProgrammingLanguages)
                //{
                //    ProgrammingLanguages.Add(pl);
                //}

                //IEnumerable<SelectListItem> lstSelectedProgrammingLanguages =
                //     from pl in ProgrammingLanguages
                //     select new SelectListItem
                //     {
                //         Selected = (model.SelectedProgrammingLanguageID == model.ProgrammingLanguageID),
                //         Text = pl.ProgrammingLanguageName,
                //         Value = pl.ProgrammingLanguageID.ToString()
                //     };

            

                //model.StateList = lstSelectedStates;
                //model.FavoriteIDEList = lstSelectedFavoriteIDEs;
                //model.SoftwareSpecialtyList = lstSelectedSoftwareSpecialties;
                //model.ProgrammingLanguageList = lstSelectedProgrammingLanguages;
           // }
            
            
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
           // ViewBag.NotificationList = GetUserNotifications();
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.ManageCurrentUser.OldPassword, model.ManageCurrentUser.NewPassword);
                    if (result.Succeeded)
                    {
                        var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                        await SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.ManageCurrentUser.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        //// GET: Account/Manage/5
        //public async Task<ActionResult> Manage(string? userId)
        //{
        //    if (userId == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    ManageUserViewModels muv = new ManageUserViewModels();
        //    muv.CurrentUser = await UserManager.FindByIdAsync(userId);

           

            
        //   // ProgrammingLanguage programmingLanguage = db.ProgrammingLanguages.Find(id);
        //    if (muv.CurrentUser == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(muv);
        //}

        //// POST: ProgrammingLanguage/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ProgrammingLanguageID,ProgrammingLanguageName")] ProgrammingLanguage programmingLanguage)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(programmingLanguage).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(programmingLanguage);
        //}

        //
        // GET: /Account/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // GET: /Account/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
            // Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Account/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Manage", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Failed to verify phone");
            return View(model);
        }

        ////
        //// GET: /Account/ChangePassword
        //public ActionResult ChangePassword()
        //{
        //    return View();
        //}

        ////
        //// POST: /Account/ChangePassword
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
        //    if (result.Succeeded)
        //    {
        //        var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
        //        if (user != null)
        //        {
        //            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //        }
        //        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
        //    }
        //    AddErrors(result);
        //    return View(model);
        //}

        //
        // GET: /Account/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Account/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }


        #endregion

        #endregion

        #region Public Actions

        #region External Login

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        #endregion

        #region LogOff

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region InvokeRequestResponseService - Needs to be Refactored - badly

        [AllowAnonymous]
        static async Task InvokeRequestResponseService(RegisterViewModel model)
        {

            string strIDE = "";
            string strSoftwareSpecialty = "";
            string strProgrammingLanguage = "";

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                // IDEs
                IEnumerable<FavoriteIDE> TFavoriteIDEs = db.FavoriteIDEs;
                IList<FavoriteIDE> FavoriteIDEs = new List<FavoriteIDE>();

                foreach (FavoriteIDE ide in TFavoriteIDEs)
                {
                    FavoriteIDEs.Add(ide);
                }

                IEnumerable<SelectListItem> lstSelectedFavorieIDEs =
                     from ide in FavoriteIDEs
                     select new SelectListItem
                     {
                         Selected = (ide.FavoriteIDEID == model.FavoriteIDEID),
                         Text = ide.FavoriteIDEName,
                         Value = ide.FavoriteIDEID.ToString()
                     };

                foreach (SelectListItem item in lstSelectedFavorieIDEs)
                {
                    if (item.Value == model.FavoriteIDEID.ToString())
                    {
                        strIDE = item.Text;
                        break;
                    }
                }


                // Software Specialties
                IEnumerable<SoftwareSpecialty> TSoftwareSpecialties = db.SoftwareSpecialties;
                IList<SoftwareSpecialty> SoftwareSpecialties = new List<SoftwareSpecialty>();

                foreach (SoftwareSpecialty ss in TSoftwareSpecialties)
                {
                    SoftwareSpecialties.Add(ss);
                }

                IEnumerable<SelectListItem> lstSelectedSoftwareSpecialties =
                     from ss in SoftwareSpecialties
                     select new SelectListItem
                     {
                         Selected = (ss.SoftwareSpecialtyID == model.SoftwareSpecialtyID),
                         Text = ss.SoftwareSpecialtyName,
                         Value = ss.SoftwareSpecialtyID.ToString()
                     };

                foreach (SelectListItem item in lstSelectedSoftwareSpecialties)
                {
                    if (item.Value == model.SoftwareSpecialtyID.ToString())
                    {
                        strSoftwareSpecialty = item.Text;
                        break;
                    }
                }

                // Programming Languages
                IEnumerable<ProgrammingLanguage> TProgrammingLanguages = db.ProgrammingLanguages;
                IList<ProgrammingLanguage> ProgrammingLanguages = new List<ProgrammingLanguage>();

                foreach (ProgrammingLanguage pl in TProgrammingLanguages)
                {
                    ProgrammingLanguages.Add(pl);
                }

                IEnumerable<SelectListItem> lstSelectedProgrammingLanguages =
                     from pl in ProgrammingLanguages
                     select new SelectListItem
                     {
                         Selected = (pl.ProgrammingLanguageID == model.ProgrammingLanguageID),
                         Text = pl.ProgrammingLanguageName,
                         Value = pl.ProgrammingLanguageID.ToString()
                     };

                foreach (SelectListItem item in lstSelectedProgrammingLanguages)
                {
                    if (item.Value == model.ProgrammingLanguageID.ToString())
                    {
                        strProgrammingLanguage = item.Text;
                        break;
                    }
                }

            }



            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {

                    Inputs = new Dictionary<string, DevNetAnalytics>() { 
                        { 
                            "Dev Profile Parameters", 
                            new DevNetAnalytics() 
                            {
                                ColumnNames = new string[] {"date Of Birth", "Favorite IDE", "Software Specialty", "Programming Language"},
                                Values = new string[,] {  { model.BirthDate.ToString(), strIDE, strSoftwareSpecialty, strProgrammingLanguage }  }
                     
                            }
                        },
                                        },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };
                const string apiKey = "71p+44dA6qfaXtMDPOqOzfJFO4T2H3grzjPLT3+0VxMPTmYxVrQUL3XC0Hl/h73eKABsbWO4ITH+juwA1oNDzQ=="; // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/e9f90a212fdc446f99acab120ed88a0a/services/4e67ba199fa946f8a9890785707cd163/execute?api-version=2.0&details=true");

                // WARNING: The 'await' statement below can result in a deadlock if you are calling this code from the UI thread of an ASP.Net application.
                // One way to address this would be to call ConfigureAwait(false) so that the execution does not attempt to resume on the original context.
                // For instance, replace code such as:
                //      result = await DoSomeTask()
                // with the following:
                //      result = await DoSomeTask().ConfigureAwait(false)


                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest).ConfigureAwait(false);



                if (response.IsSuccessStatusCode)
                {
                    //string result = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine("Result: {0}", result);

                    var lstRSSFeeds = await response.Content.ReadAsStringAsync().ContinueWith((readTask) =>
                    {

                        Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(readTask.Result);

                        return jObject;

                    });

                    DevNetAnalyticsViewModel.RSSFeed = lstRSSFeeds;
                }
                else
                {
                    Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                    // Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
                    Console.WriteLine(response.Headers.ToString());

                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                }
            }
        }

        #endregion

        #region Manage Profile

        //
        // POST: /Account/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        //
        // POST: /Account/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Generate the token and send it
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Your security code is: " + code
                };
                await UserManager.SmsService.SendAsync(message);
            }
            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        //
        // POST: /Account/RememberBrowser
        [HttpPost]
        public ActionResult RememberBrowser()
        {
            var rememberBrowserIdentity = AuthenticationManager.CreateTwoFactorRememberBrowserIdentity(User.Identity.GetUserId());
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, rememberBrowserIdentity);
            return RedirectToAction("Index", "Account");
        }

        //
        // POST: /Account/ForgetBrowser
        [HttpPost]
        public ActionResult ForgetBrowser()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);
            return RedirectToAction("Index", "Account");
        }

        //
        // POST: /Account/EnableTFA
        [HttpPost]
        public async Task<ActionResult> EnableTFA()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
            }
            return RedirectToAction("Index", "Account");
        }

        //
        // POST: /Account/DisableTFA
        [HttpPost]
        public async Task<ActionResult> DisableTFA()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
            }
            return RedirectToAction("Index", "Account");
        }

        //
        // POST: /Account/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Account");
        }

        //
        // POST: /Account/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Account");
        }

        //
        // GET: /Account/RemovePhoneNumber
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        #endregion

        #endregion

        #region Private Actions

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && _userManager != null)
        //    {
        //        _userManager.Dispose();
        //        _userManager = null;
        //    }

        //    base.Dispose(disposing);
        //}

        #endregion

        

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private void SendEmail(string email, string callbackUrl, string subject, string message)
        {
            // For information on sending mail, please visit http://go.microsoft.com/fwlink/?LinkID=320771
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }


        #endregion
    }
}