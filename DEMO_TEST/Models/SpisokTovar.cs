using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;

namespace DEMO_TEST.Models;

public partial class SpisokTovar
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Photo { get; set; }

    public string Opisanie { get; set; } = null!;

    public string Manufacturer { get; set; } = null!;

    public int Prise { get; set; }

    public int? Skidka { get; set; }

    public int? Count { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    public Bitmap? Image => Photo != null ? new Bitmap($@"Asset\\{Photo}") : null;
}
