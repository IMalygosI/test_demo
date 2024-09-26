using System;
using System.Collections.Generic;

namespace DEMO_TEST.Models;

public partial class Punktvidasci
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Zakaz> Zakazs { get; set; } = new List<Zakaz>();
}
