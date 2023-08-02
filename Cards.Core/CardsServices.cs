using Cards.Core.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

//using Microsoft.AspNetCore.Mvc;
//using System.Net;


namespace Cards.Core  
{
    public class CardsServices : ICardsServices
    {
        //constructor
        private readonly DB.AppDbContext _context;

        //private AppDbContext _context;
        private readonly DB.User _user;
        public CardsServices(DB.AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _user = _context.Users
                .First(u => u.Username == httpContextAccessor.HttpContext.User.Identity.Name);
        }



        //public Card CreateCard(DB.Card card)
        //{
        //    try
        //    {
        //        card.User = _user;
        //        _context.Add(card);
        //        _context.SaveChanges();
        //        return (Card)card;

        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle the error gracefully 
        //        return new Card { Id = -1, IdSecond = -1 };
        //    }
        //}

        public Card CreateCard(DB.Card card)
        {
            Console.WriteLine("This is a debug message.");

            try
            {
                var existingCard = _context.Cards.FirstOrDefault(c => c.User.Id == _user.Id && c.Id == card.Id && c.IdSecond == card.IdSecond);
                
                if (existingCard != null)
                {
                    throw new Exception("Card with the same primary keys already exists for this user.");
                }

                card.User = _user;
                _context.Add(card);
                _context.SaveChanges();
                return (Card)card;
            }
            catch (DbUpdateException ex)
            {
                // Handle the specific exception and provide more specific information
                var innerException = ex.InnerException;
                if (innerException != null)
                {
                    throw new Exception("Error occurred while updating the database.", innerException);
                }
                else
                {
                    throw new Exception("Error occurred while updating the database.", ex);
                }
            }
            catch (Exception ex)
            {
                // Handle other exceptions gracefully 
                return new Card { Id = -1, IdSecond = -1 };
            }
        }




        public void DeleteCard(int id)
        {
            //var dbCard = _context.Cards.First(c => c.Id == id);

            //var cardsToDelete = _context.Cards.Where(c =>  c.Id == id).ToList();
            var cardsToDelete = _context.Cards.Where(c => c.User.Id == _user.Id && c.Id == id).ToList();
            _context.Cards.RemoveRange(cardsToDelete);
            _context.SaveChanges();
        }

        public Card EditCard(Card card)
        {
            var dbCard = _context.Cards.First(c => c.User.Id == _user.Id && c.Id == card.Id && c.IdSecond == card.IdSecond);
            dbCard.Front = card.Front;
            dbCard.Back = card.Back;
            _context.SaveChanges();
            //??
            //return dbCard; 
            return card;
        }


        //returns 1 deck
        public List<Card> GetCard(int id)
        {
            return _context.Cards.Where(c => c.User.Id == _user.Id && c.Id == id).Select(c => (Card)c).ToList();

        }

        public List<Card> GetCards()
        {
            return _context.Cards
                .Where(c => c.User.Id == _user.Id)
                .Select(c => (Card)c)
                .ToList();
        }


    }
}