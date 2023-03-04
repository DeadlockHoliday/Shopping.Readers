using Shopping.Common.Data;

namespace Shopping.Common.Modules;

public interface IProductProcessor
{
    Product Process(Product product);
}