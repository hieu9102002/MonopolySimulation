using MonopolyFinal.Actions;
using MonopolyFinal.Enums;
using MonopolyFinal.Players;

namespace MonopolyFinal.Tiles
{
    public class GoToJail : Tile
    {
        public GoToJail() : base(TileName.GoJail) { }
        public override Actions.Action GetAction(Player player, string special = "")
        {
            return new GoToJailAction();
        }
    }
}
