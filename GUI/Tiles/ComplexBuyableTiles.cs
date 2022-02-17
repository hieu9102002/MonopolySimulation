using MonopolyFinal.Enums;
using MonopolyFinal.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyFinal.Tiles
{
    public class ComplexBuyableTiles : SimpleBuyableTiles
    {
        private int houseCost, baseRent, noHouses;
        private int[] houseRent;

        public override int NoHouses { get { return this.noHouses; } }
        public int HouseCost { get { return this.houseCost; } }
        public int BaseRent { get { return this.baseRent; } }

        public ComplexBuyableTiles(TileName tiles, int landCost, int baseRent, int houseCost, int[] houseRent, Group group, Board gameboard) : base(tiles, landCost, group, gameboard)
        {
            this.baseRent = baseRent;
            this.houseCost = houseCost;
            this.houseRent = houseRent;
        }

        protected override int CalculateRent(string special = "none")
        {
            if (this.noHouses > 0)
            {
                return houseRent[noHouses - 1];
            }
            else if (this._isMonopoly)
            {
                return 2 * this.baseRent;
            }
            else
            {
                return this.baseRent;
            }
        }

        public override void SellOrMortgage(Player player)
        {
            if (this.noHouses == 5) // sell off Hotel (In monopoly rules, if you don't have enough houses, you have to sell off the entire hotel instead of breaking it into houses)
            {
                if (this.gameboard.HousesLeft >= 4) // if enough houses to sell off the the hotel
                {
                    player.Cash+=(this.houseCost / 2);
                    this.noHouses = 4;
                    gameboard.HotelsLeft++;
                    gameboard.HousesLeft -= 4;
                }
                else // not enough houses
                {
                    player.Cash+=(this.houseCost * 5 / 2);
                    this.noHouses = 0;
                    gameboard.HotelsLeft++;
                }
            }
            else if (this.noHouses > 0)
            {
                player.Cash+=(this.houseCost / 2);
                this.noHouses--;
                gameboard.HousesLeft++;
            }
            else
            {
                base.SellOrMortgage(player);
            }
        }
        public override void SetHouseToZero()
        {
            noHouses = 0;
        }

        public bool BuildHouse(Board gameboard, Player player)
        {
            if (player == this._owner && !this._isMortgaged && this._isMonopoly)
            {
                if (this.noHouses == 4 && gameboard.HotelsLeft > 0)
                {
                    this.noHouses++;
                    gameboard.HousesLeft += 4;
                    gameboard.HotelsLeft--;
                    this._owner.Cash-=(this.houseCost);

                    return true;
                }

                if (this.noHouses <= 4 && gameboard.HousesLeft > 0)
                {
                    this.noHouses++;
                    gameboard.HousesLeft--;
                    this._owner.Cash-=(this.houseCost);

                    return true;
                }
            }

            return false;
        }
    }
}
