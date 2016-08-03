using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GhostHuntAR.Infrastructure.Models
{

  //[Table("UserProfile")]
  public class UserProfile
  {

    public UserProfile()
    {
      GHLocations = new List<GHLocation>();
    }

    [Key]
    public int UserId { get; set; }
    //[Column(TypeName = "VARCHAR")]
    [StringLength(450)]
    public string UserName { get; set; }
    public string Role { get; set; }
    public string Email { get; set; }
    public virtual ICollection<GHLocation> GHLocations { get; private set; }
    [Required]
    public string Name { get; set; }
    public byte[] Image { get; set; }
    [AllowHtml]
    public string Biography { get; set; }
    public virtual LastUserSettings LastUserSettings { get; set; }

  }

  public class EditUserViewModel
  {

    public EditUserViewModel()
    {

    }

    public EditUserViewModel(UserProfile user)
    {
      Email = user.Email;
      Name = user.Name;
      //Image = user.Image;
      Biography = user.Biography;
    }

    public string Email { get; set; }
    [Required]
    public string Name { get; set; }
    //public byte[] Image { get; set; }
    //[AllowHtml]
    public string Biography { get; set; }
  }

  public class RegisterExternalLoginModel
  {
    [Required]
    [Display(Name = "User name")]
    public string UserName { get; set; }

    public string ExternalLoginData { get; set; }
  }

  public class LocalPasswordModel
  {
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Current password")]
    public string OldPassword { get; set; }

    [Required]
    //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    //[RegularExpression(@"^(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(.{6,12})$", ErrorMessage = "You must enter a password with an uppercase and lowercase letter, and at least one number.")]
    [DataType(DataType.Password)]
    [Display(Name = "New password")]
    public string NewPassword { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm new password")]
    //[Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
  }

  public class LoginModel
  {
    [Required]
    [Display(Name = "User name")]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
  }

  public class RegisterModel
  {
    [Display(Name = "Name")]
    public string Name { get; set; }

    [Required]
    [Display(Name = "User name")]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "You must enter a valid email address.")]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required]
    //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    //[RegularExpression(@"^(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(.{6,12})$", ErrorMessage = "You must enter a password with an uppercase and lowercase letter, and at least one number.")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    [AllowHtml]
    public string Biography { get; set; }
  }

  public class ExternalLogin
  {
    public string Provider { get; set; }
    public string ProviderDisplayName { get; set; }
    public string ProviderUserId { get; set; }
  }
}
