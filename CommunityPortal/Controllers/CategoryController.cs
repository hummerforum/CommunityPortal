using CommunityPortal.Models;
using CommunityPortal.Models.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;

namespace CommunityPortal.Controllers
{

    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public string Get()
        {
            List<Category> Categories = _categoryService.GetList();
            string JsonData = JsonSerializer.Serialize(Categories);
            return JsonData;
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            Category category = _categoryService.GetById(id);
            string JsonData = JsonSerializer.Serialize(category);
            return JsonData;
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