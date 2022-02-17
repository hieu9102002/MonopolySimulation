using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonopolyFinal.Players;

namespace MonopolyFinal.Actions
{
    public abstract class Action
    {
        public abstract string Execute(Player player, Board board);

        public virtual bool Executable(Player player)
        {
            return true;
        }
    }
}
