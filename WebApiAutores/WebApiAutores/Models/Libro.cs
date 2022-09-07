using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.Models
{
    public class Libro : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 120, ErrorMessage = "El campo {0} no debe de tener más de {1} carácteres")]
        public string Titulo { get; set; }

        public int AutorId { get; set; }

        public Autor Autor { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Titulo))
            {
                var primeraLetra = Titulo[0].ToString();

                if (primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera letra debe ser mayúscula",
                            new string[] { nameof(Titulo) });
                }
            }

            //if (Menor > Mayor)
            //{
            //    yield return new ValidationResult("Este valor no puede ser más grande que el campo Mayor",
            //        new string[] { nameof(Menor) });
            //}
        }


    }
}
