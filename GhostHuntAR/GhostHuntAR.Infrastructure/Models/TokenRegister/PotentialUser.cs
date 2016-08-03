using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GhostHuntAR.Infrastructure.Models.TokenRegister
{
    public class PotentialUser
    {

        public PotentialUser()
        {
            SignedUp = false;
        }

        [Key]
        [HiddenInput(DisplayValue = false)]
        public long PotentialUserID { get; set; }
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "You must enter a valid email address.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string Role { get; set; }
        //[Column(TypeName = "VARCHAR")]
        [StringLength(450)]
        [HiddenInput(DisplayValue = true)]
        public string Token { get; set; }
        [HiddenInput(DisplayValue = false)]
        public bool SignedUp { get; set; }       
    }
}