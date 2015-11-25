using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoneyHulkHost.Models
{
    public class Import
    {
        [Required]
        public int ImportId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ImportDateUTC { get; set; }

        [DataType(DataType.Date)]
        public DateTime StatementDate { get; set; }

        public List<ImportLine> Lines { get; set; }
    }
}
