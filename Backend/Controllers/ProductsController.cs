using Backend.Contexts;
using Backend.Filters;
using Backend.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext _context;
        public ProductsController(DataContext context)
        {
            _context = context;
        }

        [UseApiKey]
        [HttpGet]
        public IActionResult Get()
        {
            var products = _context.Products.ToList();
            if (products.Count == 0)
            {
                return NotFound("Products not available");
            }
            return Ok(products);
        }

        [UseApiKey]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound($"Product details not found with id {id}");
            }
            return Ok(product);
        }

        [UseApiKey]
        [HttpPost]
        public IActionResult Post(ProductEntity product)
        {
            _context.Add(product);
            _context.SaveChanges();
            return Ok("Product created");
        }

        [UseApiKey]
        [HttpPut]
        public IActionResult Put(ProductEntity product)
        {
            if(product == null || product.Id == 0)
            {
                if(product == null)
                {
                    return BadRequest("Product data is invalid.");
                }
                else if(product.Id == 0)
                {
                    return BadRequest($"Product Id {product.Id} is invalid");
                }
            }
            var model = _context.Products.Find(product.Id);
            if(model == null)
            {
                return BadRequest($"Product Id {product.Id} is invalid");
            }
            model.Title = product.Title;
            model.Price = product.Price;
            model.Description = product.Description;
            model.Rating = product.Rating;
            model.ProductType = product.ProductType;
            _context.SaveChanges();
            return Ok("Product details updated");
        }

        [UseApiKey]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        { 
            var product = _context.Products.Find(id);
            if(product == null)
            {
                return NotFound($"Product not found with id {id}");
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
            return Ok("Product details deleted.");
        }
        
    }
}
