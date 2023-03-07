namespace apiPeliculas.Services
{
    public class AlmacenadorArchivosLocal : IAlmacenadorArchivos
    {
        private readonly IWebHostEnvironment env; //Obtenemos la ruta del wwwroot para poder colocar archivos en dicha carpeta
        private readonly IHttpContextAccessor accessor; //

        public AlmacenadorArchivosLocal(IWebHostEnvironment env, IHttpContextAccessor accessor)
        {
            this.env = env;
            this.accessor = accessor;
        }
        //Necesitamos la carpeta wwwroot, actua como raiz del sitio web, donde encontramos los archivos estaticos requeridos por el sitio
        public Task DeleteArchivo(string ruta, string container)
        {
            if (ruta != null)
            {
                var nombreArchivo = Path.GetFileName(ruta);
                string directorioArchivo = Path.Combine(env.WebRootPath, container, nombreArchivo);

                if (File.Exists(directorioArchivo))
                {
                    File.Delete(directorioArchivo);
                }
            }

            return Task.FromResult(0);
        }

        public async Task<string> EditArchivo(byte[] bytes, string extension, string ruta, string container, string contentType)
        {
            await DeleteArchivo(ruta, container);
            return await SaveArchivo(bytes, extension, container, contentType);
        }

        public async Task<string> SaveArchivo(byte[] bytes, string extension, string container, string contentType)
        {
            var nombreArchivo = $"{Guid.NewGuid()}{extension}";
            string folder = Path.Combine(env.WebRootPath, container);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string ruta = Path.Combine(folder, nombreArchivo);
            await File.WriteAllBytesAsync(ruta, bytes);

            var urlActual = $"{accessor.HttpContext.Request.Scheme}://{accessor.HttpContext.Request.Host}";
            var urlParaBD = Path.Combine(urlActual, container, nombreArchivo).Replace("\\", "/");
            return urlParaBD;
        }
    }
}
