using System;
using System.Collections.Generic;

namespace mvcdemo9.Models;

public partial class Vacations
{
    public int Id { get; set; }

    public int VacYear { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public string? CodeNo { get; set; }

    public string? Remark { get; set; }
}
