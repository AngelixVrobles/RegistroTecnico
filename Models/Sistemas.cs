using System.ComponentModel.DataAnnotations;

namespace RegistroTecnico.Models;
public class Sistemas
{
    [Key]
    public int SistemasId { get; set; }

    [Required(ErrorMessage = "El campo es obligatorio")]
    public string? Descripcion { get; set; }

    [Required(ErrorMessage = "El campo es obligatorio")]
    public int Complejidad { get; set; }
}