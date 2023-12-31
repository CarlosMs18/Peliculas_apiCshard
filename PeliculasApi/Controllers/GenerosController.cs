﻿using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasApi.DTOs;
using PeliculasApi.Entity;

namespace PeliculasApi.Controllers
{
    [ApiController]
    [Route("api/generos")]
    public class GenerosController : /*ControllerBase*/ CustomBaseController
    {
        //private readonly ApplicationDbContext context;
        //private readonly IMapper mapper;

        public GenerosController(
            ApplicationDbContext context,
            IMapper mapper
            ) : base(context , mapper)
        {
            //this.context = context;
            //this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GeneroDTO>>> Get()
        {
            return await Get<Genero, GeneroDTO>();
           //var entidades = await context.Generos.ToListAsync();
           // var dtos = mapper.Map<List<GeneroDTO>>(entidades);
           // return dtos;
        }

        [HttpGet("{id:int}",Name = "obtenerGenero")]
        public async Task<ActionResult<GeneroDTO>> Get(int id)
        {
            //var entidad = await context.Generos.FirstOrDefaultAsync(x => x.Id == id);
            //if(entidad == null)
            //{
            //    return NotFound();

            //}
            //var dto = mapper.Map<GeneroDTO>(entidad);
            //return dto;
            return await Get<Genero, GeneroDTO>(id);

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GeneroCreacionDTO generoCreacionDTO)
        {
            //var entidad = mapper.Map<Genero>(generoCreacionDTO);
            //context.Add(entidad);   
            //await context.SaveChangesAsync();
            //var generoDTO = mapper.Map<GeneroDTO>(entidad);

            //return new CreatedAtRouteResult("obtenerGenero", new {id = generoDTO.Id},generoDTO);
            return await Post<GeneroCreacionDTO, Genero, GeneroDTO>(generoCreacionDTO, "obtenerGenero");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] GeneroCreacionDTO generoCreacionDTO)
        {
            //var entidad = mapper.Map<Genero>(generoCreacionDTO);
            //entidad.Id = id;
            //context.Entry(entidad).State = EntityState.Modified;    
            //await context.SaveChangesAsync();
            //return NoContent();
            return await Put<GeneroCreacionDTO, Genero>(id, generoCreacionDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) 
        {
            //var existe = await context.Generos.AnyAsync(x => x.Id == id);
            //if(!existe) return NotFound();

            //context.Remove(new Genero() { Id = id });
            //await context.SaveChangesAsync();
            //return NoContent();

            return await Delete<Genero>(id);    
        }
    }
}
