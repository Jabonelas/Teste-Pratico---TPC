using Microsoft.EntityFrameworkCore;
using Teste_Pratico_API_TPC.Data;
using Teste_Pratico_API_TPC.DTOs.Usuarios;
using Teste_Pratico_API_TPC.Interfaces;
using Teste_Pratico_API_TPC.Models;

namespace Teste_Pratico_API_TPC.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly BancoTPCContext context;

        public UsuarioRepository(BancoTPCContext _context)
        {
            context = _context;
        }

        public async Task AdicionarAsync(TbUsuario _dadosUsuario)
        {
            context.TbUsuarios.Add(_dadosUsuario);
            await context.SaveChangesAsync();
        }

        public async Task<bool> VerificarEmailExisteAsync(string _email)
        {
            bool emailExiste = await context.TbUsuarios.AnyAsync(x => x.UsEmail == _email);

            return emailExiste;
        }

        public async Task<bool> VerificarLoginExisteAsync(UsuariosLoginDTO _usuarioLogin)
        {
            bool loginExiste = await context.TbUsuarios
                .AnyAsync(x => x.UsEmail == _usuarioLogin.email && x.UsSenha == _usuarioLogin.senha);

            return loginExiste;
        }

        public async Task<TbUsuario> ObterUsuarioIDAsync(int _idUsuario)
        {
            TbUsuario usuarioCompleto = await context.TbUsuarios.FirstOrDefaultAsync(x => x.IdUsuario == _idUsuario);

            return usuarioCompleto;
        }

        public async Task<IEnumerable<TbUsuario>> ObterTodosAsync()
        {
            IEnumerable<TbUsuario> listaUsuarios = await context.TbUsuarios.ToListAsync();

            return listaUsuarios;
        }
    }
}