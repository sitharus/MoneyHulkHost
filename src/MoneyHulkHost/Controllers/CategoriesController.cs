using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using MoneyHulkHost.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MoneyHulkHost.Controllers
{
    [Route("api/categories")]
    public class CategoriesController : Controller
    {
        private MHContext _Context;

        public CategoriesController(MHContext context)
        {
            _Context = context;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Category> Get() => _Context.Categories;

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var category = _Context.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (category == default(Category))
            {
                return new HttpNotFoundResult();
            }
            return new ObjectResult(category);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]ApiCategory newCategory)
        {
            _Context.Categories.Add(
                new Category
                {
                    Name = newCategory.Name,
                    Description = newCategory.Description,
                    IsIncome = newCategory.IsIncome
                });
            _Context.SaveChanges();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]ApiCategory newCategory)
        {
            var category = _Context.Categories.First(c => c.CategoryId == id);
            category.Name = newCategory.Name;
            category.Description = newCategory.Description;
            _Context.SaveChanges();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _Context.Categories.Remove(_Context.Categories.First(c => c.CategoryId == id));
            _Context.SaveChanges();
        }
    }

    public class ApiCategory
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsIncome { get; set; }
    }
}
