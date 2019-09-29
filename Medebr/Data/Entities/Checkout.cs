using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Medebr.Data.Entities
{
    public class Checkout
    {
        public Order Order { get; set; }
        public List<Product> Products = new List<Product>();

    }
}
