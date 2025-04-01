using Teste_Pratico_API_TPC.DTOs.Tarefas;
using Teste_Pratico_API_TPC.Models;

namespace Teste_Pratico_API_TPC.Interfaces
{
    public interface ITarefaRepository
    {
        Task AdicionarAsync(TbTarefa _tarefa);

        Task<bool> VerificarUsuarioExisteAsync(int _idUsuario);

        Task<IEnumerable<TbTarefa>> ObterTarefasIdAsync(int _idUsuario);
    }
}