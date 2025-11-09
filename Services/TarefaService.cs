using GerenciadorDeTarefas.Contracts.Requests;
using GerenciadorDeTarefas.Contracts.Responses;
using GerenciadorDeTarefas.Domain.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace GerenciadorDeTarefas.Services
{
    public class TarefaService : ITarefaService
    {
        private readonly ConcurrentDictionary<Guid, Tarefa> _store = new();

        public TarefaResponse Create(CriarTarefaRequest request)
        {
            var entity = new Tarefa
            {
                Name = request.Name.Trim(),
                Description = string.IsNullOrWhiteSpace(request.Description) ? null : request.Description.Trim(),
                Priority = request.Priority,
                DueDate = request.DueDate,
                Status = request.Status
            };

            _store[entity.Id] = entity;
            return ToResponse(entity);
        }

        public IEnumerable<TarefaResponse> GetAll()
            => _store.Values.Select(ToResponse);

        public TarefaResponse? GetById(Guid id)
            => _store.TryGetValue(id, out var entity) ? ToResponse(entity) : null;

        public TarefaResponse? Update(Guid id, AtualizarTarefaRequest request)
        {
            if (!_store.TryGetValue(id, out var entity)) return null;

            entity.Name = request.Name.Trim();
            entity.Description = string.IsNullOrWhiteSpace(request.Description) ? null : request.Description.Trim();
            entity.Priority = request.Priority;
            entity.DueDate = request.DueDate;
            entity.Status = request.Status;
            entity.UpdatedAt = DateTime.UtcNow;

            return ToResponse(entity);
        }

        public bool Delete(Guid id) => _store.TryRemove(id, out _);

        private static TarefaResponse ToResponse(Tarefa e) => new()
        {
            Id = e.Id,
            Name = e.Name,
            Description = e.Description,
            Priority = e.Priority,
            DueDate = e.DueDate,
            Status = e.Status,
            CreatedAt = e.CreatedAt,
            UpdatedAt = e.UpdatedAt
        };
    }
}
