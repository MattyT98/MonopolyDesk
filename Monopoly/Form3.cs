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
    public partial class Form3 : Form
    {
        Form5 EndScreen = new Form5();

        int currentMoneyP1, currentMoneyP2, currentMoneyP3, currentMoneyP4;     //Used to keep track of each players money
        int propertiesOwnedP1, propertiesOwnedP2, propertiesOwnedP3, propertiesOwnedP4; //Used to count how many properties a player owns
        int p1Position, p2Position, p3Position, p4Position;                     //Players location Used in different functions to change different values
        int p1OriginalPosition, p2OriginalPosition, p3OriginalPosition, p4OriginalPosition;

        bool jailOutOfFreeP1, jailOutOfFreeP2, jailOutOfFreeP3, jailOutOfFreeP4; //used to define if can get in or out of jail for free
        bool p1InJail, p2InJail, p3InJail, p4InJail;                             //used to set if someone is in jail or not (Set as false in board set up)

        bool p1BankRupt, p2BankRupt, p3BankRupt, p4BankRupt;
        bool p1Turn, p2Turn, p3Turn, p4Turn;             // variable used to determine which players turn it is
        bool p1Playing, p2Playing, p3Playing, p4Playing; //used to determine which players are playing to allow if statements to 
        bool p1Rolled, p2Rolled, p3Rolled, p4Rolled;     //used to allow players to roll again if they roll a double
        
        List<string> squareNames = new List<string>();             //List of names
        List<int> TileCost = new List<int>();                      //List of costs
        List<bool> TileBought = new List<bool>();                  //Tile bought or not (automatically true for non buyable)
        List<int> TileOwner = new List<int>();                     //Tile Owners 0 - 4 (0 being bank)
        List<PictureBox> TileOwnerImage = new List<PictureBox>();  //Covers price of tile if it is bought
        
        List<int> squareLocationTopP1 = new List<int>(); //p1 Locations in each tile
        List<int> squareLocationLeftP1 = new List<int>();
        List<int> squareLocationTopP2 = new List<int>(); //p2 Locations in each tile
        List<int> squareLocationLeftP2 = new List<int>();
        List<int> squareLocationTopP3 = new List<int>(); //p3 Locations in each tile
        List<int> squareLocationLeftP3 = new List<int>();
        List<int> squareLocationTopP4 = new List<int>(); //p4 Locations in each tile
        List<int> squareLocationLeftP4 = new List<int>();
        //All players space in jail is in the visiting, sepearte ones for in jail used in the function GoToJail

        List<int> propertyRentCost = new List<int>(); //Property rent cost

        public Form3()
        {
            InitializeComponent();
            InitializeSquareLocations();
            InitializeLocation();
            InitializeTileCost();
            InitializePropertyRentCost();
            TileOwned();
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            InitializePlayers();
        }

        private void InitializePlayers() //Sets all the settings chosen in settings form and default for monopoly such as money
        {
            // Sets players that are playing to true or flase based on settings form;
            // 2 players have to play by default so only checks p3 and p4 to see if they are playing
            p1Playing = true;
            p2Playing = true;
            p3Playing = false;
            p4Playing = false;
            if (label9.Visible == true)
                p3Playing = true;
            if (label13.Visible == true)
                p4Playing = true;

            p1Rolled = false;
            p2Rolled = false;
            p3Rolled = false;
            p4Rolled = false;

            // Sets game to start at p1
            p1Turn = true;
            p2Turn = false;
            p3Turn = false;
            p4Turn = false;

            // Sets starting position to 0 which is pass go
            p1Position = 0;
            p2Position = 0;
            p3Position = 0;
            p4Position = 0;

            // Sets all players out of jail
            p1InJail = false;
            p2InJail = false;
            p3InJail = false;
            p4InJail = false;

            // Sets Players to not bankrupted
            p1BankRupt = false;
            p2BankRupt = false;
            p3BankRupt = false;
            p4BankRupt = false;

            // Sets basic information about the players. here it sets all players whether they are playing or not but hides the ones that aren't playing
            currentMoneyP1 = 1500;
            currentMoneyP2 = -1500;
            currentMoneyP3 = -1500;
            currentMoneyP4 = -1500;
            propertiesOwnedP1 = 0;
            propertiesOwnedP2 = 0;
            propertiesOwnedP3 = 0;
            propertiesOwnedP4 = 0;
            jailOutOfFreeP1 = false;
            jailOutOfFreeP2 = false;
            jailOutOfFreeP3 = false;
            jailOutOfFreeP4 = false;

            // Displays the currentMoney for each playe
            label17.Text = "£" + currentMoneyP1.ToString();
            label20.Text = "£" + currentMoneyP2.ToString();
            label23.Text = "£" + currentMoneyP3.ToString();
            label26.Text = "£" + currentMoneyP4.ToString();

            // Displays the current properties own by each player
            label18.Text = propertiesOwnedP1.ToString();
            label21.Text = propertiesOwnedP2.ToString();
            label24.Text = propertiesOwnedP3.ToString();
            label27.Text = propertiesOwnedP4.ToString();

            // Starts each player of without a jail out of free card
            label19.Text = jailOutOfFreeP1.ToString();
            label22.Text = jailOutOfFreeP2.ToString();
            label25.Text = jailOutOfFreeP3.ToString();
            label28.Text = jailOutOfFreeP4.ToString();

            // Visual represenation of whose turn it is (set to p1 by default)
            pictureBox97.Visible = true;
            pictureBox98.Visible = false;
            pictureBox99.Visible = false;
            pictureBox100.Visible = false;
        }
        private void InitializeSquareLocations() //Sets tile Names in string list
        {
            // FBottom Row (10 Spaces - Including Pass Go)
            squareNames.Add("Pass Go");
            squareNames.Add("Old Kent Road");
            squareNames.Add("Community Chest");
            squareNames.Add("Whitechapel Road");
            squareNames.Add("Income Tax");
            squareNames.Add("Kings Cross Station");
            squareNames.Add("The Angel Islington");
            squareNames.Add("Chance");
            squareNames.Add("Euston Road");
            squareNames.Add("Pentonville Road");

            // Left Row (10 Spaces - Including Jail)
            squareNames.Add("Jail");
            squareNames.Add("Pall Mall");
            squareNames.Add("Electric Company");
            squareNames.Add("Whitehall");
            squareNames.Add("Northumrl'd Avenue");
            squareNames.Add("Marylebone Station");
            squareNames.Add("Bow Street");
            squareNames.Add("Community Chest");
            squareNames.Add("Marlborough Street");
            squareNames.Add("Vine Street");

            // Top Row (10 Spaces - Including  Free Parking)
            squareNames.Add("FreeParking");
            squareNames.Add("Strand");
            squareNames.Add("Chance");
            squareNames.Add("Fleet Street");
            squareNames.Add("Trafalgar Square");
            squareNames.Add("Fenchurch St. Station");
            squareNames.Add("Leicester Square");
            squareNames.Add("Coventry Street");
            squareNames.Add("Water Works");
            squareNames.Add("Piccadily");

            // Right Row (9 Spaces - Including Go To Jail)
            squareNames.Add("Go To Jail");
            squareNames.Add("Regent Street");
            squareNames.Add("Community Chest");
            squareNames.Add("Oxford Street");
            squareNames.Add("Bond Street");
            squareNames.Add("Liverpool St. Station");
            squareNames.Add("Chance");
            squareNames.Add("Park Lane");
            squareNames.Add("Super Tax");
            squareNames.Add("Mayfair");
        }
        private void InitializeLocation() //Sets locations of all tiles
        {
            //p1 bottom row - including passgo
            squareLocationTopP1.Add(727);
            squareLocationTopP1.Add(630);
            squareLocationTopP1.Add(567);
            squareLocationTopP1.Add(503);
            squareLocationTopP1.Add(442);
            squareLocationTopP1.Add(378);
            squareLocationTopP1.Add(315);
            squareLocationTopP1.Add(254);
            squareLocationTopP1.Add(191);
            squareLocationTopP1.Add(126);
            squareLocationLeftP1.Add(765);
            squareLocationLeftP1.Add(775);
            squareLocationLeftP1.Add(775);
            squareLocationLeftP1.Add(775);
            squareLocationLeftP1.Add(775);
            squareLocationLeftP1.Add(775);
            squareLocationLeftP1.Add(775);
            squareLocationLeftP1.Add(775);
            squareLocationLeftP1.Add(775);
            squareLocationLeftP1.Add(775);
            //p1 left row - including jail
            squareLocationTopP1.Add(15);
            squareLocationTopP1.Add(27);
            squareLocationTopP1.Add(27);
            squareLocationTopP1.Add(27);
            squareLocationTopP1.Add(27);
            squareLocationTopP1.Add(27);
            squareLocationTopP1.Add(27);
            squareLocationTopP1.Add(27);
            squareLocationTopP1.Add(27);
            squareLocationTopP1.Add(27);
            squareLocationLeftP1.Add(789);
            squareLocationLeftP1.Add(676);
            squareLocationLeftP1.Add(613);
            squareLocationLeftP1.Add(550);
            squareLocationLeftP1.Add(484);
            squareLocationLeftP1.Add(419);
            squareLocationLeftP1.Add(354);
            squareLocationLeftP1.Add(292);
            squareLocationLeftP1.Add(227);
            squareLocationLeftP1.Add(163);
            //p1 top row - including free parking
            squareLocationTopP1.Add(44);
            squareLocationTopP1.Add(131);
            squareLocationTopP1.Add(196);
            squareLocationTopP1.Add(261);
            squareLocationTopP1.Add(321);
            squareLocationTopP1.Add(385);
            squareLocationTopP1.Add(449);
            squareLocationTopP1.Add(508);
            squareLocationTopP1.Add(574);
            squareLocationTopP1.Add(637);
            squareLocationLeftP1.Add(69);
            squareLocationLeftP1.Add(58);
            squareLocationLeftP1.Add(58);
            squareLocationLeftP1.Add(58);
            squareLocationLeftP1.Add(58);
            squareLocationLeftP1.Add(58);
            squareLocationLeftP1.Add(58);
            squareLocationLeftP1.Add(58);
            squareLocationLeftP1.Add(58);
            squareLocationLeftP1.Add(58);
            //p1 right row - including goto jail
            squareLocationTopP1.Add(726);
            squareLocationTopP1.Add(744);
            squareLocationTopP1.Add(744);
            squareLocationTopP1.Add(744);
            squareLocationTopP1.Add(744);
            squareLocationTopP1.Add(744);
            squareLocationTopP1.Add(744);
            squareLocationTopP1.Add(744);
            squareLocationTopP1.Add(744);
            squareLocationTopP1.Add(744);
            squareLocationLeftP1.Add(29);
            squareLocationLeftP1.Add(162);
            squareLocationLeftP1.Add(223);
            squareLocationLeftP1.Add(290);
            squareLocationLeftP1.Add(352);
            squareLocationLeftP1.Add(419);
            squareLocationLeftP1.Add(483);
            squareLocationLeftP1.Add(549);
            squareLocationLeftP1.Add(613);
            squareLocationLeftP1.Add(675);

            //p2 bottom row - including passgo
            squareLocationTopP2.Add(747);
            squareLocationTopP2.Add(663);
            squareLocationTopP2.Add(600);
            squareLocationTopP2.Add(536);
            squareLocationTopP2.Add(475);
            squareLocationTopP2.Add(411);
            squareLocationTopP2.Add(348);
            squareLocationTopP2.Add(287);
            squareLocationTopP2.Add(224);
            squareLocationTopP2.Add(159);
            squareLocationLeftP2.Add(765);
            squareLocationLeftP2.Add(775);
            squareLocationLeftP2.Add(775);
            squareLocationLeftP2.Add(775);
            squareLocationLeftP2.Add(775);
            squareLocationLeftP2.Add(775);
            squareLocationLeftP2.Add(775);
            squareLocationLeftP2.Add(775);
            squareLocationLeftP2.Add(775);
            squareLocationLeftP2.Add(775);
            //p2 left row - including jail
            squareLocationTopP2.Add(50);
            squareLocationTopP2.Add(47);
            squareLocationTopP2.Add(47);
            squareLocationTopP2.Add(47);
            squareLocationTopP2.Add(47);
            squareLocationTopP2.Add(47);
            squareLocationTopP2.Add(47);
            squareLocationTopP2.Add(47);
            squareLocationTopP2.Add(47);
            squareLocationTopP2.Add(47);
            squareLocationLeftP2.Add(789);
            squareLocationLeftP2.Add(676);
            squareLocationLeftP2.Add(613);
            squareLocationLeftP2.Add(550);
            squareLocationLeftP2.Add(484);
            squareLocationLeftP2.Add(419);
            squareLocationLeftP2.Add(354);
            squareLocationLeftP2.Add(292);
            squareLocationLeftP2.Add(227);
            squareLocationLeftP2.Add(163);
            //p2 top row - including free parking
            squareLocationTopP2.Add(64);
            squareLocationTopP2.Add(151);
            squareLocationTopP2.Add(216);
            squareLocationTopP2.Add(281);
            squareLocationTopP2.Add(341);
            squareLocationTopP2.Add(405);
            squareLocationTopP2.Add(469);
            squareLocationTopP2.Add(528);
            squareLocationTopP2.Add(594);
            squareLocationTopP2.Add(657);
            squareLocationLeftP2.Add(69);
            squareLocationLeftP2.Add(58);
            squareLocationLeftP2.Add(58);
            squareLocationLeftP2.Add(58);
            squareLocationLeftP2.Add(58);
            squareLocationLeftP2.Add(58);
            squareLocationLeftP2.Add(58);
            squareLocationLeftP2.Add(58);
            squareLocationLeftP2.Add(58);
            squareLocationLeftP2.Add(58);
            //p2 right row - including goto jail
            squareLocationTopP2.Add(746);
            squareLocationTopP2.Add(764);
            squareLocationTopP2.Add(764);
            squareLocationTopP2.Add(764);
            squareLocationTopP2.Add(764);
            squareLocationTopP2.Add(764);
            squareLocationTopP2.Add(764);
            squareLocationTopP2.Add(764);
            squareLocationTopP2.Add(764);
            squareLocationTopP2.Add(764);
            squareLocationLeftP2.Add(69);
            squareLocationLeftP2.Add(162);
            squareLocationLeftP2.Add(233);
            squareLocationLeftP2.Add(290);
            squareLocationLeftP2.Add(352);
            squareLocationLeftP2.Add(419);
            squareLocationLeftP2.Add(483);
            squareLocationLeftP2.Add(549);
            squareLocationLeftP2.Add(613);
            squareLocationLeftP2.Add(675);

            //p3 bottom row - including pass go
            squareLocationTopP3.Add(727);
            squareLocationTopP3.Add(630);
            squareLocationTopP3.Add(567);
            squareLocationTopP3.Add(503);
            squareLocationTopP3.Add(442);
            squareLocationTopP3.Add(378);
            squareLocationTopP3.Add(315);
            squareLocationTopP3.Add(254);
            squareLocationTopP3.Add(191);
            squareLocationTopP3.Add(126);
            squareLocationLeftP3.Add(738);
            squareLocationLeftP3.Add(757);
            squareLocationLeftP3.Add(757);
            squareLocationLeftP3.Add(757);
            squareLocationLeftP3.Add(757);
            squareLocationLeftP3.Add(757);
            squareLocationLeftP3.Add(757);
            squareLocationLeftP3.Add(757);
            squareLocationLeftP3.Add(757);
            squareLocationLeftP3.Add(757);
            //p3 left row - including jail
            squareLocationTopP3.Add(15);
            squareLocationTopP3.Add(27);
            squareLocationTopP3.Add(27);
            squareLocationTopP3.Add(27);
            squareLocationTopP3.Add(27);
            squareLocationTopP3.Add(27);
            squareLocationTopP3.Add(27);
            squareLocationTopP3.Add(27);
            squareLocationTopP3.Add(27);
            squareLocationTopP3.Add(27);
            squareLocationLeftP3.Add(750);
            squareLocationLeftP3.Add(648);
            squareLocationLeftP3.Add(585);
            squareLocationLeftP3.Add(522);
            squareLocationLeftP3.Add(456);
            squareLocationLeftP3.Add(391);
            squareLocationLeftP3.Add(326);
            squareLocationLeftP3.Add(264);
            squareLocationLeftP3.Add(199);
            squareLocationLeftP3.Add(135);
            //p3 top row - including free parking
            squareLocationTopP3.Add(44);
            squareLocationTopP3.Add(131);
            squareLocationTopP3.Add(196);
            squareLocationTopP3.Add(261);
            squareLocationTopP3.Add(321);
            squareLocationTopP3.Add(385);
            squareLocationTopP3.Add(449);
            squareLocationTopP3.Add(508);
            squareLocationTopP3.Add(574);
            squareLocationTopP3.Add(637);
            squareLocationLeftP3.Add(42);
            squareLocationLeftP3.Add(31);
            squareLocationLeftP3.Add(31);
            squareLocationLeftP3.Add(31);
            squareLocationLeftP3.Add(31);
            squareLocationLeftP3.Add(31);
            squareLocationLeftP3.Add(31);
            squareLocationLeftP3.Add(31);
            squareLocationLeftP3.Add(31);
            squareLocationLeftP3.Add(31);
            //p3 right row - including goto jail
            squareLocationTopP3.Add(726);
            squareLocationTopP3.Add(744);
            squareLocationTopP3.Add(744);
            squareLocationTopP3.Add(744);
            squareLocationTopP3.Add(744);
            squareLocationTopP3.Add(744);
            squareLocationTopP3.Add(744);
            squareLocationTopP3.Add(744);
            squareLocationTopP3.Add(744);
            squareLocationTopP3.Add(744);
            squareLocationLeftP3.Add(42);
            squareLocationLeftP3.Add(135);
            squareLocationLeftP3.Add(199);
            squareLocationLeftP3.Add(263);
            squareLocationLeftP3.Add(325);
            squareLocationLeftP3.Add(392);
            squareLocationLeftP3.Add(456);
            squareLocationLeftP3.Add(522);
            squareLocationLeftP3.Add(586);
            squareLocationLeftP3.Add(648);

            //p4 - bottom row - including passgo
            squareLocationTopP4.Add(747);
            squareLocationTopP4.Add(663);
            squareLocationTopP4.Add(600);
            squareLocationTopP4.Add(536);
            squareLocationTopP4.Add(475);
            squareLocationTopP4.Add(411);
            squareLocationTopP4.Add(348);
            squareLocationTopP4.Add(287);
            squareLocationTopP4.Add(224);
            squareLocationTopP4.Add(159);
            squareLocationLeftP4.Add(738);
            squareLocationLeftP4.Add(757);
            squareLocationLeftP4.Add(757);
            squareLocationLeftP4.Add(757);
            squareLocationLeftP4.Add(757);
            squareLocationLeftP4.Add(757);
            squareLocationLeftP4.Add(757);
            squareLocationLeftP4.Add(757);
            squareLocationLeftP4.Add(757);
            squareLocationLeftP4.Add(757);
            //p4 left row - including jail
            squareLocationTopP4.Add(15);
            squareLocationTopP4.Add(47);
            squareLocationTopP4.Add(47);
            squareLocationTopP4.Add(47);
            squareLocationTopP4.Add(47);
            squareLocationTopP4.Add(47);
            squareLocationTopP4.Add(47);
            squareLocationTopP4.Add(47);
            squareLocationTopP4.Add(47);
            squareLocationTopP4.Add(47);

            squareLocationLeftP4.Add(709);
            squareLocationLeftP4.Add(648);

            squareLocationLeftP4.Add(590);
            squareLocationLeftP4.Add(520);
            squareLocationLeftP4.Add(460);
            squareLocationLeftP4.Add(391);
            squareLocationLeftP4.Add(326);
            squareLocationLeftP4.Add(264);
            squareLocationLeftP4.Add(199);
            squareLocationLeftP4.Add(135);
            //p4 top row - including free parking
            squareLocationTopP4.Add(64);
            squareLocationTopP4.Add(151);
            squareLocationTopP4.Add(216);
            squareLocationTopP4.Add(281);
            squareLocationTopP4.Add(341);
            squareLocationTopP4.Add(405);
            squareLocationTopP4.Add(469);
            squareLocationTopP4.Add(528);
            squareLocationTopP4.Add(594);
            squareLocationTopP4.Add(657);
            squareLocationLeftP4.Add(42);
            squareLocationLeftP4.Add(31);
            squareLocationLeftP4.Add(31);
            squareLocationLeftP4.Add(31);
            squareLocationLeftP4.Add(31);
            squareLocationLeftP4.Add(31);
            squareLocationLeftP4.Add(31);
            squareLocationLeftP4.Add(31);
            squareLocationLeftP4.Add(31);
            squareLocationLeftP4.Add(31);
            //p4 right row - including go to jail
            squareLocationTopP4.Add(746);
            squareLocationTopP4.Add(764);
            squareLocationTopP4.Add(764);
            squareLocationTopP4.Add(764);
            squareLocationTopP4.Add(764);
            squareLocationTopP4.Add(764);
            squareLocationTopP4.Add(764);
            squareLocationTopP4.Add(764);
            squareLocationTopP4.Add(764);
            squareLocationTopP4.Add(764);
            squareLocationLeftP4.Add(42);
            squareLocationLeftP4.Add(135);
            squareLocationLeftP4.Add(199);
            squareLocationLeftP4.Add(263);
            squareLocationLeftP4.Add(325);
            squareLocationLeftP4.Add(392);
            squareLocationLeftP4.Add(456);
            squareLocationLeftP4.Add(522);
            squareLocationLeftP4.Add(586);
            squareLocationLeftP4.Add(648);
        }
        private void InitializeTileCost() //Sets Costs of all tiles
        {
            //Bottom Row including passgo
            TileCost.Add(200);    //Passgo
            TileCost.Add(60);
            TileCost.Add(0);    //Community Chest
            TileCost.Add(60);
            TileCost.Add(200);  //Income Tax
            TileCost.Add(200);
            TileCost.Add(100);
            TileCost.Add(0);    //Chance
            TileCost.Add(100);
            TileCost.Add(120);

            //Left Row including jail
            TileCost.Add(0);    //Jail
            TileCost.Add(140);
            TileCost.Add(150);
            TileCost.Add(140);
            TileCost.Add(160);
            TileCost.Add(200);
            TileCost.Add(180);
            TileCost.Add(0);    //Community Chest
            TileCost.Add(180);
            TileCost.Add(200);

            //Top row including freeparking
            TileCost.Add(0);    //Freeparking
            TileCost.Add(220);
            TileCost.Add(0);    //Chance
            TileCost.Add(220);
            TileCost.Add(240);
            TileCost.Add(200);
            TileCost.Add(260);
            TileCost.Add(260);
            TileCost.Add(150);
            TileCost.Add(280);

            //right row including go to jail
            TileCost.Add(0);    //Go To Jail
            TileCost.Add(300);
            TileCost.Add(0);    //Community Chest
            TileCost.Add(300);
            TileCost.Add(320);
            TileCost.Add(200);
            TileCost.Add(0);    //Chance
            TileCost.Add(350);
            TileCost.Add(100);  //Super Tax
            TileCost.Add(400);
        }
        private void InitializePropertyRentCost()
        {
            //Bottom Row including passgo
            propertyRentCost.Add(0);    //Passgo
            propertyRentCost.Add(2);    //Old Kent Road
            propertyRentCost.Add(0);    //Community Chest
            propertyRentCost.Add(4);    //WhiteChapel Road
            propertyRentCost.Add(0);    //Income Tax
            propertyRentCost.Add(25);   //Kings Cross station
            propertyRentCost.Add(6);    // The Angel Islington
            propertyRentCost.Add(0);    //Chance
            propertyRentCost.Add(6);    //Euston Road
            propertyRentCost.Add(8);    //Pentonville Road

            //Left Row including jail
            propertyRentCost.Add(0);    //Jail
            propertyRentCost.Add(10);   //Pall Mall
            propertyRentCost.Add(0);    //electric comapny
            propertyRentCost.Add(10);   //WhiteHall
            propertyRentCost.Add(12);   //Northumberland Avenue
            propertyRentCost.Add(25);   //Marylebone Station
            propertyRentCost.Add(14);   //Bow Street
            propertyRentCost.Add(0);    //Community Chest
            propertyRentCost.Add(14);   //Marlborough Street
            propertyRentCost.Add(16);   // Vine Street

            //Top row including freeparking
            propertyRentCost.Add(0);    //Freeparking
            propertyRentCost.Add(18);   //The Strand
            propertyRentCost.Add(0);    //Chance
            propertyRentCost.Add(18);   //Fleet Street
            propertyRentCost.Add(20);   //Trafalgar Square
            propertyRentCost.Add(25);   //Fenchurch St Station
            propertyRentCost.Add(22);   //Leicester square
            propertyRentCost.Add(22);   //Coventry Street
            propertyRentCost.Add(0);    //Water Works
            propertyRentCost.Add(22);   //Piccadilly

            //right row including go to jail
            propertyRentCost.Add(0);    //Go To Jail
            propertyRentCost.Add(26);   //Regent Street
            propertyRentCost.Add(0);    //Community Chest
            propertyRentCost.Add(26);   //Oxford Street
            propertyRentCost.Add(28);   //Bond Street
            propertyRentCost.Add(25);   //Liverpool Street Station
            propertyRentCost.Add(0);    //Chance
            propertyRentCost.Add(35);   //Park Lane
            propertyRentCost.Add(0);    //Super Tax
            propertyRentCost.Add(50);   //Mayfair
        }
        private void TileOwned() //Sets if properties are owned or not and stops them being bought again if they are. Also stops special squares being bought
        {
            TileBought.Add(true); //Pass GO
            TileBought.Add(false); //Old Kent Road
            TileBought.Add(true); //Community Chest
            TileBought.Add(false); //WhiteChapel Road
            TileBought.Add(true); //Income Tax
            TileBought.Add(false); //Kings Cross Station
            TileBought.Add(false); //The Angel Islington
            TileBought.Add(true); //Chance
            TileBought.Add(false); //Euston Road
            TileBought.Add(false); //Pentonvilleroad

            TileBought.Add(true); //Jail
            TileBought.Add(false); //Pall Mall
            TileBought.Add(false); //Electric Company
            TileBought.Add(false); // Whitehall
            TileBought.Add(false); //Northumrl'd Avenue
            TileBought.Add(false); //Marylebone Station
            TileBought.Add(false); //Bow Street
            TileBought.Add(true); //Community Chest
            TileBought.Add(false); //Malborough Street
            TileBought.Add(false); //Vine Street

            TileBought.Add(true); //FreeParking
            TileBought.Add(false); //Strand
            TileBought.Add(true); //Chance
            TileBought.Add(false); //Fleet Street
            TileBought.Add(false); //Trafalgar Square
            TileBought.Add(false); //Fenchurch St. Station
            TileBought.Add(false); //Leicester Square
            TileBought.Add(false); //Coventry Street
            TileBought.Add(false); //Water Works
            TileBought.Add(false); //Piccadily

            TileBought.Add(true); //Go To Jail
            TileBought.Add(false); //Regent Street
            TileBought.Add(true); //Community CHest
            TileBought.Add(false); //Oxford Street
            TileBought.Add(false); //Bond Street
            TileBought.Add(false); //LiverPool St. Station
            TileBought.Add(true); //Chance
            TileBought.Add(false); //Park Lane
            TileBought.Add(true); //Super Tax
            TileBought.Add(false); //Mayfair

            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);

            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);

            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);

            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);
            TileOwner.Add(0);

            TileOwnerImage.Add(pictureBox45);       //picturebox45 is a filler picture to allow the count to work properly and match up to the players location as not all tiles are buyable
            TileOwnerImage.Add(pictureBox105);
            TileOwnerImage.Add(pictureBox45);
            TileOwnerImage.Add(pictureBox106);
            TileOwnerImage.Add(pictureBox45);
            TileOwnerImage.Add(pictureBox107);
            TileOwnerImage.Add(pictureBox108);
            TileOwnerImage.Add(pictureBox45);
            TileOwnerImage.Add(pictureBox109);
            TileOwnerImage.Add(pictureBox110);

            TileOwnerImage.Add(pictureBox45);
            TileOwnerImage.Add(pictureBox111);
            TileOwnerImage.Add(pictureBox112);
            TileOwnerImage.Add(pictureBox113);
            TileOwnerImage.Add(pictureBox114);
            TileOwnerImage.Add(pictureBox115);
            TileOwnerImage.Add(pictureBox116);
            TileOwnerImage.Add(pictureBox45);
            TileOwnerImage.Add(pictureBox117);
            TileOwnerImage.Add(pictureBox118);

            TileOwnerImage.Add(pictureBox45);
            TileOwnerImage.Add(pictureBox119);
            TileOwnerImage.Add(pictureBox45);
            TileOwnerImage.Add(pictureBox120);
            TileOwnerImage.Add(pictureBox121);
            TileOwnerImage.Add(pictureBox122);
            TileOwnerImage.Add(pictureBox123);
            TileOwnerImage.Add(pictureBox124);
            TileOwnerImage.Add(pictureBox125);
            TileOwnerImage.Add(pictureBox126);

            TileOwnerImage.Add(pictureBox45);
            TileOwnerImage.Add(pictureBox127);
            TileOwnerImage.Add(pictureBox45);
            TileOwnerImage.Add(pictureBox128);
            TileOwnerImage.Add(pictureBox129);
            TileOwnerImage.Add(pictureBox130);
            TileOwnerImage.Add(pictureBox45);
            TileOwnerImage.Add(pictureBox131);
            TileOwnerImage.Add(pictureBox45);
            TileOwnerImage.Add(pictureBox132);
        }
        
        private void button1_Click(object sender, EventArgs e) //Roll Dice, display dice, add totalroll and display
        {
            int rolldice1, rolldice2, totalroll;
            rolldice1 = NumberGenerator(1, 7);
            rolldice2 = NumberGenerator(15, 219) % 6; //Divide value by 6 and use the remainder value for the dice
            rolldice1 = NumberGenerator(2, 2); //Gives number to land on chance
            rolldice2 = NumberGenerator(3,3); //Gives number to land on chance
            if ((rolldice1 < 1) || (rolldice1 > 6))
                rolldice2 = NumberGenerator(1, 7);

            if ((rolldice2 < 1) || (rolldice2 > 6))
                rolldice2 = NumberGenerator(1, 7);

            switch (rolldice1)
            {
                case 1: pictureBox101.BackgroundImage = Properties.Resources.die1; break;
                case 2: pictureBox101.BackgroundImage = Properties.Resources.die2; break;
                case 3: pictureBox101.BackgroundImage = Properties.Resources.die3; break;
                case 4: pictureBox101.BackgroundImage = Properties.Resources.die4; break;
                case 5: pictureBox101.BackgroundImage = Properties.Resources.die5; break;
                case 6: pictureBox101.BackgroundImage = Properties.Resources.die6; break;
            }
            switch (rolldice2)
            {
                case 1: pictureBox102.BackgroundImage = Properties.Resources.die1; break;
                case 2: pictureBox102.BackgroundImage = Properties.Resources.die2; break;
                case 3: pictureBox102.BackgroundImage = Properties.Resources.die3; break;
                case 4: pictureBox102.BackgroundImage = Properties.Resources.die4; break;
                case 5: pictureBox102.BackgroundImage = Properties.Resources.die5; break;
                case 6: pictureBox102.BackgroundImage = Properties.Resources.die6; break;
            }

            totalroll = rolldice1 + rolldice2;
            label29.Text = totalroll.ToString();

            if ((p1Turn == true) && (p1Rolled == false))
            {
                p1OriginalPosition = p1Position;
                if (p1InJail == true)
                {
                    if (rolldice1 == rolldice2)
                    {
                        p1InJail = false;
                        Player1(totalroll, rolldice1, rolldice2);
                       
                        pictureBox93.Left = squareLocationTopP1[p1Position];
                        pictureBox93.Top = squareLocationLeftP1[p1Position];
                    }
                    else
                    {
                        p1Rolled = true;
                    } 
                }
                else
                {
                    Player1(totalroll, rolldice1, rolldice2);
                    pictureBox93.Left = squareLocationTopP1[p1Position];
                    pictureBox93.Top = squareLocationLeftP1[p1Position];
                }
            }
            else
            {
                if ((p1Turn == true) && (p1Rolled == true))
                {
                    MessageBox.Show("You Have Already Rolled");
                }
                else
                {
                    if ((p2Turn == true) && (p2Rolled == false))
                    {
                        p2OriginalPosition = p2Position;
                        if (p2InJail == true)
                        {
                            if (rolldice1 == rolldice2)
                            {
                                p2InJail = false;
                                Player2(totalroll, rolldice1, rolldice2);
                                pictureBox94.Left = squareLocationTopP2[p2Position];
                                pictureBox94.Top = squareLocationLeftP2[p2Position];
                            }
                            else
                            {
                                p2Rolled = true;
                            }
                        }
                        else
                        {
                            Player2(totalroll, rolldice1, rolldice2);
                            pictureBox94.Left = squareLocationTopP2[p2Position];
                            pictureBox94.Top = squareLocationLeftP2[p2Position];
                        }
                    }
                    else
                    {
                        if ((p2Turn == true) && (p2Rolled == true))
                        {
                            MessageBox.Show("You Have Already Rolled");
                        }
                        else
                        {
                            if ((p3Turn == true) && (p3Rolled == false))
                            {
                                p3OriginalPosition = p3Position;
                                if (p3InJail == true)
                                {
                                    if (rolldice1 == rolldice2)
                                    {
                                        p3InJail = false;
                                        Player3(totalroll, rolldice1, rolldice2);
                                        pictureBox95.Left = squareLocationTopP3[p3Position];
                                        pictureBox95.Top = squareLocationLeftP3[p3Position];
                                    }
                                    else
                                    {
                                        p3Rolled = true;
                                    }
                                }
                                else
                                {
                                    Player3(totalroll, rolldice1, rolldice2);
                                    pictureBox95.Left = squareLocationTopP3[p3Position];
                                    pictureBox95.Top = squareLocationLeftP3[p3Position];
                                }
                            }
                            else
                            {
                                if ((p3Turn == true) && (p3Rolled == true))
                                {
                                    MessageBox.Show("You Have Already Rolled");
                                }
                                else
                                {
                                    if ((p4Turn == true) && (p4Rolled == false))
                                    {
                                        p4OriginalPosition = p4Position;
                                        if (p4InJail == true)
                                        {
                                            if (rolldice1 == rolldice2)
                                            {
                                                p4InJail = false;
                                                Player4(totalroll, rolldice1, rolldice2);
                                                pictureBox96.Left = squareLocationTopP4[p4Position];
                                                pictureBox96.Top = squareLocationLeftP4[p4Position];
                                            }
                                        }
                                        else
                                        {
                                            Player4(totalroll, rolldice1, rolldice2);
                                            pictureBox96.Left = squareLocationTopP4[p4Position];
                                            pictureBox96.Top = squareLocationLeftP4[p4Position];
                                        }
                                    }
                                    else
                                    {
                                        if ((p4Turn == true) && (p4Rolled == true))
                                        {
                                            MessageBox.Show("You Have Already Rolled");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            GoToJail();
            CheckGo();
            CheckTax();
            CheckChanceChestSpace();
            PayRent(totalroll);
            CheckBankRupt();
            EndGame();
        }
        private int NumberGenerator(int Number1, int Number2) //Creates random value and returns it function that called it
        {
            return new Random((int)DateTime.Now.Ticks).Next(Number1, Number2);
        }

        private void Player1(int totalroll, int rolldice1, int rolldice2)
        {
            pictureBox97.Visible = true;
            pictureBox98.Visible = false;
            pictureBox99.Visible = false;
            pictureBox100.Visible = false;

            p1Position = MovePlayer(p1Position, totalroll);
            label31.Text = squareNames[p1Position];

            if (TileOwner[p1Position] > 0)
            {
                label32.Text = "Yes; Player " + TileOwner[p1Position];
            }
            else
            {
                label32.Text = "No";
            }
            
            if (rolldice1 == rolldice2)
            {
                p1Rolled = false;
            }
            else
            {
                p1Rolled = true;
            }
        }
        private void Player2(int totalroll, int rolldice1, int rolldice2)
        {
            pictureBox97.Visible = false;
            pictureBox98.Visible = true;
            pictureBox99.Visible = false;
            pictureBox100.Visible = false;

            p2Position = MovePlayer(p2Position, totalroll);
            label31.Text = squareNames[p2Position];

            if (TileOwner[p2Position] > 0)
            {
                label32.Text = "Yes: Player " + TileOwner[p2Position];
            }
            else
            {
                label32.Text = "No";
            }
            if (rolldice1 == rolldice2)
            {
                p2Rolled = false;
            }
            else
            {
                p2Rolled = true;
            }
        }
        private void Player3(int totalroll, int rolldice1, int rolldice2)
        {
            pictureBox97.Visible = false;
            pictureBox98.Visible = false;
            pictureBox99.Visible = true;
            pictureBox100.Visible = false;

            p3Position = MovePlayer(p3Position, totalroll);
            label31.Text = squareNames[p3Position];

            if (TileOwner[p3Position] > 0)
            {
                label32.Text = "Yes; Player " + TileOwner[p3Position];
            }
            else
            {
                label32.Text = "No";
            }
            if (rolldice1 == rolldice2)
            {
                p3Rolled = false;
            }
            else
            {
                p3Rolled = true;
            }
        }
        private void Player4(int totalroll, int rolldice1, int rolldice2)
        {
            pictureBox97.Visible = false;
            pictureBox98.Visible = false;
            pictureBox99.Visible = false;
            pictureBox100.Visible = true;

            p4Position = MovePlayer(p4Position, totalroll);
            label31.Text = squareNames[p4Position];
            if (TileOwner[p1Position] > 0)
            {
                label32.Text = "Yes; Player " + TileOwner[p4Position];
            }
            else
            {
                label32.Text = "No";
            }
            if (rolldice1 == rolldice2)
            {
                p4Rolled = false;
            }
            else
            {
                p4Rolled = true;
            }
        }

        private int MovePlayer(int playerPosition, int diceValue)
        {
            playerPosition += diceValue;

            if (playerPosition > squareNames.Count - 1)
            {
                playerPosition = (playerPosition % squareNames.Count);
            }

            return playerPosition;
        }

        private void button3_Click(object sender, EventArgs e) // Button to perform the EndTurn function
        {
            EndTurn();
        }
        private void EndTurn() //Ends players turn and sets next player turn to true
        {
            pictureBox101.BackgroundImage = null; //clear 1st dice image
            pictureBox102.BackgroundImage = null; //clear 2nd dice image
            label29.Text = "0";                   //set dice total to 0

            if ((p1Turn == true) && (p1Rolled == true)) //Move from player 1 to next player, either 2,3 or 4 depending on bankruptcy and players
            {
                if ((p2Playing == true) && (p2BankRupt == false))
                { 
                    p1Turn = false;
                    p2Turn = true;
                    p1Rolled = false;
                    p2Rolled = false;
                    pictureBox97.Visible = false;
                    pictureBox98.Visible = true;
                    pictureBox99.Visible = false;
                    pictureBox100.Visible = false;
                    label31.Text = squareNames[p2Position];
                }
                else
                {
                    if ((p3Playing == true) && (p3BankRupt == false))
                    {
                        p1Turn = false;
                        p3Turn = true;
                        p1Rolled = false;
                        p3Rolled = false;
                        pictureBox97.Visible = false;
                        pictureBox98.Visible = false;
                        pictureBox99.Visible = true;
                        pictureBox100.Visible = false;
                        label31.Text = squareNames[p3Position];
                        label32.Text = "...";
                    }
                    else
                    {
                        if ((p4Playing == true) && (p4BankRupt == false))
                        {
                            p1Turn = false;
                            p4Turn = true;
                            p1Rolled = false;
                            p4Rolled = false;
                            pictureBox97.Visible = false;
                            pictureBox98.Visible = false;
                            pictureBox99.Visible = false;
                            pictureBox100.Visible = true;
                            label31.Text = squareNames[p4Position];
                            label32.Text = "...";
                        }
                    }
                }
            }
            else
            {
                if ((p2Turn == true) && (p2Rolled == true)) //Move from player 2 to next player, either 1,3 or 4 depending on bankruptcy and players
                {
                    if ((p3Playing == true) && (p3BankRupt == false))
                    {
                        p2Turn = false;
                        p3Turn = true;
                        p2Rolled = false;
                        p3Rolled = false;
                        pictureBox97.Visible = false;
                        pictureBox98.Visible = false;
                        pictureBox99.Visible = true;
                        pictureBox100.Visible = false;
                        label31.Text = squareNames[p3Position];
                        label32.Text = "...";
                    }
                    else
                    {
                        if ((p4Playing == true) && (p4BankRupt == false))
                        {
                            p2Turn = false;
                            p4Turn = true;
                            p2Rolled = false;
                            p4Rolled = false;
                            pictureBox97.Visible = false;
                            pictureBox98.Visible = false;
                            pictureBox99.Visible = false;
                            pictureBox100.Visible = true;
                            label31.Text = squareNames[p4Position];
                            label32.Text = "...";
                        }
                        else
                        {
                            if ((p1Playing == true) && (p1BankRupt == false))
                            {
                                p2Turn = false;
                                p1Turn = true;
                                p2Rolled = false;
                                p1Rolled = false;
                                pictureBox97.Visible = true;
                                pictureBox98.Visible = false;
                                pictureBox99.Visible = false;
                                pictureBox100.Visible = false;
                                label31.Text = squareNames[p1Position];
                                label32.Text = "...";
                            }
                        }
                    }
                }
                else
                {
                    if ((p3Turn == true) && (p3Rolled == true)) //Move from player 3 to next player, either 4,1 or 2 depending on bankruptcy and players
                    {
                        if ((p4Playing == true) && (p4BankRupt == false))
                        {
                            p3Turn = false;
                            p4Turn = true;
                            p2Rolled = false;
                            p4Rolled = false;
                            pictureBox97.Visible = false;
                            pictureBox98.Visible = false;
                            pictureBox99.Visible = false;
                            pictureBox100.Visible = true;
                            label31.Text = squareNames[p4Position];
                            label32.Text = "...";
                        }
                        else
                        {
                            if ((p1Playing == true) && (p1BankRupt == false))
                            {
                                p3Turn = false;
                                p1Turn = true;
                                p3Rolled = false;
                                p1Rolled = false;
                                pictureBox97.Visible = true;
                                pictureBox98.Visible = false;
                                pictureBox99.Visible = false;
                                pictureBox100.Visible = false;
                                label31.Text = squareNames[p1Position];
                                label32.Text = "...";
                            }
                            else
                            {
                                if ((p2Playing == true) && (p2BankRupt == false))
                                {
                                    p3Turn = false;
                                    p2Turn = true;
                                    p3Rolled = false;
                                    p2Rolled = false;
                                    pictureBox97.Visible = false;
                                    pictureBox98.Visible = true;
                                    pictureBox99.Visible = false;
                                    pictureBox100.Visible = false;
                                    label31.Text = squareNames[p2Position];
                                    label32.Text = "...";
                                }
                            }

                        }
                    }
                    else
                    {
                        if ((p4Turn == true) && (p4Rolled == true)) //Move from player 4 to next player, either 1,2 or 3 depending on bankruptcy and players
                        {
                            if ((p1Playing == true) && (p1BankRupt == false))
                            {
                                p4Turn = false;
                                p1Turn = true;
                                p4Rolled = false;
                                p1Rolled = false;
                                pictureBox97.Visible = true;
                                pictureBox98.Visible = false;
                                pictureBox99.Visible = false;
                                pictureBox100.Visible = false;
                                label31.Text = squareNames[p1Position];
                                label32.Text = "...";
                            }
                            else
                            {
                                if ((p2Playing == true) && (p2BankRupt == false))
                                {
                                    p4Turn = false;
                                    p2Turn = true;
                                    p4Rolled = false;
                                    p2Rolled = false;
                                    pictureBox97.Visible = false;
                                    pictureBox98.Visible = true;
                                    pictureBox99.Visible = false;
                                    pictureBox100.Visible = false;
                                    label31.Text = squareNames[p2Position];
                                    label32.Text = "...";
                                }
                                else
                                {
                                    if ((p3Playing == true) && (p3BankRupt == false))
                                    {
                                        p4Turn = false;
                                        p3Turn = true;
                                        p4Rolled = false;
                                        p3Rolled = false;
                                        pictureBox97.Visible = false;
                                        pictureBox98.Visible = false;
                                        pictureBox99.Visible = true;
                                        pictureBox100.Visible = false;
                                        label31.Text = squareNames[p3Position];
                                        label32.Text = "...";
                                    }
                                }
                            }
                        }
                    }
                }
            }

            /*
            if ((p1Turn == true) && (p2Playing == true) && (p1Rolled == true) && (p2BankRupt == false))
            {
                p1Turn = false;
                p2Turn = true;
                p1Rolled = false;
                pictureBox97.Visible = false;
                pictureBox98.Visible = true;
                pictureBox99.Visible = false;
                pictureBox100.Visible = false;
                label31.Text = squareNames[p2Position];
            }
            else
            {
                if ((p2BankRupt == true) && (p3Playing == true)
                {
                    p1Turn = false;
                    p3Turn = true;
                    p1Rolled = false;
                    p2Rolled = false;
                    pictureBox97.Visible = false;
                    pictureBox98.Visible = false;
                    pictureBox99.Visible = true;
                    pictureBox100.Visible = false;
                    label31.Text = squareNames[p3Position];
                }
                else
                {
                    if ((p2Turn == true) && (p3Playing == true) && (p2Rolled == true) && (p3BankRupt == false))
                    {
                        p2Turn = false;
                        p3Turn = true;
                        p1Rolled = false;
                        p2Rolled = false;
                        pictureBox97.Visible = false;
                        pictureBox98.Visible = false;
                        pictureBox99.Visible = true;
                        pictureBox100.Visible = false;
                        label31.Text = squareNames[p3Position];
                    }
                    else
                    {
                        if (p3BankRupt = true)
                    }
                }
                else
                {
                    if ((p2Turn == true) && (p1Playing == true) && (p2Rolled == true) && (p1BankRupt == false))
                    {
                        p1Turn = true;
                        p2Turn = false;
                        p2Rolled = false;
                        p1Rolled = false;
                        pictureBox97.Visible = true;
                        pictureBox98.Visible = false;
                        pictureBox99.Visible = false;
                        pictureBox100.Visible = false;
                        label31.Text = squareNames[p1Position];
                    }
                    else
                    {
                        if ((p3Turn == true) && (p4Playing == true) && (p3Rolled == true ) && (p4BankRupt == false))
                        {
                            p3Turn = false;
                            p4Turn = true;
                            p3Rolled = false;
                            p4Rolled = false;
                            pictureBox97.Visible = false;
                            pictureBox98.Visible = false;
                            pictureBox99.Visible = false;
                            pictureBox100.Visible = true;
                            label31.Text = squareNames[p4Position];
                        }
                        else
                        {
                            if ((p3Turn == true) && (p1Playing == true) && (p3Rolled == true) && (p1BankRupt == false))
                            {
                                p1Turn = true;
                                p3Turn = false;
                                p3Rolled = false;
                                p1Rolled = false;
                                pictureBox97.Visible = true;
                                pictureBox98.Visible = false;
                                pictureBox99.Visible = false;
                                pictureBox100.Visible = false;
                                label31.Text = squareNames[p1Position];
                            }
                            else
                            {
                                if ((p4Turn == true) && (p1Playing == true) && (p4Rolled == true) && (p1BankRupt == false))
                                {
                                    p4Turn = false;
                                    p1Turn = true;
                                    p4Rolled = false;
                                    p1Rolled = false;
                                    pictureBox97.Visible = true;
                                    pictureBox98.Visible = false;
                                    pictureBox99.Visible = false;
                                    pictureBox100.Visible = false;
                                    label31.Text = squareNames[p1Position];
                                }
                            }
                        
                    }
                } 
            } */
        }

        private void button2_Click(object sender, EventArgs e) //Calls functions to buy property
        {
            BuyProperty();
            TileOwnerImages();
            UpdateOwner();
        }
        private void UpdateOwner() //Changes label 31 owner if you buy a tile
        {
            if (p1Turn == true)
            {
                if (TileOwner[p1Position] > 0)
                {
                    label32.Text = "Yes; Player " + TileOwner[p1Position];
                }
                else
                {
                    label32.Text = "No";
                }
            }
            else
            {
                if (p2Turn == true)
                {
                    if (TileOwner[p2Position] > 0)
                    {
                        label32.Text = "Yes; Player " + TileOwner[p2Position];
                    }
                    else
                    {
                        label32.Text = "No";
                    }
                }
                else
                {
                    if (p3Turn == true)
                    {
                        if (TileOwner[p3Position] > 0)
                        {
                            label32.Text = "Yes; Player " + TileOwner[p3Position];
                        }
                        else
                        {
                            label32.Text = "No";
                        }
                    }
                    else
                    {
                        if (p4Turn == true)
                        {
                            if (TileOwner[p4Position] > 0)
                            {
                                label32.Text = "Yes; Player " + TileOwner[p4Position];
                            }
                            else
                            {
                                label32.Text = "No";
                            }
                        }
                    }
                }
            }
        }
        private void BuyProperty() //Sets the owner of a property, changes the players current money
        {
            if (p1Turn == true)
            {
                if (TileBought[p1Position] == false)
                {
                    if (currentMoneyP1 > TileCost[p1Position])
                    {

                        currentMoneyP1 = currentMoneyP1 - TileCost[p1Position];
                        label17.Text = "£" + currentMoneyP1.ToString();

                        propertiesOwnedP1 = propertiesOwnedP1 + 1;
                        label18.Text = propertiesOwnedP1.ToString();

                        TileBought[p1Position] = true;
                        TileOwner[p1Position] = 1;
                    }
                    else
                    {
                        MessageBox.Show("You Don't Have Enough Money For This!");
                    }
                }
                else
                {
                    MessageBox.Show("The Tile Is Already Owned!");
                }
            }
            else  
            {
                if (p2Turn == true)
                {
                    if (TileBought[p2Position] == false)
                    {
                        if (currentMoneyP2 > TileCost[p2Position])
                        {
                            currentMoneyP2 = currentMoneyP2 - TileCost[p2Position];
                            label20.Text = "£" + currentMoneyP2.ToString();

                            propertiesOwnedP2 = propertiesOwnedP2 + 1;
                            label21.Text = propertiesOwnedP2.ToString();

                            TileBought[p2Position] = true;
                            TileOwner[p2Position] = 2;
                        }
                        else
                        {
                            MessageBox.Show("You Don't Have Enough Money For This!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("The Tile Is Already Owned!");
                    } 
                }
                else
                {
                    if (p3Turn == true)
                    {
                        if (TileBought[p3Position] == false)
                        {
                            if (currentMoneyP3 > TileCost[p3Position])
                            {

                                currentMoneyP3 = currentMoneyP3 - TileCost[p3Position];
                                label23.Text = "£" + currentMoneyP3.ToString();

                                propertiesOwnedP3 = propertiesOwnedP3 + 1;
                                label24.Text = propertiesOwnedP3.ToString();

                                TileBought[p3Position] = true;
                                TileOwner[p3Position] = 3;
                            }
                            else
                            {
                                MessageBox.Show("You Don't Have Enough Money For This!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("The Tile Is Already Owned!");
                        }
                    }
                    else
                    {
                        if (p4Turn == true) 
                        {
                            if (TileBought[p4Position] == false)
                            {
                                if (currentMoneyP4 > TileCost[p4Position])
                                {
                                    currentMoneyP4 = currentMoneyP4 - TileCost[p4Position];
                                    label26.Text = "£" + currentMoneyP4.ToString();

                                    propertiesOwnedP4 = propertiesOwnedP4 + 1;
                                    label27.Text = propertiesOwnedP4.ToString();

                                    TileBought[p4Position] = true;
                                    TileOwner[p4Position] = 4;
                                }
                                else
                                {
                                    MessageBox.Show("You Don't Have Enough Money For This!");
                                }
                            }
                            else
                            {
                                MessageBox.Show("The Tile Is Already Owned!");
                            }
                        }
                    }
                }
            }
        }
        private void TileOwnerImages() //Sets visual representation of an owner of a property
        {
            if (TileOwner[p1Position] == 1)
            {
                TileOwnerImage[p1Position].BackColor = Color.Red;
                TileOwnerImage[p1Position].Visible = true;
            }

            if (TileOwner[p2Position] == 2)
            {
                TileOwnerImage[p2Position].BackColor = Color.Blue;
                TileOwnerImage[p2Position].Visible = true;
            }

            if (TileOwner[p3Position] == 3)
            {
                TileOwnerImage[p3Position].BackColor = Color.Lime;
                TileOwnerImage[p3Position].Visible = true;
            }

            if (TileOwner[p4Position] == 4)
            {
                TileOwnerImage[p4Position].BackColor = Color.Black;
                TileOwnerImage[p4Position].Visible = true;
            }
        }

        private void CheckGo() //Checks if a player has passed go or landed on go and gives them +200 to their money
        {
            if (p1Turn == true)
            {
                if ((label31.Text == squareNames[0]) || (p1Position == 0))
                {
                    currentMoneyP1 = currentMoneyP1 + 200;
                    label17.Text = "£" + currentMoneyP1.ToString(); ;
                }
                else
                {
                    if (p1OriginalPosition == 7) 
                    {
                        if (p1Position < 7)
                        {
                            currentMoneyP1 = currentMoneyP1 + 200;
                            label17.Text = "£" + currentMoneyP1.ToString(); ;
                        }
                    }
                    else
                    {
                        if (p1OriginalPosition == 22)
                        {
                            if (p1Position < 22)
                            {
                                currentMoneyP1 = currentMoneyP1 + 200;
                                label17.Text = "£" + currentMoneyP1.ToString(); ;
                            }
                        }
                        else
                        {
                            if (p1OriginalPosition == 36)
                            {
                                if (p1Position < 36)
                                currentMoneyP1 = currentMoneyP1 + 200;
                                label17.Text = "£" + currentMoneyP1.ToString(); ;
                            }
                            else
                            {
                                if ((p1OriginalPosition < 39) && (p1OriginalPosition > 28))
                                {
                                    if ((p1Position >= 0) && (p1Position <= 27))
                                    {
                                        currentMoneyP1 = currentMoneyP1 + 200;
                                        label17.Text = "£" + currentMoneyP1.ToString();
                                    }
                                }
                            }

                        }
                    }
                }
               
            }
            else
            {
                if (p2Turn == true)
                {
                    if ((label31.Text == squareNames[0]) || (p2Position == 0))
                    {
                        currentMoneyP2 = currentMoneyP2 + 200;
                        label20.Text = "£" + currentMoneyP2.ToString(); ;
                    }
                    else
                    {
                        if (p2OriginalPosition == 7)
                        {
                            if (p2Position < 7)
                            {
                                currentMoneyP2 = currentMoneyP2 + 200;
                                label20.Text = "£" + currentMoneyP2.ToString(); ;
                            }
                        }
                        else
                        {
                            if (p2OriginalPosition == 22)
                            {
                                if (p2Position < 22)
                                {
                                    currentMoneyP2 = currentMoneyP2 + 200;
                                    label20.Text = "£" + currentMoneyP2.ToString(); ;
                                }
                            }
                            else
                            {
                                if (p2OriginalPosition == 36)
                                {
                                    if (p2Position < 36)
                                    {
                                        currentMoneyP2 = currentMoneyP2 + 200;
                                        label20.Text = "£" + currentMoneyP2.ToString(); ;
                                    }
                                }
                                else
                                {
                                    if ((p2OriginalPosition < 39) && (p2OriginalPosition > 28))
                                    {
                                        if ((p2Position > 0) && (p2Position <= 27))
                                        {
                                            currentMoneyP2 = currentMoneyP2 + 200;
                                            label20.Text = "£" + currentMoneyP2.ToString();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (p3Turn == true)
                    {
                        if (label31.Text == squareNames[0])
                        {
                            currentMoneyP3 = currentMoneyP3 + 200;
                            label23.Text = "£" + currentMoneyP3.ToString(); ;
                        }
                        else
                        {
                            if (p3OriginalPosition == 7)
                            {
                                if (p3Position < 7)
                                {
                                    currentMoneyP3 = currentMoneyP3 + 200;
                                    label23.Text = "£" + currentMoneyP3.ToString(); ;
                                }
                            }
                            else
                            {
                                if (p3OriginalPosition == 22)
                                {
                                    if (p3Position < 22)
                                    {
                                        currentMoneyP3 = currentMoneyP3 + 200;
                                        label23.Text = "£" + currentMoneyP3.ToString(); ;
                                    }
                                }
                                else
                                {
                                    if (p3OriginalPosition == 36)
                                    {
                                        if (p3Position < 36)
                                        {
                                            currentMoneyP3 = currentMoneyP3 + 200;
                                            label23.Text = "£" + currentMoneyP3.ToString();
                                        }
                                    }
                                    else
                                    {
                                        if ((p3OriginalPosition < 39) && (p3OriginalPosition > 28))
                                        {
                                            if ((p3Position > 0) && (p3Position <= 27))
                                            {
                                                currentMoneyP3 = currentMoneyP3 + 200;
                                                label23.Text = "£" + currentMoneyP3.ToString();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (p4Turn == true)
                        {
                            if (label31.Text == squareNames[0])
                            {
                                currentMoneyP4 = currentMoneyP4 + 200;
                                label26.Text = "£" + currentMoneyP4.ToString(); ;
                            }
                            else
                            {
                                if (p4OriginalPosition == 7)
                                {
                                    if (p4Position < 7)
                                    {
                                        currentMoneyP4 = currentMoneyP4 + 200;
                                        label26.Text = "£" + currentMoneyP4.ToString(); ;
                                    }
                                }
                                else
                                {
                                    if (p4OriginalPosition == 22)
                                    {
                                        if (p4Position < 22)
                                        {
                                            currentMoneyP4 = currentMoneyP4 + 200;
                                            label26.Text = "£" + currentMoneyP4.ToString(); ;
                                        }
                                    }
                                    else
                                    {
                                        if (p4OriginalPosition == 36)
                                        {
                                            if (p4Position < 36)
                                            {
                                                currentMoneyP4 = currentMoneyP4 + 200;
                                                label26.Text = "£" + currentMoneyP4.ToString();
                                            }
                                        }
                                        else
                                        {
                                            if ((p4OriginalPosition < 39) && (p4OriginalPosition > 28))
                                            {
                                                if ((p4Position > 0) && (p4Position <= 27))
                                                {
                                                    currentMoneyP4 = currentMoneyP4 + 200;
                                                    label26.Text = "£" + currentMoneyP4.ToString();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }    
        }
        private void CheckTax() //Checks if you've landed on a tax square and subtracts money if you have
        {
            if (p1Turn == true)
            {
                if (label31.Text == squareNames[4])
                {
                    currentMoneyP1 = currentMoneyP1 - 200;
                    label17.Text = "£" + currentMoneyP1.ToString();
                }
                if (label31.Text == squareNames[38])
                {
                    currentMoneyP1 = currentMoneyP1 - 100;
                    label17.Text = "£" + currentMoneyP1.ToString();
                }
                
            }
            else
            {
                if (p2Turn == true)
                {
                    if (label31.Text == squareNames[4])
                    {
                        currentMoneyP2 = currentMoneyP2 - 200;
                        label20.Text = "£" + currentMoneyP2.ToString();
                    }
                    if (label31.Text == squareNames[38])
                    {
                        currentMoneyP1 = currentMoneyP2 - 100;
                        label20.Text = "£" + currentMoneyP2.ToString();
                    }
                }
                else
                {
                    if (p3Turn == true)
                    {
                        if (label31.Text == squareNames[4])
                        {
                            currentMoneyP3 = currentMoneyP3 - 200;
                            label23.Text = "£" + currentMoneyP3.ToString();
                        }
                        if (label31.Text == squareNames[38])
                        {
                            currentMoneyP3 = currentMoneyP3 - 100;
                            label23.Text = "£" + currentMoneyP3.ToString();
                        }
                    }
                    else
                    {
                        if (p4Turn == true)
                        {
                            if (label31.Text == squareNames[4])
                            {
                                currentMoneyP4 = currentMoneyP4 - 200;
                                label26.Text = "£" + currentMoneyP4.ToString();
                            }
                            if (label31.Text == squareNames[38])
                            {
                                currentMoneyP1 = currentMoneyP4 - 100;
                                label26.Text = "£" + currentMoneyP4.ToString();
                            }
                        }
                    }
                }
            }
        }
        private void GoToJail() //Checks if you've landed on gotojail and sends you to jail if you have
        {
            if ((p1Turn == true) && (label31.Text == squareNames[30]))
            {
                p1InJail = true;
                pictureBox93.Top = 746;
                pictureBox93.Left =  60;
                p1Position = 10;
                label31.Text = squareNames[p1Position];
            }
            else
            {
                if ((p2Turn == true) && (label31.Text == squareNames[30]))
                {
                    p2InJail = true;
                    pictureBox94.Top = 752;
                    pictureBox94.Left = 80;
                    p2Position = 10;
                    label31.Text = squareNames[p2Position];
                }
                else
                {
                    if ((p3Turn == true) && (label31.Text == squareNames[30]))
                    {
                        p3InJail = true;
                        pictureBox95.Top = 725;
                        pictureBox95.Left = 60;
                        p3Position = 10;
                        label31.Text = squareNames[p3Position];

                    }
                    else
                    {
                        if ((p4Turn == true) && (label31.Text == squareNames[30]))
                        {
                            p4InJail = true;
                            pictureBox96.Top = 725;
                            pictureBox96.Left =80;
                            p4Position = 10;
                            label31.Text = squareNames[p4Position];
                        }
                    }
                }
            }
        }
        private void CheckBankRupt() //Checks to make sure that your money isn't in negative or sets you as bankrupt
        {
            if ((p1Turn == true) && (p1Playing == true))
            {
                if(currentMoneyP1 < 0)
                {
                    p1BankRupt = true;
                }
            }
            else
            {
                if ((p2Turn == true) && (p2Playing == true))
                {
                    if(currentMoneyP2 < 0)
                    {
                        p2BankRupt = true;
                    }
                }
                else
                {
                    if ((p3Turn == true) && (p3Playing == true))
                    {
                        if(currentMoneyP3 < 0)
                        {
                            p3BankRupt = true;
                        }
                    }
                    else
                    {
                        if ((p4Turn == true) && (p4Playing == true))
                        {
                            if (currentMoneyP4 < 0)
                            {
                                p4BankRupt = true;
                            }
                        }
                    }
                }
            } 
        }

        private void PayRent(int totalroll)
        {
            int rent, playerToPay;
            if (p1Turn == true)
            {
                if ((TileOwner[p1Position] != 1) && (TileOwner[p1Position] != 0))
                {
                    rent = RentCost(p1Position, totalroll);
                    playerToPay = PayPlayer(p1Position);
                    currentMoneyP1 = currentMoneyP1 - rent;
                    label17.Text = "£" + currentMoneyP1;

                    switch (playerToPay)
                    {
                        case 1: currentMoneyP1 = currentMoneyP1 + rent; label17.Text = "£" + currentMoneyP1; break;
                        case 2: currentMoneyP2 = currentMoneyP2 + rent; label20.Text = "£" + currentMoneyP2; break;
                        case 3: currentMoneyP3 = currentMoneyP3 + rent; label23.Text = "£" + currentMoneyP3; break;
                        case 4: currentMoneyP4 = currentMoneyP4 + rent; label26.Text = "£" + currentMoneyP4; break;
                    }
                }
            }
            else
            {
                if (p2Turn == true)
                {
                    if ((TileOwner[p2Position] != 2) && (TileOwner[p2Position] != 0))
                    {
                        rent = RentCost(p2Position, totalroll);
                        playerToPay = PayPlayer(p2Position);
                        currentMoneyP2 = currentMoneyP2 - rent;
                        label20.Text = "£" + currentMoneyP2;

                        switch (playerToPay)
                        {
                            case 1: currentMoneyP1 = currentMoneyP1 + rent; label17.Text = "£" + currentMoneyP1; break;
                            case 2: currentMoneyP2 = currentMoneyP2 + rent; label20.Text = "£" + currentMoneyP2; break;
                            case 3: currentMoneyP3 = currentMoneyP3 + rent; label23.Text = "£" + currentMoneyP3; break;
                            case 4: currentMoneyP4 = currentMoneyP4 + rent; label26.Text = "£" + currentMoneyP4; break;
                        }
                    }
                }
                else
                {
                    if (p3Turn == true)
                    {
                        if ((TileOwner[p2Position] != 3) && (TileOwner[p2Position] != 0))
                        {
                            rent = RentCost(p3Position, totalroll);
                            playerToPay = PayPlayer(p3Position);
                            currentMoneyP3 = currentMoneyP3 - rent;
                            label23.Text = "£" + currentMoneyP3;

                            switch (playerToPay)
                            {
                                case 1: currentMoneyP1 = currentMoneyP1 + rent; label17.Text = "£" + currentMoneyP1; break;
                                case 2: currentMoneyP2 = currentMoneyP2 + rent; label20.Text = "£" + currentMoneyP2; break;
                                case 3: currentMoneyP3 = currentMoneyP3 + rent; label23.Text = "£" + currentMoneyP3; break;
                                case 4: currentMoneyP4 = currentMoneyP4 + rent; label26.Text = "£" + currentMoneyP4; break;
                            }
                        }
                    }
                    else
                    {
                        if (p4Turn == true)
                        {
                            if ((TileOwner[p2Position] != 4) && (TileOwner[p2Position] != 0))
                            {
                                rent = RentCost(p4Position, totalroll);
                                playerToPay = PayPlayer(p4Position);
                                currentMoneyP4 = currentMoneyP4 - rent;
                                label26.Text = "£" + currentMoneyP4;

                                switch (playerToPay)
                                {
                                    case 1: currentMoneyP1 = currentMoneyP1 + rent; label17.Text = "£" + currentMoneyP1; break;
                                    case 2: currentMoneyP2 = currentMoneyP2 + rent; label20.Text = "£" + currentMoneyP2; break;
                                    case 3: currentMoneyP3 = currentMoneyP3 + rent; label23.Text = "£" + currentMoneyP3; break;
                                    case 4: currentMoneyP4 = currentMoneyP4 + rent; label26.Text = "£" + currentMoneyP4; break;
                                }
                            }
                        }
                    }
                }
            }
        }
        private int RentCost(int playerposition, int totalroll)   //looks up amount of rent to pay.
        {
            int rentcost;
            rentcost = propertyRentCost[playerposition];

            if ((playerposition == 5) || (playerposition == 15) || (playerposition == 25) || (playerposition == 35)) //Checks train stations to see if anyone owns more than one to increase rent
            {
                int tilecount = 0;
                switch (playerposition)
                {
                    case 5:
                        if (TileOwner[5] == TileOwner[playerposition])
                        {
                            tilecount++;
                        }
                        if (TileOwner[15] == TileOwner[playerposition])
                        {
                            tilecount++;
                        }
                        if (TileOwner[25] == TileOwner[playerposition])
                        {
                            tilecount++;
                        }
                        if (TileOwner[35] == TileOwner[playerposition])
                        {
                            tilecount++;
                        }
                        break;

                    case 15:
                        if (TileOwner[5] == TileOwner[playerposition])
                        {
                            tilecount++;
                        }
                        if (TileOwner[15] == TileOwner[playerposition])
                        {
                            tilecount++;
                        }
                        if (TileOwner[25] == TileOwner[playerposition])
                        {
                            tilecount++;
                        }
                        if (TileOwner[35] == TileOwner[playerposition])
                        {
                            tilecount++;
                        }
                        break;
                    case 25:
                        if (TileOwner[5] == TileOwner[playerposition])
                        {
                            tilecount++;
                        }
                        if (TileOwner[15] == TileOwner[playerposition])
                        {
                            tilecount++;
                        }
                        if (TileOwner[25] == TileOwner[playerposition])
                        {
                            tilecount++;
                        }
                        if (TileOwner[35] == TileOwner[playerposition])
                        {
                            tilecount++;
                        }
                        break;
                    case 35:
                        if (TileOwner[5] == TileOwner[playerposition])
                        {
                            tilecount++;
                        }
                        if (TileOwner[15] == TileOwner[playerposition])
                        {
                            tilecount++;
                        }
                        if (TileOwner[25] == TileOwner[playerposition])
                        {
                            tilecount++;
                        }
                        if (TileOwner[35] == TileOwner[playerposition])
                        {
                            tilecount++;
                        }
                        break;
                }
                switch (tilecount)
                {
                    case 1: rentcost = 25; break;
                    case 2: rentcost = 50; break;
                    case 3: rentcost = 100; break;
                    case 4: rentcost = 200; break;
                }
            }
            if ((playerposition == 12) || (playerposition == 28)) //Changes rent based on electric company and water plant
            {
                int tilecount = 0;
                switch (playerposition)
                {
                    case 12:
                        if (TileOwner[12] == TileOwner[playerposition])
                        {
                            tilecount++;
                        }
                        if (TileOwner[28] == TileOwner[playerposition])
                        {
                            tilecount++;
                        }
                        break;
                    case 28:
                        if (TileOwner[12] == TileOwner[playerposition])
                        {
                            tilecount++;
                        }
                        if (TileOwner[28] == TileOwner[playerposition])
                        {
                            tilecount++;
                        }
                        break;
                }
                switch (tilecount)
                {
                    case 1: rentcost = totalroll * 4; break;
                    case 2: rentcost = totalroll * 10; break;
                }
            }

            playerposition = rentcost;
            return playerposition;   
        }
        private int PayPlayer(int playerposition) //Tells you which player to pay
        {
            if (TileOwner[playerposition] == 1)
            {
               return 1;
            }
            else
            {
                if (TileOwner[playerposition] == 2)
                {
                    return 2;
                }
                else
                {
                    if (TileOwner[playerposition] == 3)
                    {
                        return 3;
                    }
                    else
                    {
                        if (TileOwner[playerposition] == 4)
                        {
                            return 4;
                        }
                    }
                }
            }
            return 0;
        }
    
        private void CheckChanceChestSpace() // Checks to see if you land on a chance or chest space and then performs an action
        {
            if (p1Turn == true)
            {
                p1Position = ChanceSpace(p1Position);
                p1Position = ChestSpace(p1Position);
                pictureBox93.Left = squareLocationTopP1[p1Position];
                pictureBox93.Top = squareLocationLeftP1[p1Position];
            }
            else
            {
                if (p2Turn == true)
                {
                    p2Position = ChanceSpace(p2Position);
                    p2Position = ChestSpace(p2Position);
                    pictureBox94.Left = squareLocationTopP2[p2Position];
                    pictureBox94.Top = squareLocationLeftP2[p2Position];
                }
                else
                {
                    if (p3Turn == true)
                    {
                        p3Position = ChanceSpace(p3Position);
                        p3Position = ChestSpace(p3Position);
                        pictureBox95.Left = squareLocationTopP3[p3Position];
                        pictureBox95.Top = squareLocationLeftP3[p3Position];
                    }
                    else
                    {
                        if (p4Turn == true)
                        {
                            p4Position = ChanceSpace(p4Position);
                            p4Position = ChestSpace(p4Position);
                            pictureBox96.Left = squareLocationTopP4[p4Position];
                            pictureBox96.Top = squareLocationLeftP4[p4Position];
                        }
                    }
                }
            }
            CheckGo();
            GoToJail();
        }
        private int ChanceSpace(int playerPosition)
        {
            int cardNumber;
            if ((playerPosition == 7) || (playerPosition == 22) || (playerPosition == 36))
            {
                cardNumber = NumberGenerator(1,13);
                switch (cardNumber)
                {
                    case 1: MessageBox.Show("Advance To Go, Collect £200"); playerPosition = 0; break;
                    case 2: MessageBox.Show("Advance To Trafalgar Square, If pass go, collect £200"); playerPosition = 24; label31.Text = squareNames[playerPosition]; break;
                    case 3: MessageBox.Show("Advance To Mayfair, If pass go, collect £200"); playerPosition = 39; label31.Text = squareNames[playerPosition]; break;
                    case 4: MessageBox.Show("Go To Jail! Do Not Pass Go! Do Not Collect £200"); playerPosition = 30; label31.Text = "Go To Jail"; break;
                    case 5: MessageBox.Show("Bank pays you Dividend of £50");
                        if (p1Turn == true)
                        {
                            currentMoneyP1 = currentMoneyP1 + 50;
                            label17.Text =  "£" + currentMoneyP1.ToString();
                        }
                        else
                        {
                            if (p2Turn == true)
                            {
                                currentMoneyP2 = currentMoneyP2 + 50;
                                label20.Text = "£" + currentMoneyP2.ToString();
                            }
                            else
                            {
                                if (p3Turn == true)
                                {
                                    currentMoneyP3 = currentMoneyP3 + 50;
                                    label23.Text = "£" + currentMoneyP3.ToString();
                                }
                                else
                                {
                                    if (p4Turn == true)
                                    {
                                        currentMoneyP4 = currentMoneyP4 + 50;
                                        label26.Text = "£" + currentMoneyP4.ToString();
                                    }
                                }
                            }
                        } break;
                    case 6: MessageBox.Show("Pay School Fees of £150");
                        if (p1Turn == true)
                        {
                            currentMoneyP1 = currentMoneyP1 - 150;
                            label17.Text = "£" + currentMoneyP1.ToString();
                        }
                        else
                        {
                            if (p2Turn == true)
                            {
                                currentMoneyP2 = currentMoneyP2  - 150;
                                label20.Text = "£" + currentMoneyP2.ToString();
                            }
                            else
                            {
                                if (p3Turn == true)
                                {
                                    currentMoneyP3 = currentMoneyP3 - 150;
                                    label23.Text = "£" + currentMoneyP3.ToString();
                                }
                                else
                                {
                                    if (p4Turn == true)
                                    {
                                        currentMoneyP4 = currentMoneyP4 - 150;
                                        label26.Text = "£" + currentMoneyP4.ToString();
                                    }
                                }
                            }
                        } break;
                    case 7: MessageBox.Show("Speeding Fine £15");
                        if (p1Turn == true)
                        {
                            currentMoneyP1 = currentMoneyP1 - 15;
                            label17.Text = "£" + currentMoneyP1.ToString();
                        }
                        else
                        {
                            if (p2Turn == true)
                            {
                                currentMoneyP2 = currentMoneyP2 - 15;
                                label20.Text = "£" + currentMoneyP2.ToString();
                            }
                            else
                            {
                                if (p3Turn == true)
                                {
                                    currentMoneyP3 = currentMoneyP3 - 15;
                                    label23.Text = "£" + currentMoneyP3.ToString();
                                }
                                else
                                {
                                    if (p4Turn == true)
                                    {
                                        currentMoneyP4 = currentMoneyP4 - 15;
                                        label26.Text = "£" + currentMoneyP4.ToString();
                                    }
                                }
                            }
                        } break;
                    case 8: MessageBox.Show("You Have Won A Crossword Competition. Collect £100");
                        if (p1Turn == true)
                        {
                            currentMoneyP1 = currentMoneyP1 + 100;
                            label17.Text = "£" + currentMoneyP1.ToString();
                        }
                        else
                        {
                            if (p2Turn == true)
                            {
                                currentMoneyP2 = currentMoneyP2 + 100;
                                label20.Text = "£" + currentMoneyP2.ToString();
                            }
                            else
                            {
                                if (p3Turn == true)
                                {
                                    currentMoneyP3 = currentMoneyP3 + 100;
                                    label23.Text = "£" + currentMoneyP3.ToString();
                                }
                                else
                                {
                                    if (p4Turn == true)
                                    {
                                        currentMoneyP4 = currentMoneyP4 + 100;
                                        label26.Text = "£" + currentMoneyP4.ToString();
                                    }
                                }
                            }
                        } break;
                    case 9: MessageBox.Show("Your Building And Loan Matures. Collect £150");
                        if (p1Turn == true)
                        {
                            currentMoneyP1 = currentMoneyP1 + 150;
                            label17.Text = "£" + currentMoneyP1.ToString();
                        }
                        else
                        {
                            if (p2Turn == true)
                            {
                                currentMoneyP2 = currentMoneyP2 + 150;
                                label20.Text = "£" + currentMoneyP2.ToString();
                            }
                            else
                            {
                                if (p3Turn == true)
                                {
                                    currentMoneyP3 = currentMoneyP3 + 150;
                                    label23.Text = "£" + currentMoneyP3.ToString();
                                }
                                else
                                {
                                    if (p4Turn == true)
                                    {
                                        currentMoneyP4 = currentMoneyP4 + 150;
                                        label26.Text = "£" + currentMoneyP4.ToString();
                                    }
                                }
                            }
                        } break;
                    case 10: MessageBox.Show("Drunk In Charge. Fine £20");
                        if (p1Turn == true)
                        {
                            currentMoneyP1 = currentMoneyP1 - 20;
                            label17.Text = "£" + currentMoneyP1.ToString();
                        }
                        else
                        {
                            if (p2Turn == true)
                            {
                                currentMoneyP2 = currentMoneyP2 - 20;
                                label20.Text = "£" + currentMoneyP2.ToString();
                            }
                            else
                            {
                                if (p3Turn == true)
                                {
                                    currentMoneyP3 = currentMoneyP3 - 20;
                                    label23.Text = "£" + currentMoneyP3.ToString();
                                }
                                else
                                {
                                    if (p4Turn == true)
                                    {
                                        currentMoneyP4 = currentMoneyP4 - 20;
                                        label26.Text = "£" + currentMoneyP4.ToString();
                                    }
                                }
                            }
                        } break;
                    case 11: MessageBox.Show("Take a Trip to Marylebone Station. If pass go, Collect £200"); playerPosition = 15; label31.Text = squareNames[playerPosition]; break;
                    case 12: MessageBox.Show("Advance To Pall Mall. If pass go, collect £200"); playerPosition = 11; label31.Text = squareNames[playerPosition]; break;
                    case 13: MessageBox.Show("Jail Out Of Free");
                        if (p1Turn == true)
                        {
                            jailOutOfFreeP1 = true;
                            label19.Text = jailOutOfFreeP1.ToString();
                        }
                        else
                        {
                            if (p2Turn == true)
                            {
                                jailOutOfFreeP2 = true;
                                label22.Text = jailOutOfFreeP2.ToString();
                            }
                            else
                            {
                                if (p3Turn == true)
                                {
                                    jailOutOfFreeP3 = true;
                                    label25.Text = jailOutOfFreeP3.ToString();
                                }
                                else
                                {
                                    if (p4Turn == true)
                                    {
                                        jailOutOfFreeP4 = true;
                                        label28.Text = jailOutOfFreeP4.ToString();
                                    }
                                }
                            }
                        }  break;
                }
            }
                return playerPosition;
        }
        private int ChestSpace(int playerPosition)
        {
            int cardNumber;
            if ((playerPosition == 2) || (playerPosition == 17) || (playerPosition == 32))
            {
                cardNumber = NumberGenerator(1, 13);
                switch (cardNumber)
                {
                    case 1: MessageBox.Show("Income Tax refund. Collect £20");
                        if (p1Turn == true)
                        {
                            currentMoneyP1 = currentMoneyP1 + 20;
                            label17.Text = "£" + currentMoneyP1.ToString();
                        }
                        else
                        {
                            if (p2Turn == true)
                            {
                                currentMoneyP2 = currentMoneyP2 + 20;
                                label20.Text = "£" + currentMoneyP2.ToString();
                            }
                            else
                            {
                                if (p3Turn == true)
                                {
                                    currentMoneyP3 = currentMoneyP3 + 20;
                                    label23.Text = "£" + currentMoneyP3.ToString();
                                }
                                else
                                {
                                    if (p4Turn == true)
                                    {
                                        currentMoneyP4 = currentMoneyP4 + 20;
                                        label26.Text = "£" + currentMoneyP4.ToString();
                                    }
                                }
                            }
                        } break;
                    case 2: MessageBox.Show("From Sale Of Stock. Collect £50");
                        if (p1Turn == true)
                        {
                            currentMoneyP1 = currentMoneyP1 + 50;
                            label17.Text = "£" + currentMoneyP1.ToString();
                        }
                        else
                        {
                            if (p2Turn == true)
                            {
                                currentMoneyP2 = currentMoneyP2 + 50;
                                label20.Text = "£" + currentMoneyP2.ToString();
                            }
                            else
                            {
                                if (p3Turn == true)
                                {
                                    currentMoneyP3 = currentMoneyP3 + 50;
                                    label23.Text = "£" + currentMoneyP3.ToString();
                                }
                                else
                                {
                                    if (p4Turn == true)
                                    {
                                        currentMoneyP4 = currentMoneyP4 + 50;
                                        label26.Text = "£" + currentMoneyP4.ToString();
                                    }
                                }
                            }
                        } break;
                    case 3: MessageBox.Show("Its Your Birthday, Collect £50 from Each Player");
                        if (p1Turn == true)
                        {
                            if (p2Playing == true)
                            {
                                currentMoneyP1 = currentMoneyP1 + 50;
                                currentMoneyP2 = currentMoneyP2 - 50;
                            }
                            if (p3Playing == true)
                            {
                                currentMoneyP1 = currentMoneyP1 + 50;
                                currentMoneyP3 = currentMoneyP3 - 50;
                            }
                            if (p4Playing == true)
                            {
                                currentMoneyP1 = currentMoneyP1 + 50;
                                currentMoneyP4 = currentMoneyP4 - 50;
                            }
                            label17.Text = "£" + currentMoneyP1.ToString();
                        }
                        else
                        {
                            if (p2Turn == true)
                            {
                                if (p1Playing == true)
                                {
                                    currentMoneyP2 = currentMoneyP2 + 50;
                                    currentMoneyP1 = currentMoneyP1 - 50;
                                }
                                if (p3Playing == true)
                                {
                                    currentMoneyP2 = currentMoneyP2 + 50;
                                    currentMoneyP3 = currentMoneyP3 - 50;
                                }
                                if (p4Playing == true)
                                {
                                    currentMoneyP2 = currentMoneyP2 + 50;
                                    currentMoneyP4 = currentMoneyP4 - 50;
                                }
                                label20.Text = "£" + currentMoneyP2.ToString();
                            }
                            else
                            {
                                if (p3Turn == true)
                                {
                                    if (p1Playing == true)
                                    {
                                        currentMoneyP3 = currentMoneyP3 + 50;
                                        currentMoneyP1 = currentMoneyP1 - 50;
                                    }
                                    if (p2Playing == true)
                                    {
                                        currentMoneyP3 = currentMoneyP3 + 50;
                                        currentMoneyP2 = currentMoneyP2 - 50;
                                    }
                                    if (p4Playing == true)
                                    {
                                        currentMoneyP3 = currentMoneyP3 + 50;
                                        currentMoneyP4 = currentMoneyP4 - 50;
                                    }
                                    label23.Text = "£" + currentMoneyP3.ToString();
                                }
                                else
                                {
                                    if (p4Turn == true)
                                    {
                                        if (p1Playing == true)
                                        {
                                            currentMoneyP4 = currentMoneyP4 + 50;
                                            currentMoneyP1 = currentMoneyP1 - 50;
                                        }
                                        if (p2Playing == true)
                                        {
                                            currentMoneyP4 = currentMoneyP4 + 50;
                                            currentMoneyP2 = currentMoneyP2 - 50;
                                        }
                                        if (p3Playing == true)
                                        {
                                            currentMoneyP4 = currentMoneyP4 + 50;
                                            currentMoneyP3 = currentMoneyP3 - 50;
                                        }
                                        label26.Text = "£" + currentMoneyP4.ToString();
                                    }
                                }
                            }
                        } break;
                    case 4: MessageBox.Show("Receive Interest on 7% Preference Shares. Collect £25 ");
                        if (p1Turn == true)
                        {
                            currentMoneyP1 = currentMoneyP1 + 25;
                            label17.Text = "£" + currentMoneyP1.ToString();
                        }
                        else
                        {
                            if (p2Turn == true)
                            {
                                currentMoneyP2 = currentMoneyP2 + 25;
                                label20.Text = "£" + currentMoneyP2.ToString();
                            }
                            else
                            {
                                if (p3Turn == true)
                                {
                                    currentMoneyP3 = currentMoneyP3 + 25;
                                    label23.Text = "£" + currentMoneyP3.ToString();
                                }
                                else
                                {
                                    if (p4Turn == true)
                                    {
                                        currentMoneyP4 = currentMoneyP4 + 25;
                                        label26.Text = "£" + currentMoneyP4.ToString();
                                    }
                                }
                            }
                        }  break;
                    case 5: MessageBox.Show("Pay Hospital £100");
                        if (p1Turn == true)
                        {
                            currentMoneyP1 = currentMoneyP1 - 100;
                            label17.Text = "£" + currentMoneyP1.ToString();
                        }
                        else
                        {
                            if (p2Turn == true)
                            {
                                currentMoneyP2 = currentMoneyP2 - 100;
                                label20.Text = "£" + currentMoneyP2.ToString();
                            }
                            else
                            {
                                if (p3Turn == true)
                                {
                                    currentMoneyP3 = currentMoneyP3 - 100;
                                    label23.Text = "£" + currentMoneyP3.ToString();
                                }
                                else
                                {
                                    if (p4Turn == true)
                                    {
                                        currentMoneyP4 = currentMoneyP4 - 100;
                                        label26.Text = "£" + currentMoneyP4.ToString();
                                    }
                                }
                            }
                        }  break;
                    case 6: MessageBox.Show("Advance To Go, Collect £200"); playerPosition = 0; break;
                    case 7: MessageBox.Show("You Inherit £100");
                        if (p1Turn == true)
                        {
                            currentMoneyP1 = currentMoneyP1 + 100;
                            label17.Text = "£" + currentMoneyP1.ToString();
                        }
                        else
                        {
                            if (p2Turn == true)
                            {
                                currentMoneyP2 = currentMoneyP2 + 100;
                                label20.Text = "£" + currentMoneyP2.ToString();
                            }
                            else
                            {
                                if (p3Turn == true)
                                {
                                    currentMoneyP3 = currentMoneyP3 + 100;
                                    label23.Text = "£" + currentMoneyP3.ToString();
                                }
                                else
                                {
                                    if (p4Turn == true)
                                    {
                                        currentMoneyP4 = currentMoneyP4 + 100;
                                        label26.Text = "£" + currentMoneyP4.ToString();
                                    }
                                }
                            }
                        } break;
                    case 8: MessageBox.Show("Go To Jail!Do Not Pass Go!Do Not Collect £200"); playerPosition = 30; label31.Text = "Go To Jail"; break;
                    case 9: MessageBox.Show("Doctors Fee, Pay £50");
                        if (p1Turn == true)
                        {
                            currentMoneyP1 = currentMoneyP1 - 50;
                            label17.Text = "£" + currentMoneyP1.ToString();
                        }
                        else
                        {
                            if (p2Turn == true)
                            {
                                currentMoneyP2 = currentMoneyP2 - 50;
                                label20.Text = "£" + currentMoneyP2.ToString();
                            }
                            else
                            {
                                if (p3Turn == true)
                                {
                                    currentMoneyP3 = currentMoneyP3 - 50;
                                    label23.Text = "£" + currentMoneyP3.ToString();
                                }
                                else
                                {
                                    if (p4Turn == true)
                                    {
                                        currentMoneyP4 = currentMoneyP4 - 50;
                                        label26.Text = "£" + currentMoneyP4.ToString();
                                    }
                                }
                            }
                        } break;
                    case 10: MessageBox.Show("Annuity Mature. Collect £100");
                        if (p1Turn == true)
                        {
                            currentMoneyP1 = currentMoneyP1 + 100;
                            label17.Text = "£" + currentMoneyP1.ToString();
                        }
                        else
                        {
                            if (p2Turn == true)
                            {
                                currentMoneyP2 = currentMoneyP2 + 100;
                                label20.Text = "£" + currentMoneyP2.ToString();
                            }
                            else
                            {
                                if (p3Turn == true)
                                {
                                    currentMoneyP3 = currentMoneyP3 + 100;
                                    label23.Text = "£" + currentMoneyP3.ToString();
                                }
                                else
                                {
                                    if (p4Turn == true)
                                    {
                                        currentMoneyP4 = currentMoneyP4 + 100;
                                        label26.Text = "£" + currentMoneyP4.ToString();
                                    }
                                }
                            }
                        }  break;
                    case 11: MessageBox.Show("Pay your Insurance Premium £50");
                        if (p1Turn == true)
                        {
                            currentMoneyP1 = currentMoneyP1 - 50;
                            label17.Text = "£" + currentMoneyP1.ToString();
                        }
                        else
                        {
                            if (p2Turn == true)
                            {
                                currentMoneyP2 = currentMoneyP2 - 50;
                                label20.Text = "£" + currentMoneyP2.ToString();
                            }
                            else
                            {
                                if (p3Turn == true)
                                {
                                    currentMoneyP3 = currentMoneyP3 - 50;
                                    label23.Text = "£" + currentMoneyP3.ToString();
                                }
                                else
                                {
                                    if (p4Turn == true)
                                    {
                                        currentMoneyP4 = currentMoneyP4 - 50;
                                        label26.Text = "£" + currentMoneyP4.ToString();
                                    }
                                }
                            }
                        }  break;
                    case 12: MessageBox.Show("Bank Error in your Favour. Collect £200");
                        if (p1Turn == true)
                        {
                            currentMoneyP1 = currentMoneyP1 + 200;
                            label17.Text = "£" + currentMoneyP1.ToString();
                        }
                        else
                        {
                            if (p2Turn == true)
                            {
                                currentMoneyP2 = currentMoneyP2 + 200;
                                label20.Text = "£" + currentMoneyP2.ToString();
                            }
                            else
                            {
                                if (p3Turn == true)
                                {
                                    currentMoneyP3 = currentMoneyP3 + 200;
                                    label23.Text = "£" + currentMoneyP3.ToString();
                                }
                                else
                                {
                                    if (p4Turn == true)
                                    {
                                        currentMoneyP4 = currentMoneyP4 + 200;
                                        label26.Text = "£" + currentMoneyP4.ToString();
                                    }
                                }
                            }
                        }  break;
                    case 13: MessageBox.Show("Jail Out Of Free");
                        if (p1Turn == true)
                        {
                            jailOutOfFreeP1 = true;
                            label19.Text = jailOutOfFreeP1.ToString();
                        }
                        else
                        {
                            if (p2Turn == true)
                            {
                                jailOutOfFreeP2 = true;
                                label22.Text = jailOutOfFreeP2.ToString();
                            }
                            else
                            {
                                if (p3Turn == true)
                                {
                                    jailOutOfFreeP3 = true;
                                    label25.Text = jailOutOfFreeP3.ToString();
                                }
                                else
                                {
                                    if (p4Turn == true)
                                    {
                                        jailOutOfFreeP4 = true;
                                        label28.Text = jailOutOfFreeP4.ToString();
                                    }
                                }
                            }
                        }  break;
                }
            }
            return playerPosition;
        }
        
        private void EndGame() //Checks to see if eveyrone except one person is bankrupt and if it is ends the game
        {
            if ((p1Playing == true) && (p2Playing == true) && (p3Playing == true) && (p4Playing == true))
            {
                if ((p1BankRupt == false) && (p2BankRupt == true) && (p3BankRupt == true) && (p4BankRupt == true))
                {
                    EndScreen.label2.Text = "Player 1";
                    EndScreen.Show();
                    this.Hide();
                }
                if ((p1BankRupt == true) && (p2BankRupt == false) && (p3BankRupt == true) && (p4BankRupt == true))
                {
                    EndScreen.label2.Text = "Player 2";
                    EndScreen.Show();
                    this.Hide();
                }
                if ((p1BankRupt == true) && (p2BankRupt == true) && (p3BankRupt == false) && (p4BankRupt == true))
                {
                    EndScreen.label2.Text = "Player 3";
                    EndScreen.Show();
                    this.Hide();
                }
                if ((p1BankRupt == true) && (p2BankRupt == true) && (p3BankRupt == true) && (p4BankRupt == false))
                {
                    EndScreen.label2.Text = "Player 4";
                    EndScreen.Show();
                    this.Hide();
                }
            }
            else
            {
                if ((p1Playing == true) && (p2Playing == true) && (p3Playing == true))
                {
                    if ((p1BankRupt == false) && (p2BankRupt == true) && (p3BankRupt == true))
                    {
                        EndScreen.label2.Text = "Player 1";
                        EndScreen.Show();
                        this.Hide();
                    }
                    if ((p1BankRupt == true) && (p2BankRupt == false) && (p3BankRupt == true))
                    {
                        EndScreen.label2.Text = "Player 2";
                        EndScreen.Show();
                        this.Hide();
                    }
                    if ((p1BankRupt == true) && (p2BankRupt == true) && (p3BankRupt == false))
                    {
                        EndScreen.label2.Text = "Player 3";
                        EndScreen.Show();
                        this.Hide();
                    }
                }
                else
                {
                    if ((p1Playing == true) && (p2Playing == true))
                    {
                        if ((p1BankRupt == false) && (p2BankRupt == true))
                        {
                            EndScreen.label2.Text = "Player 1";
                            EndScreen.Show();
                            this.Hide();
                        }
                        if ((p1BankRupt == true) && (p2BankRupt == false))
                        {
                            EndScreen.label2.Text = "Player 2";
                            EndScreen.Show();
                            this.Hide();
                        }
                    }
                }
            }
        }

        //--------------------------------------------------------------
        private void pictureBox99_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox96_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox36_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox21_Click(object sender, EventArgs e)
        {

        }

    }

}



