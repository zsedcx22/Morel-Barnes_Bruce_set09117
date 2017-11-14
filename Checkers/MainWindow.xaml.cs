using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Checkers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private Queue<string> replay = new Queue<string>(); //change to stack maybe for additional brownie points maybe use queue for this and/or doing the replay
        private Button last;
        private Button diag1;
        private Button diag2;
        private Button diag3;
        private Button diag4;
        private int turn = 0;

        private List<Button> pieces = new List<Button>();

        private bool blueExists = false;

        private bool greenSwitch = false;
        private List<Button> greenPieces = new List<Button>();
        private List<Button> greenYellow = new List<Button>();
        private List<Button> greenMoveable = new List<Button>();
        private List<Button> greenBlue = new List<Button>();

        private bool redSwitch = false;
        private List<Button> redPieces = new List<Button>();
        private List<Button> redYellow = new List<Button>();
        private List<Button> redMoveable = new List<Button>();
        private List<Button> redBlue = new List<Button>();

        private void clickMethod(Button button) //consider making the "blue" section into its own function
        {
            string bName = button.Name;
            if (button.Background == Brushes.Yellow)
            {
                blueExists = false;
                bool extraJump = false;
                bool lastBlue = false;
                if (last.Background == Brushes.Blue)
                {
                    char x = (char)((last.Name[0] + button.Name[0]) / 2);
                    char y = (char)((last.Name[1] + button.Name[1]) / 2);
                    Button remove = (Button)FindName(x.ToString() + y.ToString());
                    remove.Content = "";
                    remove.Foreground = Brushes.Black;
                    if (redPieces.Contains(remove))
                    {
                        redPieces.Remove(remove);
                    }
                    else if (greenPieces.Contains(remove))
                    {
                        greenPieces.Remove(remove);
                    }
                    lastBlue = true;
                }

                foreach (Button piece in redPieces)
                {
                    piece.Background = Brushes.Black;
                }

                foreach (Button piece in greenPieces)
                {
                    piece.Background = Brushes.Black;
                }

                if (button.Name[0] == 'A' || button.Name[0] == 'H')
                {
                    button.Content = "K";
                }
                else
                {
                    button.Content = last.Content;
                }

                button.Foreground = last.Foreground;
                button.Background = last.Background;
                last.Foreground = Brushes.Black;
                last.Content = "";
                if (redPieces.Contains(last))
                {
                    redPieces.Remove(last);
                    redPieces.Add(button);
                }

                if (greenPieces.Contains(last))
                {
                    greenPieces.Remove(last);
                    greenPieces.Add(button);
                }
                
                replay.Enqueue(last.Name + "|" + bName);

                if (lastBlue == true)
                {
                    bool newBlue = false;
                    if (redPieces.Contains(button)) //if a red piece in a taking position is clicked then show only valid options
                    {
                        char tmp1 = button.Name[0];
                        char tmp2 = button.Name[1];
                        tmp1--;
                        tmp2--;
                        Button tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                        if (tmp != null)
                        {
                            if (greenPieces.Contains(tmp))
                            {
                                tmp1--;
                                tmp2--;
                                Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                                if (dat != null)
                                {
                                    if (dat.Content.ToString() == "")
                                    {
                                        newBlue = true;
                                        dat.Background = Brushes.Yellow;
                                        last = button;
                                        diag1 = dat;
                                        if (redSwitch == true)
                                        {
                                            redBlue.Add(button);
                                        }
                                    }
                                }
                            }
                        }

                        tmp1 = button.Name[0];
                        tmp2 = button.Name[1];
                        tmp1--;
                        tmp2++;
                        tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                        if (tmp != null)
                        {
                            if (greenPieces.Contains(tmp))
                            {
                                tmp1--;
                                tmp2++;
                                Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                                if (dat != null)
                                {
                                    if (dat.Content.ToString() == "")
                                    {
                                        newBlue = true;
                                        dat.Background = Brushes.Yellow;
                                        last = button;
                                        diag2 = dat;
                                        if (redSwitch == true)
                                        {
                                            redBlue.Add(button);
                                        }
                                    }
                                }
                            }
                        }
                        if (button.Content.ToString() == "K") //if king check "behind" the piece
                        {
                            tmp1 = button.Name[0];
                            tmp2 = button.Name[1];
                            tmp1++;
                            tmp2++;
                            tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                            if (tmp != null)
                            {
                                if (greenPieces.Contains(tmp))
                                {
                                    tmp1++;
                                    tmp2++;
                                    Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                                    if (dat != null)
                                    {
                                        if (dat.Content.ToString() == "")
                                        {
                                            newBlue = true;
                                            dat.Background = Brushes.Yellow;
                                            last = button;
                                            diag3 = dat;
                                            if (redSwitch == true)
                                            {
                                                redBlue.Add(button);
                                            }
                                        }
                                    }
                                }
                            }

                            tmp1 = button.Name[0];
                            tmp2 = button.Name[1];
                            tmp1++;
                            tmp2--;
                            tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                            if (tmp != null)
                            {
                                if (greenPieces.Contains(tmp))
                                {
                                    tmp1++;
                                    tmp2--;
                                    Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                                    if (dat != null)
                                    {
                                        if (dat.Content.ToString() == "")
                                        {
                                            newBlue = true;
                                            dat.Background = Brushes.Yellow;
                                            last = button;
                                            diag4 = dat;
                                            if (redSwitch == true)
                                            {
                                                redBlue.Add(button);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (greenPieces.Contains(button))
                    {
                        char tmp1 = button.Name[0];
                        char tmp2 = button.Name[1];
                        tmp1++;
                        tmp2++;
                        Button tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                        if (tmp != null)
                        {
                            if (redPieces.Contains(tmp))
                            {
                                tmp1++;
                                tmp2++;
                                Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                                if (dat != null)
                                {
                                    if (dat.Content.ToString() == "")
                                    {
                                        newBlue = true;
                                        dat.Background = Brushes.Yellow;
                                        last = button;
                                        diag1 = dat;
                                        if (greenSwitch == true)
                                        {
                                            greenBlue.Add(button);
                                        }
                                    }
                                }
                            }
                        }

                        tmp1 = button.Name[0];
                        tmp2 = button.Name[1];
                        tmp1++;
                        tmp2--;
                        tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                        if (tmp != null)
                        {
                            if (redPieces.Contains(tmp))
                            {
                                tmp1++;
                                tmp2--;
                                Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                                if (dat != null)
                                {
                                    if (dat.Content.ToString() == "")
                                    {
                                        newBlue = true;
                                        dat.Background = Brushes.Yellow;
                                        last = button;
                                        diag2 = dat;
                                        if (greenSwitch == true)
                                        {
                                            greenBlue.Add(button);
                                        }
                                    }
                                }
                            }
                        }
                        if (button.Content.ToString() == "K") //if king check "behind" the piece
                        {
                            tmp1 = button.Name[0];
                            tmp2 = button.Name[1];
                            tmp1--;
                            tmp2--;
                            tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                            if (tmp != null)
                            {
                                if (redPieces.Contains(tmp))
                                {
                                    tmp1--;
                                    tmp2--;
                                    Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                                    if (dat != null)
                                    {
                                        if (dat.Content.ToString() == "")
                                        {
                                            newBlue = true;
                                            dat.Background = Brushes.Yellow;
                                            last = button;
                                            diag3 = dat;
                                            if (greenSwitch == true)
                                            {
                                                greenBlue.Add(button);
                                            }
                                        }
                                    }
                                }
                            }

                            tmp1 = button.Name[0];
                            tmp2 = button.Name[1];
                            tmp1--;
                            tmp2++;
                            tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                            if (tmp != null)
                            {
                                if (redPieces.Contains(tmp))
                                {
                                    tmp1--;
                                    tmp2++;
                                    Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                                    if (dat != null)
                                    {
                                        if (dat.Content.ToString() == "")
                                        {
                                            newBlue = true;
                                            dat.Background = Brushes.Yellow;
                                            last = button;
                                            diag4 = dat;
                                            if (greenSwitch == true)
                                            {
                                                greenBlue.Add(button);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (newBlue == true)
                    {
                        button.Background = Brushes.Blue;
                        clickMethod(button);
                        extraJump = true;
                    }
                }

                DiagReset();
                
                //MessageBox.Show(replay.Dequeue());
                /*foreach (string x in replay)
                {
                    MessageBox.Show(x);
                }*/

                if (extraJump == false)
                {
                    turn++;
                }
                lastBlue = false;

                if (greenSwitch == true && button.Foreground == Brushes.Red)
                {
                    greenMoveable = new List<Button>();
                    greenYellow = new List<Button>();
                    greenBlue = new List<Button>();
                    ComputerAction();
                }

                if (redSwitch == true && button.Foreground == Brushes.Green)
                {
                    redMoveable = new List<Button>();
                    redYellow = new List<Button>();
                    redBlue = new List<Button>();
                    ComputerAction();
                }


                if (greenPieces.Count == 0)
                {
                    MessageBox.Show("Red Wins!");
                    greenSwitch = false;
                    redSwitch = false;
                    string replayReq = MessageBox.Show("Do you want to save your game?", "", MessageBoxButton.YesNo).ToString();
                    if (replayReq == "Yes")
                    {
                        Random rand = new Random();
                        int random = rand.Next(0, 1000);
                        int random2 = rand.Next(0, 1000);
                        string path = "replay" + random + random2 + ".rp";
                        System.IO.File.WriteAllLines(path, replay.ToArray());
                        MessageBox.Show("Saved under " + path);
                    }
                }
                else if (redPieces.Count == 0)
                {
                    MessageBox.Show("Green Wins!");
                    greenSwitch = false;
                    redSwitch = false;
                    string replayReq = MessageBox.Show("Do you want to save your game?", "", MessageBoxButton.YesNo).ToString();
                    if (replayReq == "Yes")
                    {
                        Random rand = new Random();
                        int random = rand.Next(0, 1000);
                        int random2 = rand.Next(0, 1000);
                        string path = "replay" + random + random2 + ".rp";
                        System.IO.File.WriteAllLines(path, replay.ToArray());
                        MessageBox.Show("Saved under " + path);
                    }
                }
            }
            else if (button.Background == Brushes.Blue) //BLUE IS FOR TAKING
            {
                if (redPieces.Contains(button)) //if a red piece in a taking position is clicked then show only valid options
                {
                    char tmp1 = button.Name[0];
                    char tmp2 = button.Name[1];
                    tmp1--;
                    tmp2--;
                    Button tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                    if (tmp != null)
                    {
                        if (greenPieces.Contains(tmp))
                        {
                            tmp1--;
                            tmp2--;
                            Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                            if (dat != null)
                            {
                                if (dat.Content.ToString() == "")
                                {
                                    dat.Background = Brushes.Yellow;
                                    diag1 = dat;
                                    last = button;
                                    if (redSwitch == true)
                                    {
                                        redYellow.Add(dat);
                                    }
                                }
                            }
                        }
                    }

                    tmp1 = button.Name[0];
                    tmp2 = button.Name[1];
                    tmp1--;
                    tmp2++;
                    tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                    if (tmp != null)
                    {
                        if (greenPieces.Contains(tmp))
                        {
                            tmp1--;
                            tmp2++;
                            Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                            if (dat != null)
                            {
                                if (dat.Content.ToString() == "")
                                {
                                    dat.Background = Brushes.Yellow;
                                    diag2 = dat;
                                    last = button;
                                    if (redSwitch == true)
                                    {
                                        redYellow.Add(dat);
                                    }
                                }
                            }
                        }
                    }
                    if (button.Content.ToString() == "K") //if king check "behind" the piece
                    {
                        tmp1 = button.Name[0];
                        tmp2 = button.Name[1];
                        tmp1++;
                        tmp2++;
                        tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                        if (tmp != null)
                        {
                            if (greenPieces.Contains(tmp))
                            {
                                tmp1++;
                                tmp2++;
                                Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                                if (dat != null)
                                {
                                    if (dat.Content.ToString() == "")
                                    {
                                        dat.Background = Brushes.Yellow;
                                        diag3 = dat;
                                        last = button;
                                        if (redSwitch == true)
                                        {
                                            redYellow.Add(dat);
                                        }
                                    }
                                }
                            }
                        }

                        tmp1 = button.Name[0];
                        tmp2 = button.Name[1];
                        tmp1++;
                        tmp2--;
                        tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                        if (tmp != null)
                        {
                            if (greenPieces.Contains(tmp))
                            {
                                tmp1++;
                                tmp2--;
                                Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                                if (dat != null)
                                {
                                    if (dat.Content.ToString() == "")
                                    {
                                        dat.Background = Brushes.Yellow;
                                        diag4 = dat;
                                        last = button;
                                        if (redSwitch == true)
                                        {
                                            redYellow.Add(dat);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (redSwitch == true)
                    {
                        ComputerPlace();
                        redYellow = new List<Button>();
                    }
                }
                else if (greenPieces.Contains(button)) //if green
                { //change part of this for cpu to "click" the yellow part
                    char tmp1 = button.Name[0];
                    char tmp2 = button.Name[1];
                    tmp1++;
                    tmp2++;
                    Button tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                    if (tmp != null)
                    {
                        if (redPieces.Contains(tmp))
                        {
                            tmp1++;
                            tmp2++;
                            Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                            if (dat != null)
                            {
                                if (dat.Content.ToString() == "")
                                {
                                    dat.Background = Brushes.Yellow;
                                    diag1 = dat;
                                    last = button;
                                    if (greenSwitch == true)
                                    {
                                        greenYellow.Add(dat);
                                    }
                                }
                            }
                        }
                    }

                    tmp1 = button.Name[0];
                    tmp2 = button.Name[1];
                    tmp1++;
                    tmp2--;
                    tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                    if (tmp != null)
                    {
                        if (redPieces.Contains(tmp))
                        {
                            tmp1++;
                            tmp2--;
                            Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                            if (dat != null)
                            {
                                if (dat.Content.ToString() == "")
                                {
                                    dat.Background = Brushes.Yellow;
                                    diag2 = dat;
                                    last = button;
                                    if (greenSwitch == true)
                                    {
                                        greenYellow.Add(dat);
                                    }
                                }
                            }
                        }
                    }
                    if (button.Content.ToString() == "K") //if king check "behind" the piece
                    {
                        tmp1 = button.Name[0];
                        tmp2 = button.Name[1];
                        tmp1--;
                        tmp2--;
                        tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                        if (tmp != null)
                        {
                            if (redPieces.Contains(tmp))
                            {
                                tmp1--;
                                tmp2--;
                                Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                                if (dat != null)
                                {
                                    if (dat.Content.ToString() == "")
                                    {
                                        dat.Background = Brushes.Yellow;
                                        diag3 = dat;
                                        last = button;
                                        if (greenSwitch == true)
                                        {
                                            greenYellow.Add(dat);
                                        }
                                    }
                                }
                            }
                        }

                        tmp1 = button.Name[0];
                        tmp2 = button.Name[1];
                        tmp1--;
                        tmp2++;
                        tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                        if (tmp != null)
                        {
                            if (redPieces.Contains(tmp))
                            {
                                tmp1--;
                                tmp2++;
                                Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                                if (dat != null)
                                {
                                    if (dat.Content.ToString() == "")
                                    {
                                        dat.Background = Brushes.Yellow;
                                        diag4 = dat;
                                        last = button;
                                        if (greenSwitch == true)
                                        {
                                            greenYellow.Add(dat);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (greenSwitch == true)
                    {
                        ComputerPlace();
                        greenYellow = new List<Button>();
                    }
                }
            }
            else if (button.Foreground == Brushes.Green && ((turn % 2 != 0) == true)) //GREEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEN
            {
                if (blueExists == false)
                {
                    foreach (Button piece in greenPieces) //for each piece that green has check if it can take a red piece
                    {
                        char tmp1 = piece.Name[0];
                        char tmp2 = piece.Name[1];
                        tmp1++;
                        tmp2++;
                        Button tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                        if (tmp != null)
                        {
                            if (redPieces.Contains(tmp))
                            {
                                tmp1++;
                                tmp2++;
                                Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                                if (dat != null)
                                {
                                    if (dat.Content.ToString() == "")
                                    {
                                        piece.Background = Brushes.Blue;
                                        greenBlue.Add(piece); //computer player not choosing blue square, just choosing square then being checked, make separate check for computer player mb?
                                        last = button;
                                        blueExists = true;
                                    }
                                }
                            }
                        }

                        tmp1 = piece.Name[0];
                        tmp2 = piece.Name[1];
                        tmp1++;
                        tmp2--;
                        tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                        if (tmp != null)
                        {
                            if (redPieces.Contains(tmp))
                            {
                                tmp1++;
                                tmp2--;
                                Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                                if (dat != null)
                                {
                                    if (dat.Content.ToString() == "")
                                    {
                                        piece.Background = Brushes.Blue;
                                        greenBlue.Add(piece);
                                        last = button;
                                        blueExists = true;
                                    }
                                }
                            }
                        }
                        if (piece.Content.ToString() == "K") //if king check "behind" the piece
                        {
                            tmp1 = piece.Name[0];
                            tmp2 = piece.Name[1];
                            tmp1--;
                            tmp2--;
                            tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                            if (tmp != null)
                            {
                                if (redPieces.Contains(tmp))
                                {
                                    tmp1--;
                                    tmp2--;
                                    Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                                    if (dat != null)
                                    {
                                        if (dat.Content.ToString() == "")
                                        {
                                            piece.Background = Brushes.Blue;
                                            greenBlue.Add(piece);
                                            last = button;
                                            blueExists = true;
                                        }
                                    }
                                }
                            }

                            tmp1 = piece.Name[0];
                            tmp2 = piece.Name[1];
                            tmp1--;
                            tmp2++;
                            tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                            if (tmp != null)
                            {
                                if (redPieces.Contains(tmp))
                                {
                                    tmp1--;
                                    tmp2++;
                                    Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                                    if (dat != null)
                                    {
                                        if (dat.Content.ToString() == "")
                                        {
                                            piece.Background = Brushes.Blue;
                                            greenBlue.Add(piece);
                                            last = button;
                                            blueExists = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (blueExists == true)
                    {
                        if (button.Background == Brushes.Blue)
                        {
                            clickMethod(button);
                        }
                        else
                        {
                            if (greenSwitch == true)
                            {
                                ComputerAction();
                            }
                            else
                            {
                                MessageBox.Show("You must select a piece with a blue background as you must take a piece.");
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("You must select a piece with a blue background as you must take a piece.");
                }

                if (blueExists == false)
                {
                    DiagReset();
                    char temp1 = bName[0];
                    int temp2 = (int)Char.GetNumericValue(bName[1]);
                    temp1++;
                    temp2--;
                    diag1 = (Button)FindName(temp1.ToString() + temp2);
                    if (diag1 != null)
                    {
                        if (diag1.Content.ToString() == "" && diag1.Background != Brushes.Yellow)
                        {
                            diag1.Background = Brushes.Yellow;
                            last = button;
                            greenYellow.Add(diag1);
                        }
                        else
                        {
                            diag1.Background = Brushes.Black;
                        }
                    }

                    temp1 = bName[0];
                    temp2 = (int)Char.GetNumericValue(bName[1]);
                    temp1++;
                    temp2++;
                    diag2 = (Button)FindName(temp1.ToString() + temp2);
                    if (diag2 != null)
                    {
                        if (diag2.Content.ToString() == "" && diag2.Background != Brushes.Yellow)
                        {
                            diag2.Background = Brushes.Yellow;
                            last = button;
                            greenYellow.Add(diag2);
                        }
                        else
                        {
                            diag2.Background = Brushes.Black;
                        }

                    }

                    if (button.Content.ToString() == "K")
                    {
                        temp1 = bName[0];
                        temp2 = (int)Char.GetNumericValue(bName[1]);
                        temp1--;
                        temp2++;
                        diag3 = (Button)FindName(temp1.ToString() + temp2);
                        if (diag3 != null)
                        {
                            if (diag3.Content.ToString() == "" && diag3.Background != Brushes.Yellow)
                            {
                                diag3.Background = Brushes.Yellow;
                                last = button;
                                greenYellow.Add(diag3);
                            }
                            else
                            {
                                diag3.Background = Brushes.Black;
                            }

                        }

                        temp1 = bName[0];
                        temp2 = (int)Char.GetNumericValue(bName[1]);
                        temp1--;
                        temp2--;
                        diag4 = (Button)FindName(temp1.ToString() + temp2);
                        if (diag4 != null)
                        {
                            if (diag4.Content.ToString() == "" && diag4.Background != Brushes.Yellow)
                            {
                                diag4.Background = Brushes.Yellow;
                                last = button;
                                greenYellow.Add(diag4);
                            }
                            else
                            {
                                diag4.Background = Brushes.Black;
                            }

                        }
                    }
                    if (greenSwitch == true)
                    {
                        if (greenYellow.Count != 0 && greenPieces.Contains(button))
                        {
                            ComputerPlace();
                            greenYellow = new List<Button>();
                        }
                        else
                        {
                            ComputerAction();
                        }
                    }
                }
            }
            else if ((button.Foreground == Brushes.Red) && ((turn % 2 == 0) == true)) //REEEEEEEEEEEEEEEEEEEEED
            {

                //IF NO BLUES
                if (blueExists == false)
                {
                    foreach (Button piece in redPieces) //for each piece that red has check if
                    {
                        char tmp1 = piece.Name[0];
                        char tmp2 = piece.Name[1];
                        tmp1--;
                        tmp2--;
                        Button tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                        if (tmp != null)
                        {
                            if (greenPieces.Contains(tmp))
                            {
                                tmp1--;
                                tmp2--;
                                Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                                if (dat != null)
                                {
                                    if (dat.Content.ToString() == "")
                                    {
                                        piece.Background = Brushes.Blue;
                                        redBlue.Add(piece);
                                        last = button;
                                        blueExists = true;
                                    }
                                }
                            }
                        }

                        tmp1 = piece.Name[0];
                        tmp2 = piece.Name[1];
                        tmp1--;
                        tmp2++;
                        tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                        if (tmp != null)
                        {
                            if (greenPieces.Contains(tmp))
                            {
                                tmp1--;
                                tmp2++;
                                Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                                if (dat != null)
                                {
                                    if (dat.Content.ToString() == "")
                                    {
                                        piece.Background = Brushes.Blue;
                                        redBlue.Add(piece);
                                        last = button;
                                        blueExists = true;
                                    }
                                }
                            }
                        }
                        if (piece.Content.ToString() == "K") //if king check "behind" the piece
                        {
                            tmp1 = piece.Name[0];
                            tmp2 = piece.Name[1];
                            tmp1++;
                            tmp2++;
                            tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                            if (tmp != null)
                            {
                                if (greenPieces.Contains(tmp))
                                {
                                    tmp1++;
                                    tmp2++;
                                    Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                                    if (dat != null)
                                    {
                                        if (dat.Content.ToString() == "")
                                        {
                                            piece.Background = Brushes.Blue;
                                            redBlue.Add(piece);
                                            last = button;
                                            blueExists = true;
                                        }
                                    }
                                }
                            }

                            tmp1 = piece.Name[0];
                            tmp2 = piece.Name[1];
                            tmp1++;
                            tmp2--;
                            tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                            if (tmp != null)
                            {
                                if (greenPieces.Contains(tmp))
                                {
                                    tmp1++;
                                    tmp2--;
                                    Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                                    if (dat != null)
                                    {
                                        if (dat.Content.ToString() == "")
                                        {
                                            piece.Background = Brushes.Blue;
                                            redBlue.Add(piece);
                                            last = button;
                                            blueExists = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (blueExists == true)
                    {
                        if (button.Background == Brushes.Blue)
                        {
                            clickMethod(button);
                        }
                        else
                        {
                            if (redSwitch == true)
                            {
                                ComputerAction();
                            }
                            else
                            {
                                MessageBox.Show("You must select a piece with a blue background as you must take a piece.");
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("You must select a piece with a blue background as you must take a piece."); //error
                }

                if (blueExists == false)
                {
                    DiagReset();
                    char temp1 = bName[0];
                    int temp2 = (int)Char.GetNumericValue(bName[1]);
                    temp1--;
                    temp2--;
                    diag1 = (Button)FindName(temp1.ToString() + temp2);
                    if (diag1 != null)
                    {
                        if (diag1.Content.ToString() == "" && diag1.Background != Brushes.Yellow)
                        {
                            diag1.Background = Brushes.Yellow;
                            last = button;
                            redYellow.Add(diag1);
                        }
                        else
                        {
                            diag1.Background = Brushes.Black;
                        }
                    }

                    temp1 = bName[0];
                    temp2 = (int)Char.GetNumericValue(bName[1]);
                    temp1--;
                    temp2++;
                    diag2 = (Button)FindName(temp1.ToString() + temp2);
                    if (diag2 != null)
                    {
                        if (diag2.Content.ToString() == "" && diag2.Background != Brushes.Yellow)
                        {
                            diag2.Background = Brushes.Yellow;
                            last = button;
                            redYellow.Add(diag2);
                        }
                        else
                        {
                            diag2.Background = Brushes.Black;
                        }
                    }
                    if (button.Content.ToString() == "K")
                    {
                        temp1 = bName[0];
                        temp2 = (int)Char.GetNumericValue(bName[1]);
                        temp1++;
                        temp2++;
                        diag3 = (Button)FindName(temp1.ToString() + temp2);
                        if (diag3 != null)
                        {
                            if (diag3.Content.ToString() == "" && diag3.Background != Brushes.Yellow)
                            {
                                diag3.Background = Brushes.Yellow;
                                last = button;
                                redYellow.Add(diag3);
                            }
                            else
                            {
                                diag3.Background = Brushes.Black;
                            }

                        }

                        temp1 = bName[0];
                        temp2 = (int)Char.GetNumericValue(bName[1]);
                        temp1++;
                        temp2--;
                        diag4 = (Button)FindName(temp1.ToString() + temp2);
                        if (diag4 != null)
                        {
                            if (diag4.Content.ToString() == "" && diag4.Background != Brushes.Yellow)
                            {
                                diag4.Background = Brushes.Yellow;
                                last = button;
                                redYellow.Add(diag4);
                            }
                            else
                            {
                                diag4.Background = Brushes.Black;
                            }

                        }
                    }
                    if (redSwitch == true)
                    {
                        if (redYellow.Count != 0 && redPieces.Contains(button))
                        {
                            ComputerPlace();
                            redYellow = new List<Button>();
                        }
                        else
                        {
                            ComputerAction();
                        }
                    }
                }
            }
        }

        private void ComputerAction()
        {
            if (greenSwitch == true && ((turn % 2 != 0) == true))
            {
                if (greenBlue.Count == 0)
                {
                    Button rand;
                    System.Threading.Thread.Sleep(50);
                    if (greenPieces.Count != 0)
                    {
                        rand = greenPieces[new Random().Next(0, greenPieces.Count)];
                        if (greenMoveable.Contains(rand) == false)
                        {
                            greenMoveable.Add(rand);
                            clickMethod(rand);
                        }
                        else
                        {
                            ComputerAction();
                        }
                    }
                }
                else
                {
                    Button rand;
                    System.Threading.Thread.Sleep(50);
                    rand = greenBlue[new Random().Next(0, greenBlue.Count)];
                    if (greenMoveable.Contains(rand) == false)
                    {
                        greenMoveable.Add(rand);
                        clickMethod(rand);
                    }
                    else
                    {
                        ComputerAction();
                    }
                }
            }

            if (redSwitch == true && ((turn % 2 != 0) == false))
            {
                if (redBlue.Count == 0)
                {
                    Button rand;
                    System.Threading.Thread.Sleep(50);
                    if (redPieces.Count != 0)
                    {
                        rand = redPieces[new Random().Next(0, redPieces.Count)];
                        if (redMoveable.Contains(rand) == false)
                        {
                            redMoveable.Add(rand);
                            clickMethod(rand);
                        }
                        else
                        {
                            ComputerAction();
                        }
                    }
                }
                else
                {
                    Button rand;
                    System.Threading.Thread.Sleep(50);
                    rand = redBlue[new Random().Next(0, redBlue.Count)];
                    if (redMoveable.Contains(rand) == false)
                    {
                        redMoveable.Add(rand);
                        clickMethod(rand);
                    }
                    else
                    {
                        ComputerAction();
                    }
                }
            }
        }

        private void ComputerPlace()
        {
            if (greenSwitch == true && ((turn % 2 == 0) == false))
            {
                Button rand = greenYellow[new Random().Next(0, greenYellow.Count)];
                System.Threading.Thread.Sleep(50);
                MessageBox.Show(last.Name + "|" + rand.Name);
                clickMethod(rand);
                foreach (Button item in greenMoveable.ToList())
                {
                    greenMoveable.Remove(item);
                }
            }
            else if (redSwitch == true && ((turn % 2 == 0) == true))
            {
                Button rand = redYellow[new Random().Next(0, redYellow.Count)];
                System.Threading.Thread.Sleep(50);
                MessageBox.Show(last.Name + "|" + rand.Name);
                clickMethod(rand);
                foreach (Button item in redMoveable.ToList())
                {
                    redMoveable.Remove(item);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            clickMethod((Button)sender);
        }

        private void vsAI_Click(object sender, RoutedEventArgs e)
        {
            if (sender == vsGreenAI)
            {
                greenSwitch = true;
                MessageBox.Show("Green AI On");
            }
            else if (sender == vsRedAI)
            {
                redSwitch = true;
                MessageBox.Show("Red AI On");
            }
        }

        private void DiagReset()
        {
            if (diag1 != null)
            {
                diag1.Background = Brushes.Black;
            }
            if (diag2 != null)
            {
                diag2.Background = Brushes.Black;
            }
            if (diag3 != null)
            {
                diag3.Background = Brushes.Black;
            }
            if (diag4 != null)
            {
                diag4.Background = Brushes.Black;
            }
            diag1 = null;
            diag2 = null;
            diag3 = null;
            diag4 = null;
        }

        private void ReplaySetup()
        {
            replay = new Queue<string>();//make this in an if file exists function
            foreach (string line in System.IO.File.ReadAllLines(inputBox.Text))
            {
                replay.Enqueue(line);
            }
            CheckersWindow.Hide();
            Replay replayWin = new Replay(replay);
            replayWin.Show();
        }

        private void tempSet_Click(object sender, RoutedEventArgs e)
        {
            //if((Button)FindName(button.name[0]--.toString()+button.name[1]--.toString()).content.toString()==O)
            //Button benjamin = (Button)FindName("A1");
            //pieces.Add(benjamin);
            //MessageBox.Show(pieces.Contains(A1).ToString());
            greenPieces.Add(A2);
            greenPieces.Add(A4);
            greenPieces.Add(A6);
            greenPieces.Add(A8);
            greenPieces.Add(B1);
            greenPieces.Add(B3);
            greenPieces.Add(B5);
            greenPieces.Add(B7);
            greenPieces.Add(C2);
            greenPieces.Add(C4);
            greenPieces.Add(C6);
            greenPieces.Add(C8);
            redPieces.Add(F1);
            redPieces.Add(F3);
            redPieces.Add(F5);
            redPieces.Add(F7);
            redPieces.Add(G2);
            redPieces.Add(G4);
            redPieces.Add(G6);
            redPieces.Add(G8);
            redPieces.Add(H1);
            redPieces.Add(H3);
            redPieces.Add(H5);
            redPieces.Add(H7);
            if (redSwitch == true)
            {
                ComputerAction();
            }
        }

        private void loadReplay_Click(object sender, RoutedEventArgs e)
        {
            ReplaySetup();
        }
    }
}
