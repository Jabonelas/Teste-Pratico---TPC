using Teste_Pratico_API_TPC.DTOs;
using Teste_Pratico_API_TPC.DTOs.Usuarios;
using Teste_Pratico_API_TPC.Models;

namespace Teste_Pratico_API_TPC.Interfaces
{
    public interface IUsuarioService
    {
        Task CadastrarUsuarioAsync(UsuarioCadastroDTO _dadosUsuarioCadastro);

        Task<UsuariosConsultaDTO> UsuarioIDAsync(int _idUsuario);

        Task<IEnumerable<UsuariosConsultaDTO>> ListaUsuariosAsync();

        Task<UserToken> UsuarioLoginAsync(UsuariosLoginDTO _usuarioLogin);
    }
}