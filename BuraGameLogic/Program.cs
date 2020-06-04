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
            Scores s = new Scores(2);
            s.UpdateScore(new int?[] { 40, 80 });
            s.UpdateScore(new int?[] { 40, 80 });
            s.UpdateScore(new int?[] { 40, 80 });
            s.UpdateScore(new int?[] { 40, 80 });
            s.UpdateScore(new int?[] { 40, 80 });
            s.UpdateScore(new int?[] { 40, 80 });
            
        }
    }
}
