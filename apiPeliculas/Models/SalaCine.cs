using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;

namespace apiPeliculas.Models
{
    public class SalaCine : IId
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public Point Ubicacion { get; set; } //Tipo de dato de NetTopology. En sql server se mapeara a un tipo de dato geographi
        public List<PeliculasSalasCine> PeliculasSalasCines { get; set; }
    }
}
