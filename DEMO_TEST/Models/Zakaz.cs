using System;
using System.Collections.Generic;

namespace DEMO_TEST.Models;

public partial class Zakaz
{
    public int Id { get; set; }

    public DateOnly Datez { get; set; }

    public int Numberzakaz { get; set; }

    public string? Sostavzakaz { get; set; }

    public int Summazakaz { get; set; }

    public int? Summaskidka { get; set; }

    public string Punktvidachi { get; set; } = null!;

    public int Kod { get; set; }

    public int IdPunktvidachi { get; set; }

    public virtual Punktvidasci IdPunktvidachiNavigation { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
