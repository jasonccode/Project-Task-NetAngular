
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_task_netangular.Models;



namespace project_task_netangular.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TareaController : ControllerBase
    {

        private readonly DbTareasContext _baseDatos;

        public TareaController(DbTareasContext baseDatos)
        {
            _baseDatos = baseDatos;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var listaTareas = await _baseDatos.Tareas.ToListAsync();
            return Ok(listaTareas);
        }

        [HttpPost]
        [Route("Agregar")]
        public async Task<IActionResult> Agregar([FromBody] Tarea request)
        {
            await _baseDatos.Tareas.AddAsync(request);
            await _baseDatos.SaveChangesAsync();
            return Ok(request);
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var tareaEliminar = await _baseDatos.Tareas.FindAsync(id);

            if (tareaEliminar == null)
                return BadRequest("No existe la tarea.");

            _baseDatos.Tareas.Remove(tareaEliminar);
            await _baseDatos.SaveChangesAsync();
            return Ok();
        }
    }
}