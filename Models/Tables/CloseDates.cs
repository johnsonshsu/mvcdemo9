using System;
using System.Collections.Generic;

namespace mvcdemo9.Models;

public partial class CloseDates
{
    public int Id { get; set; }

    public string? CodeNo { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public string? Remark { get; set; }
}
