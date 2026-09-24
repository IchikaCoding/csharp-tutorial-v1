using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class Resident
    {
        // 名前、種類、職業、住民になった日付
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? AnimalType { get; set; }
        public string? Job { get; set; }
        [DataType(DataType.Date)]
        public DateTime ResidentSince { get; set; }
    }
}