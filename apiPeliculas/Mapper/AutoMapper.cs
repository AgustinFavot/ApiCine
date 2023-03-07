using AutoMapper;
using apiPeliculas.Models;
using apiPeliculas.DTOs.ActorDTOs;
using apiPeliculas.DTOs.GeneroDTOs;
using apiPeliculas.DTOs.PeliculaDTOs;
using apiPeliculas.DTOs.SalaCineDTOs;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace apiPeliculas.Mapper
{
    public class AutoMapper : Profile
    {

        public AutoMapper()
        {
            #region Mapper Generos
            CreateMap<Genero, GeneroDTO>().ReverseMap();
            CreateMap<GeneroCreationDTO, Genero>();
            #endregion

            #region Mapper Actores
            CreateMap<Actor, ActorDTO>().ReverseMap();
            CreateMap<Actor, ActorCreationDTO>().ReverseMap();
            #endregion

            #region Mapper Peliculas
            CreateMap<Pelicula, PeliculaDTO>().ForMember(x => x.Autores, options => options.MapFrom(MapPeliculaPeliculaDTO)).ForMember(x => x.Generos, options => options.MapFrom(MapGeneroGeneroDTO)).ReverseMap();
            CreateMap<PeliculaCreationDTO, Pelicula>().ForMember(x => x.PeliculasActores, options => options.MapFrom(MapPeliculaActores)).ForMember(x => x.PeliculasGeneros, options => options.MapFrom(MapPeliculaGeneros)).ReverseMap();
            #endregion

            #region Mapper Sala Cine
            var geometry = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);//sistema de coordenada que se utiliza en el planeta tierra
            //TSource TDestination
            CreateMap<SalaCineCreationDTO, SalaCine>().ForMember(x => x.Ubicacion, x => x.MapFrom(y => geometry.CreatePoint(new Coordinate(y.Longitud, y.Latitud))));
            //Mapeo de point a latitud y longitud
            CreateMap<SalaCine, SalaCineDTO>().ForMember(x => x.Latitud, x => x.MapFrom(y => y.Ubicacion.Y)).
                                                ForMember(x => x.Longitud, x => x.MapFrom(y => y.Ubicacion.X));
            
            CreateMap<SalaCineDTO, SalaCine>().ForMember(x => x.Ubicacion, x => x.MapFrom(y => geometry.CreatePoint(new Coordinate(y.Longitud, y.Latitud))));
            //Mapeo de longitud y latitud a Point
            #endregion

        }

        private List<PeliculaDTO> MapSalaCineSalaCineDTO(SalaCine salaCine, SalaCineDTO arg)
        {
            //Mappear de PeliculasSalaCine a PeliculaDTO
            var list = new List<PeliculaDTO>();
            if (salaCine.PeliculasSalasCines == null) return list;
            foreach (var item in salaCine.PeliculasSalasCines)
            {
                var peli = new Pelicula();
                peli.PeliculasActores = item.Pelicula.PeliculasActores;
                peli.PeliculasGeneros = item.Pelicula.PeliculasGeneros;
                list.Add(new PeliculaDTO()
                {
                    Id = item.PeliculaId,
                    Titulo = item.Pelicula.Titulo,
                    EnCine = item.Pelicula.EnCines,                    
                    Autores = MapPeliculaPeliculaDTO( peli, new PeliculaDTO() ),
                    Generos = MapGeneroGeneroDTO(peli, new PeliculaDTO())
                }); ;
            }
            return list;
        }

        private List<PeliculasSalasCine> MapSalaCineCreationDTOSalaCine(SalaCineCreationDTO salaCineCreationDTO, SalaCine arg)
        {
            //Mappear de int a PelciulasSalasCine
            var list = new List<PeliculasSalasCine>();
            if (salaCineCreationDTO.PeliculasId == null) return list;
            foreach (var item in salaCineCreationDTO.PeliculasId)
            {
                list.Add(new PeliculasSalasCine()
                {
                    PeliculaId = item
                });
            }
            return list;
        }

        private List<GeneroDTO> MapGeneroGeneroDTO(Pelicula arg, PeliculaDTO peliculaDTO)
        {
            var list = new List<GeneroDTO>();
            if (arg.PeliculasActores == null) return list;
            foreach (var item in arg.PeliculasGeneros)
            {
                list.Add(new GeneroDTO()
                {
                    Id = item.GeneroId,
                    Nombre = item.Genero.Nombre
                });
            }
            return list;
        }

        private List<ActorDTO> MapPeliculaPeliculaDTO(Pelicula arg, PeliculaDTO peliculaDTO)
        {
            var list = new List<ActorDTO>();
            if (arg.PeliculasActores == null) return list;
            foreach (var item in arg.PeliculasActores)
            {
                list.Add(new ActorDTO()
                {
                    Id = item.ActorId,
                    Nombre = item.Actor.Nombre
                });
            }
            return list;
        }

        private List<PeliculasActores> MapPeliculaActores(PeliculaCreationDTO peliculaCreationDTO, Pelicula pelicula)
        {
            var list = new List<PeliculasActores>();
            
            if (peliculaCreationDTO.ActoresId == null) return list;
            
            foreach (var item in peliculaCreationDTO.ActoresId)
            {
                list.Add(new PeliculasActores()
                {
                    ActorId = item
                });
            }
            return list;
        }

        private List<PeliculasGeneros> MapPeliculaGeneros(PeliculaCreationDTO peliculaCreationDTO, Pelicula pelicula)
        {
            var list = new List<PeliculasGeneros>();

            if (peliculaCreationDTO.GenerosId == null) return list;

            foreach (var item in peliculaCreationDTO.GenerosId)
            {
                list.Add(new PeliculasGeneros()
                {
                    GeneroId = item
                });
            }
            return list;
        }
    }
}
