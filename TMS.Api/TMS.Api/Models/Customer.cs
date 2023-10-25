using System;
using System.Collections.Generic;

namespace TMS.Api.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? CustomerName { get; set; }

    public string? CustomerEmail { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
