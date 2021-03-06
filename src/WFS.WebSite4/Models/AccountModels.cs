﻿using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WFS.Contract;
using WFS.Framework;

namespace WFS.WebSite4.Models
{
    public interface ILoginConverter
    {
        RegisterModel ToLogin();
    }
    //public class ChangePasswordModel : ILoginConverter
    //{
    //    [Required]
    //    [DataType(DataType.Password)]
    //    [Display(Name = "Current password")]
    //    public string OldPassword { get; set; }

    //    [Required]
    //    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    //    [DataType(DataType.Password)]
    //    [Display(Name = "New password")]
    //    public string NewPassword { get; set; }

    //    [DataType(DataType.Password)]
    //    [Display(Name = "Confirm new password")]
    //    [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
    //    public string ConfirmPassword { get; set; }

    //    public LoginModel ToLogin()
    //    {
    //        return new LoginModel { UserName = HttpContext.Current.User.Identity.Name };
    //    }
    //}

    //public class LoginModel : ILoginConverter
    //{
    //    [Required]
    //    [Display(Name = "User name")]
    //    public string UserName { get; set; }

    //    [Required]
    //    [DataType(DataType.Password)]
    //    [Display(Name = "Password")]
    //    public string Password { get; set; }

    //    [Display(Name = "Remember me?")]
    //    public bool RememberMe { get; set; }

    //    public LoginModel ToLogin()
    //    {
    //        return this;
    //    }
    //}

    public class RegisterModel : EditModelBase<WFSUser>, ILoginConverter
    {
        public RegisterModel()
        {
            AddressInfo = new PhoneAddress();
        }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public PhoneAddress AddressInfo { get; set; }
        public RegisterModel ToLogin()
        {
            return this;
        }
    }

    public class UpdateAccountModel : EditModelBase<WFSUser>
    {
        public WFSUser UserInfo { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }

    public static class AccountModelExtensions
    {
        public static RegisterModel GetLoginModel(this ILoginConverter subject)
        {
            return subject.ToLogin();
        }
    }
}
