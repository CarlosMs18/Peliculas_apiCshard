using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasApi.DTOs;
using PeliculasApi.Entity;
using PeliculasApi.Helpers;

namespace PeliculasApi.Controllers
{
    public class CustomBaseController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CustomBaseController(
            ApplicationDbContext context,
            IMapper mapper
            )
        {
            this.context = context;
            this.mapper = mapper;
        }

        protected async Task<List<TDTO>> Get<TEntidad, TDTO>() where TEntidad : class //donde Tentiendad es una clase
        {
            var entidades = await context.Set<TEntidad>().AsNoTracking().ToListAsync(); //LE DECIMOS A ENTITY FRAMEWORK QUE QUEREMOS TRABHAJAR CON
                                                                         //CUALQUIER ENTIDAD QUE NOS ENVIE Y TOLISTANSYC PARA RECUPERAR EL LISTADO DE ENTIDADES
            var dtos = mapper.Map<List<TDTO>>(entidades);
            return dtos;


        }

        protected async Task<List<TDTO>> Get<TEntidad, TDTO>(PaginacionDTO paginacionDTO) where TEntidad : class
        {
            var queryable = context.Set<TEntidad>().AsQueryable();//metodo queryable que le pasaremos al metodo de extension
            await HttpContext.InsertarParametrosPaginacion(queryable, paginacionDTO.CantidadRegistrosPorPagina);

            var entidades = await queryable.Paginar(paginacionDTO).ToListAsync();
            return mapper.Map<List<TDTO>>(entidades);
        }

        protected async Task<ActionResult<TDTO>> Get<TEntidad, TDTO>(int id) where TEntidad : class, IId //signicia que laentidad TEntidad tiene que implemntar la interfaz IID
        {
            var entidad = await context.Set<TEntidad>()
                .AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if(entidad == null)
            {
                return NotFound();
            }

              return mapper.Map<TDTO>(entidad);
        }

        protected async Task<ActionResult> Post<TCreacion, TEntidad, TLectura>
                        (TCreacion creacionDTO, string nombreRuta) where TEntidad: class, IId
        {
            var entidad = mapper.Map<TEntidad>(creacionDTO);
            context.Add(entidad);
            await context.SaveChangesAsync();
            var dtoLectura = mapper.Map<TLectura>(entidad);

            return new CreatedAtRouteResult(nombreRuta, new { id = entidad.Id }, dtoLectura);
        }

        protected async Task<ActionResult> Put<TCreacion, TEntidad>
            (int id, TCreacion creacionDTO) where TEntidad : class, IId
        {
            var entidad = mapper.Map<TEntidad>(creacionDTO);
            entidad.Id = id;
            context.Entry(entidad).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        protected async Task<ActionResult> Patch<TEntidad, TDTO>
            (int id, JsonPatchDocument<TDTO> patchDocument)
            where TDTO : class
            where TEntidad : class, IId
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var entidadDB = await context.Set<TEntidad>().FirstOrDefaultAsync(x => x.Id == id);
            if (entidadDB == null)
            {
                return NotFound();
            }

            var entidadDTO = mapper.Map<TDTO>(entidadDB);

            patchDocument.ApplyTo(entidadDTO, ModelState);

            var esValido = TryValidateModel(entidadDTO); //si es valido, es decir si se respetaron las reglas de validacion
            if (!esValido)
            {
                return BadRequest(ModelState);
            }

            mapper.Map(entidadDTO, entidadDB);

            await context.SaveChangesAsync();
            return NoContent();
        }


        protected async Task<ActionResult> Delete<TEntidad>(int id) where TEntidad : class, IId , new() //que tiene un contructor vaci
                                                                                                        // o al mandar una entidad en el delete lo enviamos sin parametros por eso se debe de colocar asi
        {//por eso tenemos que decir que nuestro generico va a tener un constructor vacio para poder hacer esto

            var existe = await context.Set<TEntidad>().AnyAsync(x => x.Id == id);
            if (!existe) return NotFound();

            context.Remove(new TEntidad() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
