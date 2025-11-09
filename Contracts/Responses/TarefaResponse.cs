using GerenciadorDeTarefas.Domain.Enums;
using System;

namespace GerenciadorDeTarefas.Contracts.Responses
{
    public class TarefaResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = default!;
        public string? Description { get; init; }
        public Prioridade Priority { get; init; }
        public DateTime DueDate { get; init; }
        public StatusTarefa Status { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }
    }
}
