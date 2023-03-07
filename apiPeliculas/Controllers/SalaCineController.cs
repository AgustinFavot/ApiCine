using AutoMapper;
using apiPeliculas.DTOs;
using apiPeliculas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apiPeliculas.DTOs.SalaCineDTOs;
using Microsoft.AspNetCore.JsonPatch;
using NetTopologySuite.Geometries;

namespace apiPeliculas.Controllers
{
    [ApiController]
    [Route("api/cines")]
    public class SalaCineController : CustomBaseController
    {
        private readonly IMapper mapper;
        private readonly GeometryFactory geometry;
        private readonly ApplicationDbContext context;

        public SalaCineController(ApplicationDbContext context, IMapper mapper, GeometryFactory geometry) :base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this.geometry = geometry;
        }

        [HttpGet]
        public async Task<ActionResult<List<SalaCineDTO>>> GetAllSalaCines([FromQuery] PaginacionDTO paginacionDTO) 
        {
            var queryable = context.SalaCines.Include(x => x.PeliculasSalasCines).ThenInclude(x => x.Pelicula).AsQueryable();
            return await Get<SalaCine, SalaCineDTO>(paginacionDTO, queryable); 
        }


        [HttpGet("{id:int}", Name = "obtenerSalaCine")]
        public async Task<ActionResult<SalaCineDTO>> GetSalaCineById(int id)
        {
            return await Get<SalaCine, SalaCineDTO>(id);
        }

        //VER TEMAS DE CLASES COMO FACTORIZARLAS USAR HERENCIA
        [HttpPost]
        public async Task<ActionResult<SalaCineDTO>> CreateSalaCine([FromBody] SalaCineCreationDTO salaCineCreationDTO)
        {
            return await Post<SalaCineCreationDTO, SalaCine, SalaCineDTO>(salaCineCreationDTO, "obtenerSalaCine");
        }

        [HttpPut]
        public async Task<ActionResult<SalaCineDTO>> UpdateSalaCine(int id, [FromBody] SalaCineCreationDTO salaCineCreationDTO)
        {
            return await Put<SalaCineCreationDTO, SalaCine>(id, salaCineCreationDTO);                
        }

        [HttpPatch]
        public async Task<ActionResult<SalaCineDTO>> PatchSalaCine(int id, [FromBody] JsonPatchDocument<SalaCineCreationDTO> jsonPatch)
        {
            return await Patch<SalaCine, SalaCineCreationDTO>(id, jsonPatch);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteSalaCine(int id)
        {
            return await Delete<SalaCine>(id);
        }

        [HttpGet("Cercanos")]
        public async Task<ActionResult<List<SalaCineCercanoDTO>>> GetCinesCercanos([FromQuery] SalaCineCercanoFiltroDTO filtroDTO)
        {
            var ubicacion = geometry.CreatePoint(new Coordinate(filtroDTO.Longitud, filtroDTO.Latitud));

            var salasCines = await context.SalaCines.OrderBy(x => x.Ubicacion.Distance(ubicacion)).
                Where(x => x.Ubicacion.IsWithinDistance(ubicacion, filtroDTO.DistanciaEnKms * 1000)).
                Select(x => new SalaCineCercanoDTO()
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Latitud = x.Ubicacion.Y,
                    Longitud = x.Ubicacion.X,
                    DistanciaEnMtr = Math.Round(x.Ubicacion.Distance(ubicacion))
                }).ToListAsync();

            return salasCines;
        }

    }
}
