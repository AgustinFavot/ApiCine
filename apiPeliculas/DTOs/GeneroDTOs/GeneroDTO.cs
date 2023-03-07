using System.ComponentModel.DataAnnotations;

namespace apiPeliculas.DTOs.GeneroDTOs
{
    public class GeneroDTO
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3), MaxLength(20)]
        public string Nombre { get; set; }
    }
}
