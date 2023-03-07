using System.ComponentModel.DataAnnotations;

namespace apiPeliculas.DTOs.ActorDTOs
{
    public class ActorDTO
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3), MaxLength(30)]
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}
