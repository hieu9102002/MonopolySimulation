using MonopolyFinal.Actions;
using MonopolyFinal.Enums;
using MonopolyFinal.Players;

namespace MonopolyFinal.Tiles
{
    public class Tax : Tile
    {
        private int _tax;
        public Tax(TileName name, int tax) : base(name)
        {
            this._tax = tax;
        }
        public override Actions.Action GetAction(Player player, string special = "")
        {
            return new GainOrLoseMoneyAction(_tax*-1);
        }
    }
}
