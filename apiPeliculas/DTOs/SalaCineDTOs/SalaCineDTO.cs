﻿using apiPeliculas.DTOs.PeliculaDTOs;

namespace apiPeliculas.DTOs.SalaCineDTOs
{
    public class SalaCineDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
    }
}
