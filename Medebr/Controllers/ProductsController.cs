using Medebr.Data.Entities;
using Medebr.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Medebr.Controllers
{
    public class ProductsController:Controller
    {
        private readonly IProductRepository _repository;

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("products")]
        public IActionResult Products(string catagory) {
            var results = _repository.GetProductsByCatagory(catagory);
            return View(results);
        }

        [HttpGet("product")]
        public IActionResult Product(int id)
        {
            var results = _repository.GetProductsById(id);
            return View(results);
        }

        [HttpGet("product/add")]
        public IActionResult AddProduct()
        {
            ViewBag.Success = "";
            return View();
        }
        [HttpPost("product/add")]
        public IActionResult AddProduct(Product product,IFormFile Image1, IFormFile Image2, IFormFile Image3, IFormFile Image4) {
            byte[] Img1 = null;
            byte[] Img2 = null;
            byte[] Img3 = null;
            byte[] Img4 = null;
            ViewBag.Success = "";
            if (ModelState.IsValid) {
                if (Image1.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        Image1.CopyTo(stream);
                        Img1 = stream.ToArray();
                    }
                }
                if (Image2.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        Image2.CopyTo(stream);
                        Img2 = stream.ToArray();
                    }
                }

                if (Image3.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        Image3.CopyTo(stream);
                        Img3 = stream.ToArray();
                    }
                }
                if (Image4.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        Image4.CopyTo(stream);
                        Img4 = stream.ToArray();
                    }
                }
                product.Image1 = Img1;
                product.Image2 = Img2;
                product.Image3 = Img3;
                product.Image4 = Img4;
                _repository.Save(product);
                ViewBag.Success = "Product Added Successfully";
                ModelState.Clear();
            }
            
            return View();
        }
    }
}
