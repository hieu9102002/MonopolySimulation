using MonopolyFinal;
using System;
using System.IO;
using System.Windows.Forms;

namespace GUI
{
    public partial class Form1 : Form
    {
        public static Stream filename;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                OutputLabel.Text = "One second";
                StreamWriter output = new StreamWriter(dialog.SelectedPath + "\\output.txt");
                if (GamesNo.Value > 1)
                {
                    output.WriteLine(GamesNo.Value);
                    output.WriteLine(PlayerNo.Value);
                    output.WriteLine(MaxMoves.Value);
                    Game game = Game.GameInstance;
                    for (int i = 0; i < GamesNo.Value; i++)
                    {
                        game.Reset((int)PlayerNo.Value, (int)MaxMoves.Value);
                        output.WriteLine(game.Play());
                    }
                }
                else
                {
                    output.WriteLine(PlayerNo.Value);
                    output.WriteLine(MaxMoves.Value);
                    Game game = Game.GameInstance;
                    game.Reset((int)PlayerNo.Value, (int)MaxMoves.Value);
                    output.WriteLine(game.PlayOneGame());
                }
                output.Close();
                OutputLabel.Text = "Finished";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Browse Simulation Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "txt",
                Filter = "txt files (*.txt)|*.txt",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.OpenFile();

                Form2 frm2 = new Form2();
                frm2.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Browse Simulation Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "txt",
                Filter = "txt files (*.txt)|*.txt",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.OpenFile();

                Form3 frm3 = new Form3();
                frm3.Show();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OutputLabel.Text = "One second";
            StreamWriter output = new StreamWriter("C:\\Users\\MSI GF\\Desktop\\Swinburne\\machinelearning\\ex1-ex8-matlab\\ex2\\data.txt");

            Game game = Game.GameInstance;
            for (int i = 0; i < GamesNo.Value; i++)
            {
                game.Reset((int)PlayerNo.Value, (int)MaxMoves.Value);
                output.Write(game.GetData((int)TurnsBeforeEnd.Value));
            }

            output.Close();
            OutputLabel.Text = "Finished";
        }
    }
}
