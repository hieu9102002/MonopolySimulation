using MonopolyFinal.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyFinal.Actions
{
    public class PayOtherPlayerAction : Action
    {
        private int _money;
        private Player _otherPlayer;
        public PayOtherPlayerAction(int money, Player otherPlayer)
        {
            _money = money;
            _otherPlayer = otherPlayer;
        }
        public override string Execute(Player player, Board board)
        {
            player.Cash -= _money;
            _otherPlayer.Cash += _money;

            return player.Name + " paid " + _otherPlayer.Name + " $" + _money;
        }
    }
}
