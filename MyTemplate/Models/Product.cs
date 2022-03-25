using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyTemplate.Models
{   
    [Table("Product")]
    public class Product
    {
        [Key]
        public int ProductId { set; get; }
        public string Name { set; get; }
        [DataType(DataType.Text)]
        public string Description { set; get; }
        [DataType(DataType.Currency)]
        public float Price { set;get; }
    }
}
