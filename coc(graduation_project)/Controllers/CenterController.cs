using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coc_graduation_project_.Services;
using coc_graduation_project_.ViewModel;
using coc_graduation_project_.ViewModelRecieveData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace coc_graduation_project_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CenterController : ControllerBase
    {
        private readonly ICenterService _centerservices;
        private readonly IMailService _mailservice;
        private readonly IConfiguration _configuration;

        public CenterController(ICenterService centerservices, IMailService mailservice, IConfiguration configuration)
        {
            _centerservices = centerservices;
            _mailservice = mailservice;
            _configuration = configuration;

        }

        [HttpPost("AddBranch")]
        public async Task<IActionResult> AddBranch([FromForm] BranchViewModel Model, int centerID)
        {
            if (ModelState.IsValid)
            {
                var result = await _centerservices.AddBranchAsync(Model, centerID);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some Properties Are InValid");
        }

        [HttpPost("EditCenter")]
        public async Task<IActionResult> EditCenter([FromForm] EditCenter Model, int CenterID)
        {
            if (ModelState.IsValid)
            {
                var result =await _centerservices.EditCenter(Model, CenterID);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some Properties Are InValid");
        }
        [HttpGet("ReturnCenterProfile")]
        public async Task<IActionResult> ReturnCenterProfile(int CenterID)
        {
            if (ModelState.IsValid)
            {
                var result =await _centerservices.ReturnCenterProfile(CenterID);
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
