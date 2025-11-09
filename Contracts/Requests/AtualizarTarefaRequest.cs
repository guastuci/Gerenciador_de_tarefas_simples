using System;
using System.ComponentModel.DataAnnotations;
using GerenciadorDeTarefas.Domain.Enums;
using GerenciadorDeTarefas.Validation;

namespace GerenciadorDeTarefas.Contracts.Requests
{
    public class AtualizarTarefaRequest
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = default!;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public Prioridade Priority { get; set; }

        [Required, DataFutura(ErrorMessage = "dueDate deve ser no futuro.")]
        public DateTime DueDate { get; set; }

        [Required]
        public StatusTarefa Status { get; set; }
    }
}
