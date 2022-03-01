using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using TicketManagement.WebUI.Models;
using TrainTable.DAL.Models;
using WebUI.ViewModels.Account;

namespace WebUI.Controllers
{
    /// <summary>
    /// This controller is used for Account and User profiles services like User profile, registration, login and password change.
    /// </summary>
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;


        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="userManager">An UserManager service item.</param>
        /// <param name="orderTicketProxyService">An OrderTicketProxyService service item.</param>
        /// <param name="eventSeatProxyService">An EventSeatsProxyItem service serviceitem.</param>
        /// <param name="eventProxyService">An EventProxyService service item.</param>
        /// <param name="eventAreaProxyService">An EventAreaProxy service item.</param>
        /// <param name="signInManager">A SignInManager item.</param>
        /// <param name="localizer">A Localizer item.</param>
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        
        }

        /// <summary>
        /// Registration GET.
        /// </summary>
        /// <returns>Empty view.</returns>
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// This method gets data from Registration form POST and creates new User.
        /// </summary>
        /// <param name="model">A RegisterViewModel model item.</param>
        /// <returns>View with model.</returns>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FullName = model.Fullname,
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");

                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(model);
        }

        /// <summary>
        /// Login GET.
        /// </summary>
        /// <returns>Empty view.</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// This action recives POST from Login page and Signs In user.
        /// </summary>
        /// <param name="user">A LoginViewModel model.</param>
        /// <returns>Redirect to Action SessionSetUp.</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel user)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(user);
        }

        /// <summary>
        /// This action logs out an User.
        /// </summary>
        /// <returns>Redirect to Login Action.</returns>
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }

        /// <summary>
        /// This action changes password of an User.
        /// </summary>
        /// <param name="id">An user id.</param>
        /// <returns>a View Model with user data.</returns>
        public async Task<IActionResult> ChangePassword(string id)
        {
            User user = await _userManager.FindByEmailAsync(id);
            
            if (user == null)
            {
                return NotFound();
            }

            ChangePasswordModel model = new ChangePasswordModel { Id = user.Id, Email = user.Email };
            return View(model);
        }

        /// <summary>
        /// This is POST action of ChangePassword. Used for changing user data.
        /// </summary>
        /// <param name="model">A ChangePasswordViewModel model.</param>
        /// <returns>Redirect to Profile action.</returns>
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByEmailAsync(model.Id);
                if (user != null)
                {
                    IdentityResult result =
                        await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("User", "Profile");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User was not found");
                }
            }

            return View(model);
        }
    }
}