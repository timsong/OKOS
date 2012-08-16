using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WFS.Contract;
using WFS.Contract.Enums;
using WFS.Contract.ReqResp;
using WFS.Domain.Managers;
using WFS.Framework;
using WFS.Framework.Extensions;
using WFS.Framework.Responses;
using WFS.WebSite4.Models;

namespace WFS.WebSite4.Controllers
{

    [Authorize]
    public class AccountController : BaseController
    {
        private readonly WFSUserManager _wfsUSerManager;

        public AccountController(WFSUserManager wfsUSerManager)
        {
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
                    return Redirect(returnUrl);
                else
                    return RedirectToAction("Index", "Home");
            }
            else
                ModelState.AddModelError("", "The user name or password provided is incorrect.");

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
                var acct = new WFSUser();
                acct.FirstName = model.FirstName;
                acct.LastName = model.LastName;
                acct.Password = model.Password;
                acct.UserType = WFSUserTypeEnum.Customer;
                acct.EmailAddress = model.Email;
                acct.BillingAddress = new PhoneAddress()
                {
                    Address1 = model.AddressInfo.Address1,
                    Address2 = model.AddressInfo.Address2,
                    City = model.AddressInfo.City,
                    State = model.AddressInfo.State,
                    ZipCode = model.AddressInfo.ZipCode,
                    PhoneNumber = model.AddressInfo.PhoneNumber,
                    PhoneExt = model.AddressInfo.PhoneExt,
                };

                var resp = _wfsUSerManager.SaveCustomer(new SaveWFSUserRequest() { UserInfo = acct });

                if (resp.Status == Status.Success)
                {
                    Roles.AddUserToRole(model.Email, WFSRoleEnum.Customer.ToString());
                    FormsAuthentication.SetAuthCookie(model.Email, createPersistentCookie: false);

                    AddAuthCookie(resp.UserInfo.UserId, resp.UserInfo.MembershipGuid);

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

        public ActionResult UpdateAccount()
        {
            var resp = _wfsUSerManager.GetWfsUserInfoByMembershipId(new GetWfsUserInfoByMembershipIdRequest() { MembershipId = AuthenticatedMembershipId });

            var m = new UpdateAccountModel()
            {   
                 UserInfo = resp.Value
            };

            m.Merge(resp);
            return View(m);
        }

        [HttpPost]
        public ActionResult UpdateAccountPost(UpdateAccountModel model)
        {
            var resp = _wfsUSerManager.SaveCustomer(new SaveWFSUserRequest()
            {
                UserInfo = model.UserInfo
            });


            if (resp.Status == Status.Success)
            {
                var uiresponse = resp.ToUIResult<UpdateAccountModel, WFSUser>(x => model, x => RenderPartialViewToString("UpdateAccount", x));
                return Json(uiresponse);
            }
            else
            {
                var uiResp = resp.ToUIResult<UpdateAccountModel, WFSUser>(x => model, x =>
                {
                    x.Merge(resp);
                    return RenderPartialViewToString("UpdateAccount", model);
                });
                return Json(uiResp);
            }
        }

        [HttpPost]
        public ActionResult UpdatePassword(UpdateAccountModel model)
        {
            var user = Membership.GetUser(model.UserInfo.MembershipGuid);
            var res = user.ChangePassword(model.OldPassword, model.Password);

            if (res)
            {
                var uiresponse = new UIResponse<UpdateAccountModel>();
                uiresponse.Subject = model;
                uiresponse.Status = Status.Success;
                return Json(uiresponse);
            }
            else
            {
                var uiresponse = new UIResponse<UpdateAccountModel>();
                uiresponse.Subject = model;
                uiresponse.Status = Status.Error;
                return Json(uiresponse);
            }
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
