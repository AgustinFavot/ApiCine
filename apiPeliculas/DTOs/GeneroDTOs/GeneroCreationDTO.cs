using System.ComponentModel.DataAnnotations;

namespace apiPeliculas.DTOs.GeneroDTOs
{
    public class GeneroCreationDTO
    {
        [Required]
        [MinLength(3), MaxLength(20)]
        public string Nombre { get; set; }
    }
}
