using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuraGameLogic
{
    public class NowMove
    {
        public Player Player { get; private set; }
        public TypeOfMove ExpectedTypeOfMove { get; private set; }

        public NowMove(Player player, TypeOfMove expectedTypeOfMove)
        {
            Player = player;
            ExpectedTypeOfMove = expectedTypeOfMove;
        }

    }

    public enum TypeOfMove
    {
        OnEmptyTable = 0b1,
        Beat = 0b10,
        ClosedFold = 0b100,
        OpenedFold = 0b1000,
        Bura = 0b10000
    }
}
