using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StolenMobilesApi.Models
{
    [Table("Mobiles", Schema = "dbo")]
    public class Mobiles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "MobileID")]
        public int Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string MobileName { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Description { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string StolenIn { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Image { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string IMEI { get; set; }

        //public static explicit operator List<object>(Mobiles v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
