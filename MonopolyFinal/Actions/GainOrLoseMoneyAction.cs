using MonopolyFinal.Players;
using System;

namespace MonopolyFinal.Actions
{
    public class GainOrLoseMoneyAction : Action
    {
        private int _money;
        public GainOrLoseMoneyAction(int money)
        {
            _money = money;
        }
        public override string Execute(Player player, Board board)
        {
            player.Cash += _money;
            return _money > 0 ? player.Name + " got from the bank " + _money : player.Name + " paid the bank " + Math.Abs(_money);
        }
    }
}
