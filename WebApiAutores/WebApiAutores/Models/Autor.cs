using System.ComponentModel.DataAnnotations;
using WebApiAutores.Validaciones;

namespace WebApiAutores.Models
{
    public class Autor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 120, ErrorMessage = "El campo {0} no debe de tener más de {1} carácteres")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }


        //[Range(18, 120)]
        //[NotMapped] // no pertene a una columna de tabla db
        //[CreditCard]
        //[Url]

        public List<Libro> Libros{ get; set; }
    }
}
