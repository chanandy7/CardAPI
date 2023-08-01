//using Cards.DB;
using Cards.Core.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
//using System;

namespace Cards.Core

{
    public class CardsServices : ICardsServices
    {
        //constructor
        private DB.AppDbContext _context;
        private readonly DB.User _user;
        public CardsServices(DB.AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _user = _context.Users
                .First(u => u.Username == httpContextAccessor.HttpContext.User.Identity.Name);
        }



        public Card CreateCard(DB.Card card)
        {
            try
            {
                card.User = _user;
                _context.Add(card);
                _context.SaveChanges();
                return (Card)card;
            }
            catch (Exception ex)
            {
                // Handle the error gracefully
                return new Card { Id = -1, IdSecond = -1};
            }
        }




        public void DeleteCard(int id)
        {
            //var dbCard = _context.Cards.First(c => c.User.Id == _user.Id && c.Id == id);


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
                .Where(c=>c.User.Id == _user.Id)
                .Select(c => (Card)c)
                .ToList();
        }
    }
}