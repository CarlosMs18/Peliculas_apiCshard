using PeliculasApi.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace PeliculasApi.Entity
{
    public class Actor
    {
        public int Id { get; set; }
        [Required]
        [StringLength(120)]
        public string  Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        [PesoArchivoValidacion(PesoMaximoEnMegabBytes: 4)]
        [TipoArchivoValidacion(grupoTipoArchivo: GrupoTipoArchivo.Imagen)]
        public string Foto { get; set; }
    }
}
