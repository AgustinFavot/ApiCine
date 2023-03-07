using apiPeliculas.DTOs.ActorDTOs;
using apiPeliculas.DTOs.GeneroDTOs;

namespace apiPeliculas.DTOs.PeliculaDTOs
{
    public class PeliculaDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public bool EnCine { get; set; }
        public DateTime FechaEstreno { get; set; }
        public List<ActorDTO> Autores { get; set; }
        public List<GeneroDTO> Generos { get; set; }
    }
}
