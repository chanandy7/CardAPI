using Cards.DB;

namespace Cards.Core

{
    public class CardsServices : ICardsServices
    {
        //constructor
        private AppDbContext _context;
        public CardsServices(AppDbContext context)
        {
            _context = context;
        }

        public Card CreateCard(Card card)
        {
            _context.Add(card);
            _context.SaveChanges();
            return card;

        }

        public void DeleteCard(int id)
        {
            var cardsToDelete = _context.Cards.Where(c => c.Id == id).ToList();
            _context.Cards.RemoveRange(cardsToDelete);
            _context.SaveChanges();
        }

        public Card EditCard(Card card)
        {
           var dbCard = _context.Cards.First(c => c.Id == card.Id && c.IdSecond == card.IdSecond);
            dbCard.Front = card.Front;
            dbCard.Back = card.Back;
            _context.SaveChanges();

            return dbCard;
        }


        //returns 1 deck
        public List<Card> GetCard(int id)
        {
            return _context.Cards.Where(c => c.Id == id).ToList();
        }

        public List<Card> GetCards()
        {
            return _context.Cards.ToList();
        }
    }
}