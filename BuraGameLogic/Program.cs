using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuraGameLogic
{
    class Program
    {
        static void Main(string[] args)
        {
            CardMask cm1 = new CardMask(CardMaskRank.Ace, null);
            CardMask cm2 = new CardMask(CardMaskRank.HighCard, null);
            CardMask cm3 = new CardMask(CardMaskRank.Ace, CardMaskSuit.Trump);
            CardMask cm4 = new CardMask(CardMaskRank.HighCard, CardMaskSuit.Trump);

            CardsSetMask csm1 = new CardsSetMask(new CardMask[] { cm4, cm2, cm2, cm1 });
            CardsSetMask csm2 = new CardsSetMask(new CardMask[] { cm2, cm2, cm2, cm3 });

            Combination[] combinations = new Combination[] { new Combination("Москва", new CardsSetMask[] { csm1, csm2 }) };

            CombinationsChecker combinationsChecker = new CombinationsChecker(combinations);

            Suit trump = Suit.Spades;
            Card[] cards = new Card[]{new Card(Rank.Ten, trump),
                                      new Card(Rank.Ten, Suit.Hearts),
                                      new Card(Rank.Six, Suit.Hearts),
                                      new Card(Rank.Jack, Suit.Diamonds) };

            var res = combinationsChecker.GetRealizedCombinations(cards, trump);
            int sjk = 489;
            /*bool dsf = MatrixUtils.HasFullDiagonal(new bool[][] { new bool[] { true , true, true }, 
                                                                  new bool[] { true, true , true }, 
                                                                  new bool[] { false, true , false } });

            int count = 3;

            Card[] ac = new Card[count];
            Card[] dc = new Card[count];

            bool[][] b = new bool[count][];
            for (int i = 0; i < count; i++)
                b[i] = new bool[count];

            Random r = new Random();
            do
            {
                Suit trump = (Suit)r.Next(4);

                for (int i = 0; i < count; i++)
                {
                    Card tc;

                    do
                    {
                        tc = new Card((Rank)r.Next(6, 15), (Suit)r.Next(4));
                    }
                    while (dc.Contains(tc) || ac.Contains(tc));
                    ac[i] = tc;

                    do
                    {
                        tc = new Card((Rank)r.Next(6, 15), (Suit)r.Next(4));
                    }
                    while (dc.Contains(tc) || ac.Contains(tc));
                    dc[i] = tc;
                }

                for (int i = 0; i < count; i++)
                {
                    for (int j = 0; j < count; j++)
                    {
                        b[i][j] = Card.CanBeat(dc[j], ac[i], trump);
                    }
                }

                string log = $"Trump={trump}\n";
                for (int i = -1; i < count; i++)
                {
                    for (int j = -1; j < count; j++)
                    {
                        if (i == -1 && j == -1) log += string.Format("{0,17}|", " ");

                        if (i == -1 && j != -1)
                            log += $"{dc[j],17}|";

                        if (j == -1 && i != -1)
                            log += $"{ac[i],17}|";

                        if (i != -1 && j != -1)
                            log += string.Format("{0,17}|", b[i][j]);
                    }
                    log += "\n";
                }
                log += $"does it beat? {MatrixUtils.HasFullDiagonal(b)}\n\n\n";

                Console.WriteLine(log);
            }
            while (Console.ReadKey().KeyChar != 'q');*/
        }
    }
}
