using RegistroTecnico.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Tickets
{
    [Key]
    public int TicketId { get; set; }

    public DateTime Fecha { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "El Campo es obligatorio")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "No se permiten Numero")]
    public string? Prioridad { get; set; }

    [Required(ErrorMessage = "El campo es obligatoria")]
    public string? Asunto { get; set; }

    [Required(ErrorMessage = "El Campo es obligatorio.")]
    public string? Descripcion { get; set; }

    [Required(ErrorMessage = "El Campo es obligatorio.")]
    public int TiempoInvertido { get; set; }

    public int ClienteId { get; set; }

    [ForeignKey("ClienteId")]
    public virtual Clientes Clientes { get; set; } = null!;

    public int TecnicoId { get; set; }

    [ForeignKey("TecnicoId")]
    public virtual Tecnicos Tecnicos { get; set; } = null!;
}

public class PaginacionTickets
{
    public List<Tickets> Tickets { get; set; } = new();
    public int TotalRegistros { get; set; }
}

