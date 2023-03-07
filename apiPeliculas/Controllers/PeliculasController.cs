using apiPeliculas.DTOs;
using apiPeliculas.Models;
using apiPeliculas.DTOs.PeliculaDTOs;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.JsonPatch;

namespace apiPeliculas.Controllers
{
    [ApiController]
    [Route("api/peliculas")]
    public class PeliculasController : CustomBaseController
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PeliculasController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<List<PeliculaDTO>>> getAllPeliculas([FromQuery] FiltroPeliculaDTO filtroPeliculaDTO)
        {
            //ejecucion diferida
            var queryable = context.Peliculas.Include(x => x.PeliculasGeneros).ThenInclude(x => x.Genero).Include(x => x.PeliculasActores).ThenInclude(x => x.Actor).AsQueryable();

            if (!string.IsNullOrEmpty(filtroPeliculaDTO.Titulo))
            {
                queryable = queryable.Where(x => x.Titulo.Contains(filtroPeliculaDTO.Titulo));
            }

            if (filtroPeliculaDTO.EnCine)
            {
                queryable = queryable.Where(x => x.EnCines);
            }

            if (filtroPeliculaDTO.GeneroId != 0)
            {
                queryable = queryable.Where(x => x.PeliculasGeneros.Select(x => x.GeneroId).Contains(filtroPeliculaDTO.GeneroId));
            }

            if (!string.IsNullOrEmpty(filtroPeliculaDTO.CampoOrdenar))
            {
                //System.Linq.Dynamic.Core
                var orden = filtroPeliculaDTO.Ascendente ? "ascending" : "descending";

                queryable = queryable.OrderBy($"{filtroPeliculaDTO.CampoOrdenar} {orden}");
            }

            return await Get<Pelicula, PeliculaDTO>(filtroPeliculaDTO, queryable);
        }


        [HttpGet("{id:int}", Name = "obtenerPelicula")]
        public async Task<ActionResult<PeliculaDTO>> GetPeliculaById(int id)
        {
            return await Get<Pelicula, PeliculaDTO>(id);
        }


        [HttpPost]
        public async Task<ActionResult<PeliculaDTO>> CreatePelicula([FromBody] PeliculaCreationDTO peliculaCreationDTO)
        {

            var exist = await context.Peliculas.AnyAsync(x => x.Titulo == peliculaCreationDTO.Titulo);

            if (exist) return BadRequest("La Pelicula ya existe");          

            var actores = await context.Actores.Where(x => peliculaCreationDTO.ActoresId.Contains(x.Id)).Select(x => x.Id).ToListAsync();

            if (actores.Count != peliculaCreationDTO.ActoresId.Count)
            {
                return BadRequest("Generos no existentes");
            }

            if (peliculaCreationDTO.FechaEstreno < DateTime.Now) return BadRequest("La fecha de estreno no puede pertenecer al pasado");

            return await Post<PeliculaCreationDTO, Pelicula, PeliculaDTO>(peliculaCreationDTO, "obtenerPelicula");
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult<PeliculaDTO>> UpdatePelicula(int id, [FromBody] PeliculaCreationDTO peliculaCreationDTO)
        {
            var pelicula = await context.Peliculas.FirstOrDefaultAsync(x => x.Id == id);
            
            if (pelicula == null) return BadRequest("La peliculaActoresCreation que intenta modificar no existe");

            return await Put<PeliculaCreationDTO, Pelicula>(id, peliculaCreationDTO);
        }


        [HttpPatch("{id:int}")]
        public async Task<ActionResult> PatchPelicula(int id, [FromBody] JsonPatchDocument<PeliculaCreationDTO> jsonPatchDocument)
        {
            var pelicula = await context.Peliculas.FirstOrDefaultAsync(x => x.Id == id);
            
            if (pelicula == null) return BadRequest("La peliculaActoresCreation no se puede modificar porque no existe");

            return await Patch<Pelicula, PeliculaCreationDTO>(id, jsonPatchDocument);
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeletePelicula(int id)
        {
            return await Delete<Pelicula>(id);
        }
    }
}
