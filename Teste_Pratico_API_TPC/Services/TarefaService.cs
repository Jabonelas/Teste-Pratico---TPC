using Teste_Pratico_API_TPC.DTOs.Tarefas;
using Teste_Pratico_API_TPC.Interfaces;
using Teste_Pratico_API_TPC.Models;

namespace Teste_Pratico_API_TPC.Services
{
    public class TarefaService : ITarefaService
    {
        private readonly ITarefaRepository iTarefaRepository;

        public TarefaService(ITarefaRepository _iTarefaRepository)
        {
            iTarefaRepository = _iTarefaRepository;
        }

        public async Task CadastrarTarefaAsync(int _idUsuario, TarefaCadastroDTO _dadosTarefaCadastro)
        {
            if (!await UsuarioExiste(_idUsuario))
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }

            if (!StatusValido(_dadosTarefaCadastro.status))
            {
                throw new InvalidOperationException("Status inválido. Por favor, informe um dos seguintes: 'pendente', 'em andamento' ou 'concluído'.");
            }

            var tarefa = new TbTarefa()
            {
                TaTitulo = _dadosTarefaCadastro.titulo,
                TaDescricao = _dadosTarefaCadastro.descricao,
                TaStatus = _dadosTarefaCadastro.status.ToLower(),
                FkUsuario = _idUsuario,
            };

            await iTarefaRepository.AdicionarAsync(tarefa);
        }

        public async Task<bool> UsuarioExiste(int _idUsuario)
        {
            bool usuarioExiste = await iTarefaRepository.VerificarUsuarioExisteAsync(_idUsuario);

            return usuarioExiste;
        }

        public bool StatusValido(string _status)
        {
            switch (_status.ToLower())
            {
                case "pendente":
                case "em andamento":
                case "concluído":

                    return true;

                default:
                    return false;
            }
        }

        public async Task<IEnumerable<TarefaConsultaDTO>> ListaTarefasIdAsync(int _idUsuario)
        {
            var listaTarefasCompleta = await iTarefaRepository.ObterTarefasIdAsync(_idUsuario);

            if (listaTarefasCompleta.Count() == 0)
            {
                throw new KeyNotFoundException($"Não foi encontrado tarefas cadastradas para esse usuário.");
            }

            var listaTarefas = listaTarefasCompleta.Select(x => new TarefaConsultaDTO()
            {
                id = x.IdTarefa,
                titulo = x.TaTitulo,
                descricao = x.TaDescricao,
                status = x.TaStatus,
            });

            return listaTarefas;
        }
    }
}