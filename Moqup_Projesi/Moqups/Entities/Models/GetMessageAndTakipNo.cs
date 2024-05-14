namespace Entities.Models
{
    public class GetMessageAndTakipNo
    {
        public int takipNoId { get; set; }
        public int messageId { get; set; }
        public string? takipNoString { get; set; }
        public string? message { get; set; }
        public string? adSoyad { get; set; }
        public string? kayitTipi { get; set; }
        public string? oneri { get; set; }
        public bool? isOk { get; set; }
    }
}
