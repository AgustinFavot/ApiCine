using System.ComponentModel.DataAnnotations;

namespace apiPeliculas.DTOs.PeliculaDTOs
{
    public class PeliculaCreationDTO
    {
        [Required]
        public string Titulo { get; set; }
        public bool EnCine { get; set; }
        public DateTime FechaEstreno { get; set; }
        public List<int> ActoresId { get; set; }
        public List<int> GenerosId { get; set; }
    }
}
