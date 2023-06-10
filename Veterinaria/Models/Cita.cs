using System;
using System.Collections.Generic;

namespace Veterinaria.Models;

public partial class Cita
{
    public int Id { get; set; }

    public DateTime? Fecha { get; set; }

    public int? MascotaId { get; set; }

    public int? VeterinarioId { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<Expediente> Expedientes { get; set; } = new List<Expediente>();

    public virtual Mascota? Mascota { get; set; }

    public virtual Veterinario? Veterinario { get; set; }
}
