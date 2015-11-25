using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoneyHulkHost.Models
{
    public class AccountLine
    {
        [Required]
        public int AccountLineId { get; set; }

        [Required]
        public Account Account { get; set; }

        [Required]
        public decimal Value { get; set; }

        public ImportLine ImportedFrom { get; set; }

        public Import Statement { get; set; }

        public Category Category { get; set; }
    }
}
