using MonopolyFinal.Enums;
using MonopolyFinal.Actions;
using MonopolyFinal.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyFinal.Tiles
{
    public abstract class Tile
    {
        private TileName _name;

        public Tile(TileName name)
        {
            _name = name;
        }

        public TileName Name { get {  return _name; } }
        public int PosOnBoard { get { return (int)_name; } }

        public abstract Actions.Action GetAction(Player player, string special = "");
    }
}
