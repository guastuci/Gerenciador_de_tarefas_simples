using GerenciadorDeTarefas.Contracts.Requests;
using GerenciadorDeTarefas.Contracts.Responses;
using System;
using System.Collections.Generic;

namespace GerenciadorDeTarefas.Services
{
    public interface ITarefaService
    {
        TarefaResponse Create(CriarTarefaRequest request);
        IEnumerable<TarefaResponse> GetAll();
        TarefaResponse? GetById(Guid id);
        TarefaResponse? Update(Guid id, AtualizarTarefaRequest request);
        bool Delete(Guid id);
    }
}
