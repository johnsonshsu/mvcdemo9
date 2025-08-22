using System;
using System.Collections.Generic;

namespace mvcdemo9.Models;

public partial class CodeBases
{
    public int Id { get; set; }

    public bool IsAdmin { get; set; }

    public string? BaseNo { get; set; }

    public string? BaseName { get; set; }

    public string? DefaultValue { get; set; }

    public string? Remark { get; set; }
}
