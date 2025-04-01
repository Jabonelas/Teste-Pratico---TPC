using System.ComponentModel.DataAnnotations;

namespace Teste_Pratico_API_TPC.DTOs.Usuarios
{
    public class UsuariosLoginDTO
    {
        [Required(ErrorMessage = "O email é obrigatório!")]
        public string email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatório!")]
        [MaxLength(50, ErrorMessage = "O senha deve ter no máximo 50 caracteres.")]
        public string senha { get; set; }
    }
}