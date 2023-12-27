
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
            try
            {
                Console.WriteLine("Entrando al método Agregar");
                await _baseDatos.Tareas.AddAsync(request);
                await _baseDatos.SaveChangesAsync();
                Console.WriteLine("Tarea agregada con éxito");
                return Ok(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar tarea: {ex.Message}");
                return StatusCode(500, "Error interno al procesar la solicitud.");
            }
        }


        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                Console.WriteLine($"Entrando al método Eliminar con id: {id}");
                var tareaEliminar = await _baseDatos.Tareas.FindAsync(id);

                if (tareaEliminar == null)
                {
                    Console.WriteLine("No existe la tarea.");
                    return BadRequest("No existe la tarea.");
                }

                _baseDatos.Tareas.Remove(tareaEliminar);
                await _baseDatos.SaveChangesAsync();
                Console.WriteLine("Tarea eliminada con éxito");
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar tarea: {ex.Message}");
                return StatusCode(500, "Error interno al procesar la solicitud.");
            }
        }
    }
}