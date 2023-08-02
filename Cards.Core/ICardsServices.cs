//using Cards.Core.DTO;
using Cards.DB;
using System.Collections.Generic;



namespace Cards.Core
{
    public interface ICardsServices
    {
        //gets all decks
        List<Card> GetCards();
        //gets a certain deck
        List<Card> GetCard(int id);
        //create a deck
        //Card CreateCard(DB.Card card);
        Card CreateCard(Card card);


        //delete a deck
        void DeleteCard(int id);
        //get a deck and edit a certain id, id2
        Card EditCard(Card card);
    }
}