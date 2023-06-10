using System;
using System.Collections.Generic;

namespace Veterinaria.Models;

public partial class Expediente
{
    public int Id { get; set; }

    public int? MascotaId { get; set; }

    public int? CitaId { get; set; }

    public string? Diagnostico { get; set; }

    public string? Recetas { get; set; }

    public virtual Cita? Cita { get; set; }

    public virtual Mascota? Mascota { get; set; }
}
