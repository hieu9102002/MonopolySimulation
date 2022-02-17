using Accord.Statistics.Visualizations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Stream filename = Form1.filename;
            StreamReader file = new StreamReader(filename);

            int gamesNo = int.Parse(file.ReadLine());
            int playersNo = Int32.Parse(file.ReadLine());
            int maxMoves = Int32.Parse(file.ReadLine());

            int gamesEnded = 0;
            int gamesNotEnded = 0;

            Dictionary<string, int> playerToWins = new Dictionary<string, int>();
            List<double> turnsToWin = new List<double>();
            Dictionary<string, int> monopolyToWins = new Dictionary<string, int>();

            for (int i = 0; i < playersNo; i++)
            {
                playerToWins.Add("Player " + i, 0);
            }

            foreach (string monopolyColour in Enum.GetNames(typeof(MonopolyFinal.Enums.Group)))
            {
                if (monopolyColour == "Utility" || monopolyColour == "Rail") continue;
                monopolyToWins.Add(monopolyColour, 0);
            }



            playerToWins.Add("null", 0);

            for (int i = 0; i < gamesNo; i++)
            {
                string playerWon = file.ReadLine();
                double noTurns = double.Parse(file.ReadLine());
                string rawMonopolies = file.ReadLine();

                string[] monopolies = rawMonopolies.Split(',');

                foreach (string monopolyColour in monopolies)
                    if (monopolyToWins.ContainsKey(monopolyColour))
                        monopolyToWins[monopolyColour]++;

                playerToWins[playerWon]++;
                if (noTurns != maxMoves)
                {
                    turnsToWin.Add(noTurns);
                    gamesEnded++;
                }
                else
                    gamesNotEnded++;
            }

            for (int i = 0; i < playerToWins.Count; i++)
            {
                chart1.Series["Wins"].Points.AddXY(playerToWins.ElementAt(i).Key, playerToWins.ElementAt(i).Value);
            }

            histogramView1.Histogram = new Histogram().FromData(turnsToWin.ToArray());


            chart2.Series["Games"].Points.AddXY("Games ended", gamesEnded);
            chart2.Series["Games"].Points.AddXY("Games not ended", gamesNotEnded);

            for (int i = 0; i < monopolyToWins.Count; i++)
            {
                chart3.Series["GamesWon"].Points.AddXY(monopolyToWins.ElementAt(i).Key, monopolyToWins.ElementAt(i).Value);
            }

            file.Close();
        }
    }
}
