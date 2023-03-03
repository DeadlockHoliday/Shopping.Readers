using Shopping.Common.Data.Products;

namespace Shopping.Common.Modules;

public interface IProductProcessor
{
    Product Process(Product product);
}