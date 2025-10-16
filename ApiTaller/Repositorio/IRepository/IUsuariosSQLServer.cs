using ApiTaller.Modelo;
using System.Threading.Tasks;

namespace ApiTaller.Repositorio.IRepository
{
    public interface IUsuariosSQLServer
    {
       Task<UsuarioAPI> DameUsuario(LoginAPI login);
    }
}
