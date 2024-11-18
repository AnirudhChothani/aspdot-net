using System.ComponentModel.DataAnnotations;

namespace Forjob.Models
{
    public class UserModel
    {
       
            public int? UserID { get; set; }

            [Required(ErrorMessage = "User Name is Required")]
            public string UserName { get; set; }

            [Required]
            [Display(Name = "Email")]
            [EmailAddress(ErrorMessage = "Require")]
            [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Your Email is not valid.")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Password")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile Number Must Be 10 Digits!")]
            public string MobileNo { get; set; }

            [Required(ErrorMessage = "Address is Required")]
            public string Address { get; set; }

            [Required]
            [Display(Name = "Active")]
            public bool IsActive { get; set; }
        
    }
}
