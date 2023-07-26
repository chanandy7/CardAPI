using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cards.DB
{
    public class Card
    {
        [Key]
        [Column(Order = 1)]
        public int Id { get; set; }

        [Key]
        [Column("IdSecond", Order = 2)]
        public int IdSecond { get; set; }
        public string Front { get; set; }
        public string Back { get; set; }
    }
}