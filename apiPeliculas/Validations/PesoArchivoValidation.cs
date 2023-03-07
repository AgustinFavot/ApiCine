using System.ComponentModel.DataAnnotations;

namespace apiPeliculas.Validations
{
    public class PesoArchivoValidation : ValidationAttribute
    {
        private readonly int PesoMaxMB;

        public PesoArchivoValidation(int PesoMaxMB)
        {
            this.PesoMaxMB = PesoMaxMB;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            IFormFile formFile = value as IFormFile;

            if (formFile == null) return ValidationResult.Success;

            //PesoMaxMB * 1024 = KB *1024 = MB
            if (formFile.Length > PesoMaxMB * 1024 * 1024) return new ValidationResult($"El peso del archivo no debe ser mayo a {PesoMaxMB} MB");

            return ValidationResult.Success;
        }    
    }
}
