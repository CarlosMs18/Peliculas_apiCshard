using PeliculasApi.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace PeliculasApi.DTOs
{
    public class ActorCreacionDTO : ActorPatchDTO
    {
       

        [PesoArchivoValidacion(PesoMaximoEnMegabBytes: 4)]
        [TipoArchivoValidacion(grupoTipoArchivo: GrupoTipoArchivo.Imagen)]
        public IFormFile Foto { get; set; }
    }
}
