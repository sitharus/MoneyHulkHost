using System.ComponentModel.DataAnnotations;

namespace MoneyHulkHost.Models
{
    public class Budget
    {
        [Required]
        public int BudgetId { get; set; }

        [Required]
        public Category Category { get; set; }
    }
}
