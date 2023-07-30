using System.ComponentModel.DataAnnotations;

namespace PeliculasApi.DTOs
{
    public class SalaDeCineCreacionDTO
    {
        [Required]
        [StringLength(100)]
        public string  Nombre { get; set; }


    }
}
