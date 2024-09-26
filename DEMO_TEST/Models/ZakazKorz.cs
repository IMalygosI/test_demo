using System;
using System.Collections.Generic;

namespace DEMO_TEST.Models;

public partial class ZakazKorz
{
    public int Id { get; set; }

    public DateOnly Datazakaza { get; set; }

    public int Numberzakaz { get; set; }

    public string? Sostavzakaza { get; set; }

    public int SkidkaZakaz { get; set; }

    public int Summaskidka { get; set; }

    public int IdPunktvidashi { get; set; }

    public int Kodpoluscenia { get; set; }

    public int IdTovar { get; set; }

    public virtual Punktvidasci IdPunktvidashiNavigation { get; set; } = null!;

    public virtual SpisokTovar IdTovarNavigation { get; set; } = null!;
}
