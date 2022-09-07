using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApiAutores.Models;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger<AutoresController> logger;

        public AutoresController(ApplicationDbContext context,ILogger<AutoresController> logger)
        {
            this.context = context;
            this.logger = logger;
        }


        [HttpGet("{id:int}")]
        //[HttpGet("{id:int}/{parametro2}")] //opcional el segundo parametro 
        //[HttpGet("{id:int}/{parametro2=angel}")]//Valor por default el segundo parametro
        public async Task<ActionResult<Autor>> Get(int id)
        {
            logger.LogInformation("Listadon un autor");
            //CRITICAL
            //ERROR
            // WARNING
            //INFORMATION
            //DEBUG
            //TRACER

            var autor =  await context.Autores.Include(x => x.Libros).FirstOrDefaultAsync(x => x.Id == id);
            if (autor == null)
            {
                return NotFound();
            }
            return autor;
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<Autor>> Get(string nombre)
        {
            var autor = await context.Autores.Include(x => x.Libros).FirstOrDefaultAsync(x => x.Nombre.Contains(nombre));
            if (autor == null)
            {
                return NotFound();
            }
            return autor;
        }

        [HttpGet]
        [HttpGet("listar")] //api/autores/listar
        [HttpGet("/listar")] //listar

        public async Task<ActionResult<List<Autor>>> Get()
        {
            return await context.Autores.Include(x => x.Libros).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Autor autor)
        {
            var existe = await context.Autores.AnyAsync(x => x.Nombre == autor.Nombre);

            if (existe)
            {
                return NotFound();
            }

            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")] // api/autores/1 
        public async Task<ActionResult> Put(Autor autor, int id)
        {
            if (autor.Id != id)
            {
                return BadRequest("El id del autor no coincide con el id de la URL");
            }

            var existe = await context.Autores.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return BadRequest("El nombre del autor ya existe");
            }

            context.Update(autor);
            await context.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete("{id:int}")] // api/autores/2
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Autores.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new Autor() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
        
        //Get([FromBody] int id) parametros de body
        //Get([FromForm] int id) parametros de formulario
        //Get([FromHeader] int id) parametros de cabecera head
        //Get([FromQuery] int id) parametros de query ejem: lista?id=1&nombre=2
        //Get([FromRoute] int id) parametros de url
        //Get([FromServices] int id) parametros de servicio

    }
}
