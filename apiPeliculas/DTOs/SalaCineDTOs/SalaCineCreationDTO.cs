using System.ComponentModel.DataAnnotations;

namespace apiPeliculas.DTOs.SalaCineDTOs
{
    public class SalaCineCreationDTO
    {
        [Required]
        public string Nombre { get; set; }
        [Range(-90,90)]
        public double Latitud { get; set; }
        [Range(-180, 180)]
        public double Longitud { get; set; }
        public List<int> PeliculasId { get; set; }
    }
}
