using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoneyHulkHost.Models
{
    public enum AccountKind
    {
        Cash = 0,
        DayToDay = 1,
        Savings = 2,
        Credit = 3,
        Loan = 4
    }

    public class Account
    {
        [Required]
        public int AccountId { get; set; }

        [StringLength(100), Required]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        public AccountKind Kind { get; set; }

        public List<AccountLine> AccountEntries { get; set; }
    }
}
