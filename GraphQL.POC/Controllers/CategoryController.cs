using GraphQL.POC.Models;
using GraphQL.POC.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphQL.POC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IRepository repository;

        public CategoryController(IRepository repository) => this.repository = repository;

        [HttpGet]
        public IEnumerable<Category> Get() => repository.GetCategoryQuery();

        [HttpGet("{id}")]
        public Category Get(Guid id) => repository.GetCategoryQuery().FirstOrDefault(e => e.Id == id);

        [HttpGet("search")]
        public IEnumerable<Category> Search(string value) => repository.GetCategoryQuery().Where(e => 
            e.Name.IndexOf(value, StringComparison.InvariantCultureIgnoreCase) > -1
        ).ToList();
    }
}
