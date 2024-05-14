using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Entities.Models
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int TakipNoId { get; set; } // Yeni eklenen foreign key
        public string? adSoyad { get; set; }
        public string? kayitTipi { get; set; }
        public string? mesaj { get; set; }
        public string? oneri { get; set; }
        public bool? isOk { get; set; }

        [ForeignKey("TakipNoId")] // Foreign key ilişkisi
        public TakipNo TakipNo { get; set; } // Navigation property


    }
}
