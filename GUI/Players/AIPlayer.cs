using MonopolyFinal.Actions;
using MonopolyFinal.Enums;
using MonopolyFinal.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonopolyFinal.Players
{
    public class AIPlayer : Player
    {
        private List<SimpleBuyableTiles> _offeredTiles;
        public AIPlayer(string name) : base(name) 
        { 
            _offeredTiles = new List<SimpleBuyableTiles>();
        }
        public override void PlayerTurn(List<TradeAction> tradeOffers, Board board)
        {
            if (Lost) return;
            bool secondTurn;
            do
            {
                secondTurn = false;
                List<Actions.Action> actions = GetAvailableActionsAndMoveOrJail(tradeOffers, board);

                foreach(Actions.Action action in actions)
                {
                    action.Execute(this, board);
                    if (action is EndTurnAction) return;
                    if (action is GoDoublesAction) secondTurn = true;
                }
            }
            while (secondTurn);

            CheckBankrupt(board);
        }

        public override void CheckBankrupt(Board board)
        {
            while (this.Cash < 0)
            {
                SimpleBuyableTiles worstTile = this.ChoosePropertyToDowngrade(board);
                if (worstTile == null)
                {
                    this.Lost = true;
                    board.SellAll(this);
                    this.GroupStillNeed = new int[10] { 2, 2, 4, 3, 3, 3, 3, 3, 3, 2 };
                    return;
                }
                else
                {
                    worstTile.SellOrMortgage(this);
                }
            }
        }

        public SimpleBuyableTiles ChoosePropertyToDowngrade(Board gameboard)
        {
            SortedSet<SimpleBuyableTiles> propertyToDowngrade = new SortedSet<SimpleBuyableTiles>();

            foreach (SimpleBuyableTiles propertyOwned in AllPropertyList)
            {
                if (!propertyOwned.IsMortgaged)
                {
                    propertyToDowngrade.Add(propertyOwned);
                }
            }

            if (propertyToDowngrade.Count() != 0)
            {
                return propertyToDowngrade.ElementAt(0);
            }
            else
            {
                return null;
            }
        }

        public List<Actions.Action> GetAvailableActionsAndMoveOrJail(List<TradeAction> tradeOffers, Board board)
        {
            List<Actions.Action> actions= new List<Actions.Action>();

            if (Lost)
                return actions;

            if (MortgagedProperties.Count != 0)
            {
                MortgagedProperties.Sort(delegate (SimpleBuyableTiles x, SimpleBuyableTiles y)
                {
                    return -x.LandCost.CompareTo(y.LandCost);
                });
                actions.Add(new UnmortgageTileAction(MortgagedProperties[0]));

            }

            foreach (TradeAction tradeAction in tradeOffers)
            {
                if (tradeAction.Executable(this))
                {
                    actions.Add(tradeAction);
                }
            }

            BuildHouseAction buildHouseAction = GetBuildHouseActions(board);
            if (buildHouseAction != null)
                actions.Add(buildHouseAction);

            //roll dices (Get random from 1 to 6 inclusive)
            int dice1 = Game.RandomInstance.Next(1, 7);
            int dice2 = Game.RandomInstance.Next(1, 7);

            //check for jail
            if (this.jailed)
            {
                //TODO: check if early game, if yes, pay to get out asap
                if (dice1 == dice2)
                {
                    this.jailed = false;
                }
                else if (this.HasJailChance)
                {
                    this.HasJailChance = false;
                    board.ReturnCard(6, true);
                    this.jailed = false;
                }
                else if (this.HasJailCommunity)
                {
                    this.HasJailCommunity = false;
                    board.ReturnCard(4, false);
                    this.jailed = false;
                }
                else
                {
                    this.turnJailed += 1;
                    if (this.turnJailed < 3)
                    {
                        actions.Add(new EndTurnAction());
                    }
                    else
                    {
                        //Forced to pay the fine
                        this.Cash-=50;
                        this.jailed=false;
                    }
                }

            }

            //check for double
            if (dice1 == dice2 && !this.jailed)
            {
                actions.Add(new GoDoublesAction());
                this.consecutiveDoubles += 1;

                //if dice doubles more than twice
                if (this.consecutiveDoubles > 2)
                {
                    //jail
                    this.GoToJail();
                    this.consecutiveDoubles = 0;
                }
            }
            else
            {
                //dice not matching => reset and no repeat
                this.consecutiveDoubles = 0;
            }

            //move
            if(!this.jailed) this.MoveForward(dice1 + dice2);

            actions.Add(board.Gameboard[Pos].GetAction(this));

            return actions;
        }

        public BuildHouseAction GetBuildHouseActions(Board board)
        {
            List<BuildHouseAction> buildHouseActions= new List<BuildHouseAction>();

            foreach(Group monopolyGroup in Monopolies)
            {
                int lowestHouse = 5;
                List<ComplexBuyableTiles> tiles = new List<ComplexBuyableTiles>();
                for(int i = 0; i<board.PropertyMap[(int)monopolyGroup].Count; i++)
                {
                    ComplexBuyableTiles tile = (ComplexBuyableTiles)board.Gameboard[board.PropertyMap[(int)monopolyGroup][i]];
                    tiles.Add(tile);
                    lowestHouse = Math.Min(lowestHouse, tile.NoHouses);
                }
                foreach(ComplexBuyableTiles tile in tiles)
                {
                    if (tile.NoHouses == lowestHouse && tile.NoHouses < 5 && !tile.IsMortgaged)
                        buildHouseActions.Add(new BuildHouseAction(tile));
                }
            }

            buildHouseActions.Sort();

            if (buildHouseActions.Count == 0)
                return null;
            else
                return buildHouseActions[0];
        }

        public override List<SimpleBuyableTiles> GetOfferTiles()
        {
            return _offeredTiles;
        }

        public override void RefreshOfferTiles(Board board)
        {
            List<SimpleBuyableTiles> newWantedList = new List<SimpleBuyableTiles>();
            List<SimpleBuyableTiles> newNoTradeList = new List<SimpleBuyableTiles>();

            for (int i = 0; i < 10; i++)
            {
                if (GroupStillNeed[i] == 0)
                {
                    for (int j = 0; j < board.PropertyMap[i].Count; j++)
                    {
                        int k = board.PropertyMap[i][j];
                        SimpleBuyableTiles tile = (SimpleBuyableTiles)board.Gameboard[k];
                        newNoTradeList.Add(tile);
                    }
                }
                else if (GroupStillNeed[i] == 1)
                {
                    for (int j = 0; j < board.PropertyMap[i].Count; j++)
                    {
                        int k = board.PropertyMap[i][j];
                        SimpleBuyableTiles tile = (SimpleBuyableTiles)board.Gameboard[k];
                        if (tile.Owner != this)
                        {
                            newWantedList.Add(tile);
                        }
                        else
                        {
                            newNoTradeList.Add(tile);
                        }
                    }
                }
            }
            this.WantedTiles = newWantedList;
            this._offeredTiles = this.AllPropertyList.Except(newNoTradeList).ToList();
        }
    }
}
