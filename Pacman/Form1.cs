using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using GraphAlgorithmLib.Algorithms;
using GraphAlgorithmLib.DataStructures;

namespace Pacman
{
    public partial class Form1 : Form
    {
        const int roomSize = 21;
        int points = 0;
        int health;
        int directionPacman = 1,
            directionPurpleM = 1,
            directionBlueM = 1,
            directionYellowM = 1,
            directionNext = 1;
        const int speedPacman = 30,
            speedPurpleM = 30,
            speedBlueM = 15,
            speedYellowM = 30;
        int counterBlueM = 0;
        Stack<Vertex> pathBlueM = new Stack<Vertex>();
        Stack<Vertex> pathYellowM = new Stack<Vertex>();
        Stack<Vertex> pathPurpleM = new Stack<Vertex>();
        readonly Queue<Point> toDelete = new Queue<Point>();
        public static DirectedUnweightedGraph mapGraph = new DirectedUnweightedGraph(false);

        // nacteni obrazku pro jednotlive tiles
        #region Loading pictures
        readonly Image pacmanUp = Image.FromFile(@"..\..\Resources\pacmanUp.gif");
        readonly Image pacmanRight = Image.FromFile(@"..\..\Resources\pacmanRight.gif");
        readonly Image pacmanDown = Image.FromFile(@"..\..\Resources\pacmanDown.gif");
        readonly Image pacmanLeft = Image.FromFile(@"..\..\Resources\pacmanLeft.gif");
        readonly Image wall = Image.FromFile(@"..\..\Resources\wall.png");
        readonly Image purpleM = Image.FromFile(@"..\..\Resources\purpleM.gif");
        readonly Image yellowM = Image.FromFile(@"..\..\Resources\yellowM.gif");
        readonly Image deadM = Image.FromFile(@"..\..\Resources\deadM.gif");
        readonly Image blueM = Image.FromFile(@"..\..\Resources\blueM.gif");
        readonly Image coin = Image.FromFile(@"..\..\Resources\coin.gif");
        readonly Image powerUp = Image.FromFile(@"..\..\Resources\powerUp.gif");
        readonly System.Media.SoundPlayer Music = new System.Media.SoundPlayer();
        #endregion

        // seznam bloku
        readonly PictureBox pacman = new PictureBox()
                 , purpleMonstrum = new PictureBox()
                 , blueMonstrum = new PictureBox()
                 , yellowMonstrum = new PictureBox();

        public Form1()
        {
            InitializeComponent();
        }

        #region Hnadlery pro mrtve prisery
        private void purpleTimer_Tick(object sender, EventArgs e)
        {
            purpleTimer.Stop();
            purpleMonstrum.Image = purpleM;
        }

        private void blueTimer_Tick(object sender, EventArgs e)
        {
            blueTimer.Stop();
            blueMonstrum.Image = blueM;
        }

        private void yellowTimer_Tick(object sender, EventArgs e)
        {
            yellowTimer.Stop();
            yellowMonstrum.Image = yellowM;
        }
        #endregion

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W: directionNext = 0; break;
                case Keys.D: directionNext = 1; break;
                case Keys.S: directionNext = 2; break;
                case Keys.A: directionNext = 3; break;
            }

        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            string level = "level_1";
            startButton.Enabled = false;
            if (Hardmode.Checked == true)
            {
                health = 1;
                lives.Text = "1";
            }
            else
            {
                health = 3;
                lives.Text = "3";
            }
            Hardmode.Enabled = false;
            levelPicker.Enabled = false;
            switch (levelPicker.SelectedIndex)
            {
                case 0: level = "level_1"; break;
                case 1: level = "level_2"; break;
                case 2: level = "level_3"; break;
                case 3:
                    level = "level_4";
                    health = 1;
                    lives.Text = "1";
                    MessageBox.Show("Tohle je super těžký level, na který je pouze jeden život.", "Hardcore^2", MessageBoxButtons.OK);
                    break;
            }

