using Shopping.Common.Data.Products;

namespace Shopping.Common.Modules;

public interface IProductProcessor
{
    ProcessedProduct Process(Product product);
}