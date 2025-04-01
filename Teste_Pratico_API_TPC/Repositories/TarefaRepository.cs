using Microsoft.EntityFrameworkCore;
using Teste_Pratico_API_TPC.Data;
using Teste_Pratico_API_TPC.Interfaces;
using Teste_Pratico_API_TPC.Models;


namespace Teste_Pratico_API_TPC.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly BancoTPCContext context;

        public TarefaRepository(BancoTPCContext _context)
        {
            context = _context;
        }

        public async Task AdicionarAsync(TbTarefa _dadosTarefa)
        {
            context.TbTarefas.Add(_dadosTarefa);
            await context.SaveChangesAsync();
        }

        public async Task<bool> VerificarUsuarioExisteAsync(int _idUsuario)
        {
            bool usuarioExiste = context.TbUsuarios.Any(x => x.IdUsuario == _idUsuario);

            return usuarioExiste;
        }

        public async Task<IEnumerable<TbTarefa>> ObterTarefasIdAsync(int _idUsuario)
        {
            IEnumerable<TbTarefa> listaTarefas = await context.TbTarefas.Where(x => x.FkUsuario == _idUsuario).ToListAsync();

            return listaTarefas;
        }
    }
}