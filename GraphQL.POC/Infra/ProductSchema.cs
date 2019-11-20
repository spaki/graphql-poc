using GraphQL.Types;

namespace GraphQL.POC.Infra
{
    public class ProductSchema : Schema
    {
        public ProductSchema(IDependencyResolver resolver) : base(resolver) => Query = resolver.Resolve<ProductQuery>();
    }
}
