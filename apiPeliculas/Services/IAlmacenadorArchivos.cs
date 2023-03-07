namespace apiPeliculas.Services
{
    public interface IAlmacenadorArchivos
    {
        Task<string> SaveArchivo(byte[] bytes, string extension, string container, string contentType);
        Task<string> EditArchivo(byte[] bytes, string extension, string ruta, string container, string contentType);
        Task DeleteArchivo( string ruta, string container);

        //un contenedor en azure Storage es un carpeta donde guardamos informacion relacionada
        //ejemplo: contenedor de actores donde guardamos las fotos de los actores. Es un tema de organizacion
    }
}