using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace BuraGameLogic
{
    [DataContract]
    public class Card
    {
        [DataMember]
        public Rank Rank { get; private set; }
        [DataMember]
        public Suit Suit { get; private set; }
        public Card(Rank rank, Suit suit)
        {
            Rank = rank;
            Suit = suit;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (!GetType().Equals(obj.GetType()))
                return false;

            Card convertedObj = (Card)obj;
            return convertedObj.Rank == Rank && convertedObj.Suit == Suit;
        }
        public override string ToString()
        {
            return $"{Rank} of {Suit}";
        }

        public static bool CanBeat(Card defendingCard, Card attackCard, Suit trump)
        {
            if (defendingCard.Suit == attackCard.Suit)
            {
                return defendingCard.Rank < attackCard.Rank;
            }
            else
            {
                return attackCard.Suit == trump;
            }
        }
    }
    public enum Rank
    {
        Six   = 0b1,
        Seven = 0b10,
        Eight = 0b100,
        Nine  = 0b1000,
        Jack  = 0b10000,
        Queen = 0b100000,
        King  = 0b10000000,
        Ten   = 0b100000000,
        Ace   = 0b1000000000
    }
    public enum Suit
    {
        Diamonds = 0b1,
        Clubs    = 0b10,
        Hearts   = 0b100,
        Spades   = 0b1000
    }
    public static class ValueOfCards
    {
        private static int[] values = new int[] { 0, 0, 0, 0, 2, 3, 4, 10, 11 };
        public static int ValueOf(Card card)
        {
            if (card == null)
                throw new ArgumentNullException("card");
            return values[(int)card.Rank - 6];
        }
    }
}
