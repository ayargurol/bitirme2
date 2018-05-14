using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApp2.Models.Sql
{
    [Table("SearchedItems")]
    public class SearchedItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RowID { get; set; }
        [Required]
        public string SearchedWord { get; set; }
        public int count { get; set; }
    }
}
