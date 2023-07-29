using Microsoft.EntityFrameworkCore;

namespace PeliculasApi.Helpers
{
    public static class HttpContextExtentions
    {
        //HttpContext httpContext//para agregar llas cabeceras http en las respuestas
        public async static Task InsertarParametrosPaginacion<T>(this HttpContext httpContext,
                       IQueryable<T> queryable, int cantidadRegistroPorPagina) 
        {//IQueryable a traves de esto determunar la cantidad total de regitros en la pagina

            double cantidad = await queryable.CountAsync(); //conteo
            double cantidadPaginas = Math.Ceiling(cantidad / cantidadRegistroPorPagina);
            httpContext.Response.Headers.Add("cantidadPaginas", cantidadPaginas.ToString());
        }
    }
}
