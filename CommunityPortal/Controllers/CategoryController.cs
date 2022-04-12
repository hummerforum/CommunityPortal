using CommunityPortal.Models;
using CommunityPortal.Models.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CommunityPortal.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public string Get()
        {
            List<Category> Categories = _categoryService.GetList();
            return JsonConvert.SerializeObject(Categories);
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            Category category = _categoryService.GetById(id);
            return JsonConvert.SerializeObject(category);
        }

        [HttpGet("GetParentList")]
        public string GetParentList()
        {
            List<Category> Categories = _categoryService.GetParentList();
            return JsonConvert.SerializeObject(Categories);
        }

        [HttpPost]
        public void Post([FromBody] Category category)
        {
            _categoryService.Add(category);
        }

        [HttpPut]
        public void Put([FromBody] Category category)
        {
            _categoryService.Update(category);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _categoryService.Delete(id);
        }
    }

}