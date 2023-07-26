using System.ComponentModel.DataAnnotations;

namespace PeliculasApi.Validaciones
{
    public class PesoArchivoValidacion : ValidationAttribute
    {
        private readonly int pesoMaximoEnMegabBytes;

        public PesoArchivoValidacion(int PesoMaximoEnMegabBytes)
        {
            pesoMaximoEnMegabBytes = PesoMaximoEnMegabBytes;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value == null)
            {
                return ValidationResult.Success;
            }

            IFormFile formFile = value as IFormFile;
            if(formFile == null) //si la transgormacion a formfile no es exitosa
            {
                return ValidationResult.Success;
            }

            if(formFile.Length > pesoMaximoEnMegabBytes * 1024 * 1024)
            {
                return new ValidationResult($"El peso del archivo no debe de ser mayor a {pesoMaximoEnMegabBytes}mb");
            }
            return ValidationResult.Success;
        }
    }
}
