using AutoMapper;
using apiPeliculas.DTOs;
using apiPeliculas.Models;
using Microsoft.AspNetCore.Mvc;
using apiPeliculas.DTOs.ActorDTOs;
using Microsoft.AspNetCore.JsonPatch;

namespace apiPeliculas.Controllers
{
    [ApiController]
    [Route("api/actores")]
    public class ActoresController : CustomBaseController
    {
        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;

        public ActoresController(ApplicationDbContext context, IMapper mapper) :base(context, mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ActorDTO>>> GetAllActores([FromQuery] PaginacionDTO paginacion)
        {
            var queryable = context.Actores.AsQueryable();
            return await Get<Actor, ActorDTO>(paginacion, queryable);
        }


        [HttpGet("{id:int}", Name = "obtenerActor")]
        public async Task<ActionResult<ActorDTO>> GetActorById(int id)
        {
            return await Get<Actor, ActorDTO>(id);    
        }


        [HttpPost]
        public async Task<ActionResult<ActorDTO>> CreateActor([FromForm] ActorCreationDTO actorCreationDTO)
        {
            return await Post<ActorCreationDTO, Actor, ActorDTO>(actorCreationDTO, "obtenerActor");
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateActor(int id, [FromBody] ActorCreationDTO actorCreationDTO)
        {
            return await Put<ActorCreationDTO, Actor>(id, actorCreationDTO);
        }


        [HttpPatch("{id:int}")]
        public async Task<ActionResult> PatchActor(int id, [FromBody] JsonPatchDocument<ActorCreationDTO> jsonPatch)
        {
            if (jsonPatch == null) return BadRequest();

            return await Patch<Actor, ActorCreationDTO>(id, jsonPatch);
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteActor(int id) 
        {
            return await Delete<Actor>(id);
        }
    }
}
