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
    public class ProductController : ControllerBase
    {
        private readonly IRepository repository;

        public ProductController(IRepository repository) => this.repository = repository;

        [HttpGet]
        public IEnumerable<Product> Get() => repository.GetProductsQuery();

        [HttpGet("{id}")]
        public Product Get(Guid id) => repository.GetProductsQuery().FirstOrDefault(e => e.Id == id);

        [HttpGet("search")]
        public IEnumerable<Product> Search(string value) => repository.GetProductsQuery().Where(e => 
            e.Name.IndexOf(value, StringComparison.InvariantCultureIgnoreCase) > -1
            || e.Description.IndexOf(value, StringComparison.InvariantCultureIgnoreCase) > -1
        ).ToList();
    }
}
