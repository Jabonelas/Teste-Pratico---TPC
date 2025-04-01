using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Teste_Pratico_API_TPC.DTOs.Tarefas
{
    public class TarefaCadastroDTO
    {
        [Required(ErrorMessage = "O título é obrigatório!")]
        [MaxLength(50, ErrorMessage = "O título deve ter no máximo 50 caracteres.")]
        public string titulo { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatório!")]
        [MaxLength(200, ErrorMessage = "A descrição deve ter no máximo 200 caracteres.")]
        public string descricao { get; set; }

        [Required(ErrorMessage = "O status é obrigatório!")]
        [MaxLength(50, ErrorMessage = "O status deve ter no máximo 50 caracteres.")]
        public string status { get; set; }
    }
}