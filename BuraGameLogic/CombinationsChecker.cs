using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace BuraGameLogic
{
    [DataContract]
    public class CombinationsChecker
    {
        [DataMember]
        private Combination[] combinations;

        public CombinationsChecker(Combination[] combinations)
        {
            this.combinations = (Combination[])combinations.Clone();
        }

        public IEnumerable<Combination> Combinations { get => combinations; }

        public Combination[] GetRealizedCombinations(Card[] cards, Suit trump)
        {
            return combinations.Where(x => x.DoesCardsSatisfy(cards, trump)).ToArray();
        }
    }

    [DataContract]
    public class Combination
    {
        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        private CardsSetMask[] cardsSetMasks;

        public Combination(string name, CardsSetMask[] cardsSetMasks)
        {
            Name = name;
            this.cardsSetMasks = (CardsSetMask[])cardsSetMasks.Clone();
        }

        public IEnumerable<CardsSetMask> CardsSetMasks { get => cardsSetMasks; }

        public bool DoesCardsSatisfy(Card[] cards, Suit trump)
        {
            foreach (var mask in cardsSetMasks)
            {
                if (mask.DoesCardsSatisfy(cards, trump))
                    return true;
            }

            return false;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    [DataContract]
    public class CardsSetMask
    {
        [DataMember]
        private CardMask[] cardMasks;

        public CardsSetMask(CardMask[] cardMasks)
        {
            this.cardMasks = (CardMask[])cardMasks.Clone();
        }

        public IEnumerable<CardMask> CardMasks { get => cardMasks; }

        public bool DoesCardsSatisfy(Card[] cards, Suit trump)
        {
            if (cards.Length < cardMasks.Length)
                return false;

            bool[][] tableOfSatisfying = new bool[cards.Length][];
            for (int i = 0; i < cards.Length; i++)
                tableOfSatisfying[i] = new bool[cards.Length];

            for (int i = 0; i < cards.Length; i++)
            {
                for (int j = 0; j < cards.Length; j++)
                {
                    if (i < cardMasks.Length)
                    {
                        tableOfSatisfying[i][j] = cardMasks[i].DoesCardSatisfy(cards[j], trump);
                    }
                    else
                    {
                        tableOfSatisfying[i][j] = true;
                    }
                }
            }
            return MatrixUtils.HasFullDiagonal(tableOfSatisfying);
        }
    }

    [DataContract]
    public class CardMask
    {
        [DataMember]
        public CardMaskRank? ExpectedRank { get; private set; }
        [DataMember]
        public CardMaskSuit? ExpectedSuit { get; private set; }

        public CardMask(CardMaskRank? expectedRank, CardMaskSuit? expectedSuit)
        {
            ExpectedRank = expectedRank;
            ExpectedSuit = expectedSuit;
        }

        public bool DoesCardSatisfy(Card card, Suit trump)
        {
            Rank? rankToCompatison = (Rank?)ExpectedRank;
            Suit? suitToComparison = (ExpectedSuit == CardMaskSuit.Trump ? trump : (Suit?)ExpectedSuit);

            return (!ExpectedRank.HasValue || (rankToCompatison & card.Rank) != 0) && (!ExpectedSuit.HasValue || (suitToComparison & card.Suit) != 0);
        }
    }

    public enum CardMaskRank
    {
        Six = Rank.Six,
        Seven = Rank.Seven,
        Eight = Rank.Eight,
        Nine = Rank.Nine,
        Jack = Rank.Jack,
        Queen = Rank.Queen,
        King = Rank.King,
        Ten = Rank.Ten,
        Ace = Rank.Ace,
        HighCard = Rank.Jack | Rank.Queen | Rank.King | Rank.Ten | Rank.Ace
    }

    public enum CardMaskSuit
    {
        Diamonds = Suit.Diamonds,
        Clubs = Suit.Clubs,
        Hearts = Suit.Hearts,
        Spades = Suit.Spades,
        Trump = 0b10000
    }
}
