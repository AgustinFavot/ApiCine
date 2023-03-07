namespace apiPeliculas.Models
{
    public class PeliculasSalasCine
    {
        public int SalaCineId { get; set; }
        public int PeliculaId { get; set; }
        public SalaCine SalaCine { get; set; }
        public Pelicula Pelicula { get; set; }
    }
}
