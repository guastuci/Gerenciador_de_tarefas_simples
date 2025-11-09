using GerenciadorDeTarefas.Domain.Enums;
using System;

namespace GerenciadorDeTarefas.Domain.Entities
{
    public class Tarefa
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public Prioridade Priority { get; set; }
        public DateTime DueDate { get; set; }
        public StatusTarefa Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
