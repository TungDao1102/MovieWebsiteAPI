using System;
using System.Collections.Generic;

namespace APIWebMovie.Models;

public partial class Bill
{
    public int Idbill { get; set; }

    public int UserId { get; set; }

    public string PaymentId { get; set; } = null!;

    public string OrderId { get; set; } = null!;

    public string Amount { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Number { get; set; } = null!;

    public DateTime TimePayment { get; set; }

    public bool? Status { get; set; }

    public virtual User User { get; set; } = null!;
}
