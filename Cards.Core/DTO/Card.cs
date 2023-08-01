

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Cards.Core.DTO
{
    public class Card
    {
        public int Id { get; set; }
        public int IdSecond { get; set; }
        public string Front { get; set; }
        public string Back { get; set; }

        public static explicit operator Card(DB.Card c) => new Card
        {
            Id = c.Id,
            IdSecond = c.IdSecond,
            Front = c.Front,
            Back = c.Back

        };
    }
}
