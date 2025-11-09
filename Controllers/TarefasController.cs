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
            try
            {
                var created = _service.Create(request);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
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
            try
            {
                var updated = _service.Update(id, request);
                if (updated == null)
                    return NotFound();

                return Ok(updated);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var ok = _service.Delete(id);
            return ok ? NoContent() : NotFound();
        }
    }
}
