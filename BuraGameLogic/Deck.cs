using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

using System.IO;
using System.Runtime.Serialization.Json;

namespace BuraGameLogic
{
    [DataContract]
    class Deck
    {
        [DataMember]
        private Card[] cards;
        [DataMember]
        private int indexOfCardToDraw;

        public Deck()
        {
            cards = StandartDeck.Cards.ToArray();
            Shuffler.ShuffleCards(cards);
            indexOfCardToDraw = 0;
        }

        public Card TakeCard()
        {
            if (indexOfCardToDraw == cards.Length)
                throw new InvalidOperationException("Нет карт для раздачи.");

            return cards[indexOfCardToDraw++];
        }
        public int CardsLeft()
        {
            return cards.Length - indexOfCardToDraw;
        }

        public IEnumerable<Card> Cards { get => cards; }
        public Suit Trump
        {
            get => (indexOfCardToDraw < cards.Length ? cards[cards.Length - 2].Suit : cards.Last().Suit);
        }
    }

    static class StandartDeck
    {
        public static IEnumerable<Card> Cards { get; }
        static StandartDeck()
        {
            using (FileStream fs = new FileStream(@"Assets\StandartDeck.txt", FileMode.Open))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Card[]));
                Cards = (Card[])ser.ReadObject(fs);
            }
        }
    }

    static class Shuffler
    {
        private static Random r;
        private static int maxCountOfAssignments = 256;
        private static int minCountOfAssignments = 10;
        public static void ShuffleCards(Card[] cards)
        {
            if (cards == null)
                throw new ArgumentNullException("cards");
            if (cards.Length <= 1)
                return;



            int countOfAssignments = r.Next(maxCountOfAssignments - minCountOfAssignments + 1) + minCountOfAssignments;
            int[] assignments = new int[cards.Length];

            int i1, i2;
            Card t;
            while (!assignments.All(x => x >= countOfAssignments))
            {
                do
                {
                    i1 = r.Next(cards.Length);
                    i2 = r.Next(cards.Length);
                }
                while (i1 == i2);

                t = cards[i1];
                cards[i1] = cards[i2];
                cards[i2] = t;
                assignments[i1]++;
                assignments[i2]++;
            }
        }

        static Shuffler()
        {
            r = new Random();
        }
    }
}
