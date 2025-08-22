using System;
using System.Collections.Generic;

namespace mvcdemo9.Models;

public partial class Payments
{
    public int Id { get; set; }

    public string? PaymentNo { get; set; }

    public string? PaymentName { get; set; }

    public string? Remark { get; set; }
}
