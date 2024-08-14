using celsia.Data;
using celsia.Models;

namespace celsia.Services.Interfaces{
    public interface IAuthRepository{
        Administrador Login(string UserName, string Password);
        string GenerateToken(Administrador User);
        void LogOutAsync();
        IEnumerable<Administrador> GetAll();
    }
}