namespace apiPeliculas.DTOs
{
    public class PaginacionDTO
    {
        public int Pagina { get; set; } = 1; //Paguina que se encuentra el usuario
        private int registrosPorPagina { get; set; } = 10; //Cantidad de registros por paginas

        private readonly int CantidadMaximaPorPagina = 10; //Cantidad maxima de registros por paginas

        public int RegistrosPorPagina {
            get => registrosPorPagina; 
            set => registrosPorPagina = value < CantidadMaximaPorPagina ? value : CantidadMaximaPorPagina; 
        }
    }
}
