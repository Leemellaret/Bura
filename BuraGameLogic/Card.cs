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

        public static bool CanBeat(Card c1, Card c2, Suit trump)
        {
            if (c1.Suit == c2.Suit)
            {
                return c1.Rank < c2.Rank;
            }
            else
            {
                return c2.Suit == trump;

            }
        }
    }
    public enum Rank
    {
        Six = 6,
        Seven,
        Eight,
        Nine,
        Jack,
        Queen,
        King,
        Ten,
        Ace
    }
    public enum Suit
    {
        Diamonds,
        Clubs,
        Hearts,
        Spades
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
