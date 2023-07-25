using AutoMapper;
using PeliculasApi.DTOs;
using PeliculasApi.Entity;

namespace PeliculasApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {
            //GENERO
            CreateMap<Genero, GeneroDTO>().ReverseMap();

            CreateMap<GeneroCreacionDTO, Genero>();

            //ACTOR
            CreateMap<Actor, ActorDTO>().ReverseMap();

            CreateMap<ActorCreacionDTO, Actor>();   

        }
    }
}
