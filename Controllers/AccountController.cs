using JopSy.Data;
using JopSy.Models;
using JopSy.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JopSy.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
            if (user == null)
            {
                TempData["Error"] = "Invalid email or password.";
                return View(loginViewModel);
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
            if (!passwordCheck)
            {
                TempData["Error"] = "Invalid email or password.";
                return View(loginViewModel);
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false);
            if (!result.Succeeded)
            {
                TempData["Error"] = "Login failed. Please try again.";
                return View(loginViewModel);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid for registration. Errors: {Errors}",
                 string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));
                return View(registerViewModel);
            }

            var user = await _userManager.FindByEmailAsync(registerViewModel.Email);
            if (user != null)
            {
                _logger.LogWarning("Registration attempt with already used email: {Email}", registerViewModel.Email);
                TempData["Error"] = "This email address is already in use";
                return View(registerViewModel);
            }

            var newUser = new User()
            {
                FullName = registerViewModel.Name,
                Email = registerViewModel.Email,
                UserName = registerViewModel.Email,
                EntityType = registerViewModel.EntityType
                
            };

            _logger.LogInformation("Attempting to create user: Email={Email}, FullName={FullName}, EntityType={EntityType}",
                newUser.Email, newUser.FullName, newUser.EntityType);

            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if (newUserResponse.Succeeded)
            {
                _logger.LogInformation("User created successfully: {Email}", newUser.Email);
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                _logger.LogInformation("User {Email} assigned to role: {Role}", newUser.Email, UserRoles.User);
                return RedirectToAction("Index", "Race");
            }

            _logger.LogError("User creation failed for {Email}. Errors: {Errors}",
                newUser.Email, string.Join(", ", newUserResponse.Errors.Select(e => e.Description)));

            foreach (var error in newUserResponse.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(registerViewModel);
        }

        public IActionResult Verify()
        {
            return View();
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}