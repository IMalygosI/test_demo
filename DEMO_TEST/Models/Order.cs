using System;
using System.Collections.Generic;

namespace DEMO_TEST.Models;

public partial class Order
{
    public int Id { get; set; }

    public int IdTovar { get; set; }

    public int IdZakaz { get; set; }

    public virtual SpisokTovar IdTovarNavigation { get; set; } = null!;

    public virtual Zakaz IdZakazNavigation { get; set; } = null!;
}
