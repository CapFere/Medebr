using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medebr.Data.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Name Can't Be Empty")]
        [MinLength(4, ErrorMessage = "Name Must Be Atleast 4 Letters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description Can't Be Empty")]
        [MinLength(10, ErrorMessage = "Minimum Description Is 10 Letters")]
        [MaxLength(400, ErrorMessage = "Maximum Description Is 400 Letters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price Is Required")]
        public int Price { get; set; }
        [Required(ErrorMessage = "Image Is Not Selected")]
        public byte[] Image1 { get; set; }
        [Required(ErrorMessage = "Image Is Not Selected")]
        public byte[] Image2 { get; set; }
        [Required(ErrorMessage = "Image Is Not Selected")]
        public byte[] Image3 { get; set; }
        [Required(ErrorMessage = "Image Is Not Selected")]
        public byte[] Image4 { get; set; }
        [Required(ErrorMessage = "Catagory Is Not Selected")]       
        public string Catagory { get; set; }
    }
}
