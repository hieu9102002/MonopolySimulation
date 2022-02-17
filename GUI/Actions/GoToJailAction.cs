using MonopolyFinal.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyFinal.Actions
{
    public class GoToJailAction : Action
    {
        public override string Execute(Player player, Board board)
        {
            player.GoToJail();
            return player.Name + " went to jail";
        }
    }
}
