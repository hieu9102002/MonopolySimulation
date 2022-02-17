using MonopolyFinal.Players;
using MonopolyFinal.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyFinal.Actions
{
    public class BuildHouseAction : Action, IComparable<BuildHouseAction>
    {
        private ComplexBuyableTiles _tile;
        public BuildHouseAction(ComplexBuyableTiles tile)
        {
            _tile = tile;
        }
        public override string Execute(Player player, Board board)
        {
            _tile.BuildHouse(board, player);
            return player.Name + " built house on " + _tile.Name;
        }

        public override bool Executable(Player player)
        {
            return player.Cash>=_tile.HouseCost;
        }

        public int CompareTo(BuildHouseAction other)
        {
            return this._tile.HouseCost == other._tile.HouseCost ? -this._tile.BaseRent.CompareTo(other._tile.BaseRent) : this._tile.HouseCost.CompareTo(other._tile.HouseCost);
        }
    }
}
