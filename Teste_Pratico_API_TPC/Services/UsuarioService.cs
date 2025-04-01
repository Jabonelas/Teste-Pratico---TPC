using System.ComponentModel.DataAnnotations;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using Teste_Pratico_API_TPC.DTOs;
using Teste_Pratico_API_TPC.DTOs.Usuarios;
using Teste_Pratico_API_TPC.Interfaces;
using Teste_Pratico_API_TPC.Models;

namespace Teste_Pratico_API_TPC.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository iUsuarioRepository;

        private readonly IAutenticacao iAutenticacao;

        public UsuarioService(IUsuarioRepository _iUsuarioRepository, IAutenticacao _iAutenticacao)
        {
            iUsuarioRepository = _iUsuarioRepository;
            iAutenticacao = _iAutenticacao;
        }

        public async Task CadastrarUsuarioAsync(UsuarioCadastroDTO _dadosUsuarioCadastro)
        {
            if (await EmailExiste(_dadosUsuarioCadastro.email))
            {
                throw new InvalidOperationException("E-mail já cadastrado.");
            }

            string senhaCriptografada = CriptografarSenha(_dadosUsuarioCadastro.senha);

            var usuario = new TbUsuario
            {
                UsEmail = _dadosUsuarioCadastro.email,
                UsNome = _dadosUsuarioCadastro.nome,
                UsSenha = senhaCriptografada
            };

            await iUsuarioRepository.AdicionarAsync(usuario);
        }

        public async Task<bool> EmailExiste(string _email)
        {
            bool emailExiste = await iUsuarioRepository.VerificarEmailExisteAsync(_email);

            return emailExiste;
        }

        public string CriptografarSenha(string _senhaDigitada)
        {
            string senhaCriptografada;

            byte[] senhaBytes = Encoding.UTF8.GetBytes(_senhaDigitada);

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] senhaHash = sha256.ComputeHash(senhaBytes);

                senhaCriptografada = Convert.ToBase64String(senhaHash);
            }

            return senhaCriptografada;
        }

        public async Task<UsuariosConsultaDTO> UsuarioIDAsync(int _idUsuario)
        {
            TbUsuario usuarioCompleto = await iUsuarioRepository.ObterUsuarioIDAsync(_idUsuario);

            if (usuarioCompleto == null)
            {
                throw new KeyNotFoundException($"Usuário com ID {_idUsuario} não encontrado.");
            }

            UsuariosConsultaDTO usuario = new UsuariosConsultaDTO()
            {
                id = usuarioCompleto.IdUsuario,
                nome = usuarioCompleto.UsNome,
                email = usuarioCompleto.UsEmail
            };

            return usuario;
        }

        public async Task<UserToken> UsuarioLoginAsync(UsuariosLoginDTO _usuarioLogin)
        {
            string senhaCriptografada = CriptografarSenha(_usuarioLogin.senha);

            _usuarioLogin.senha = senhaCriptografada;

            if (!await UsuarioLoginValidoAsync(_usuarioLogin))
            {
                throw new UnauthorizedAccessException("Falha na autenticação: credenciais inválidas.");
            }

            var token = iAutenticacao.GenerateToken(_usuarioLogin.email, _usuarioLogin.senha);

            return new UserToken
            {
                Token = token
            };
        }

        public async Task<bool> UsuarioLoginValidoAsync(UsuariosLoginDTO _usuarioLogin)
        {
            bool loginValido = await iUsuarioRepository.VerificarLoginExisteAsync(_usuarioLogin);

            return loginValido;
        }

        public async Task<IEnumerable<UsuariosConsultaDTO>> ListaUsuariosAsync()
        {
            var listaUsuariosCompleta = await iUsuarioRepository.ObterTodosAsync();

            if (listaUsuariosCompleta == null)
            {
                throw new KeyNotFoundException($"Não foi encontrado usuários cadastrados.");
            }

            var listaUsuarios = listaUsuariosCompleta.Select(x => new UsuariosConsultaDTO()
            {
                id = x.IdUsuario,
                nome = x.UsNome,
                email = x.UsEmail
            });

            return listaUsuarios;
        }
    }
}