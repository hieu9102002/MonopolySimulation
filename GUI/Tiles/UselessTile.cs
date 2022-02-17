using MonopolyFinal.Actions;
using MonopolyFinal.Enums;
using MonopolyFinal.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyFinal.Tiles
{
    public class UselessTile : Tile
    {
        public UselessTile(TileName tiles) : base(tiles) { }

        public override Actions.Action GetAction(Player player, string special = "")
        {
            return new DoNothingAction();
        }
    }
}
