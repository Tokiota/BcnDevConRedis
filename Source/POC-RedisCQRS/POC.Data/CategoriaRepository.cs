using System.Linq;

using POC.Data.Models;

namespace POC.Data
{
    public class CategoriaRepository:IRepository<Category>
    {
        public Category GetOne(int id)
        {
            Category item;
            using (var context = new NorthwindContext())
            {
                item = context.Categories.FirstOrDefault(c=>c.CategoryID == id);
                
            }
            return item;
        }
        #region Implementation of IRepository<in Category>

        public void Add(Category newEntity)
        {
            using (var context = new NorthwindContext())
            {
                context.Categories.Add(newEntity);
            }
        }

        #endregion
    }
    

    
}
