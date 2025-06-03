using System.ComponentModel.DataAnnotations;    
using System.ComponentModel.DataAnnotations.Schema; 


namespace RegistroTecnico.Models;

public class Tecnicos
{

    [Key]

    public int TecnicoId { get; set; }
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "No se permiten numeros.")]
    [Required(ErrorMessage = "Nombre obligatorio")]

    public string? Nombres { get; set; }
    [Required(ErrorMessage = "Introducir el sueldo hora del tecnico.")]
    [Range(1, 2000000, ErrorMessage = "El sueldo hora debe ser mayor a 1 y menor a 2000000")]

    public float SueldoHora { get; set; }


}
