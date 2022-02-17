using MonopolyFinal.Actions;
using MonopolyFinal.Enums;
using MonopolyFinal.Players;
using MonopolyFinal.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyFinal
{
    public class Board
    {
        private Player[] players;
        public Player[] Players { get { return players; } }
        public Player Winner { get; private set; }
        private List<Group> winnerMonopolies;
        public Tile[] Gameboard { get; }
        private int housesLeft, hotelsLeft;
        public int HousesLeft { get { return housesLeft; } set { housesLeft = value; } }
        public int HotelsLeft { get { return hotelsLeft; } set { hotelsLeft = value; } }

        public List<int>[] PropertyMap { get; }

        private Queue<int> chanceCards, communityCards;

        private int playersRemaining;
        public int PlayersRemaining { get { return playersRemaining; } }

        public Board(Player[] player)
        {
            this.players = player;
            Gameboard = new Tile[40]
            {
                new UselessTile(TileName.Go),
                new ComplexBuyableTiles(TileName.Brown1,60,2,50,new int[]{10,30,90,160,250},Group.Brown,this),
                new ChanceOrChest(TileName.CommunityChest1,this, false),
                new ComplexBuyableTiles(TileName.Brown2,60,4,50,new int[]{20,60,180,320,450},Group.Brown,this),
                new Tax(TileName.IncomeTax,200),
                new SimpleBuyableTiles(TileName.Rail1,200,Group.Rail,this),
                new ComplexBuyableTiles(TileName.Blue1,100,6,50,new int[]{30,90,270,400,550},Group.Blue,this),
                new ChanceOrChest(TileName.Chance1,this, false),
                new ComplexBuyableTiles(TileName.Blue2,100,6,50,new int[]{30,90,270,400,550},Group.Blue,this),
                new ComplexBuyableTiles(TileName.Blue3,120,8,50,new int[]{40,100,300,450,600},Group.Blue,this),

                new UselessTile(TileName.Jail),
                new ComplexBuyableTiles(TileName.Pink1,140,10,100,new int[]{50,150,450,625,750},Group.Pink,this),
                new SimpleBuyableTiles(TileName.Utility1,150,Group.Utility,this),
                new ComplexBuyableTiles(TileName.Pink2,140,10,100,new int[]{50,150,450,625,750},Group.Pink,this),
                new ComplexBuyableTiles(TileName.Pink3,160,12,100,new int[]{60,180,500,700,900},Group.Pink,this),
                new SimpleBuyableTiles(TileName.Rail2,200, Group.Rail,this),
                new ComplexBuyableTiles(TileName.Orange1,180,14,100,new int[]{70,200,550,750,950},Group.Orange,this),
                new ChanceOrChest(TileName.CommunityChest2,this, false),
                new ComplexBuyableTiles(TileName.Orange2,180,14,100,new int[]{70,200,550,750,950},Group.Orange,this),
                new ComplexBuyableTiles(TileName.Orange3,200,16,100,new int[]{80,220,600,800,1000},Group.Orange,this),

                new UselessTile(TileName.FreeParking),
                new ComplexBuyableTiles(TileName.Red1,220,18,150,new int[]{90,250,700,875,1050},Group.Red,this),
                new ChanceOrChest(TileName.Chance2,this, false),
                new ComplexBuyableTiles(TileName.Red2,220,18,150,new int[]{90,250,700,875,1050},Group.Red,this),
                new ComplexBuyableTiles(TileName.Red3,240,20,150,new int[]{100,300,750,925,1100},Group.Red,this),
                new SimpleBuyableTiles(TileName.Rail3,200, Group.Rail,this),
                new ComplexBuyableTiles(TileName.Yellow1,260,22,150,new int[]{110,330,800,975,1150},Group.Yellow,this),
                new ComplexBuyableTiles(TileName.Yellow2,260,22,150,new int[]{110,330,800,975,1150},Group.Yellow,this),
                new SimpleBuyableTiles(TileName.Utility2,150, Group.Utility,this),
                new ComplexBuyableTiles(TileName.Yellow3,280,24,150,new int[]{120,360,850,1025,1200},Group.Yellow,this),

                new GoToJail(),
                new ComplexBuyableTiles(TileName.Green1,300,26,200,new int[]{130,390,900,1100,1275},Group.Green,this),
                new ComplexBuyableTiles(TileName.Green2,300,26,200,new int[]{130,390,900,1100,1275},Group.Green,this),
                new ChanceOrChest(TileName.CommunityChest3,this, false),
                new ComplexBuyableTiles(TileName.Green3,320,28,200,new int[]{150,450,1000,1200,1400},Group.Green,this),
                new SimpleBuyableTiles(TileName.Rail4,200, Group.Rail,this),
                new ChanceOrChest(TileName.Chance3,this, false),
                new ComplexBuyableTiles(TileName.Purple1,350,35,200,new int[]{175,500,1100,1300,1500},Group.Purple,this),
                new Tax(TileName.LuxuryTax, 100),
                new ComplexBuyableTiles(TileName.Purple2,400,50,200,new int[]{200,600,1400,1700,2000},Group.Purple,this),
            };

            this.housesLeft = 32;
            this.hotelsLeft = 12;

            this.chanceCards = new Queue<int>(this.Shuffle(Enumerable.Range(0, 16).ToArray()));
            this.communityCards = new Queue<int>(this.Shuffle(Enumerable.Range(0, 16).ToArray()));

            this.PropertyMap = new List<int>[]
            {
                new List<int>(new int[]{12,28}),
                new List<int>(new int[]{1,3}),
                new List<int>(new int[]{5,15,25,35}),
                new List<int>(new int[]{6,8,9}),
                new List<int>(new int[]{11,13,14}),
                new List<int>(new int[]{16,18,19}),
                new List<int>(new int[]{21,23,24}),
                new List<int>(new int[]{26,27,29}),
                new List<int>(new int[]{31,32,34}),
                new List<int>(new int[]{37,39})
            };
            this.Winner = null;
            this.playersRemaining = players.Count();
        }
        public int GetCard(bool chanceCard)
        {
            return chanceCard ? this.chanceCards.Dequeue() : this.communityCards.Dequeue();
        }

        public void ReturnCard(int card, bool chanceCard)
        {
            if (chanceCard)
            {
                chanceCards.Enqueue(card);
            }
            else
            {
                communityCards.Enqueue(card);
            }
        }
        public void CheckMonopolies()
        {
            Player[] monopolies = new Player[10];

            bool[] isMonopoly = Enumerable.Repeat(true, 10).ToArray();

            foreach (SimpleBuyableTiles buyableTile in this.Gameboard.OfType<SimpleBuyableTiles>())
            {
                int groupID = ((int)buyableTile.Group);
                Player owner = buyableTile.Owner;

                if (owner == null)
                {
                    isMonopoly[groupID] = false;
                }
                else if (monopolies[groupID] == null)
                {
                    monopolies[groupID] = owner;
                }
                else if (monopolies[groupID] != owner)
                {
                    isMonopoly[groupID] = false;
                }
            }

            foreach (SimpleBuyableTiles buyableTile in this.Gameboard.OfType<SimpleBuyableTiles>())
            {
                int groupID = ((int)buyableTile.Group);

                if (isMonopoly[groupID])
                {
                    buyableTile.IsMonopoly = true;
                }
                else
                {
                    buyableTile.IsMonopoly = false;
                }
            }
        }
        public int[] Shuffle(int[] cards)
        {
            int[] returnCards = cards;
            for (int i = returnCards.Length - 1; i > 0; i--)
            {
                int n = Game.RandomInstance.Next(i + 1);
                int temp = returnCards[i];
                returnCards[i] = returnCards[n];
                returnCards[n] = temp;
            }
            return returnCards;
        }
        public void SellAll(Player player)
        {
            foreach(SimpleBuyableTiles tile in player.AllPropertyList)
            {
                tile.SetHouseToZero();
                tile.Owner = null;
                tile.IsMortgaged = false;
            }
        }

        public string PlayOneTurn()
        {
            string output = "";

            foreach (Player player in players)
            {
                Recalculate();
                List<TradeAction> tradeoffer = new();
                foreach(Player otherPlayer in players)
                {
                    if (otherPlayer != player && !otherPlayer.Lost)
                    {
                        foreach(SimpleBuyableTiles tile in otherPlayer.GetOfferTiles())
                        {
                            tradeoffer.Add(new TradeAction(tile, otherPlayer));
                        }
                    }
                }
                player.PlayerTurn(tradeoffer, this);

                output += player.Cash.ToString() + "\n";
            }


            return output;
        }

        public void Recalculate()
        {
            CheckMonopolies();
            foreach (Player player in players)
            {
                player.RefreshPropertyList(this);
                player.RefreshMonopoliesList();
                player.RefreshOfferTiles(this);
            }
        }

        public bool IsGameOver()
        {
            int playersRemaining = 0;
            foreach (Player player in players)
            {
                if (!player.Lost)
                {
                    playersRemaining++;
                    Winner = player;
                    winnerMonopolies = player.Monopolies;
                }
            }
            this.playersRemaining = playersRemaining;
            return playersRemaining < 2;
        }

        public string GetWinnerMonopolies()
        {
            String output = "";
            foreach (Group groups in winnerMonopolies)
                output += groups.ToString() + ',';
            return output;
        }
    }
}
