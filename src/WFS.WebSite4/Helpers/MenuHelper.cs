using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Security;
namespace WFS.WebSite.Helpers
{
    public enum RouteAccessType
    {
        NonAuth, Auth, All
    }
    [Flags]
    public enum UserType
    {
        Unknown = 0x0, Admin = 0x1, SystemAdmin= 0x2, UserAdmin = 0x4, Client = 0x8, Contractor = 0x16
    }

    public class RouteInfo
    {
        private Func<bool> _isDisabled = () => false;
        private Func<bool> _isActive = () => true;

        public RouteInfo()
        {
            this.AccessType = RouteAccessType.All;
            this.RouteValues = new { };
            this.HtmlAttributes = new { };
        }

        public string LinkText { get; set; }
        public string RouteName { get; set; }
        public object RouteValues { get; set; }
        public object HtmlAttributes { get; set; }
        public bool ExactMatch { get; set; }
        public string RouteMatchClass { get; set; }
        public RouteAccessType AccessType { get; set; }
        public UserType UserType { get; set; }

        //public bool ActiveOrganizationOnly { get; set; }

        public Func<bool> IsDisabled
        {
            get { return this._isDisabled; }
            set { this._isDisabled = value; }
        }

        public Func<bool> IsActive
        {
            get { return this._isActive; }
            set { this._isActive = value; }
        }
    }

    public static class MenuHelper
    {
        const string TMP_URL = @"<li class=""{0} {1}"">{2}</li>";

        const string EXC_ROUTE_NOTFOUND = "RouteLinkExists: Route {0} not found in route collection.";

		public static MvcHtmlString RouteLinkExists(this HtmlHelper helper, string linkText, string routeName, object routeValues, object htmlAttributes, string routeMatchesClass)
        {
            return helper.RouteLinkExists(linkText, routeName, routeValues, htmlAttributes, routeMatchesClass, false);
        }
		public static MvcHtmlString RouteLinkExists(this HtmlHelper helper, string linkText, string routeName, object routeValues, object htmlAttributes, string routeMatchesClass, bool exactMatch)
        {
            RouteInfo routeInfo = new RouteInfo();
            routeInfo.AccessType = RouteAccessType.All;
            routeInfo.ExactMatch = exactMatch;
            routeInfo.HtmlAttributes = htmlAttributes;
            routeInfo.LinkText = linkText;
            routeInfo.RouteMatchClass = routeMatchesClass;
            routeInfo.RouteName = routeName;
            routeInfo.RouteValues = routeValues;
            return helper.RouteLinkExists(routeInfo);
        }
        public static MvcHtmlString RouteLinkExists(this HtmlHelper helper, RouteInfo routeInfo)
        {
			bool isNotAuthType = true;

			if (routeInfo.AccessType == RouteAccessType.Auth)
			{
				foreach (string role in Roles.GetRolesForUser())
				{
					var userType = (UserType)Enum.Parse(typeof(UserType), role);
					if (isNotAuthType)
						isNotAuthType = !(((routeInfo.UserType & userType) == userType) && routeInfo.AccessType == RouteAccessType.Auth);
				}
			}
			else
			{
				isNotAuthType = false;
			}
		
            bool isNotAuth =	HttpContext.Current.User.Identity.IsAuthenticated && routeInfo.AccessType == RouteAccessType.NonAuth;
			bool isAuth =		HttpContext.Current.User.Identity.IsAuthenticated && routeInfo.AccessType == RouteAccessType.NonAuth;
            bool isActive = routeInfo.IsActive();

            // auth type filter
            if (isNotAuth || isNotAuthType || !isActive)
            {
                return new MvcHtmlString (string.Empty);
            }

            if (helper.RouteCollection[routeInfo.RouteName] == null)
            {
                throw new Exception(string.Format(EXC_ROUTE_NOTFOUND, routeInfo.RouteName));
            }

            string area = (((System.Web.Routing.Route)(helper.RouteCollection[routeInfo.RouteName])).DataTokens["area"] ?? "").ToString();

            string currentArea = (helper.ViewContext.RouteData.DataTokens["area"] ?? "").ToString();

            string url = String.Format("/{0}", ((System.Web.Routing.Route)(helper.RouteCollection[routeInfo.RouteName])).Url);

            if (routeInfo.IsDisabled())
            {
                return new MvcHtmlString (string.Format(TMP_URL, "disabledMenuItem", "", helper.RouteLink(routeInfo.LinkText, routeInfo.RouteName, routeInfo.RouteValues, routeInfo.HtmlAttributes).ToHtmlString()));
            }
            else if (url == HttpContext.Current.Request.Url.AbsolutePath)
            {
                return new MvcHtmlString (string.Format(TMP_URL, "currentMenuItem", routeInfo.RouteMatchClass, routeInfo.LinkText));
            }
            else if (area == currentArea && !routeInfo.ExactMatch)
            {
                return new MvcHtmlString (string.Format(TMP_URL, "menuItem", "currentMenuItem", helper.RouteLink(routeInfo.LinkText, routeInfo.RouteName, routeInfo.RouteValues, routeInfo.HtmlAttributes).ToHtmlString()));
            }

            return new MvcHtmlString(string.Format(TMP_URL, "menuItem", "", helper.RouteLink(routeInfo.LinkText, routeInfo.RouteName, routeInfo.RouteValues, routeInfo.HtmlAttributes).ToHtmlString()));
        }
    }
}
