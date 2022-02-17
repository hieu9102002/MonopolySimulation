using MonopolyFinal.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyFinal
{
    public class Game
    {
        private static Game _game;
        List<Player> playerList = new List<Player>();
        Board gameboard;
        int maxMoves;
        private static Random random;
        private Game()
        {
            random = new Random(Guid.NewGuid().GetHashCode());
        }

        public static Game GameInstance
        {
            get
            {
                if (_game == null)
                    _game = new Game();
                return _game;
            }
        }

        public static Random RandomInstance { get { return random; } }

        public void Reset(int noPlayer, int maxMoves)
        {
            List<Player> newPlayerList = new List<Player>();
            for (int i = 0; i < noPlayer; i++)
{
                newPlayerList.Add(new AIPlayer("Player " + i.ToString()));
            }
            playerList = newPlayerList;
            gameboard = new Board(playerList.ToArray());
            this.maxMoves = maxMoves;
        }

        public string Play()
        {
            string output = "";
            for (int i = 0; i < maxMoves; i++)
            {
                if (gameboard.IsGameOver())
                {
                    output += gameboard.Winner.Name + "\n";
                    output += i + "\n";
                    output += gameboard.GetWinnerMonopolies();
                    break;
                }

                gameboard.PlayOneTurn();
            }

            if (output == "")
                output += "null\n" + maxMoves + "\n" + "";

            return output;
        }

        public string GetData(int turnGetData)
        {
            string output = "";
            List<List<Array>> boardStateByTurn = new List<List<Array>>();

            for(int i = 0; i < maxMoves; i++)
            {
                boardStateByTurn.Add(gameboard.GetBoardState());
                if (gameboard.IsGameOver())
                {
                    break;
                }
                gameboard.PlayOneTurn();
            }

            int turnEnd = boardStateByTurn.Count * turnGetData / 100;

            if (turnEnd <= 10) return "";

            List<Array> boardStateAtTurn = boardStateByTurn[turnEnd-1];

            int[] PlayersCash = (int[])boardStateAtTurn[boardStateAtTurn.Count-1];

            int PlayerLeft = -1;

            for (int i = 0; i < PlayersCash.Length; i++)
            {
                if (PlayersCash[i] > 0)
                {
                    //output += PlayersCash[i] + ",";
                    PlayerLeft = i;
                    break;
                }
            }

            if (PlayerLeft == -1) return "";

            for (int i = 0; i < boardStateAtTurn.Count-1; i++)
            {
                int[] CastedState = (int[])boardStateAtTurn[i];
                if (CastedState[0] == PlayerLeft) output += CastedState[1] + ",";
                else output += "0,";
            }

            if(gameboard.Winner == gameboard.Players[PlayerLeft])
            {
                output += "1\n";
            }
            else
            {
                output += "0\n";
            }
            return output;
        }

        public string PlayOneGame()
        {
            string output = "";
            foreach (Player player in playerList)
            {
                output += player.Cash.ToString() + "\n";
            }

            for (int i = 0; i < maxMoves; i++)
            {
                if (gameboard.IsGameOver())
                    break;

                output += gameboard.PlayOneTurn();
            }
            return output;
        }
    }
}
