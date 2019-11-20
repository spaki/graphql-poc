using System;
using System.Collections.Generic;

namespace GraphQL.POC.Models
{
    public class Category
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
