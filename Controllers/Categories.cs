using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using saif.Data;
using saif.Models;

namespace saif.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly AppDbContext _dbContext;

        public CategoriesController(ILogger<CategoriesController> logger, AppDbContext dbContext ){
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            Category? category = _dbContext.Categories.Find(id);
            if (category == null)
            {
               return  NotFound("No record");
            }
            return Ok(category);
        }


        [HttpPost]
        public ActionResult Add(Category category)
        {
            var result = _dbContext.Categories.FirstOrDefault(dbCategories => dbCategories.Name == category.Name);
                if(result != null)
                {
                   return Ok("Catergory already exist");
                }
                else
                {
                    _dbContext.Categories.Add(category);
                    _dbContext.SaveChanges();
                    return Created("created", category);
                }
        }

    }
}