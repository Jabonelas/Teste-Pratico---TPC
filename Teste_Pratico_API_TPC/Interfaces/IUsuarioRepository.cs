using System.Linq.Expressions;
using Teste_Pratico_API_TPC.DTOs.Usuarios;
using Teste_Pratico_API_TPC.Models;

namespace Teste_Pratico_API_TPC.Interfaces
{
    public interface IUsuarioRepository
    {
        Task AdicionarAsync(TbUsuario _usuario);

        Task<TbUsuario> ObterUsuarioIDAsync(int _idUsuario);

        Task<IEnumerable<TbUsuario>> ObterTodosAsync();

        Task<bool> VerificarEmailExisteAsync(string _email);

        Task<bool> VerificarLoginExisteAsync(UsuariosLoginDTO _usuarioLogin);
    }
}