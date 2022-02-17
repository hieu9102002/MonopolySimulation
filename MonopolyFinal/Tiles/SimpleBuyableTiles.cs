using MonopolyFinal.Actions;
using MonopolyFinal.Enums;
using MonopolyFinal.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyFinal.Tiles
{
    public class SimpleBuyableTiles : Tile, IComparable<SimpleBuyableTiles>
    {
        protected int _landCost;
        protected Player _owner;
        protected bool _isMortgaged, _isMonopoly;
        protected Board gameboard;
        private Group _group;
        public bool IsMortgaged { get { return _isMortgaged; } set { _isMortgaged = value; } }
        public bool IsMonopoly { get { return _isMonopoly; } set { _isMonopoly = value; } }
        public Group Group { get { return _group; } }
        public Player Owner { get { return _owner; } set { _owner = value; } }
        public int LandCost { get { return _landCost; } }
        public virtual int NoHouses { get { return 0; } } //dumb function for comparing, don't bother

        public SimpleBuyableTiles(TileName tiles, int landCost, Group group, Board gameboard) : base(tiles)
        {
            this._landCost = landCost;
            this._group = group;
            this.gameboard = gameboard;
        }

        public override Actions.Action GetAction(Player player, string special = "")
        {
            if (this._owner == player || this._isMortgaged)
                return new DoNothingAction();
            else if (this._owner == null)
            {
                if (player.CanBuyTile(this._landCost, this._group))
                    return new BuyTileAction(this);
                else // implement trade
                    return new DoNothingAction();
            }
            else
                return new PayOtherPlayerAction(CalculateRent(special), _owner);
        }
        public int CompareTo(SimpleBuyableTiles otherTile)
        {
            if (otherTile._group != this._group)
            {
                if (this._owner.GroupStillNeed[((int)this._group)] < otherTile._owner.GroupStillNeed[(int)otherTile._group])
                {
                    return 1;
                }
                else if (this._owner.GroupStillNeed[((int)this._group)] > otherTile._owner.GroupStillNeed[(int)otherTile._group])
                {
                    return -1;
                }
                else
                {
                    return ((int)this._group).CompareTo(((int)otherTile._group));
                }
            }
            else
            {
                return -this.NoHouses.CompareTo(otherTile.NoHouses);
            }
        }
        protected virtual int CalculateRent(string special)
        {
            if (this.Group == Group.Utility)
            {
                if (this._isMonopoly || special == "utility_chance")
                {
                    int rent = (Game.RandomInstance.Next(1, 7) + Game.RandomInstance.Next(1, 7)) * 10;
                    return rent;
                }
                else
                {
                    int rent = (Game.RandomInstance.Next(1, 7) + Game.RandomInstance.Next(1, 7)) * 4;
                    return rent;
                }
            }
            else
            {
                int rent = 25 * (int)(Math.Pow(2, 3 - this._owner.GroupStillNeed[((int)Group.Rail)])); //magic, don't ask why or how
                return special == "rail_chance" ? rent * 2 : rent;
            }
        }
        public virtual void SellOrMortgage(Player player)
        {
            this._isMortgaged = true;
            player.Cash += (this._landCost / 2);
            player.MortgagedProperties.Add(this);
        }

        public virtual void SetHouseToZero()
        {
            //empty
        }

        public void Unmortgage(Player player)
        {
            this._isMortgaged = false;
            player.Cash-=((int)(this._landCost / 2 * 1.1));
            player.MortgagedProperties.RemoveAt(0);
        }
    }
}
