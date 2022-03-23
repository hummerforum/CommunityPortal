using CommunityPortal.Models;
using CommunityPortal.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace CommunityPortal.Controllers
{

    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("Get")]
        public string Get()
        {
            List<Category> Categories = _dbContext.Categories.ToList();
            string JsonData = JsonSerializer.Serialize(Categories);
            return JsonData;
        }
    }

}