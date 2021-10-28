using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coc_graduation_project_.Models;
using coc_graduation_project_.Services;
using coc_graduation_project_.ViewModel;
using coc_graduation_project_.ViewModelRecieveData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity; //use from roleManager
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace coc_graduation_project_.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    //[AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IUserServices _userservices;
        private readonly IMailService _mailservice;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _rolemanager;
        public AuthController(IUserServices userservices, IMailService mailservice, IConfiguration configuration, RoleManager<IdentityRole> rolemanager)
        {
            _userservices = userservices;
            _mailservice = mailservice;
            _configuration = configuration;
            _rolemanager = rolemanager;
        }

        [HttpGet("AddAllRole")]
        public async Task<IActionResult> AddAllRole()
        {
            try
            {
                //assign 3 item in role table
                IdentityRole NormalRole = new IdentityRole() { Name = "NormalRole" };
                IdentityRole AdminRole = new IdentityRole() { Name = "AdminRole" };

                //create 3 item in database in role table
                var result1 = await _rolemanager.CreateAsync(NormalRole);
                var result2 = await _rolemanager.CreateAsync(AdminRole);
                if (result1.Succeeded && result2.Succeeded)
                {
                    return Ok();
                }
                return BadRequest("An Error Occuer");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           

        }
        [HttpPost("AddProfileData")]
        public async Task<IActionResult> AddProfileData([FromForm]EditStudent Model,int StudentID)
        {
            if (ModelState.IsValid)
            {
                var Result = await _userservices.AddProfileDtaAsync(Model, StudentID);
                if (Result.IsSuccess)
                {
                    return Ok(Result);
                }
                return BadRequest(Result);
            }
            else
                return BadRequest("Some Properties Are Not Valid");
        }

        // path=/api/Auth/Register
        [HttpPost("Register")]
        public async Task<IActionResult>Register([FromForm]RegisterViewModel Model)
        {
            if (ModelState.IsValid)
            {
                var Result = await _userservices.RegisterUserAsync(Model);
                if (Result.IsSuccess)
                {
                    return Ok(Result);
                }
                    return BadRequest(Result);
            }
            else
                return BadRequest("Some Properties Are Not Valid");
        }

        [HttpPost("RegisterAdminAsync")]
        public async Task<IActionResult> RegisterAdminAsync([FromForm] RegisterAdmin Model)
        {
            if (ModelState.IsValid)
            {
                var Result = await _userservices.RegisterAdminAsync(Model);
                if (Result.IsSuccess)
                {
                    return Ok(Result);
                }
                return BadRequest(Result);
            }
            else
                return BadRequest("Some Properties Are Not Valid");
        }
        // path=/api/auth/Login
        [HttpPost("Login")]
        public async Task<IActionResult>Login([FromForm]LoginViewModel Model)
        {
            if (ModelState.IsValid)
            {
                var Result = await _userservices.LoginUserAsync(Model);
                if (Result.IsSuccess)
                {
                    await _mailservice.SendEmailAsync(Model.Email, "New Login", "<h1>Hey!, new login to your account noticed</h1><p>New login to your account at " + DateTime.Now + "</p>");
                    return Ok(Result);
                }
                else
                {
                    return BadRequest(Result);
                }
            }
            else
                return BadRequest("Some Properties Are Not Valid");
        }
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userid,string token)
        {
            if (string.IsNullOrWhiteSpace(userid)||string.IsNullOrWhiteSpace(token))
                return NotFound();

            var result = await _userservices.ConfirmEmailAsync(userid, token);
            if (result.IsSuccess)
            {
                return Redirect($"{_configuration["URL"]}/ConfirmEmail.html");
            }

            return BadRequest(result);
        }
        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
                return NotFound();

            var result = await _userservices.ForgetPasswordAsync(email);

            if (result.IsSuccess)
                return Ok(result); // 200

            return BadRequest(result); // 400
        }
        // api/auth/resetpassword
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userservices.ResetPasswordAsync(model);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }

        [HttpGet("ReturnStudentProfile")]
        public async Task<IActionResult> ReturnStudentProfile(int StudentID)
        {
            if (ModelState.IsValid)
            {
                var result =await _userservices.ReturnStudentProfile(StudentID);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some Properties Are InValid");
        }

    }
}
