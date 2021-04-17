using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using TarefasBackEnd.Model;
using TarefasBackEnd.Repositories;

namespace TarefasBackEnd.Controllers
{
    [Authorize]
    [ApiController]
    [Route("tarefa")]
    public class TarefaController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromServices] ITarefaRepository repository)
        {
            var id = new Guid(User.Identity.Name);
            var tarefas = repository.Read(id);
            return Ok(tarefas);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Tarefa model, [FromServices] ITarefaRepository repository)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            model.UsuarioId = new Guid(User.Identity.Name);

            repository.Create(model);

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, Tarefa model ,[FromServices] ITarefaRepository repository)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            repository.Update(new Guid(id), model);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id, [FromServices] ITarefaRepository repository)
        {
            repository.Delete(new Guid(id));

            return Ok();
        }
    }
}
