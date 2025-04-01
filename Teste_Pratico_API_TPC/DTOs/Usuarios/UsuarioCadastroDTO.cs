using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Teste_Pratico_API_TPC.DTOs.Usuarios
{
    public class UsuarioCadastroDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório!")]
        [MaxLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string nome { get; set; }

        [Required(ErrorMessage = "O email é obrigatório!")]
        [MaxLength(100, ErrorMessage = "O email deve ter no máximo 100 caracteres.")]
        public string email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatório!")]
        [MaxLength(50, ErrorMessage = "O senha deve ter no máximo 50 caracteres.")]
        public string senha { get; set; }
    }
}