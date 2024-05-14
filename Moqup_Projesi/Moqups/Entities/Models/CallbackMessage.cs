using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class CallbackMessage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? geriDonusMesaj { get; set; }
        public int TakipNoId { get; set; }
        
        [ForeignKey("TakipNoId")] // Foreign key ilişkisi
        public TakipNo TakipNo { get; set; } // Navigation property
    }
}
