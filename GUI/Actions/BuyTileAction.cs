using MonopolyFinal.Players;
using MonopolyFinal.Tiles;

namespace MonopolyFinal.Actions
{
    public class BuyTileAction : Action
    {
        private SimpleBuyableTiles _tile;
        public BuyTileAction(SimpleBuyableTiles tile)
        {
            _tile = tile;
        }
        public override string Execute(Player player, Board board)
        {
            player.Cash -= _tile.LandCost;
            _tile.Owner = player;
            player.GroupStillNeed[(int)_tile.Group]--;

            return player.Name + " bought " + _tile.Name;
        }
    }
}
