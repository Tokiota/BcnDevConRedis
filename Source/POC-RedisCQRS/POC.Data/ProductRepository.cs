using POC.Data.Models;

namespace POC.Data
{
    public class ProductRepository : IRepository<Product>
    {
        
        #region Implementation of IRepository<in Product>

        public void Add(Product newEntity)
        {
            using (var context = new NorthwindContext())
            {
                context.Products.Add(newEntity);
                context.SaveChanges();
            }
        }

        #endregion
    }
}