using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobPortal.Models;
using JobPortal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace JobPortal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<JobProfile> _userManager;
        private readonly JobListingService _jobListingService;

        private readonly IConfiguration _configuration;


        public AdminController(
            ILogger<AdminController> logger,
            UserManager<JobProfile> userManager,
            RoleManager<IdentityRole> roleManager,
            JobListingService jobListingService,
            IConfiguration configuration)
        {
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
            _jobListingService = jobListingService;
            _configuration = configuration;
        }

        [Authorize]
        [HttpGet("makeadmin")]
        public async Task<ActionResult> MakeAdmin(string apiKey)
        {
            if (_configuration["SuperAdmin:ApiKey"] == apiKey)
            {
                var user = await _userManager.GetUserAsync(User);
                var roleExists = await _roleManager.RoleExistsAsync("Admin");
                if (!roleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                await _userManager.AddToRoleAsync(user, "Admin");
                return Ok($"{user.FullName} is now admin");
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
