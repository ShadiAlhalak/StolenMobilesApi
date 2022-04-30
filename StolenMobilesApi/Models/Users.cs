using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StolenMobilesApi.Models
{
    [Table("Users",Schema ="dbo")]
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name="UserId")]
        public int Id { get; set; }
         
        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string UserName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string UserPassword { get; set; }

        [Required]
        [EmailAddress]
        [Column(TypeName = "nvarchar(max)")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Sutdy { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Jop { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Age { get; set; }
    }
}
