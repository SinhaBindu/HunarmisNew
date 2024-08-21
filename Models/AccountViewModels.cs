﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hunarmis.Models
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
        [Display(Name = "User Name")]
        //[EmailAddress]
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
        [Display(Name = "Name")]
        public string Name { get; set; }
        //[Required]
        //[Display(Name = "User Name")]
        //public string UserName { get; set; }
        [Required]
        [Phone]
        [MinLength(length:10)]
        [Display(Name = "Phone No")]
        public string PhoneNo { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email ID")]
        public string EmailId { get; set; }

        //[Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        //[DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "District")]
        public int DistrictID { get; set; }
        public int DistrictName { get; set; }
        [Display(Name = "Role")]
        public string RoleID { get; set; }
        [Display(Name = "Role")]
        public string RoleName { get; set; }
        public string Id { get; set; }
    }

    public class UserViewModel
    {
        public string UserId { get; set; }    
        public string UserName { get; set; }    
        public string Name { get; set; }    
        public string Phone { get; set; }    
        public string EmailId { get; set; }    
        public string DistrictId { get; set; }    
        public string District { get; set; }    
        public string RoleId { get; set; }    
        public string Role { get; set; }    
        public string MappedTCenterIds { get; set; }    
        public string MappedTCenters { get; set; }    
        public string LockoutEnabled { get; set; }    
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
