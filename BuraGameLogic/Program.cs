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
            Scores s = new Scores(3);
            s.UpdateScore(new int?[] { 40, 40, 40 });
            s.UpdateScore(new int?[] { 19, 41, 60 });
            s.UpdateScore(new int?[] { 19, 41, 60 });
            s.UpdateScore(new int?[] { 60, 19, 41 });
            s.UpdateScore(new int?[] { 19, 41, 60 });
            s.StartNewGame();

        }
    }
}
