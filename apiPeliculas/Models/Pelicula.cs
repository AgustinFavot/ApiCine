using System.ComponentModel.DataAnnotations;

namespace apiPeliculas.Models
{
    public class Pelicula: IId
    {
        public int Id { get; set; }
        [Required]
        [StringLength(300)]
        public string Titulo { get; set; }
        public bool EnCines { get; set; }
        public DateTime FechaEstreno { get; set; }
        public List<PeliculasActores> PeliculasActores { get; set; }
        public List<PeliculasGeneros> PeliculasGeneros { get; set; }
        public List<PeliculasSalasCine> PeliculasSalasCines { get; set; }
    }
}
