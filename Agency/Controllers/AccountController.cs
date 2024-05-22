using Agency.Business.Helpers.Account;
using Agency.Core.Models;
using Agency.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Agency.Controllers
{

    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signinmanager;
        private readonly RoleManager<IdentityRole> _rolemanager;
        private readonly UserManager<User> _userManager;
        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signinmanager = signInManager;
            _userManager = userManager;
            _rolemanager = roleManager;
        }
       public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult LogOut()
        {
            _signinmanager.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> CreateRole()
        {
            foreach(var item in Enum.GetValues(typeof(UserRole)))
            {
                await _rolemanager.CreateAsync(new IdentityRole()
                {
                    Name=item.ToString(),
                });
            }
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return View();
            var user = await _userManager.FindByNameAsync(loginDto.UserNameOrEmai);
            if(user==null)
            {
                user = await _userManager.FindByEmailAsync(loginDto.UserNameOrEmai);
                if (user == null)
                {
                    ModelState.AddModelError("", "UsernameOrEmail duz deyil");
                    return View();
                }
            }
            var res=await _signinmanager.CheckPasswordSignInAsync(user,loginDto.Password,true);
            if (res.IsLockedOut)
            {
                ModelState.AddModelError("", "Birazdan cehd edin");
                return View();
            }
            await _signinmanager.SignInAsync(user, loginDto.IsRemembered);
            return RedirectToAction("Index", "Portfolio", new { Area = "Admin" });

        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            User user = new User()
            {
                Name = registerDto.Name,
                Surname=registerDto.Surname,    
                UserName=registerDto.Username,
                Email=registerDto.Email,
            };
            var res = await _userManager.CreateAsync(user,registerDto.Password);
            if (!res.Succeeded)
            {
                foreach (var item in res.Errors)
                {
                    ModelState.AddModelError("", item.Description);

                }
                return View();
            }   
                await _userManager.AddToRoleAsync(user, UserRole.Admin.ToString());
                return RedirectToAction(nameof(Login));
        }

    }
}
