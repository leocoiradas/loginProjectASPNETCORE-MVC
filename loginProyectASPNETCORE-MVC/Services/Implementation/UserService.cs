using Microsoft.EntityFrameworkCore;
using loginProyectASPNETCORE_MVC.Models;
using loginProyectASPNETCORE_MVC.Services.Contract;

namespace loginProyectASPNETCORE_MVC.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly DbpruebaContext _dbpruebaContext;

        public UserService(DbpruebaContext dbContext)
        {
            _dbpruebaContext = dbContext;
        }
        public async Task<Usuario> GetUser(string email, string password)
        {
            Usuario user_found = await _dbpruebaContext.Usuarios.Where(u => u.Email == email && u.Password == password).FirstOrDefaultAsync();
            return user_found;
        }

        public async Task<Usuario> SaveUser(Usuario model)
        {
            _dbpruebaContext.Usuarios.Add(model);
            _dbpruebaContext.SaveChangesAsync();
            return model;
        }
    }
}