            Map.LoadMap(@"..\..\Resources\" + level + ".txt");
            Print();

            yellowMonstrum.BringToFront();
            purpleMonstrum.BringToFront();
            blueMonstrum.BringToFront();
            pacman.BringToFront();

            Music.SoundLocation = @"..\..\Resources\ChocolateRain.wav";
            Music.Play();

            movement.Start();
        }
        private void Win()
        {
            if (Map.coinAmount == 0)
            {
                movement.Stop();
                DialogResult OK = MessageBox.Show("Vyhráli jste.", "Ejjj", MessageBoxButtons.OK);
                if (OK == DialogResult.OK)
                {
                    Close();
                }
            }
        }
        private void GameOver()
        {
            movement.Stop();
            DialogResult OK = MessageBox.Show("Prohráli jste.", "Ajaj", MessageBoxButtons.OK);
            if (OK == DialogResult.OK)
            {
                Close();
            }
        }
        private void AddEdgesToGraph(int i, int j)
        {
            string vertexName = i + " " + j;
            try
            {
                if (Map.mapa[i+1,j] != 'X')
                {
                    i++;
                    mapGraph.AddEdge(vertexName, i + " " + j);
                    i--;
                }
                if (Map.mapa[i, j+1] != 'X')
                {
                    j++;
                    mapGraph.AddEdge(vertexName, i + " " + j);
                    j--;
                }
                if (Map.mapa[i-1, j] != 'X')
                {
                    i--;
                    mapGraph.AddEdge(vertexName, i + " " + j);
                    i++;
                }
                if (Map.mapa[i, j-1] != 'X')
                {
                    j--;
                    mapGraph.AddEdge(vertexName, i + " " + j);
                    j++;
                }
            }
            catch (Exception)
            {

            }
        }
        private void Print()
        {
            for (int y = 0; y < roomSize; y++)
            {
                for (int x = 0; x < roomSize; x++)
                {
                    switch (Map.mapa[x, y])        //tady se vypere prislusny obrazek
                    {
                        case 'X':
                            {
                                PictureBox tile = new PictureBox();
                                tile.Image = wall;
                                tile.Margin = new Padding(0);
                                tile.Size = new Size(30, 30);
                                tile.Location = new Point(x * 30, (y + 1) * 30);
                                Controls.Add(tile);
                                break;
                            }
                        case 'o':
                            {
                                mapGraph.AddVertex(x + " " + y);
                                PictureBox tile = new PictureBox();
                                tile.Image = coin;
                                tile.Margin = new Padding(0);
                                tile.Size = new Size(30, 30);
                                tile.Location = new Point(x * 30, (y + 1) * 30);
                                Controls.Add(tile);
                                break;
                            }
                        case 'p':
                            {
                                mapGraph.AddVertex(x + " " + y);
                                PictureBox tile = new PictureBox();
                                tile.Image = powerUp;
                                tile.Margin = new Padding(0);
                                tile.Size = new Size(30, 30);
                                tile.Location = new Point(x * 30, (y + 1) * 30);
                                Controls.Add(tile);
                                break;
                            }
                        case '>':
                            {
                                mapGraph.AddVertex(x + " " + y);
                                pacman.Image = pacmanRight;
                                pacman.Margin = new Padding(0);
                                pacman.Size = new Size(30, 30);
                                pacman.Location = new Point(x * 30, (y + 1) * 30);
                                Controls.Add(pacman);
                                break;
                            }
                        case 'f':
                            {
                                mapGraph.AddVertex(x + " " + y);
                                purpleMonstrum.Image = purpleM;
                                purpleMonstrum.Margin = new Padding(0);
                                purpleMonstrum.Size = new Size(30, 30);
                                purpleMonstrum.Location = new Point(x * 30, (y + 1) * 30);
                                Controls.Add(purpleMonstrum);
                                break;
                            }
                        case 'm':
                            {
                                mapGraph.AddVertex(x + " " + y);
                                blueMonstrum.Image = blueM;
                                blueMonstrum.Margin = new Padding(0);
                                blueMonstrum.Size = new Size(30, 30);
                                blueMonstrum.Location = new Point(x * 30, (y + 1) * 30);
                                Controls.Add(blueMonstrum);
                                break;
                            }
                        case 'z':
                            {
                                mapGraph.AddVertex(x + " " + y);
                                yellowMonstrum.Image = yellowM;
                                yellowMonstrum.Margin = new Padding(0);
                                yellowMonstrum.Size = new Size(30, 30);
                                yellowMonstrum.Location = new Point(x * 30, (y + 1) * 30);
                                Controls.Add(yellowMonstrum);
                                break;
                            }
                        default:
                            {
                                mapGraph.AddVertex(x + " " + y);
                                PictureBox tile = new PictureBox();
                                tile.Image = null;
                                tile.Margin = new Padding(0);
                                tile.Size = new Size(30, 30);
                                tile.Location = new Point(x * 30, (y + 1) * 30);
                                Controls.Add(tile);
                                break;
                            }
                    }

                }
            }
            for (int y = 0; y < roomSize; y++)
            {
                for (int x = 0; x < roomSize; x++)
                {
                    if (Map.mapa[x,y] != 'X')
                    {
                        AddEdgesToGraph(x, y);
                    }
                }
            }
        }
        private void DecreaseHealth()
        {
            if (pacman.Bounds.IntersectsWith(purpleMonstrum.Bounds) && purpleMonstrum.Image == purpleM)
            {
                purpleMonstrum.Image = deadM;
                health--;
                lives.Text = Convert.ToString(health);
                purpleTimer.Start();
            }
            if (pacman.Bounds.IntersectsWith(blueMonstrum.Bounds) && blueMonstrum.Image == blueM)
            {
                blueMonstrum.Image = deadM;
                health--;
                lives.Text = Convert.ToString(health);
                blueTimer.Start();
            }
            if (pacman.Bounds.IntersectsWith(yellowMonstrum.Bounds) && yellowMonstrum.Image == yellowM)
            {
                yellowMonstrum.Image = deadM;
                health--;
                lives.Text = Convert.ToString(health);
                yellowTimer.Start();
            }
            if (health == 0)
            {
                GameOver();
            }
        }
        private bool ChangeDirection(int next)
        {
            int i, j;
            switch (next)
            {
                case 0:
                    {

                        i = ((pacman.Location.X) / 30);
                        j = ((pacman.Location.Y) / 30) - 2;
                        if (Map.mapa[i, j] == 'X')
                        {
                            return false;
                        }
                        else pacman.Image = pacmanUp;
                        break;


                    }
                case 1:
                    {
                        i = ((pacman.Location.X) / 30) + 1;
                        j = (((pacman.Location.Y) / 30) - 1);
                        if (Map.mapa[i, j] == 'X')
                        {
                            return false;
                        }
                        else pacman.Image = pacmanRight;
                        break;
                    }
                case 2:
                    {
                        i = (pacman.Location.X / 30);
                        j = ((pacman.Location.Y) / 30) - 1 + 1;
                        if (Map.mapa[i, j] == 'X')
                        {
                            return false;
                        }
                        else pacman.Image = pacmanDown;
                        break;

                    }
                case 3:
                    {

                        i = ((pacman.Location.X) / 30);
                        j = ((pacman.Location.Y) / 30) - 1;
                        if (Map.mapa[i, j] == 'X')
                        {
                            return false;
                        }
                        else pacman.Image = pacmanLeft;
                        break;
                    }
            }

            return true;
        }
        private void MovementPacman(int direction)
        {
            int i, j;
            switch (direction)
            {
                case 0:
                    {
                        i = ((pacman.Location.X) / 30);
                        j = ((pacman.Location.Y) / 30) - 2;

                        if (Map.mapa[i, j] == 'o')
                        {

                            Point Mazatko = new Point(pacman.Location.X, pacman.Location.Y - 30);
                            PictureBox Smazat = GetChildAtPoint(Mazatko) as PictureBox;
                            if (Smazat.Image == coin)
                            {
                                Map.mapa[i, j] = ' ';
                                points += 100;
                                Map.coinAmount--;
                                score.Text = Convert.ToString(points);
                                Smazat.Image = null;
                            }

                        }
                        if (Map.mapa[i, j] == 'p')
                        {
                            Map.mapa[i, j] = ' ';
                            health++;
                            lives.Text = Convert.ToString(health);
                            Point Mazatko = new Point(pacman.Location.X, pacman.Location.Y - 30);
                            var Smazat = GetChildAtPoint(Mazatko);
                            Controls.Remove(Smazat);
                            Smazat.Dispose();

                        }
                        if (Map.mapa[i, j] != 'X')
                        {
                            pacman.Location = new Point(pacman.Location.X, pacman.Location.Y - speedPacman);
                        }

                        break;
                    }
                case 1:
                    {
                        i = ((pacman.Location.X) / 30) + 1;
                        j = ((pacman.Location.Y) / 30) - 1;

                        if (Map.mapa[i, j] == 'o')
                        {
                            Point Mazatko = new Point(pacman.Location.X + 30, pacman.Location.Y);
                            PictureBox Smazat = GetChildAtPoint(Mazatko) as PictureBox;
                            if (Smazat.Image == coin)
                            {
                                Map.mapa[i, j] = ' ';
                                points += 100;
                                Map.coinAmount--;
                                score.Text = Convert.ToString(points);
                                Smazat.Image = null;
                            }
                        }
                        if (Map.mapa[i, j] == 'p')
                        {
                            Map.mapa[i, j] = ' ';
                            health++;
                            lives.Text = Convert.ToString(health);
                            Point Mazatko = new Point(pacman.Location.X + 30, pacman.Location.Y);
                            toDelete.Enqueue(Mazatko);
                            PictureBox Smazat = GetChildAtPoint(Mazatko) as PictureBox;
                            Controls.Remove(Smazat);
                            Smazat.Dispose();

                        }
                        if (Map.mapa[i, j] != 'X')
                        {
                            pacman.Location = new Point(pacman.Location.X + speedPacman, pacman.Location.Y);
                        }

                        break;
                    }
                case 2:
                    {
                        i = (pacman.Location.X / 30);
                        j = (pacman.Location.Y / 30);


                        if (Map.mapa[i, j] == 'o')
                        {
                            Point Mazatko = new Point(pacman.Location.X, pacman.Location.Y + 30);
                            PictureBox Smazat = GetChildAtPoint(Mazatko) as PictureBox;
                            if (Smazat.Image == coin)
                            {
                                Map.mapa[i, j] = ' ';
                                points += 100;
                                Map.coinAmount--;
                                score.Text = Convert.ToString(points);
                                Smazat.Image = null;
                            }
                        }
                        if (Map.mapa[i, j] == 'p')
                        {
                            Map.mapa[i, j] = ' ';
                            health++;
                            lives.Text = Convert.ToString(health);
                            Point Mazatko = new Point(pacman.Location.X, pacman.Location.Y + 30);
                            var Smazat = GetChildAtPoint(Mazatko);
                            Controls.Remove(Smazat);
                            Smazat.Dispose();

                        }
                        if (Map.mapa[i, j] != 'X')
                        {
                            pacman.Location = new Point(pacman.Location.X, pacman.Location.Y + speedPacman);
                        }

                        break;
                    }
                case 3:
                    {

                        i = ((pacman.Location.X) / 30) - 1;
                        j = ((pacman.Location.Y) / 30) - 1;

                        if (Map.mapa[i, j] == 'o')
                        {
                            Point Mazatko = new Point(pacman.Location.X - 30, pacman.Location.Y);
                            PictureBox Smazat = GetChildAtPoint(Mazatko) as PictureBox;
                            if (Smazat.Image == coin)
                            {
                                Map.mapa[i, j] = ' ';
                                points += 100;
                                Map.coinAmount--;
                                score.Text = Convert.ToString(points);
                                Smazat.Image = null;
                            }

                        }
                        if (Map.mapa[i, j] == 'p')
                        {
                            Map.mapa[i, j] = ' ';
                            health++;
                            lives.Text = Convert.ToString(health);
                            Point Mazatko = new Point(pacman.Location.X - 30, pacman.Location.Y);
                            var Smazat = GetChildAtPoint(Mazatko);
                            Controls.Remove(Smazat);
                            Smazat.Dispose();

                        }
                        if (Map.mapa[i, j] != 'X')
                        {
                            pacman.Location = new Point(pacman.Location.X - speedPacman, pacman.Location.Y);
                        }

                        break;
                    }
            }
            Win();
        }
        private int findPath(int xM, int yM)
        {
            int xP = 0;
            int yP = 0;
            switch (directionPacman)
            {
                case 0:
                    {
                        xP = ((pacman.Location.X) / 30);
                        yP = ((pacman.Location.Y) / 30) - 1;
                        break;
                    }
                case 1:
                    {
                        xP = ((pacman.Location.X) / 30);
                        yP = (((pacman.Location.Y) / 30) - 1);
                        break;
                    }
                case 2:
                    {
                        xP = (pacman.Location.X / 30);
                        yP = ((pacman.Location.Y) / 30)-1;
                        break;
                    }
                case 3:
                    {
                        xP = ((pacman.Location.X) / 30);
                        yP = ((pacman.Location.Y) / 30) - 1;
                        break;
                    }
            }
                    BreathFirstSearch<DirectedUnweightedGraph>.Search(mapGraph, xM + " " + yM);

            var path = BreathFirstSearch<DirectedUnweightedGraph>.GetShortestPath(mapGraph, xP + " " + yP);
            path.Pop(); // popne první vertex, na kterým už monstrum je 
            try
            {
                var (xR, yR) = ReadInt.ReadVertexName(path.Pop().name);

                if (xM - xR == 1)       // pojede doleva
                {
                    return 3;
                }
                else if (xM - xR == -1) // pojede doprava
                {
                    return 1;
                }
                else if (yM - yR == 1)  // pojede nahoru
                {
                    return 0;
                }
                else //if (yM - yR == -1) // pojede dolu
                {
                    return 2;
                }
            }
            catch (InvalidOperationException)
            {
                return 5;
            }

        }

