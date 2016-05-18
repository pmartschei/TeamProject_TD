using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.GameLogic.EnemySystem
{
    public class TilePos
    {
        public int x;
        public int y;
        public TilePos(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public override bool Equals(object obj)
        {
            if (!(obj is TilePos))
                return false;
            TilePos other = (TilePos)obj;
            return x == other.x && y == other.y;
        }
    }
}
