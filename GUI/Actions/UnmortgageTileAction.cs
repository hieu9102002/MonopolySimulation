using MonopolyFinal.Players;
using MonopolyFinal.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyFinal.Actions
{
    public class UnmortgageTileAction : Action
    {
        private SimpleBuyableTiles _tile;
        public UnmortgageTileAction(SimpleBuyableTiles tile)
        {
            _tile = tile;
        }
        public override string Execute(Player player, Board board)
        {
            _tile.Unmortgage(player);
            return player.Name + " unmortgaged " + _tile.Name;
        }

        public override bool Executable(Player player)
        {
            return player.Cash >= _tile.LandCost/2*1.1*3;
        }
    }
}
