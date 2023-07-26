using Microsoft.EntityFrameworkCore;

namespace PeliculasApi.Helpers
{
    public static class HttpContextExtentions
    {
        public async static Task InsertarParametrosPaginacion<T>(this HttpContext httpContext,
                       IQueryable<T> queryable, int cantidadRegistroPorPagina)
        {
            double cantidad = await queryable.CountAsync(); //conteo
            double cantidadPaginas = Math.Ceiling(cantidad / cantidadRegistroPorPagina);
            httpContext.Response.Headers.Add("cantidadPaginas", cantidadPaginas.ToString());
        }
    }
}
