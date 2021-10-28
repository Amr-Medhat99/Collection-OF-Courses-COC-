using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using coc_graduation_project_.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace coc_graduation_project_.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [DataType(DataType.Text, ErrorMessage = "UserName Not Be Formal Of Name")]
        [Display(Name = "User Name")]
        [StringLength(30, ErrorMessage = "Max Length MustBe Less Than 30 Character", MinimumLength = 5)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress,ErrorMessage ="Email Address Not Be Formal Of Email")]
        [Display(Name ="Email Address")]
        [StringLength(30, ErrorMessage = "Max Length MustBe Less Than 30 Character", MinimumLength = 5)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Password")]
        [StringLength(30, ErrorMessage = "Max Length MustBe Less Than 30 Number", MinimumLength =5)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Confirm Password")]
        [Compare("Password",ErrorMessage ="Password And Confirm Password Are No Match")]
        [StringLength(30,ErrorMessage ="Max Length MustBe Less Than 30 Number",MinimumLength =5)]
        public string ConfirmPassword { get; set; }

    }
    public class RegisterAdmin
    {
        [Required]
        [DataType(DataType.Text, ErrorMessage = "UserName Not Be Formal Of Name")]
        [Display(Name = "User Name")]
        //[StringLength(30, ErrorMessage = "Max Length MustBe Less Than 30 Character", MinimumLength = 5)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email Address Not Be Formal Of Email")]
        [Display(Name = "Email Address")]
        //[StringLength(30, ErrorMessage = "Max Length MustBe Less Than 30 Character", MinimumLength = 5)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        //[StringLength(30, ErrorMessage = "Max Length MustBe Less Than 30 Number", MinimumLength = 5)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password And Confirm Password Are No Match")]
        //[StringLength(30, ErrorMessage = "Max Length MustBe Less Than 30 Number", MinimumLength = 5)]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
