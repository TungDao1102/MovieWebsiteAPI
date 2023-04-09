using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelAccess.ViewModel
{
    public class BillView
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
    }
}
