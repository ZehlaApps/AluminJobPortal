using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using JobPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Text.Encodings.Web;

namespace JobPortal.Areas.Identity.Pages.Account
{
    public class AuthorizeModel : PageModel
    {
        
        // private const string salt = "sG59-a9!A2Y5Nn4";

        [BindProperty]
        public InputModel Input { get; set; }

        private readonly UserManager<JobProfile> _userManager;
        private readonly SignInManager<JobProfile> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;

        private readonly ILogger<AuthorizeModel> _logger;

        // public string AlumniId { get; set; }

        
        public AuthorizeModel(
            UserManager<JobProfile> userManager,
            SignInManager<JobProfile> signInManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<AuthorizeModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Full name")]
            public string FullName { get; set; }

            [Display(Name = "Organisation/College")]
            [DataType(DataType.Text)]
            public string Organisation { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }


        public ActionResult OnGet()
        {
            // AlumniId = alumniId;

            // if(User.Identity.IsAuthenticated)
            // {
            //     return LocalRedirect(Url.Content("~/"));
            // }

            // if(AlumniId != null)
            // {
            //     var user = await _userManager.FindByIdAsync(alumniId);
            //     if(user != null)
            //     {
            //         await _signInManager.SignInAsync(user, isPersistent: true);
            //         return LocalRedirect(Url.Content("~/"));
            //     }
            // }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
                var userid = Guid.NewGuid().ToString();

            if (ModelState.IsValid)
            {
                // var userid = Guid.NewGuid().ToString();
                var user = new JobProfile
                {
                    Id = userid,
                    FullName = Input.FullName,
                    Role = Roles.Applicant,
                    Organisation = Input.Organisation,
                    UserName = Input.Email,
                    Email = Input.Email,
                    Resume = new JobResume
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserId = userid
                    }
                };

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var roleExists = await _roleManager.RoleExistsAsync("Applicant");
                    if (!roleExists)
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Applicant"));
                    }

                    await _userManager.AddToRoleAsync(user, "Applicant");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = user.Id, code = code },
                            protocol: Request.Scheme);

                        await _emailSender.SendEmailAsync(Input.Email,
                            "Confirm your email",
                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: true);
                        return LocalRedirect(Url.Content("~/"));
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
    }
}