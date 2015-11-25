using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoneyHulkHost.Models
{


    public class ImportLine
    {
        [Required]
        public int ImportLineId { get; set; }

        [Required]
        public Import Import { get; set; }

        public List<AccountLine> CreatedLines { get; set; }
    }
}
