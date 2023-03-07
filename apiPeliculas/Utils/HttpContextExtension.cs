using Microsoft.EntityFrameworkCore;

namespace apiPeliculas.Utils
{
    public static class HttpContextExtension
    {
        //Agregamos cabeceras en las respuestas
        public async static Task InsertarParametrosPaginacionEnCabecera<T>(this HttpContext httpContext,
            IQueryable<T> queryable, int cantidadPorPagina) //A traves de IQueryable voy a poder determinar la cantidad de registros en la tabla
        {
            //Validacion
            if (httpContext == null) { throw new ArgumentNullException(nameof(httpContext)); }

            if (cantidadPorPagina <= 0) { throw new DivideByZeroException(); }

            double cantidad = await queryable.CountAsync();
            double cantidadPaginas = Math.Ceiling(cantidad / cantidadPorPagina);

            httpContext.Response.Headers.Add("Cantidad-de-registros: ", cantidad.ToString());
            httpContext.Response.Headers.Add("Cantidad-de-registros-por-pagina: ", cantidadPorPagina.ToString());
            httpContext.Response.Headers.Add("Cantidad-de-paginas: ", cantidadPaginas.ToString());
        }
    }
}
