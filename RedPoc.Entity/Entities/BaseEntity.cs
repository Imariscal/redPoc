using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RedPoc.Entity.Entities
{
    public class BaseEntity
    {
        [Key]
        [Column("id", TypeName = "uniqueidentifier")]
        [Required]
        public Guid Id { get; set; }

        [Column("create_date", TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }

        [Column("created_by", TypeName = "varchar")]
        [Required]
        [MaxLength(100)]
        public string CreatedBy { get; set; }
    }
}
