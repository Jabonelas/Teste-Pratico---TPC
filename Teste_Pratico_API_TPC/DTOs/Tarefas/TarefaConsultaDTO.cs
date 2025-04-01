using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Teste_Pratico_API_TPC.DTOs.Tarefas
{
    public class TarefaConsultaDTO
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public string descricao { get; set; }
        public string status { get; set; }
    }
}