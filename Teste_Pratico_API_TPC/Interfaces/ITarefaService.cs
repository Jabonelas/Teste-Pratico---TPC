using Teste_Pratico_API_TPC.DTOs.Tarefas;
using Teste_Pratico_API_TPC.Models;

namespace Teste_Pratico_API_TPC.Interfaces
{
    public interface ITarefaService
    {
        Task CadastrarTarefaAsync(int _idUsuario, TarefaCadastroDTO _tarefa);

        Task<bool> UsuarioExiste(int _idUsuario);

        Task<IEnumerable<TarefaConsultaDTO>> ListaTarefasIdAsync(int _idUsuario);
    }
}