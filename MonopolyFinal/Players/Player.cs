using MonopolyFinal.Actions;
using MonopolyFinal.Enums;
using MonopolyFinal.Tiles;
using System;
using System.Collections.Generic;

namespace MonopolyFinal.Players
{
    public abstract class Player
    {
        private string _name;
        private int _cash;
        public int Cash { get { return _cash; } set { _cash = value; } }

        public string Name { get { return _name; } protected set { _name = value; }}

        protected bool jailed;
        protected int turnJailed;

        private int _pos;
        public int Pos { get { return _pos; } protected set { _pos = value; } }
        protected int consecutiveDoubles;

        private bool _lost;
        public bool Lost { get { return _lost; } protected set { _lost = value; } }

        private bool _hasJailChance, _hasJailCommunity;
        public bool HasJailChance { get {  return _hasJailChance; } set {  _hasJailChance = value; } }
        public bool HasJailCommunity { get { return _hasJailCommunity; } set { _hasJailCommunity = value; } }

        private List<SimpleBuyableTiles> _mortgagedProperties;
        public List<SimpleBuyableTiles> MortgagedProperties { get { return _mortgagedProperties; } protected set {  _mortgagedProperties = value; } }

        private List<Group> _monopolies;
        public List<Group> Monopolies { get { return _monopolies; } set { _monopolies = value; } }

        public int[] GroupStillNeed;

        public List<SimpleBuyableTiles> AllPropertyList;
        public List<SimpleBuyableTiles> WantedTiles;

        public Player(string name)
        {
            this._name = name;
            this._cash = 1500;
            this.jailed = false;
            this.turnJailed = 0;
            this._lost = false;
            this._pos = 0;
            this.consecutiveDoubles = 0;
            this._hasJailChance = false;
            this._hasJailCommunity = false;
            this._monopolies = new List<Group>();
            this.GroupStillNeed = new int[10] { 2, 2, 4, 3, 3, 3, 3, 3, 3, 2 };
            this.AllPropertyList = new List<SimpleBuyableTiles>();
            this.WantedTiles = new List<SimpleBuyableTiles>();
            this.MortgagedProperties = new();
        }

        public void MoveTo(int position)
        {
            this._pos = position;
        }

        public void MoveForward(int amount)
        {
            this._pos += amount;
            if (this._pos >= 40)
            {
                this._pos -= 40;
                this._cash+=200;
            }
        }

        public void GoToJail()
        {
            MoveTo((int)TileName.Jail);
            jailed = true;  
            turnJailed= 0;
        }

        public bool CanBuyTile(int landCost, Group group)
        {
            return (this._cash - landCost >= 0);
        }

        public abstract void PlayerTurn(List<TradeAction> tradeOffers, Board board);

        public abstract List<SimpleBuyableTiles> GetOfferTiles();

        public void RefreshPropertyList(Board gameboard)
        {
            List<SimpleBuyableTiles> newPropertyList = new List<SimpleBuyableTiles>();
            for (int i = 0; i < gameboard.Gameboard.Length; i++)
            {
                if (gameboard.Gameboard[i] is SimpleBuyableTiles tile)
                {
                    if (tile.Owner == this)
                    {
                        newPropertyList.Add(tile);
                    }
                }
            }
            this.AllPropertyList = newPropertyList;
        }

        public void RefreshMonopoliesList()
        {
            List<Group> newMonopolyList = new();
            for (int i = 0; i < 10; i++)
            {
                if (GroupStillNeed[i] == 0 && i != (int)Group.Rail && i != (int)Group.Utility)
                {
                    newMonopolyList.Add((Group)i);
                }
            }
            Monopolies = newMonopolyList;
        }

        public void MakeRepairs(Board gameboard, bool fromChance)
        {
            int repairCost = 0;
            foreach (SimpleBuyableTiles tile in AllPropertyList)
            {
                if (tile is ComplexBuyableTiles)
                {
                    if (tile.NoHouses == 5)
                    {
                        repairCost += fromChance ? 100 : 115;
                    }
                    else
                    {
                        repairCost += fromChance ? 25 * tile.NoHouses : 40 * tile.NoHouses;
                    }

                    Cash-=(repairCost);
                }
            }
        }

        public abstract void CheckBankrupt(Board gameboard);
        public abstract void RefreshOfferTiles(Board board);
    }
}
