using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnico.Models;

public class Clientes
{
    [Key]
    public int ClienteId { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [RegularExpression(@"^[a-zA-ZÁÉÍÓÚÜÑáéíóúüñ'’\s]+$", ErrorMessage = "El nombre solo puede contener letras y caracteres válidos")]
    public string Nombres { get; set; } = string.Empty;

    [Required(ErrorMessage = "La fecha de ingreso es obligatoria")]
    public DateTime FechaIngreso { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "La dirección es obligatoria")]
    public string Direccion { get; set; } = string.Empty;

    [Required(ErrorMessage = "El RNC es obligatorio")]
    [RegularExpression(@"^\d{3}-?\d{7}-?\d{1}$", ErrorMessage = "El RNC debe tener 10 dígitos, con o sin guiones")]
    public string RNC { get; set; } = string.Empty;

    [Required(ErrorMessage = "El límite de crédito es obligatorio")]
    [Range(0, 9999999999999.99, ErrorMessage = "El límite debe ser un valor positivo")]
    public decimal LimiteCredito { get; set; }

    [Required(ErrorMessage = "Debe seleccionar un técnico")]
    public int TecnicoId { get; set; }

    [ForeignKey("TecnicoId")]
    public Tecnicos? Tecnico { get; set; }
}
public class PaginacionClientes
{
    public List<Clientes> Clientes { get; set; } = new();
    public int TotalRegistros { get; set; }
}


