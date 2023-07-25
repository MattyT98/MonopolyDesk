using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monopoly
{
    public partial class Form5 : Form
    {
        Form1 StartScreen = new Form1();
        List<PictureBox> WinnerColour = new List<PictureBox>();
        int winnerPictureBoxAmount;
        
        public Form5()
        {
            InitializeComponent();
            InitializePictures();
            WinnerWinnerChickenDinner();
            timer1.Enabled = true;
        }

        private void InitializePictures() //Adds all of the picture boxes to a list to be manipulated
        {
            WinnerColour.Add(pictureBox1);
            WinnerColour.Add(pictureBox2);
            WinnerColour.Add(pictureBox3);
            WinnerColour.Add(pictureBox4);
            WinnerColour.Add(pictureBox5);
            WinnerColour.Add(pictureBox6);
            WinnerColour.Add(pictureBox7);
            WinnerColour.Add(pictureBox8);
            WinnerColour.Add(pictureBox9);
            WinnerColour.Add(pictureBox10);
            WinnerColour.Add(pictureBox11);
            WinnerColour.Add(pictureBox12);
            WinnerColour.Add(pictureBox13);
            WinnerColour.Add(pictureBox14);
            WinnerColour.Add(pictureBox15);
            WinnerColour.Add(pictureBox16);
            WinnerColour.Add(pictureBox17);
            WinnerColour.Add(pictureBox18);
            WinnerColour.Add(pictureBox19);
            WinnerColour.Add(pictureBox20);
            
            pictureBox1.Visible = true;
            pictureBox3.Visible = true;
            pictureBox5.Visible = true;
            pictureBox7.Visible = true;
            pictureBox9.Visible = true;
            pictureBox11.Visible = true;
            pictureBox13.Visible = true;
            pictureBox15.Visible = true;
            pictureBox17.Visible = true;
            pictureBox19.Visible = true;
            pictureBox2.Visible = false;
            pictureBox4.Visible = false;
            pictureBox6.Visible = false;
            pictureBox8.Visible = false;
            pictureBox10.Visible = false;
            pictureBox12.Visible = false;
            pictureBox14.Visible = false;
            pictureBox16.Visible = false;
            pictureBox18.Visible = false;
            pictureBox20.Visible = false;
        }
        private void WinnerWinnerChickenDinner() //Changes Colour of border depending on winner
        {
            int count = 1;
            winnerPictureBoxAmount = WinnerColour.Count % 19 - 1;
            if (label2.Text == "Player 1")
            {
                while (count < 21)
                {
                    WinnerColour[winnerPictureBoxAmount].BackColor = Color.Red;
                    winnerPictureBoxAmount = winnerPictureBoxAmount + 1;
                    count++;
                }   
            }
            else
            {
                if (label2.Text == "Player 2")
                {
                    while (count < 21)
                    {
                        WinnerColour[winnerPictureBoxAmount].BackColor = Color.Blue;
                        winnerPictureBoxAmount = winnerPictureBoxAmount + 1;
                        count++;
                    }
                }
                else
                {
                    if (label2.Text == "Player 3")
                    {
                        while (count < 21)
                        {
                            WinnerColour[winnerPictureBoxAmount].BackColor = Color.Green;
                            winnerPictureBoxAmount = winnerPictureBoxAmount + 1;
                            count++;
                        }
                    }
                    else
                    {
                        if (label2.Text == "Player 4")
                        {
                            while (count < 21)
                            {
                                WinnerColour[winnerPictureBoxAmount].BackColor = Color.Black;
                                winnerPictureBoxAmount = winnerPictureBoxAmount + 1;
                                count++;
                            }
                        }
                    }
                }
            }
        }
     
        private void button2_Click(object sender, EventArgs e) //Play again
        {
            StartScreen.Show();
            this.Hide();
        }
        private void button1_Click(object sender, EventArgs e) //Exit Game
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
                pictureBox1.Visible = true;
                pictureBox3.Visible = true;
                pictureBox5.Visible = true;
                pictureBox7.Visible = true;
                pictureBox9.Visible = true;
                pictureBox11.Visible = true;
                pictureBox13.Visible = true;
                pictureBox15.Visible = true;
                pictureBox17.Visible = true;
                pictureBox19.Visible = true;

                pictureBox2.Visible = false;
                pictureBox4.Visible = false;
                pictureBox6.Visible = false;
                pictureBox8.Visible = false;
                pictureBox10.Visible = false;
                pictureBox12.Visible = false;
                pictureBox14.Visible = false;
                pictureBox16.Visible = false;
                pictureBox18.Visible = false;
                pictureBox20.Visible = false;

            timer2.Enabled = true;
            timer1.Enabled = false;
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            pictureBox2.Visible = true;
            pictureBox4.Visible = true;
            pictureBox6.Visible = true;
            pictureBox8.Visible = true;
            pictureBox10.Visible = true;
            pictureBox12.Visible = true;
            pictureBox14.Visible = true;
            pictureBox16.Visible = true;
            pictureBox18.Visible = true;
            pictureBox20.Visible = true;
           
            pictureBox1.Visible = false;
            pictureBox3.Visible = false;
            pictureBox5.Visible = false;
            pictureBox7.Visible = false;
            pictureBox9.Visible = false;
            pictureBox11.Visible = false;
            pictureBox13.Visible = false;
            pictureBox15.Visible = false;
            pictureBox17.Visible = false;
            pictureBox19.Visible = false;

            timer1.Enabled = true;
            timer2.Enabled = false;
        }
    }
}
