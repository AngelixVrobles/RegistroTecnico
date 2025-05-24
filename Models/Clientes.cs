using RegistroTecnico.Services;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnico.Models
{
    public class Clientes
    {
        [Key]
    public int ClienteId { get; set; }

    public DateTime FechaIngreso { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "El Nombres obligatorio")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "No se permiten Numero")]
    public string? Nombres { get; set; }

    [Required(ErrorMessage = "La direccion es obligatoria")]
    public string? Direccion { get; set; }

    [Required(ErrorMessage = "El RNC es obligatorio.")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "El RNC debe contener 10 dígitos.")]
    public string? RNC { get; set; } 

    [Required(ErrorMessage = "El límite de crédito es obligatorio.")]
    [Range(0, 1000000, ErrorMessage = "El límite de crédito debe estar entre 0 y 1,000,000.")]
    public decimal LimiteCredito { get; set; }

    [Required(ErrorMessage = "El Campo es obligatorio.")]
    public int TecnicoId { get; set; }

    [ForeignKey("TecnicoId")]
    public virtual Tecnicos Tecnicos { get; set; } = null!;


    }
}
