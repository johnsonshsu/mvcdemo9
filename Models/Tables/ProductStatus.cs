using System;
using System.Collections.Generic;

namespace mvcdemo9.Models;

public partial class ProductStatus
{
    public int Id { get; set; }

    public string? StatusNo { get; set; }

    public string? StatusName { get; set; }

    public string? Remark { get; set; }
}
