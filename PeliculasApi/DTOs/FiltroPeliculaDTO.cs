namespace PeliculasApi.DTOs
{
    public class FiltroPeliculaDTO
    {
        public int Pagina { get; set; }
        public int CantidadRegistrosPorPagina { get; set; }

        public PaginacionDTO Paginacion
        {
            get { return new PaginacionDTO () 
            { Pagina = Pagina, CantidadRegistrosPorPagina = CantidadRegistrosPorPagina };}
        }

        public string Titulo { get; set; }  
        public int GeneroId { get; set; }   
        public bool EnCines { get; set; }

        public bool ProximosEstrenos { get; set; }
    }
}
