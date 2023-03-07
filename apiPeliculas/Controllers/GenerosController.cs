using apiPeliculas.DTOs;
using apiPeliculas.DTOs.GeneroDTOs;
using apiPeliculas.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apiPeliculas.Controllers
{
    [ApiController]
    [Route("api/generos")]
    public class GenerosController : CustomBaseController
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public GenerosController(ApplicationDbContext context, IMapper mapper) :base(context, mapper)
        {        
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GeneroDTO>>> GetAllGeneros([FromQuery] PaginacionDTO paginacion )
        {
            var queryable = context.Generos.AsQueryable();
            return await Get<Genero, GeneroDTO>(paginacion, queryable);
        }
        
        [HttpGet("{id:int}", Name ="obtenerGenero")]
        public async Task<ActionResult<GeneroDTO>> GetGeneroById(int id)
        {
            return await Get<Genero, GeneroDTO>(id);
        }

        [HttpPost]
        public async Task<ActionResult<GeneroDTO>> CreateGenero([FromBody] GeneroCreationDTO generoCreation) 
        {

            var genero = await context.Generos.Where(x => x.Nombre == generoCreation.Nombre).FirstOrDefaultAsync();

            if (genero != null)
            {
                return BadRequest($"El genero {genero.Nombre} ya existe");
            }

            return await Post<GeneroCreationDTO, Genero, GeneroDTO>(generoCreation, "obtenerGenero");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateGenero(int id, [FromBody] GeneroCreationDTO generoCreation) 
        {

            var exist = await context.Generos.AnyAsync(x => x.Id == id);

            if (!exist) return BadRequest($"El identificador {id} no coincide con ningun genero");

            return await Put<GeneroCreationDTO, Genero>(id, generoCreation);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteGenero(int id)
        {
            return await Delete<Genero>(id);
        }
    }
}
