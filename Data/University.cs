using EUP.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace EUP.Data
{
    public class University
    {
        [Key]
        public int ID { get; set; }
        public CountryCode Country { get; set; }
        public string? WorldRank { get; set; }
        public string? CountryRank { get; set; }
        public string? Name { get; set; }
        public string? City { get; set; }
        public string? state { get; set; }
        public string?  ScholarShip {get; set; }
        public string? Language { get; set; }
        public List<Proffesor>? Proffesors { get; set; }
    }
}
