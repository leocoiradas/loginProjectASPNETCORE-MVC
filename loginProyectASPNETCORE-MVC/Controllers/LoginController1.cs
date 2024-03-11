using Microsoft.AspNetCore.Mvc;
using loginProyectASPNETCORE_MVC.Models;
using loginProyectASPNETCORE_MVC.Resources;
using loginProyectASPNETCORE_MVC.Services.Contract;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace loginProyectASPNETCORE_MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        public LoginController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> SignUp(Usuario user)
        {
            user.Password = Utilities.EncryptPassword(user.Password);
            Usuario user_created = await _userService.SaveUser(user);

            //Condicional que verifica si el id del usuario creado no es nulo (es decir, que el usuario se creó correctamente)

            if (user_created.UserId > 0)
            {
                return RedirectToAction("Login", "Home");
            }
            //En caso de no poder crearse el usuario, se muestra el siguiente mensaje al usuario a través de ViewData

            ViewData["Message"] = "User could not be created";
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {

            return View();


        }
    }
}
