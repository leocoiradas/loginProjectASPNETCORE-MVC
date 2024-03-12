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
                return RedirectToAction("Login", "Login");
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
            //Se busca al usuario en la base de datos con el método GetUser creado en UserService
            //Se pasa como parametro el correo y la contraseña la cual es encriptada con el método que creamos en Utilities
            Usuario user_found = await _userService.GetUser(email, Utilities.EncryptPassword(password));

            if (user_found == null)
            {
                ViewData["Message"] = "No se encontraron coincidencias";
                return View();
            }

            /*A continuación se implementa la lógica correspondiente a autenticación con ASPNETCORE Identity*/

            List<Claim> claims = new List<Claim>() 
            {
                new Claim(ClaimTypes.Name, user_found.Username) 
            };
            /*Nota: Cada claim corresponde a un fragmento de información del usuario que se puede dividir en tipos
             (los mas comunes siendo email, name, country o role) investigar para tener más info*/

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties() 
            {
                AllowRefresh = true,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
                );
            return RedirectToAction("Index", "Home");
        }
    }
}
