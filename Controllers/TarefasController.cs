using GerenciadorDeTarefas.Contracts.Requests;
using GerenciadorDeTarefas.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GerenciadorDeTarefas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarefasController : ControllerBase
    {
        private readonly ITarefaService _service;

        public TarefasController(ITarefaService service) => _service = service;

        [HttpPost]
        public IActionResult Create([FromBody] CriarTarefaRequest request)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var created = _service.Create(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }


        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAll());

        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var tarefa = _service.GetById(id);

            if (tarefa == null)
                return NotFound(); // 404

            return Ok(tarefa); // 200
        }


        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] AtualizarTarefaRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = _service.Update(id, request);

            if (updated == null)
                return NotFound(); // tarefa não existe

            return Ok(updated);
        }


        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var ok = _service.Delete(id);
            return ok ? NoContent() : NotFound();
        }
    }
}
