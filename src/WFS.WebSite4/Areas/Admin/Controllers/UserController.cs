using System;
using System.Web.Mvc;
using WFS.Contract;
using WFS.Contract.Enums;
using WFS.Contract.ReqResp;
using WFS.Domain.Managers;
using WFS.Framework;
using WFS.Framework.Extensions;
using WFS.Framework.Responses;
using WFS.WebSite4.Areas.Admin.Models;
using WFS.WebSite4.Controllers;

namespace WFS.WebSite4.Areas.Admin.Controllers
{
    [RoleAuthorize(WFSRoleEnum.Admin)]
    public class UserController : BaseController
    {
        private readonly WFSUserManager _userManager;

        public UserController(WFSUserManager userManager)
        {
            _userManager = userManager;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PerformSearch(string searchText, string filter)
        {
            var resp = _userManager.GetUserSearchByNameAndFilter(new UserSearchRequest() { SearchText = searchText.Trim(), RoleFilter = (filter == "All") ? string.Empty : filter.Trim() });

            var m = new SearchResultsViewModel();

            foreach (var item in resp.Values)
            {
                m.Results.Add(new SearchResult()
                    {
                        DisplayName = String.Format("{0}, {1}", item.LastName, item.FirstName),
                        EmailAddress = item.EmailAddress,
                        UserId = item.UserId,
                        MembershipId = item.MembershipGuid,
                        AccountType = item.UserType.ToString()
                    });
            }

            var uiresult = new UIResponse<SearchResultsViewModel>();
            uiresult.Subject = m;
            uiresult.HtmlResult = RenderPartialViewToString("SearchResults", m);
            uiresult.Status = resp.Status;
            return Json(uiresult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUserInfo(int userId)
        {
            var resp = _userManager.GetWfsUserInfoById(new GetWfsUserInfoByIdRequest() { UserId = userId });

            var m = new UserEditModel()
            {
                UserInfo = resp.Value
            };

            var uiresult = new UIResponse<UserEditModel>();
            uiresult.Subject = m;
            uiresult.HtmlResult = RenderPartialViewToString("UserInfo", m);
            uiresult.Status = resp.Status;
            return Json(uiresult, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult UpdateUserInfo(UserEditModel model)
        {
            var resp = _userManager.SaveCustomer(new SaveWFSUserRequest()
            {
                UserInfo = model.UserInfo
            });


            if (resp.Status == Status.Success)
            {
                var uiresponse = resp.ToUIResult<UserEditModel, WFSUser>(x => model, x => RenderPartialViewToString("UserInfo", x));
                return Json(uiresponse);
            }
            else
            {
                var uiResp = resp.ToUIResult<UserEditModel, WFSUser>(x => model, x =>
                {
                    x.Merge(resp);
                    return RenderPartialViewToString("UserInfo", model);
                });
                return Json(uiResp);
            }
        }

        [HttpPost]
        public ActionResult UpdateUserBalance(UserEditModel model)
        {
            var resp = _userManager.SaveUserAccountCredits(new SaveWFSUserRequest()
            {
                UserInfo = model.UserInfo
            });

            if (resp.Status == Status.Success)
            {
                var uiresponse = resp.ToUIResult<UserEditModel, WFSUser>(x => model, x => RenderPartialViewToString("UserInfo", x));
                return Json(uiresponse);
            }
            else
            {
                var uiResp = resp.ToUIResult<UserEditModel, WFSUser>(x => model, x =>
                {
                    x.Merge(resp);
                    return RenderPartialViewToString("UserInfo", model);
                });
                return Json(uiResp);
            }
        }
    }
}
