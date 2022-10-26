
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RedPoc.Entity.Entities
{
    [Table("order")]
    public class Order : BaseEntity
    {       
        [Column("order_type", TypeName = "varchar")]
        [Required]
        [MaxLength(150)]
        public OrderType OrderType { get; set; }

        [Column("customer_name", TypeName = "varchar")]
        [Required]
        [MaxLength(100)]
        public string CustomerName { get; set; }      
    }
}
