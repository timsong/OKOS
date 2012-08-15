using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using WFS.Contract;
using WFS.Contract.Enums;
using WFS.Contract.ReqResp;
using WFS.Domain.Managers;
using WFS.Framework;
using WFS.Framework.Responses;
using WFS.WebSite4.Models;

namespace WFS.WebSite4.Controllers
{

    [Authorize]
    public class AccountController : BaseController
    {
        private readonly CustomerManager _customerManager;
        private readonly WFSUserManager _wfsUSerManager;

        public AccountController(CustomerManager customerManager, WFSUserManager wfsUSerManager)
        {
            _customerManager = customerManager;
            _wfsUSerManager = wfsUSerManager;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(RegisterModel model, string returnUrl)
        {
            if (Membership.ValidateUser(model.Email, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            var m = new RegisterModel();
            return View(m);
        }

        [AllowAnonymous]
        public ActionResult ShowTerms()
        {
            var uiresponse = new UIResponse<RegisterModel>();

            uiresponse.Status = Status.Success;
            uiresponse.HtmlResult = RenderPartialViewToString("TermsAndCond", null);

            return Json(uiresponse, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var acct = new CustomerAccount();
                acct.User.FirstName = model.FirstName;
                acct.User.LastName = model.LastName;
                acct.User.Password = model.Password;
                acct.User.UserType = WFSUserTypeEnum.Customer;
                acct.User.EmailAddress = model.Email;
                acct.AddressInfo = new PhoneAddress()
                {
                    Address1 = model.AddressInfo.Address1,
                    Address2 = model.AddressInfo.Address2,
                    City = model.AddressInfo.City,
                    State = model.AddressInfo.State,
                    ZipCode = model.AddressInfo.ZipCode,
                    PhoneNumber = model.AddressInfo.PhoneNumber,
                    PhoneExt = model.AddressInfo.PhoneExt,
                };

                var resp = _customerManager.SaveCustomer(new Contract.ReqResp.CreateCustomerAccountRequest()
                    {
                        AccountInfo = acct
                    });

                if (resp.Status == Status.Success)
                {
                    Roles.AddUserToRole(model.Email, WFSRoleEnum.Customer.ToString());
                    FormsAuthentication.SetAuthCookie(model.Email, createPersistentCookie: false);

                    var uiresponse = new UIResponse<Guid>();
                    uiresponse.Subject = resp.AccountInfo.User.MembershipGuid;
                    return Json(uiresponse);
                }
                else
                {
                    var uiresponse = new UIResponse<RegisterModel>();

                    model.Merge(resp);
                    uiresponse.Status = resp.Status;
                    uiresponse.Subject = model;
                    uiresponse.HtmlResult = RenderPartialViewToString("Register", model);

                    return Json(uiresponse);
                }
            }

            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        public ActionResult ChangePassword()
        {
            var m = new RegisterModel();

            var resp = _wfsUSerManager.GetWfsUserInfoByUserName(new GetWfsUserInfoByUserNameRequest() { UserName = User.Identity.Name });

            m.FirstName = resp.Value.FirstName;
            m.LastName = resp.Value.LastName;

            return View(m);
        }

        //
        // POST: /Account/ChangePassword

        //[HttpPost]
        //public ActionResult ChangePassword(ChangePasswordModel model)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        // ChangePassword will throw an exception rather
        //        // than return false in certain failure scenarios.
        //        bool changePasswordSucceeded;
        //        try
        //        {
        //            MembershipUser currentUser = Membership.GetUser(User.Identity.Name, userIsOnline: true);
        //            changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
        //        }
        //        catch (Exception)
        //        {
        //            changePasswordSucceeded = false;
        //        }

        //        if (changePasswordSucceeded)
        //        {
        //            return RedirectToAction("ChangePasswordSuccess");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        private IEnumerable<string> GetErrorsFromModelState()
        {
            return ModelState.SelectMany(x => x.Value.Errors.Select(error => error.ErrorMessage));
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