        private Stack<Vertex> getPathStackBFS(int xM, int yM)
        {
            int xP = 0;
            int yP = 0;
            switch (directionPacman)
            {
                case 0:
                    {
                        xP = ((pacman.Location.X) / 30);
                        yP = ((pacman.Location.Y) / 30) - 1;
                        break;
                    }
                case 1:
                    {
                        xP = ((pacman.Location.X) / 30);
                        yP = (((pacman.Location.Y) / 30) - 1);
                        break;
                    }
                case 2:
                    {
                        xP = (pacman.Location.X / 30);
                        yP = ((pacman.Location.Y) / 30)-1;
                        break;
                    }
                case 3:
                    {
                        xP = ((pacman.Location.X ) / 30);
                        yP = ((pacman.Location.Y) / 30) - 1;
                        break;
                    }
            }
            BreathFirstSearch<DirectedUnweightedGraph>.Search(mapGraph, xM + " " + yM);

            var path = BreathFirstSearch<DirectedUnweightedGraph>.GetShortestPath(mapGraph, xP + " " + yP);
            path.Pop();
            return path;
        }
        private Stack<Vertex> getPathStackDFS(int xM, int yM)
        {
            int xP = 0;
            int yP = 0;
            switch (directionPacman)
            {
                case 0:
                    {
                        xP = ((pacman.Location.X) / 30);
                        yP = ((pacman.Location.Y) / 30) - 1;
                        break;
                    }
                case 1:
                    {
                        xP = ((pacman.Location.X) / 30);
                        yP = (((pacman.Location.Y) / 30) - 1);
                        break;
                    }
                case 2:
                    {
                        xP = (pacman.Location.X / 30);
                        yP = ((pacman.Location.Y) / 30) - 1;
                        break;
                    }
                case 3:
                    {
                        xP = ((pacman.Location.X) / 30);
                        yP = ((pacman.Location.Y) / 30) - 1;
                        break;
                    }
            }
            DepthFirstSearch<DirectedUnweightedGraph>.Search(mapGraph, xM + " " + yM);

            var path = DepthFirstSearch<DirectedUnweightedGraph>.GetPath(mapGraph, xP + " " + yP);
            path.Pop();
            return path;
        }
        private void MovementMonster(ref int smer, PictureBox prisera, int rychlost)
        {
            if (prisera.Image == deadM) // mrtva prisera se nehejbe
            {
                return;
            }
            int i = ((prisera.Location.X) / 30);
            int j = ((prisera.Location.Y) / 30) - 1;
            if (prisera.Image == blueM)
            {
                if (counterBlueM % 2 == 0)
                {
                    smer = findPath(i, j);
                    directionBlueM = smer;
                }
                else
                {
                    smer = directionBlueM;
                }
                counterBlueM++;

            }
            else if (prisera.Image == yellowM)
            {
                if (pathYellowM.Count == 0)
                {
                    pathYellowM = getPathStackBFS(i, j);
                }
                try
                {
                    var (xR, yR) = ReadInt.ReadVertexName(pathYellowM.Pop().name);

                    if (i - xR == 1)       // pojede doleva
                    {
                        smer = 3;
                    }
                    else if (i - xR == -1) // pojede doprava
                    {
                        smer = 1;
                    }
                    else if (j - yR == 1)  // pojede nahoru
                    {
                        smer = 0;
                    }
                    else //if (yM - yR == -1) // pojede dolu
                    {
                        smer = 2;
                    }
                }
                catch (InvalidOperationException)
                {
                    smer = 5;
                }
            }
            else
            {
                if (pathPurpleM.Count == 0)
                {
                    pathPurpleM = getPathStackDFS(i, j);
                }
                try
                {
                    var (xR, yR) = ReadInt.ReadVertexName(pathPurpleM.Pop().name);

                    if (i - xR == 1)       // pojede doleva
                    {
                        smer = 3;
                    }
                    else if (i - xR == -1) // pojede doprava
                    {
                        smer = 1;
                    }
                    else if (j - yR == 1)  // pojede nahoru
                    {
                        smer = 0;
                    }
                    else //if (yM - yR == -1) // pojede dolu
                    {
                        smer = 2;
                    }
                }
                catch (InvalidOperationException)
                {
                    smer = 5;
                }
            }
            switch (smer)
            {
                case 0:
                    {
                        prisera.Location = new Point(prisera.Location.X, prisera.Location.Y - rychlost);
                        break;
                    }
                case 1:
                    {
                        prisera.Location = new Point(prisera.Location.X + rychlost, prisera.Location.Y);
                        break;
                    }
                case 2:
                    {
                        prisera.Location = new Point(prisera.Location.X, prisera.Location.Y + rychlost);
                        break;
                    }
                case 3:
                    {
                        prisera.Location = new Point(prisera.Location.X - rychlost, prisera.Location.Y);
                        break;
                    }
                case 5:
                    {
                        break;
                    }
            }
        }

