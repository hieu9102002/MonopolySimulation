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
