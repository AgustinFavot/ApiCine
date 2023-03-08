using AutoMapper;
using apiPeliculas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using apiPeliculas.DTOs;
using apiPeliculas.Utils;

namespace apiPeliculas.Controllers
{
    public class CustomBaseController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CustomBaseController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

                                            //Mostrar    //Contexto  //Mostrar                           //IQueryable
        protected async Task<ActionResult<List<TDTO>>> Get<TEntidad, TDTO>(PaginacionDTO paginacionDTO, IQueryable<TEntidad> queryable) where TEntidad : class
        {
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable, paginacionDTO.RegistrosPorPagina);
            var entities = await queryable.Paginar(paginacionDTO).ToListAsync();
            var list = mapper.Map<List<TDTO>>(entities);
            return list;
        }


        protected async Task<ActionResult<TDTO>> Get<TEntidad, TDTO>(int id) where TEntidad : class, IId
        {
            var entidad = await context.Set<TEntidad>().FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<TDTO>(entidad);
        }


                                        // Lo que recibo  contexto  a mostrar  parametro
        protected async Task<ActionResult> Post<TCreation, TEntidad, TLectura>(TCreation creationDTO, string nombreRuta) where TEntidad : class, IId
        {
            var entidad = mapper.Map<TEntidad>(creationDTO);

            context.Add(entidad);

            await context.SaveChangesAsync();

            var dtoLectura = mapper.Map<TLectura>(entidad);

            return new CreatedAtRouteResult(nombreRuta, new { id = entidad.Id }, dtoLectura);
        }


        protected async Task<ActionResult> Put<TCreation, TEntidad>(int id, TCreation creationDTO) where TEntidad : class, IId
        {
            var entidad = mapper.Map<TEntidad>(creationDTO);
            entidad.Id = id;

            await context.SaveChangesAsync();

            return NoContent();
        }


        protected async Task<ActionResult> Patch<TEntidad, TDTO>(int id, JsonPatchDocument<TDTO> jsonPatch) where TEntidad : class, IId where TDTO : class
        {
            var entity = await context.Set<TEntidad>().FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null) return NotFound();

            var entityDTO = mapper.Map<TDTO>(entity);

            jsonPatch.ApplyTo(entityDTO, ModelState);

            var IsValid = TryValidateModel(entityDTO);

            if (!IsValid) return BadRequest(ModelState);

            mapper.Map(entityDTO, entity);

            await context.SaveChangesAsync();

            return NoContent();
        }

        protected async Task<ActionResult> Delete<TEntidad>(int id) where TEntidad : class, IId, new()
        {
            var entity = await context.Set<TEntidad>().FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null) 
            {
                return BadRequest("El elemento a eliminar no existe");
            }

            context.Entry(entity: entity).State = EntityState.Deleted; 
         
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
