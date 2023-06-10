using System;
using System.Collections.Generic;

namespace Veterinaria.Models;

public partial class Veterinario
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Telefono { get; set; }

    public string? Sexo { get; set; }

    public string? Direccion { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();
}
