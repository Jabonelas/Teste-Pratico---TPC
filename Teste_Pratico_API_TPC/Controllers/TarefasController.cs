﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Teste_Pratico_API_TPC.DTOs.Tarefas;
using Teste_Pratico_API_TPC.DTOs.Usuarios;
using Teste_Pratico_API_TPC.Models;
using Teste_Pratico_API_TPC.Responses;
using Teste_Pratico_API_TPC.Services;

namespace Teste_Pratico_API_TPC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class TarefasController : Controller
    {
        private readonly TarefaService tarefaService;

        public TarefasController(TarefaService _tarefaService)
        {
            tarefaService = _tarefaService;
        }

        /// <summary>
        /// Cadastra uma nova tarefa para um usuário específico
        /// </summary>
        /// <remarks>
        /// Requer autenticação via JWT.
        ///
        /// Exemplo de requisição:
        ///
        ///     POST /usuarios/1/tarefas
        ///     {
        ///        "titulo": "Implementar API",
        ///        "descricao": "Desenvolver endpoints da aplicação",
        ///        "status": "pendente"
        ///     }
        ///
        /// Exemplo de resposta de sucesso:
        ///
        ///     "message": "Tarefa cadastrada com sucesso!"
        ///
        /// </remarks>
        /// <param name="_idUsuario">ID do usuário (número inteiro positivo) que receberá a tarefa</param>
        /// <param name="_dadosTarefaCadastro">DTO com os dados necessários para cadastro da tarefa</param>
        /// <returns>Mensagem de confirmação do cadastro</returns>
        /// <response code="201">Retorna mensagem de sucesso ao cadastrar a tarefa</response>
        /// <response code="400">Se os dados da tarefa forem inválidos</response>
        /// <response code="401">Acesso não autorizado (token inválido ou ausente)</response>
        /// <response code="404">Se o usuário não for encontrado</response>
        /// <response code="422">Status inválido. Informe um dos seguintes: 'pendente', 'em andamento' ou 'concluído'</response>
        /// <response code="500">Erro interno no servidor</response>
        [HttpPost("usuarios/{_idUsuario}/tarefas")]
        [ProducesResponseType(typeof(IEnumerable<TarefaCadastroDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse400), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CadastrarTarefa(int _idUsuario, TarefaCadastroDTO _dadosTarefaCadastro)
        {
            try
            {
                await tarefaService.CadastrarTarefaAsync(_idUsuario, _dadosTarefaCadastro);

                return Created("", new ErrorResponse { message = "Tarefa cadastrada com sucesso!" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ErrorResponse { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(422, new ErrorResponse { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse { message = "Erro interno ao cadastrar tarefa." });
            }
        }

        /// <summary>
        /// Lista todas as tarefas de um usuário específico
        /// </summary>
        /// <remarks>
        /// Requer autenticação via JWT.
        ///
        /// Exemplo de requisição:
        ///
        ///     GET /usuarios/1/tarefas
        ///
        /// Exemplo de resposta de sucesso:
        ///
        ///     [
        ///        {
        ///           "id": 1,
        ///           "titulo": "Implementar API",
        ///           "descricao": "Desenvolver endpoints da aplicação",
        ///           "status": "concluído"
        ///        },
        ///        {
        ///           "id": 2,
        ///           "titulo": "Criar documentação",
        ///           "descricao": "Documentar endpoints no Swagger",
        ///           "status": "pendente"
        ///        }
        ///     ]
        ///
        /// </remarks>
        /// <param name="_idUsuario">ID do usuário (número inteiro positivo)</param>
        /// <returns>Lista de tarefas do usuário no formato JSON</returns>
        /// <response code="200">Retorna a lista de tarefas do usuário</response>
        /// <response code="401">Acesso não autorizado (token inválido ou ausente)</response>
        /// <response code="404">Se o usuário não for encontrado</response>
        /// <response code="500">Erro interno no servidor</response>
        [HttpGet("usuarios/{_idUsuario}/tarefas")]
        [ProducesResponseType(typeof(IEnumerable<TarefaConsultaDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<TarefaConsultaDTO>>> ListaTarefas(int _idUsuario)
        {
            try
            {
                var listaTarefas = await tarefaService.ListaTarefasIdAsync(_idUsuario);

                return Ok(listaTarefas);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ErrorResponse { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse { message = "Erro interno ao buscar lista tarefas." });
            }
        }
    }
}