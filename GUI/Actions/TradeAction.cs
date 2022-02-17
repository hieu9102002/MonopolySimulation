using MonopolyFinal.Players;
using MonopolyFinal.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonopolyFinal.Actions
{
    public class TradeAction : Action
    {
        private SimpleBuyableTiles _offeredTile;
        private Player _seller;
        public SimpleBuyableTiles OfferedTile { get { return _offeredTile; } }

        public TradeAction(SimpleBuyableTiles offeredTile, Player seller)
        {
            _offeredTile = offeredTile;
            _seller = seller;
        }

        public override string Execute(Player player, Board board)
        {
            SimpleBuyableTiles tileTrade = Union(player);
            //compensate by money
            bool mineMoreExp = tileTrade.LandCost > _offeredTile.LandCost;
            int priceDiff = Math.Abs(tileTrade.LandCost - _offeredTile.LandCost);

            //have enough money?
            if (mineMoreExp)
            {
                _seller.Cash-=priceDiff;
                player.Cash+=priceDiff;
            }
            else
            {
                _seller.Cash+=priceDiff;
                player.Cash-=priceDiff;
            }
            _offeredTile.Owner = player;
            tileTrade.Owner = _seller;

            player.GroupStillNeed[(int)_offeredTile.Group]--;
            player.GroupStillNeed[(int)tileTrade.Group]++;
            _seller.GroupStillNeed[(int)tileTrade.Group]--;
            _seller.GroupStillNeed[(int)_offeredTile.Group]++;

            return player.Name + " traded " + tileTrade.Name.ToString() + " to " + _seller.Name + " for " + _offeredTile.Name.ToString();
        }

        public override bool Executable(Player player)
        {
            return Union(player) != null;
        }

        private SimpleBuyableTiles Union(Player buyer)
        {
            List<SimpleBuyableTiles> union = _seller.WantedTiles.Intersect(buyer.GetOfferTiles()).ToList();

            if (union.Count == 0)
            {
                return null;
            }
            else
            {
                return union[0];
            }
        }
    }
}
