using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace StolenMobilesApi.Models
{
    [Table("Images", Schema = "dbo")]
    public class Images
    {
        //[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ImageId")]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        [Display(Name = "MobileName")]
        public string ImageTitle { get; set; }

        [Column(TypeName = "varbinary(max)")]
        [Display(Name = "Image")]
        public byte[] ImageData { get; set; }

    }
}
