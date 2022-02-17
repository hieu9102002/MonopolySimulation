using MonopolyFinal.Actions;
using MonopolyFinal.Enums;
using MonopolyFinal.Players;
using System;

namespace MonopolyFinal.Tiles
{
    public class ChanceOrChest : Tile
    {
        private Board _board;
        private bool _chanceCard;
        public ChanceOrChest(TileName tiles, Board board, bool chanceCard) : base(tiles)
        {
            this._board = board;
            this._chanceCard = chanceCard;
        }
        public override Actions.Action GetAction(Player player, string special = "")
        {
            int card = _board.GetCard(_chanceCard);
            if (this._chanceCard)
            {
                if (card != 6) // not get out of jail free
                {
                    _board.ReturnCard(card, true);
                }
                switch (card)
                {
                    case 0:
                        //Advance to Go
                        player.Cash+=(200);
                        player.MoveTo((int)TileName.Go);
                        break;
                    case 1:
                        //Advance to Illinois Avenue
                        if (player.Pos >= (int)TileName.Red3) //Will pass go
                        {
                            player.Cash+=(200);
                        }
                        player.MoveTo((int)TileName.Red3);
                        return _board.Gameboard[player.Pos].GetAction(player);
                        break;
                    case 2:
                        //Advance to St. Charles Place
                        if (player.Pos >= (int)TileName.Pink1) //Will pass go
                        {
                            player.Cash += (200);
                        }
                        player.MoveTo((int)TileName.Pink1);
                        return _board.Gameboard[player.Pos].GetAction(player);
                        break;
                    case 3:
                        //Advance to nearest utility
                        if (player.Pos > (int)TileName.Utility1 && player.Pos <= (int)TileName.Utility2) //Closer to water utility
                        {
                            player.MoveTo((int)TileName.Utility2);
                        }
                        else
                        {
                            player.MoveTo((int)TileName.Utility1);
                        }
                        return _board.Gameboard[player.Pos].GetAction(player, "utility_chance");
                        break;
                    case 4:
                        //Advance to nearest Railroad
                        if (player.Pos == (int)TileName.Chance1) //closest railroad
                        {
                            player.MoveTo((int)TileName.Rail2);
                        }
                        else if (player.Pos == (int)TileName.Chance2)
                        {
                            player.MoveTo((int)TileName.Rail3);
                        }
                        else
                        {
                            player.MoveTo((int)TileName.Rail1);
                        }
                        return _board.Gameboard[player.Pos].GetAction(player, "rail_chance");
                        break;
                    case 5:
                        //Bank pays 50
                        return new GainOrLoseMoneyAction(50);
                        break;
                    case 6:
                        //Get out of jail free
                        player.HasJailChance = true;
                        break;
                    case 7:
                        //Go back 3 spaces
                        player.MoveForward(-3);
                        return _board.Gameboard[player.Pos].GetAction(player);
                        break;
                    case 8:
                        //Go to jail
                        return new GoToJailAction();
                        break;
                    case 9:
                        //General repairs
                        player.MakeRepairs(_board, true);
                        break;
                    case 10:
                        //Speeding fine 15
                        return new GainOrLoseMoneyAction(-15);
                        break;
                    case 11:
                        //Go to reading
                        player.MoveTo((int)TileName.Rail1);
                        player.Cash+=(200);
                        return _board.Gameboard[player.Pos].GetAction(player);
                        break;
                    case 12:
                        //Advance to Broadwalk
                        player.MoveTo((int)TileName.Purple2);
                        return _board.Gameboard[player.Pos].GetAction(player);
                        break;
                    case 13:
                        //Elected chairman
                        foreach (Player otherPlayer in _board.Players)
                        {
                            if (otherPlayer != player && !otherPlayer.Lost)
                            {
                                player.Cash-=(50);
                                otherPlayer.Cash+=(50);
                            }
                        }
                        break;
                    case 14:
                        //Loan matures
                        return new GainOrLoseMoneyAction(150);
                        break;
                    case 15:
                        //Crossword competition
                        return new GainOrLoseMoneyAction(100);
                        break;
                }
            }
            else
            {
                if (card != 4) // not get out of jail free
                {
                    _board.ReturnCard(card, false);
                }
                switch (card)
                {
                    case 0:
                        //Advance to Go
                        player.Cash+=200;
                        player.MoveTo((int)TileName.Go);
                        break;
                    case 1:
                        //Bank error
                        return new GainOrLoseMoneyAction(200);
                        break;
                    case 2:
                        //Doctor fees
                        return new GainOrLoseMoneyAction(-50);
                        break;
                    case 3:
                        //Sale of stocks
                        return new GainOrLoseMoneyAction(45);
                        break;
                    case 4:
                        //Get out of jail free
                        player.HasJailCommunity = true;
                        break;
                    case 5:
                        //Go to jail
                        return new GoToJailAction();
                        break;
                    case 6:
                        //Grand opera night
                        foreach (Player otherPlayer in _board.Players)
                        {
                            if (otherPlayer != player && !otherPlayer.Lost)
                            {
                                player.Cash+=(50);
                                otherPlayer.Cash-=(50);
                                otherPlayer.CheckBankrupt(_board);
                            }
                        }
                        break;
                    case 7:
                        //Holiday funds mature
                        return new GainOrLoseMoneyAction(100);
                        break;
                    case 8:
                        //Income tax refund
                        return new GainOrLoseMoneyAction(20);
                        break;
                    case 9:
                        //Hospital fees
                        return new GainOrLoseMoneyAction(-100);
                        break;
                    case 10:
                        //Life insurance matures
                        return new GainOrLoseMoneyAction(-100);
                        break;
                    case 11:
                        //School fees
                        return new GainOrLoseMoneyAction(-150);
                        break;
                    case 12:
                        //Consultancy fees
                        return new GainOrLoseMoneyAction(25);
                        break;
                    case 13:
                        //Street repairs
                        player.MakeRepairs(_board, false);
                        break;
                    case 14:
                        //Beauty contest
                        return new GainOrLoseMoneyAction(10);
                        break;
                    case 15:
                        //Inherit 100
                        return new GainOrLoseMoneyAction(100);
                        break;
                }
            }
            return new DoNothingAction(); //should never run, don't worry
        }
    }
}
