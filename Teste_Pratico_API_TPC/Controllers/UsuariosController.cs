using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;
using Teste_Pratico_API_TPC.DTOs;
using Teste_Pratico_API_TPC.DTOs.Usuarios;
using Teste_Pratico_API_TPC.Responses;
using Teste_Pratico_API_TPC.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Teste_Pratico_API_TPC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService usuarioService;

        public UsuariosController(UsuarioService _usuarioService)
        {
            usuarioService = _usuarioService;
        }

        /// <summary>
        /// Cadastra um novo usuário no sistema
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /usuarios
        ///     {
        ///        "nome": "João Silva",
        ///        "email": "joao@exemplo.com",
        ///        "senha": "SuaSenhaForte@123",
        ///     }
        ///
        /// Exemplo de resposta de sucesso:
        ///
        ///     {
        ///        "message": "Usuário cadastrado com sucesso!"
        ///     }
        ///
        /// </remarks>
        /// <param name="_dadosUsuarioCadastro">DTO com os dados necessários para cadastro</param>
        /// <returns>Mensagem de sucesso ou erro detalhado</returns>
        /// <response code="201">Usuário cadastrado com sucesso</response>
        /// <response code="400">Se os dados forem inválidos</response>
        /// <response code="409">Se houver conflito na operação (ex: Email já cadastrado)</response>
        /// <response code="500">Erro interno no servidor</response>
        [HttpPost(Name = "usuarios")]
        [ProducesResponseType(typeof(UsuarioCadastroDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse400), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CadastrarUsuario(UsuarioCadastroDTO _dadosUsuarioCadastro)
        {
            try
            {
                await usuarioService.CadastrarUsuarioAsync(_dadosUsuarioCadastro);

                return Created("", new ErrorResponse { message = "Usuário cadastrado com sucesso!" });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new ErrorResponse { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse { message = "Erro interno ao cadastrar usuário." });
            }
        }

        /// <summary>
        /// Obtém um usuário específico pelo ID
        /// </summary>
        /// <remarks>
        /// Requer autenticação via JWT.
        ///
        /// Exemplo de requisição:
        ///
        ///     GET /usuarios/1
        ///
        /// Exemplo de resposta de sucesso:
        ///
        ///     {
        ///        "id": 1,
        ///        "nome": "João Silva",
        ///        "email": "joao@exemplo.com",
        ///     }
        ///
        /// </remarks>
        /// <param name="_idUsuario">ID do usuário a ser consultado (número inteiro)</param>
        /// <returns>Dados completos do usuário no formato JSON</returns>
        /// <response code="200">Retorna os dados do usuário solicitado</response>
        /// <response code="401">Acesso não autorizado (token inválido ou ausente)</response>
        /// <response code="404">Se nenhum usuário for encontrado com o ID especificado</response>
        /// <response code="500">Erro interno no servidor</response>
        [HttpGet("{_idUsuario}")]
        [Authorize]
        [ProducesResponseType(typeof(UsuariosConsultaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UsuariosConsultaDTO>> UsuarioID(int _idUsuario)
        {
            try
            {
                UsuariosConsultaDTO usuarioCadastro = await usuarioService.UsuarioIDAsync(_idUsuario);

                return Ok(usuarioCadastro);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ErrorResponse { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse { message = "Erro interno ao buscar usuário por id." });
            }
        }

        /// <summary>
        /// Retorna a lista completa de usuários cadastrados
        /// </summary>
        /// <remarks>
        /// Requer autenticação via JWT.
        ///
        /// Exemplo de resposta de sucesso:
        ///
        ///     GET /usuarios
        ///     [
        ///        {
        ///           "id": 1,
        ///           "nome": "João Silva",
        ///           "email": "joao@exemplo.com"
        ///        },
        ///        {
        ///           "id": 2,
        ///           "nome": "Maria Souza",
        ///           "email": "maria@exemplo.com"
        ///        }
        ///     ]
        ///
        /// </remarks>
        /// <returns>Lista de usuários no formato JSON</returns>
        /// <response code="200">Retorna a lista de usuários cadastrados</response>
        /// <response code="401">Acesso não autorizado (token inválido ou ausente)</response>
        /// <response code="404">Se nenhum usuário for encontrado</response>
        /// <response code="500">Erro interno no servidor</response>
        [HttpGet(Name = "usuarios")]
        [Authorize]
        [ProducesResponseType(typeof(UsuariosConsultaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<UsuariosConsultaDTO>>> ListaUsuarios()
        {
            try
            {
                var listaUsuarios = await usuarioService.ListaUsuariosAsync();

                return Ok(listaUsuarios);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ErrorResponse { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse { message = "Erro interno ao buscar lista usuários." });
            }
        }

        /// <summary>
        /// Autentica um usuário e retorna um token JWT
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /login
        ///     {
        ///        "email": "usuario@exemplo.com",
        ///        "senha": "SuaSenhaSegura123"
        ///     }
        ///
        /// Exemplo de resposta de sucesso:
        ///
        ///     {
        ///        "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
        ///     }
        ///
        /// </remarks>
        /// <param name="_usuarioLogin">DTO contendo email e senha do usuário</param>
        /// <returns>Token JWT para autenticação</returns>
        /// <response code="200">Retorna o token JWT gerado</response>
        /// <response code="400">Se as credenciais forem inválidas ou estiverem em formato incorreto</response>
        /// <response code="401">Se a autenticação falhar (usuário/senha incorretos)</response>
        /// <response code="500">Erro interno no servidor</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(UsuariosConsultaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse400), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserToken>> UsuarioLogin(UsuariosLoginDTO _usuarioLogin)
        {
            try
            {
                UserToken token = await usuarioService.UsuarioLoginAsync(_usuarioLogin);

                return token;
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new ErrorResponse { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse { message = "Erro interno ao realizar login." });
            }
        }
    }
}