        private void movement_Tick(object sender, EventArgs e)
        {
            
            if (directionNext != directionPacman && ChangeDirection(directionNext))            
            {
                MovementPacman(directionNext);
                directionPacman = directionNext;
            }
            else
            {
                MovementPacman(directionPacman);
            }
            
            MovementMonster(ref directionPurpleM, purpleMonstrum, speedPurpleM);
            MovementMonster(ref directionYellowM, yellowMonstrum, speedYellowM);
            MovementMonster(ref directionBlueM, blueMonstrum, speedBlueM);
            DecreaseHealth();
        }

    }
    class Map
    {
        public static int coinAmount = 0;
        public static int[,] mapa = new int[21, 21];
        public static void LoadMap(string level)
        {
            StreamReader file = new StreamReader(level);
            for (int y = 0; y < 21; y++)
            {
                for (int x = 0; x < 21; x++)
                {
                    mapa[x, y] = file.Read();
                    if (mapa[x, y] == 'o')
                    {
                        coinAmount++;
                    }
                }
                file.ReadLine();
            }
        }
    }

    class ReadInt
    {
        public static (int,int) ReadVertexName(string text)
        {
            int i = 0;
            int x=0, y = 0;
            int digit = text[i];

            while ((digit >= 48) && (digit <= 57))
            {
                x = x * 10 + digit - 48;
                i++;
                digit = text[i];
            }
            i++;
            digit = text[i];
            while ((digit >= 48) && (digit <= 57))
            {
                y = y * 10 + digit - 48;
                i++;
                if (i >= text.Length)
                {
                    return (x, y);
                }
                digit = text[i];
            }
            return (x,y);
        }
    }

}
