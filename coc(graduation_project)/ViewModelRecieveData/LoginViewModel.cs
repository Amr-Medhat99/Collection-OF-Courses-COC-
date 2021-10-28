using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Please Enter Email")]
        [Display(Name ="Email")]
        //[DataType(DataType.EmailAddress)]
        //[StringLength(maximumLength:30, ErrorMessage = "The Maximum Number Must Be Less Than 50 Char And Greater Than 12 Char", MinimumLength =5)]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Please Enter Password")]
        [Display(Name = "Password")]
        //[DataType(DataType.Password)]
        //[StringLength(maximumLength: 30,ErrorMessage = "The Maximum Number Must Be Less Than 20 Number And Greater Than 10 Number", MinimumLength = 5)]
        public string Password { get; set; }
    }
}