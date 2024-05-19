using SharedKernel;

namespace Product.Domain;

public class Category : ValueObject
{
    public Category ChangeCategoryName(string newName)
    {
        return new Category()
        {
            CategoryName = newName,
            CategoryDescription = CategoryDescription
        };
    }

    public string? CategoryName { get; private set; }
    public string? CategoryDescription { get; private set; }
    public List<ProductCategory> Products { get; set; } = new();

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return CategoryName;
        yield return CategoryDescription;
    }
}