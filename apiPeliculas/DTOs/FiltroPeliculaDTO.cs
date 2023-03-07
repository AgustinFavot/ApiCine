namespace apiPeliculas.DTOs
{
    public class FiltroPeliculaDTO : PaginacionDTO
    {
        public string Titulo { get; set; }
        public int GeneroId { get; set; }
        public bool EnCine { get; set; }
        public string CampoOrdenar { get; set; }
        public bool Ascendente { get; set; } = true;
        public bool Descendente { get; set; }
    }
}
