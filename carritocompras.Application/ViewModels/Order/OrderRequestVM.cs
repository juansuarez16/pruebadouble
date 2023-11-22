using carritocompra.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace carritocompras.Application.ViewModels.Order
{
    public class OrderRequestVM
    {
        public OrderType Type { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public int BasketId { get; set; }
    }
}
