using System;
using System.Web;
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

        #region Login
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
                var resp = _wfsUSerManager.GetWfsUserInfoByUserName(new GetWfsUserInfoByUserNameRequest() { UserName = model.Email });

                AddAuthCookie(resp.Value.UserId, resp.Value.MembershipGuid);

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
        #endregion

        #region Registration
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

                    AddAuthCookie(resp.AccountInfo.User.UserId, resp.AccountInfo.User.MembershipGuid);

                    var uiresponse = new UIResponse<Guid>();
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
        #endregion

        public ActionResult ChangePassword()
        {
            var m = new RegisterModel();

            var resp = _wfsUSerManager.GetWfsUserInfoByUserName(new GetWfsUserInfoByUserNameRequest() { UserName = User.Identity.Name });

            m.FirstName = resp.Value.FirstName;
            m.LastName = resp.Value.LastName;

            return View(m);
        }



        private static void AddAuthCookie(int userId, Guid membershipId)
        {
            var cookieName = "WHSUserId";
            var myCookie = System.Web.HttpContext.Current.Request.Cookies[cookieName] ?? new HttpCookie(cookieName);
            myCookie.Values["UserId"] = userId.ToString();
            myCookie.Values["MembershipId"] = membershipId.ToString();

            System.Web.HttpContext.Current.Response.Cookies.Add(myCookie);
        }
    }
}
