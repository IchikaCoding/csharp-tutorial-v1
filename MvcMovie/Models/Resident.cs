using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class Resident
    {
        // 名前、種類、職業、住民になった日付
        public int Id { get; set; }
        [StringLength(30)]
        public string Name { get; set; } = "";
        public int Age { get; set; }
        public string? AnimalType { get; set; }
        [DisplayFormat(NullDisplayText = "未登録")]
        [StringLength(50)]
        public string? Job { get; set; }
        [DataType(DataType.Date)]
        public DateTime ResidentSince { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}