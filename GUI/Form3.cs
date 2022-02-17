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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Stream filename = Form1.filename;
            StreamReader file = new StreamReader(filename);

            int playersNo = Int32.Parse(file.ReadLine());
            int maxMoves = Int32.Parse(file.ReadLine());

            List<int> player0 = new List<int>();
            List<int> player1 = new List<int>();
            List<int> player2 = new List<int>();
            List<int> player3 = new List<int>();

            while (!file.EndOfStream)
            {
                try
                {
                    player0.Add(int.Parse(file.ReadLine()));
                    player1.Add(int.Parse(file.ReadLine()));
                    player2.Add(int.Parse(file.ReadLine()));
                    player3.Add(int.Parse(file.ReadLine()));
                }
                catch (Exception ex) { break; }
            }

            for (int i = 0; i < player0.Count; i++)
            {
                chart1.Series["Player 0"].Points.AddXY(i, player0.ElementAt(i));
                chart1.Series["Player 1"].Points.AddXY(i, player1.ElementAt(i));
                chart1.Series["Player 2"].Points.AddXY(i, player2.ElementAt(i));
                chart1.Series["Player 3"].Points.AddXY(i, player3.ElementAt(i));
            }

            file.Close();
        }
    }
}
