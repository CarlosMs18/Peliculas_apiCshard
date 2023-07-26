namespace PeliculasApi.Servicios
{
    public class AlmacenadorArchivosLocal : IAlmacenadorArchivos
    {
        private readonly IWebHostEnvironment env;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AlmacenadorArchivosLocal(IWebHostEnvironment env, //mediante env podemosacceder al wwroot paraa sber la ruta de la magen
            IHttpContextAccessor httpContextAccessor)//saber el dominio donde esta la webapi y asi poder consturiry la urlque es ña cual se guardara en la BBDD de actores
        {
            this.env = env;
            this.httpContextAccessor = httpContextAccessor;
        }

        public Task BorrarArchivo(string ruta, string contenedor)
        {
                if(ruta != null)
            {
                var nombreArchivo = Path.GetFileName(ruta);
                string directorioArchivo = Path.Combine(env.WebRootPath, contenedor, nombreArchivo);
                if(File.Exists(directorioArchivo))
                {
                    File.Delete(directorioArchivo);
                }

               
            }
            return Task.FromResult(0);

        }

        public async Task<string> EditarArchivo(byte[] contenido, string extension, string contenedor, string ruta, string contentType)
        {
            await BorrarArchivo(ruta, contenedor);
            return await GuardarArchivo(contenido, extension, contenedor, contentType);
        }

        public async  Task<string> GuardarArchivo(byte[] contenido, string extension, string contenedor, string contentType)
        {
            var nombreArchivo = $"{Guid.NewGuid()}{extension}";
            string folder = Path.Combine(env.WebRootPath, contenedor);

            if(!Directory.Exists(folder)) //si el directorio no existe
            {
                Directory.CreateDirectory(folder);
            }

            string ruta = Path.Combine(folder, nombreArchivo);
            await File.WriteAllBytesAsync(ruta, contenido);

            var urlActual = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}"; //HTTPS;
            var urlParaBD = Path.Combine(urlActual, contenedor, nombreArchivo).Replace("\\","/");
            return urlParaBD;
           
        }
    }
}
