using System.ComponentModel.DataAnnotations;

namespace apiPeliculas.DTOs.SalaCineDTOs
{
    public class SalaCineCercanoFiltroDTO
    {
        [Range(-90, 90)]
        public double Latitud { get; set; }
        [Range(-180, 180)]
        public double Longitud { get; set; }

        private int distanciaEnKm = 10;
        private int distanciaMaxEnKm = 50;
        public int DistanciaEnKms 
        {
            get { return distanciaEnKm; }
            set 
            {
                distanciaEnKm = (value > distanciaMaxEnKm) ? distanciaMaxEnKm : value;
            }
        }

    }
}
