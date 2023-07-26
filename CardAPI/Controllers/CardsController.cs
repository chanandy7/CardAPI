using Cards.Core;
using Cards.DB;
using Microsoft.AspNetCore.Mvc;

namespace testingDB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardsController : ControllerBase
    {



        private ICardsServices _cardsServices;
        public CardsController(ICardsServices cardsServices)
        {
            _cardsServices = cardsServices;

        }

        [HttpGet]
        public IActionResult GetCards()
        {
            return Ok(_cardsServices.GetCards());

        }

        [HttpGet("{id}", Name = "GetCard")]
        public IActionResult GetCard(int id)
        {
            return Ok(_cardsServices.GetCard(id));

        }
        [HttpPost]
        public IActionResult CreateCard(Card card)
        {
            var newCard = _cardsServices.CreateCard(card);
            return CreatedAtRoute("GetCard", new { newCard.Id, newCard.IdSecond }, newCard);
        }

        [HttpDelete]
        public IActionResult DeleteCard(int id)
        {
            _cardsServices.DeleteCard(id); return Ok();
        }

        [HttpPut]
        public IActionResult PutCard(Card card)
        {
            return Ok(_cardsServices.EditCard(card));
        }


    }
}