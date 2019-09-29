using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medebr.Data.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public List<OrderDetail> OrderLines { get; set; }
        [Required(ErrorMessage = "First Name Can't Be Empty")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name Can't Be Empty")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "State Is Not Selected")]
        public string State { get; set; }
        [Required(ErrorMessage = "City Name Can't Be Empty")]
        public string City { get; set; }
        [Required(ErrorMessage = "Address Name Can't Be Empty")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Phone Name Can't Be Empty")]
        public string PhoneNumber { get; set; }
        [BindNever]
        [ScaffoldColumn(false)]
        public decimal OrderTotal { get; set; }
        [BindNever]
        [ScaffoldColumn(false)]
        public DateTime OrderPlaced { get; set; }
    }
}
