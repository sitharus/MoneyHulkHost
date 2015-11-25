using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoneyHulkHost.Models
{

    public class Category
    {
        [Required]
        public int CategoryId { get; set; }

        [StringLength(100), Required]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        public bool IsIncome { get; set; }

        public List<AccountLine> AccountEntries { get; set; }
        
        public Budget Budget { get; set; }
    }
}
