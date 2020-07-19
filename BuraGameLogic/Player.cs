using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace BuraGameLogic
{
    [DataContract]
    public class Player
    {
        [DataMember]
        public string Name { get; }
        [DataMember]
        public Team Team { get; }
        [DataMember]
        private List<Card> cards;

        public Player(string name, Team team)
        {
            if (name == "")
                throw new ArgumentException("Имя не может быть пустым", "name");
            Name = name;
            cards = new List<Card>(4);
        }

        internal void AddCard(Card card)
        {
            if (card == null)
                throw new ArgumentNullException("card");
            if (cards.Count == 4)
                throw new InvalidOperationException("У игрока на руках уже максимальное количество карт");

            cards.Add(card);
        }

        internal void RemoveCard(Card card)
        {
            if (card == null)
                throw new ArgumentNullException("card");
            if (cards.Count == 0)
                throw new InvalidOperationException("У игрока нет карт");
            if (!cards.Contains(card))
                throw new InvalidOperationException($"У игрока нет карты {card}.");

            cards.Remove(card);
        }

        public IEnumerable<Card> Cards { get => cards; }
    }
}
