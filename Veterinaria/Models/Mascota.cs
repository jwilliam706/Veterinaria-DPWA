using System;
using System.Collections.Generic;

namespace Veterinaria.Models;

public partial class Mascota
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Tipo { get; set; }

    public string? Sexo { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    public int? ClienteId { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual Cliente? Cliente { get; set; }

    public virtual ICollection<Expediente> Expedientes { get; set; } = new List<Expediente>();
}
