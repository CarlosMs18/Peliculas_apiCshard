using System.ComponentModel.DataAnnotations;

namespace PeliculasApi.DTOs
{
    public class PeliculaPatchDTO 
    {

        [Required]
        [StringLength(300)]
        public string Titulo { get; set; }

        public bool enCines { get; set; }

        public DateTime FechaEstreno { get; set; }
    }
}
