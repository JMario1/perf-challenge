using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using saif.Data;
using saif.Models;

namespace saif.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly AppDbContext _dbContext;

        public ProductController(ILogger<ProductController> logger, AppDbContext dbContext ){
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            Product? product = _dbContext.Products.Include(ctx => ctx.Category).FirstOrDefault(product => product.Id == id);
            if (product == null)
            {
               return  NotFound("No record");
            }
            return Ok(product);
        }


        [HttpPost]
        public ActionResult<Product> Add(CreateProduct product)
        {
            Product newProduct = new Product();

            var result = _dbContext.Categories.FirstOrDefault(dbCategories => dbCategories.Name == product.CategoryName);

                    if(result == null)
                    {
                    return NotFound("Catergory does not exist");
                    }
                    else
                    {
                        newProduct.Name = product.Name;
                        newProduct.CategoryId = result.Id;
                        newProduct.Description = product.Description;
                        newProduct.Price = product.Price;

                        _dbContext.Products.Add(newProduct);
                        _dbContext.SaveChanges();
                        return Created("created", newProduct);
                    }
        }

        [HttpPut("{id}")]
        public ActionResult<Product> Update(int id, [FromBody]CreateProduct product)
        {
           Product? dbProduct = _dbContext.Products.Find(id);
            if (dbProduct == null)
            {
               return  NotFound("No record");
            }
            var result = _dbContext.Categories.FirstOrDefault(dbCategories => dbCategories.Name == product.CategoryName);

                if(result == null)
                {
                return NotFound("Catergory does not exist");
                }
                else
                {
                    dbProduct.Name = product.Name;
                    dbProduct.CategoryId = result.Id;
                    dbProduct.Description = product.Description;
                    dbProduct.Price = product.Price;

                    _dbContext.Products.Update(dbProduct);
                    _dbContext.SaveChanges();
                    return Created("created", dbProduct);
                }
        }

    }
}