using Microsoft.EntityFrameworkCore;
using loginProyectASPNETCORE_MVC.Models;

namespace loginProyectASPNETCORE_MVC.Services.Contract
{
    public interface IUserService
    {
        Task<Usuario> GetUser(string email, string password);
        Task<Usuario> SaveUser(Usuario model);
    }
}
