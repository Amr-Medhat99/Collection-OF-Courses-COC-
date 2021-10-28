using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coc_graduation_project_.Services;
using coc_graduation_project_.ViewModel;
using coc_graduation_project_.ViewModelRecieveData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace coc_graduation_project_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstController : ControllerBase
    {
        private readonly InstService _instService;
        private readonly IMailService _mailservice;
        private readonly IConfiguration _configuration;
        public InstController(InstService instService, IMailService mailservice, IConfiguration configuration)
        {
            _instService = instService;
            _mailservice = mailservice;
            _configuration = configuration;
        }
              
        [HttpPost("EditInstructor")]
        public async Task<IActionResult> EditInstructor([FromForm] EditInstructor Model, int InstructorID)
        {
            if (ModelState.IsValid)
            {
                var result = await _instService.EditInstructor(Model, InstructorID);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some Properties Are InValid");
        }
 
        [HttpGet("ReturnInstructorProfile")]
        public async Task<IActionResult> ReturnInstructorProfile(int InstructorID)
        {
            if (ModelState.IsValid)
            {
                var result = await _instService.ReturnInstructorProfile(InstructorID);
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
