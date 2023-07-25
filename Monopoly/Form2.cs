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
    public partial class Form2 : Form
    {
        Form3 game = new Form3();
 
        Image character1, character2, character3, character4, character5, character6, character7, character8;
        bool p1Playing, p2Playing, p3Playing, p4Playing;
        public Form2()
        {
            InitializeComponent();
            InitializeImages();
            SetDefaultBoardSettings();
        }

        private void SetDefaultBoardSettings() //Changes some values in form3(board) ready to start the game
        {
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
            pictureBox4.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            listBox1.Visible = false;
            listBox2.Visible = false;
            listBox3.Visible = false;
            listBox4.Visible = false;

            listBox1.SelectedItem = "Boat";
            listBox2.SelectedItem = "Boat";
            listBox3.SelectedItem = "Boat";
            listBox4.SelectedItem = "Boat";

            p1Playing = true;

            game.pictureBox93.Visible = true;
            game.pictureBox94.Visible = true;
            game.pictureBox95.Visible = false;
            game.pictureBox96.Visible = false;
            game.label1.Visible = true;
            game.label2.Visible = true;
            game.label3.Visible = true;
            game.label4.Visible = true;
            game.label5.Visible = true;
            game.label6.Visible = true;
            game.label7.Visible = true;
            game.label8.Visible = true;
            game.label9.Visible = false;
            game.label10.Visible = false;
            game.label11.Visible = false;
            game.label12.Visible = false;
            game.label13.Visible = false;
            game.label14.Visible = false;
            game.label15.Visible = false;
            game.label16.Visible = false;

            game.label17.Visible = true;
            game.label18.Visible = true;
            game.label19.Visible = true;
            game.label20.Visible = true;
            game.label21.Visible = true;
            game.label22.Visible = true;
            game.label23.Visible = false;
            game.label24.Visible = false;
            game.label25.Visible = false;
            game.label26.Visible = false;
            game.label27.Visible = false;
            game.label28.Visible = false;
        }
        private void InitializeImages() //Sets images to character variable to be used to change character
        { 
            character1 = Properties.Resources.character_Boat;
            character2 = Properties.Resources.character_Boot;
            character3 = Properties.Resources.character_Car;
            character4 = Properties.Resources.character_Dog;
            character5 = Properties.Resources.character_Hat;
            character6 = Properties.Resources.character_Iron;
            character7 = Properties.Resources.character_Shaker;
            character8 = Properties.Resources.character_Wheels;  
        }

        private void CheckDifferentCharacters()
        {
            if (p4Playing == true)
            {
                if (pictureBox1.BackgroundImage == pictureBox2.BackgroundImage || pictureBox1.BackgroundImage == pictureBox3.BackgroundImage || pictureBox1.BackgroundImage == pictureBox4.BackgroundImage ||
                    pictureBox2.BackgroundImage == pictureBox3.BackgroundImage || pictureBox2.BackgroundImage == pictureBox4.BackgroundImage || pictureBox3.BackgroundImage == pictureBox4.BackgroundImage)

                {
                    MessageBox.Show("Please Make Sure You Have Different Characters To Each Other And That You Have Chosen A Character!");
                }
                else
                {
                    game.pictureBox93.BackgroundImage = pictureBox1.BackgroundImage;
                    game.pictureBox94.BackgroundImage = pictureBox2.BackgroundImage;
                    game.pictureBox95.BackgroundImage = pictureBox3.BackgroundImage;
                    game.pictureBox96.BackgroundImage = pictureBox4.BackgroundImage;
                    game.Show();
                    this.Hide();
                }
            }
            else
            {
                if (p3Playing == true)
                {
                    if (pictureBox1.BackgroundImage == pictureBox2.BackgroundImage || pictureBox1.BackgroundImage == pictureBox3.BackgroundImage || pictureBox2.BackgroundImage == pictureBox3.BackgroundImage)
                    {
                        MessageBox.Show("Please Make Sure You Have Different Characters To Each Other And That You Have Chosen A Character!");
                    }
                    else
                    {
                        game.pictureBox93.BackgroundImage = pictureBox1.BackgroundImage;
                        game.pictureBox94.BackgroundImage = pictureBox2.BackgroundImage;
                        game.pictureBox95.BackgroundImage = pictureBox3.BackgroundImage;
                        game.Show();
                        this.Hide();
                    }
                }
                else
                {
                    if (p2Playing == true)
                    {
                        if (pictureBox1.BackgroundImage == pictureBox2.BackgroundImage)
                        {
                            MessageBox.Show("Please Make Sure You Have Different Characters To Each Other And That You Have Chosen A Character!");
                        }
                        else
                        {
                            game.pictureBox93.BackgroundImage = pictureBox1.BackgroundImage;
                            game.pictureBox94.BackgroundImage = pictureBox2.BackgroundImage;
                            game.Show();
                            this.Hide();
                        }
                    }
                    else
                    {
                        if (p1Playing == true)
                        {
                            MessageBox.Show("Please Make Sure You Have Different Characters To Each Other And That You Have Chosen A Character!");
                        }
                        else
                        {
                            game.pictureBox93.BackgroundImage = pictureBox1.BackgroundImage;
                            game.pictureBox94.BackgroundImage = pictureBox2.BackgroundImage;
                            game.Show();
                            this.Hide();
                        }  
                    }
                }
            }
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e) //Character Selection
        {
            if (listBox4.SelectedItem.ToString() == "Boat")
            {
                pictureBox4.BackgroundImage = character1;
            }
            if (listBox4.SelectedItem.ToString() == "Boot")
            {
                pictureBox4.BackgroundImage = character2;
            }
            if (listBox4.SelectedItem.ToString() == "Car")
            {
                pictureBox4.BackgroundImage = character3;
            }
            if (listBox4.SelectedItem.ToString() == "Dog")
            {
                pictureBox4.BackgroundImage = character4;
            }
            if (listBox4.SelectedItem.ToString() == "Hat")
            {
                pictureBox4.BackgroundImage = character5;
            }
            if (listBox4.SelectedItem.ToString() == "Iron")
            {
                pictureBox4.BackgroundImage = character6;
            }
            if (listBox4.SelectedItem.ToString() == "Thimble")
            {
                pictureBox4.BackgroundImage = character7;
            }
            if (listBox4.SelectedItem.ToString() == "Wheel")
            {
                pictureBox4.BackgroundImage = character8;
            }
        }
        private void listBox3_SelectedIndexChanged(object sender, EventArgs e) //Character Selection
        {
            if (listBox3.SelectedItem.ToString() == "Boat")
            {
                pictureBox3.BackgroundImage = character1;
            }
            if (listBox3.SelectedItem.ToString() == "Boot")
            {
                pictureBox3.BackgroundImage = character2;
            }
            if (listBox3.SelectedItem.ToString() == "Car")
            {
                pictureBox3.BackgroundImage = character3;
            }
            if (listBox3.SelectedItem.ToString() == "Dog")
            {
                pictureBox3.BackgroundImage = character4;
            }
            if (listBox3.SelectedItem.ToString() == "Hat")
            {
                pictureBox3.BackgroundImage = character5;
            }
            if (listBox3.SelectedItem.ToString() == "Iron")
            {
                pictureBox3.BackgroundImage = character6;
            }
            if (listBox3.SelectedItem.ToString() == "Thimble")
            {
                pictureBox3.BackgroundImage = character7;
            }
            if (listBox3.SelectedItem.ToString() == "Wheel")
            {
                pictureBox3.BackgroundImage = character8;
            }
        }
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e) //Character Selection
        {
            if (listBox2.SelectedItem.ToString() == "Boat")
            {
                pictureBox2.BackgroundImage = character1;
            }
            if (listBox2.SelectedItem.ToString() == "Boot")
            {
                pictureBox2.BackgroundImage = character2;
            }
            if (listBox2.SelectedItem.ToString() == "Car")
            {
                pictureBox2.BackgroundImage = character3;
            }
            if (listBox2.SelectedItem.ToString() == "Dog")
            {
                pictureBox2.BackgroundImage = character4;
            }
            if (listBox2.SelectedItem.ToString() == "Hat")
            {
                pictureBox2.BackgroundImage = character5;
            }
            if (listBox2.SelectedItem.ToString() == "Iron")
            {
                pictureBox2.BackgroundImage = character6;
            }
            if (listBox2.SelectedItem.ToString() == "Thimble")
            {
                pictureBox2.BackgroundImage = character7;
            }
            if (listBox2.SelectedItem.ToString() == "Wheel")
            {
                pictureBox2.BackgroundImage = character8;
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) //Character Selection
        {
            if (listBox1.SelectedItem.ToString() == "Boat")
            {
                pictureBox1.BackgroundImage = character1;
            }
            if (listBox1.SelectedItem.ToString() == "Boot")
            {
                pictureBox1.BackgroundImage = character2;
            }
            if (listBox1.SelectedItem.ToString() == "Car")
            {
                pictureBox1.BackgroundImage = character3;
            }
            if (listBox1.SelectedItem.ToString() == "Dog")
            {
                pictureBox1.BackgroundImage = character4;
            }
            if (listBox1.SelectedItem.ToString() == "Hat")
            {
                pictureBox1.BackgroundImage = character5;
            }
            if (listBox1.SelectedItem.ToString() == "Iron")
            {
                pictureBox1.BackgroundImage = character6;
            }
            if (listBox1.SelectedItem.ToString() == "Thimble")
            {
                pictureBox1.BackgroundImage = character7;
            }
            if (listBox1.SelectedItem.ToString() == "Wheel")
            {
                pictureBox1.BackgroundImage = character8;
            }
        }

        private void button3_Click(object sender, EventArgs e) //change number of players
        {
            game.label1.Visible = true;
            game.label2.Visible = true;
            game.label3.Visible = true;
            game.label4.Visible = true;
            game.label5.Visible = true;
            game.label6.Visible = true;
            game.label7.Visible = true;
            game.label8.Visible = true;
            game.label9.Visible = false;
            game.label10.Visible = false;
            game.label11.Visible = false;
            game.label12.Visible = false;
            game.label13.Visible = false;
            game.label14.Visible = false;
            game.label15.Visible = false;
            game.label16.Visible = false;

            game.pictureBox93.Visible = true;
            game.pictureBox94.Visible = true;
            game.pictureBox95.Visible = false;
            game.pictureBox96.Visible = false;

            game.pictureBox133.Visible = true;
            game.pictureBox134.Visible = true;
            game.pictureBox135.Visible = false;
            game.pictureBox136.Visible = false;

            pictureBox1.Visible = true;
            pictureBox2.Visible = true;
            pictureBox3.Visible = false;
            pictureBox4.Visible = false;

            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = false;
            label6.Visible = false;

            listBox1.Visible = true;
            listBox2.Visible = true;
            listBox3.Visible = false;
            listBox4.Visible = false;

            listBox3.SelectedItem = "Boat";
            listBox4.SelectedItem = "Boat";

            p1Playing = true;
            p2Playing = true;
            p3Playing = false;
            p4Playing = false;

            game.label17.Visible = true;
            game.label18.Visible = true;
            game.label19.Visible = true;
            game.label20.Visible = true;
            game.label21.Visible = true;
            game.label22.Visible = true;
            game.label23.Visible = false;
            game.label24.Visible = false;
            game.label25.Visible = false;
            game.label26.Visible = false;
            game.label27.Visible = false;
            game.label28.Visible = false;
        }
        private void button4_Click(object sender, EventArgs e) //change number of players
        {
            game.label1.Visible = true;
            game.label2.Visible = true;
            game.label3.Visible = true;
            game.label4.Visible = true;
            game.label5.Visible = true;
            game.label6.Visible = true;
            game.label7.Visible = true;
            game.label8.Visible = true;
            game.label9.Visible = true;
            game.label10.Visible = true;
            game.label11.Visible = true;
            game.label12.Visible = true;
            game.label13.Visible = false;
            game.label14.Visible = false;
            game.label15.Visible = false;
            game.label16.Visible = false;

            pictureBox1.Visible = true;
            pictureBox2.Visible = true;
            pictureBox3.Visible = true;
            pictureBox4.Visible = false;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = false;
            listBox1.Visible = true;
            listBox2.Visible = true;
            listBox3.Visible = true;
            listBox4.Visible = false;

            listBox4.SelectedItem = "Boat";

            p1Playing = true;
            p2Playing = true;
            p3Playing = true;
            p4Playing = false;

            game.label17.Visible = true;
            game.label18.Visible = true;
            game.label19.Visible = true;
            game.label20.Visible = true;
            game.label21.Visible = true;
            game.label22.Visible = true;
            game.label23.Visible = true;
            game.label24.Visible = true;
            game.label25.Visible = true;
            game.label26.Visible = false;
            game.label27.Visible = false;
            game.label28.Visible = false;

            game.pictureBox93.Visible = true;
            game.pictureBox94.Visible = true;
            game.pictureBox95.Visible = true;
            game.pictureBox96.Visible = false;

            game.pictureBox133.Visible = true;
            game.pictureBox134.Visible = true;
            game.pictureBox135.Visible = true;
            game.pictureBox136.Visible = false;
        }
        private void button5_Click(object sender, EventArgs e) //change number of players
        {
            game.label1.Visible = true;
            game.label2.Visible = true;
            game.label3.Visible = true;
            game.label4.Visible = true;
            game.label5.Visible = true;
            game.label6.Visible = true;
            game.label7.Visible = true;
            game.label8.Visible = true;
            game.label9.Visible = true;
            game.label10.Visible = true;
            game.label11.Visible = true;
            game.label12.Visible = true;
            game.label13.Visible = true;
            game.label14.Visible = true;
            game.label15.Visible = true;
            game.label16.Visible = true;

            pictureBox1.Visible = true;
            pictureBox2.Visible = true;
            pictureBox3.Visible = true;
            pictureBox4.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            listBox1.Visible = true;
            listBox2.Visible = true;
            listBox3.Visible = true;
            listBox4.Visible = true;

            p1Playing = true;
            p2Playing = true;
            p3Playing = true;
            p4Playing = true;

            game.label17.Visible = true;
            game.label18.Visible = true;
            game.label19.Visible = true;
            game.label20.Visible = true;
            game.label21.Visible = true;
            game.label22.Visible = true;
            game.label23.Visible = true;
            game.label24.Visible = true;
            game.label25.Visible = true;
            game.label26.Visible = true;
            game.label27.Visible = true;
            game.label28.Visible = true;

            game.pictureBox93.Visible = true;
            game.pictureBox94.Visible = true;
            game.pictureBox95.Visible = true;
            game.pictureBox96.Visible = true;

            game.pictureBox133.Visible = true;
            game.pictureBox134.Visible = true;
            game.pictureBox135.Visible = true;
            game.pictureBox136.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e) //calls Checkdifferentcharacters function to start the game
        {
            CheckDifferentCharacters();
        }
    }
}
