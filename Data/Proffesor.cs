using EUP.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace EUP.Data
{
    public class Proffesor
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Interest { get; set; }
        public string? Website { get; set; }
        public string? Email { get; set; }
        public bool Related { get; set; }
        public string? Subject { get; set; }
        public string? EmailText { get; set; }
        public DateTime? EmailDate { get; set; }
        public ResultType? Result { get; set; }
        public DateTime? UpdateDate { get; set; }

        public University? university { get; set; }
    }
}
