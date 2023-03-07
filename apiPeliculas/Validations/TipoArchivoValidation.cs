using System.ComponentModel.DataAnnotations;

namespace apiPeliculas.Validations
{
    public class TipoArchivoValidation : ValidationAttribute
    {
        private readonly string[] tipoArchivo;

        public TipoArchivoValidation(TipoArchivo tipoArchivo)
        {
            if (tipoArchivo == TipoArchivo.Imagen)
            {
                this.tipoArchivo = new string[] { "image/jpeg", "image/png", "image/gif"};
            }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            IFormFile formFile = value as IFormFile;

            if (formFile == null) return ValidationResult.Success;

            if(!tipoArchivo.Contains(formFile.ContentType)) return new ValidationResult($"Solo se permiten los siguientes tipos de archivos: {string.Join(", ", tipoArchivo)}");

            return ValidationResult.Success;
        }

    }
}
