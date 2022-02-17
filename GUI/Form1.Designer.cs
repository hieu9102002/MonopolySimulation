
namespace GUI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.PlayerNo = new System.Windows.Forms.NumericUpDown();
            this.GamesNo = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.MaxMoves = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.OutputLabel = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.TurnsBeforeEnd = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PlayerNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GamesNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxMoves)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TurnsBeforeEnd)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Players";
            // 
            // PlayerNo
            // 
            this.PlayerNo.Location = new System.Drawing.Point(107, 115);
            this.PlayerNo.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.PlayerNo.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.PlayerNo.Name = "PlayerNo";
            this.PlayerNo.Size = new System.Drawing.Size(120, 22);
            this.PlayerNo.TabIndex = 2;
            this.PlayerNo.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // GamesNo
            // 
            this.GamesNo.Location = new System.Drawing.Point(107, 162);
            this.GamesNo.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.GamesNo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.GamesNo.Name = "GamesNo";
            this.GamesNo.Size = new System.Drawing.Size(120, 22);
            this.GamesNo.TabIndex = 3;
            this.GamesNo.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 164);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Games";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(82, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Play Simulation";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(94, 230);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Play";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MaxMoves
            // 
            this.MaxMoves.Location = new System.Drawing.Point(109, 197);
            this.MaxMoves.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.MaxMoves.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.MaxMoves.Name = "MaxMoves";
            this.MaxMoves.Size = new System.Drawing.Size(120, 22);
            this.MaxMoves.TabIndex = 7;
            this.MaxMoves.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 197);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "MaxMoves";
            // 
            // OutputLabel
            // 
            this.OutputLabel.AutoSize = true;
            this.OutputLabel.Location = new System.Drawing.Point(104, 327);
            this.OutputLabel.Name = "OutputLabel";
            this.OutputLabel.Size = new System.Drawing.Size(39, 16);
            this.OutputLabel.TabIndex = 9;
            this.OutputLabel.Text = "Await";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(489, 182);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(177, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "AnalyseSimulation";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(489, 230);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(177, 23);
            this.button3.TabIndex = 11;
            this.button3.Text = "Analyse One Game";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(94, 260);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 12;
            this.button4.Text = "Generate";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // TurnsBeforeEnd
            // 
            this.TurnsBeforeEnd.Location = new System.Drawing.Point(109, 360);
            this.TurnsBeforeEnd.Name = "TurnsBeforeEnd";
            this.TurnsBeforeEnd.Size = new System.Drawing.Size(120, 22);
            this.TurnsBeforeEnd.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 362);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 16);
            this.label5.TabIndex = 14;
            this.label5.Text = "TurnBeforeEnd";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TurnsBeforeEnd);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.OutputLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.MaxMoves);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.GamesNo);
            this.Controls.Add(this.PlayerNo);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.PlayerNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GamesNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxMoves)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TurnsBeforeEnd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown PlayerNo;
        private System.Windows.Forms.NumericUpDown GamesNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown MaxMoves;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label OutputLabel;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.NumericUpDown TurnsBeforeEnd;
        private System.Windows.Forms.Label label5;
    }
}