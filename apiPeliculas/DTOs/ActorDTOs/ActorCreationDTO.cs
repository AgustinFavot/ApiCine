using System.ComponentModel.DataAnnotations;

namespace apiPeliculas.DTOs.ActorDTOs
{
    public class ActorCreationDTO
    {
        [Required]
        [MinLength(3), MaxLength(30)]
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}
