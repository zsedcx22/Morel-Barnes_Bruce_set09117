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

        private bool redSwitch = false;
        private List<Button> redPieces = new List<Button>();
        private List<Button> redYellow = new List<Button>();
        private List<Button> redMoveable = new List<Button>();



        private void clickMethod(Button button)
        {
            string bName = button.Name;
            string diagName1 = "";
            string diagName2 = "";


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
                if (button.Foreground == Brushes.Red)
                {
                    redPieces.Remove(last);
                    redPieces.Add(button);
                }

                if (button.Foreground == Brushes.Green)
                {
                    greenPieces.Remove(last);
                    greenPieces.Add(button);
                }

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
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (button.Foreground == Brushes.Green)
                    {
                        char tmp1 = button.Name[0];
                        char tmp2 = button.Name[1];
                        tmp1++;
                        tmp2++;
                        string full = tmp1.ToString() + tmp2.ToString();
                        Button tmp = (Button)FindName(full);
                        if (tmp != null)
                        {
                            if (redPieces.Contains(tmp))
                            {
                                tmp1++;
                                tmp2++;
                                full = tmp1.ToString() + tmp2.ToString();
                                Button dat = (Button)FindName(full);
                                if (dat != null)
                                {
                                    if (dat.Content.ToString() == "")
                                    {
                                        newBlue = true;
                                        dat.Background = Brushes.Yellow;
                                        last = button;
                                        diag1 = dat;
                                    }
                                }
                            }
                        }

                        tmp1 = button.Name[0];
                        tmp2 = button.Name[1];
                        tmp1++;
                        tmp2--;
                        full = tmp1.ToString() + tmp2.ToString();
                        tmp = (Button)FindName(full);
                        if (tmp != null)
                        {
                            if (redPieces.Contains(tmp))
                            {
                                tmp1++;
                                tmp2--;
                                full = tmp1.ToString() + tmp2.ToString();
                                Button dat = (Button)FindName(full);
                                if (dat != null)
                                {
                                    if (dat.Content.ToString() == "")
                                    {
                                        newBlue = true;
                                        dat.Background = Brushes.Yellow;
                                        last = button;
                                        diag2 = dat;
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
                            full = tmp1.ToString() + tmp2.ToString();
                            tmp = (Button)FindName(full);
                            if (tmp != null)
                            {
                                if (redPieces.Contains(tmp))
                                {
                                    tmp1--;
                                    tmp2--;
                                    full = tmp1.ToString() + tmp2.ToString();
                                    Button dat = (Button)FindName(full);
                                    if (dat != null)
                                    {
                                        if (dat.Content.ToString() == "")
                                        {
                                            newBlue = true;
                                            dat.Background = Brushes.Yellow;
                                            last = button;
                                            diag3 = dat;
                                        }
                                    }
                                }
                            }

                            tmp1 = button.Name[0];
                            tmp2 = button.Name[1];
                            tmp1--;
                            tmp2++;
                            full = tmp1.ToString() + tmp2.ToString();
                            tmp = (Button)FindName(full);
                            if (tmp != null)
                            {
                                if (redPieces.Contains(tmp))
                                {
                                    tmp1--;
                                    tmp2++;
                                    full = tmp1.ToString() + tmp2.ToString();
                                    Button dat = (Button)FindName(full);
                                    if (dat != null)
                                    {
                                        if (dat.Content.ToString() == "")
                                        {
                                            newBlue = true;
                                            dat.Background = Brushes.Yellow;
                                            last = button;
                                            diag4 = dat;
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
                
                if (diag1 != null)
                {
                    if (diag2 != null)
                    {
                        diag1.Background = Brushes.Black;
                        diag2.Background = Brushes.Black;
                    }
                    else
                    {
                        diag1.Background = Brushes.Black;
                    }
                }
                replay.Enqueue(last.Name + "|" + bName);
                //MessageBox.Show(replay.Dequeue());
                /*foreach (string x in replay)
                {
                    MessageBox.Show(x);
                }*/
                diag1 = null;
                diag2 = null;
                if (extraJump == false)
                {
                    turn++;
                }
                lastBlue = false;



                if (greenSwitch == true && button.Foreground == Brushes.Red)
                {
                    ComputerAction();
                }
                else if (greenSwitch == true && button.Foreground == Brushes.Green)
                {
                    greenPieces.Remove(last);
                    greenPieces.Add(button);
                }

                if (redSwitch == true && button.Foreground == Brushes.Green)
                {
                    ComputerAction();
                }
                else if (redSwitch == true && button.Foreground == Brushes.Red)
                {
                    redPieces.Remove(last);
                    redPieces.Add(button);
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
                                    last = button;
                                    diag1 = dat;
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
                                    last = button;
                                    diag2 = dat;
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
                                        last = button;
                                        diag3 = dat;
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
                                        last = button;
                                        diag4 = dat;
                                    }
                                }
                            }
                        }
                    }
                }
                else if (button.Foreground == Brushes.Green)
                {
                    char tmp1 = button.Name[0];
                    char tmp2 = button.Name[1];
                    tmp1++;
                    tmp2++;
                    string full = tmp1.ToString() + tmp2.ToString();
                    Button tmp = (Button)FindName(full);
                    if (tmp != null)
                    {
                        if (redPieces.Contains(tmp))
                        {
                            tmp1++;
                            tmp2++;
                            full = tmp1.ToString() + tmp2.ToString();
                            Button dat = (Button)FindName(full);
                            if (dat != null)
                            {
                                if (dat.Content.ToString() == "")
                                {
                                    dat.Background = Brushes.Yellow;
                                    last = button;
                                    diag1 = dat;
                                }
                            }
                        }
                    }

                    tmp1 = button.Name[0];
                    tmp2 = button.Name[1];
                    tmp1++;
                    tmp2--;
                    full = tmp1.ToString() + tmp2.ToString();
                    tmp = (Button)FindName(full);
                    if (tmp != null)
                    {
                        if (redPieces.Contains(tmp))
                        {
                            tmp1++;
                            tmp2--;
                            full = tmp1.ToString() + tmp2.ToString();
                            Button dat = (Button)FindName(full);
                            if (dat != null)
                            {
                                if (dat.Content.ToString() == "")
                                {
                                    dat.Background = Brushes.Yellow;
                                    last = button;
                                    diag2 = dat;
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
                        full = tmp1.ToString() + tmp2.ToString();
                        tmp = (Button)FindName(full);
                        if (tmp != null)
                        {
                            if (redPieces.Contains(tmp))
                            {
                                tmp1--;
                                tmp2--;
                                full = tmp1.ToString() + tmp2.ToString();
                                Button dat = (Button)FindName(full);
                                if (dat != null)
                                {
                                    if (dat.Content.ToString() == "")
                                    {
                                        dat.Background = Brushes.Yellow;
                                        last = button;
                                        diag3 = dat;
                                    }
                                }
                            }
                        }

                        tmp1 = button.Name[0];
                        tmp2 = button.Name[1];
                        tmp1--;
                        tmp2++;
                        full = tmp1.ToString() + tmp2.ToString();
                        tmp = (Button)FindName(full);
                        if (tmp != null)
                        {
                            if (redPieces.Contains(tmp))
                            {
                                tmp1--;
                                tmp2++;
                                full = tmp1.ToString() + tmp2.ToString();
                                Button dat = (Button)FindName(full);
                                if (dat != null)
                                {
                                    if (dat.Content.ToString() == "")
                                    {
                                        dat.Background = Brushes.Yellow;
                                        last = button;
                                        diag4 = dat;
                                    }
                                }
                            }
                        }
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
                        string full = tmp1.ToString() + tmp2.ToString();
                        Button tmp = (Button)FindName(full);
                        if (tmp != null)
                        {
                            if (redPieces.Contains(tmp))
                            {
                                tmp1++;
                                tmp2++;
                                full = tmp1.ToString() + tmp2.ToString();
                                Button dat = (Button)FindName(full);
                                if (dat != null)
                                {
                                    if (dat.Content.ToString() == "")
                                    {
                                        piece.Background = Brushes.Blue;
                                        blueExists = true;
                                    }
                                }
                            }
                        }

                        tmp1 = piece.Name[0];
                        tmp2 = piece.Name[1];
                        tmp1++;
                        tmp2--;
                        full = tmp1.ToString() + tmp2.ToString();
                        tmp = (Button)FindName(full);
                        if (tmp != null)
                        {
                            if (redPieces.Contains(tmp))
                            {
                                tmp1++;
                                tmp2--;
                                full = tmp1.ToString() + tmp2.ToString();
                                Button dat = (Button)FindName(full);
                                if (dat != null)
                                {
                                    if (dat.Content.ToString() == "")
                                    {
                                        piece.Background = Brushes.Blue;
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
                            full = tmp1.ToString() + tmp2.ToString();
                            tmp = (Button)FindName(full);
                            if (tmp != null)
                            {
                                if (redPieces.Contains(tmp))
                                {
                                    tmp1--;
                                    tmp2--;
                                    full = tmp1.ToString() + tmp2.ToString();
                                    Button dat = (Button)FindName(full);
                                    if (dat != null)
                                    {
                                        if (dat.Content.ToString() == "")
                                        {
                                            piece.Background = Brushes.Blue;
                                            blueExists = true;
                                        }
                                    }
                                }
                            }

                            tmp1 = piece.Name[0];
                            tmp2 = piece.Name[1];
                            tmp1--;
                            tmp2++;
                            full = tmp1.ToString() + tmp2.ToString();
                            tmp = (Button)FindName(full);
                            if (tmp != null)
                            {
                                if (redPieces.Contains(tmp))
                                {
                                    tmp1--;
                                    tmp2++;
                                    full = tmp1.ToString() + tmp2.ToString();
                                    Button dat = (Button)FindName(full);
                                    if (dat != null)
                                    {
                                        if (dat.Content.ToString() == "")
                                        {
                                            piece.Background = Brushes.Blue;
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
                            MessageBox.Show("You must select a piece with a blue background as you must take a piece.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("You must select a piece with a blue background as you must take a piece.");
                }

                if (blueExists == false)
                {
                    if (diag1 != null)
                    {
                        if (diag2 != null)
                        {
                            diag1.Background = Brushes.Black;
                            diag2.Background = Brushes.Black;
                        }
                        else
                        {
                            diag1.Background = Brushes.Black;
                        }
                    }
                    diag1 = null;
                    diag2 = null;

                    char temp1 = bName[0];
                    int temp2 = (int)Char.GetNumericValue(bName[1]);

                    temp1++;
                    temp2--;
                    diagName1 = temp1.ToString() + temp2;

                    temp2++;
                    temp2++;
                    diagName2 = temp1.ToString() + temp2;

                    Button correct1 = (Button)FindName(diagName1);
                    if (correct1 != null)
                    {
                        if (correct1.Content.ToString() == "" && correct1.Background != Brushes.Yellow)
                        {
                            correct1.Background = Brushes.Yellow;
                            greenYellow.Add(correct1);
                        }
                        else if (correct1.Content.ToString() != "" && correct1.Background != Brushes.Yellow && correct1.Foreground == Brushes.Red)
                        {
                            temp1 = bName[0];
                            temp2 = (int)Char.GetNumericValue(bName[1]);
                            temp1++;
                            temp1++;
                            temp2--;
                            temp2--;
                            diagName1 = temp1.ToString() + temp2;
                            Button reality = (Button)FindName(diagName1);
                            if (reality != null)
                            {
                                if (reality.Content.ToString() == "" && reality.Background != Brushes.Yellow)
                                {
                                    reality.Background = Brushes.Yellow;
                                    greenYellow.Add(reality);
                                }
                            }
                        }
                        else
                        {
                            correct1.Background = Brushes.Black;
                        }
                    }

                    Button correct2 = (Button)FindName(diagName2);
                    if (correct2 != null)
                    {
                        if (correct2.Content.ToString() == "" && correct2.Background != Brushes.Yellow)
                        {
                            correct2.Background = Brushes.Yellow;
                            greenYellow.Add(correct2);
                        }
                        else if (correct2.Content.ToString() != "" && correct2.Background != Brushes.Yellow && correct2.Foreground == Brushes.Red)
                        {
                            temp1 = bName[0];
                            temp2 = (int)Char.GetNumericValue(bName[1]);
                            temp1++;
                            temp2++;
                            temp1++;
                            temp2++;
                            diagName2 = temp1.ToString() + temp2;
                            Button reality = (Button)FindName(diagName2);
                            if (reality != null)
                            {
                                if (reality.Content.ToString() == "" && reality.Background != Brushes.Yellow)
                                {
                                    reality.Background = Brushes.Yellow;
                                    greenYellow.Add(reality);
                                }
                            }
                        }
                        else
                        {
                            correct2.Background = Brushes.Black;
                        }
                        
                    }
                    last = button;
                    diag1 = (Button)FindName(diagName1);
                    diag2 = (Button)FindName(diagName2);
                    if (greenSwitch == true)
                    {
                        if (greenYellow.Count != 0)
                        {
                            ComputerPlace();
                            foreach (Button yellow in greenYellow.ToList())
                            {
                                greenYellow.Remove(yellow);
                            }
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
                            MessageBox.Show("You must select a piece with a blue background as you must take a piece."); //error
                        }
                    }
                }
                else
                {
                    MessageBox.Show("You must select a piece with a blue background as you must take a piece."); //error
                }

                if (blueExists == false)
                {
                    if (diag1 != null)
                    {
                        if (diag2 != null)
                        {
                            diag1.Background = Brushes.Black;
                            diag2.Background = Brushes.Black;
                        }
                        else
                        {
                            diag1.Background = Brushes.Black;
                        }
                    }
                    diag1 = null;
                    diag2 = null;

                    char temp1 = bName[0];
                    int temp2 = (int)Char.GetNumericValue(bName[1]);

                    temp1--;
                    temp2--;
                    diagName1 = temp1.ToString() + temp2;

                    temp2++;
                    temp2++;
                    diagName2 = temp1.ToString() + temp2;

                    Button correct1 = (Button)FindName(diagName1);
                    if (correct1 != null)
                    {
                        if (correct1.Content.ToString() == "" && correct1.Background != Brushes.Yellow)
                        {
                            correct1.Background = Brushes.Yellow;
                        }
                        else if (correct1.Content.ToString() != "" && correct1.Background != Brushes.Yellow && correct1.Foreground == Brushes.Green)
                        {
                            temp1 = bName[0];
                            temp2 = (int)Char.GetNumericValue(bName[1]);
                            temp1--;
                            temp1--;
                            temp2--;
                            temp2--;
                            diagName1 = temp1.ToString() + temp2;
                            Button reality = (Button)FindName(diagName1);
                            if (reality != null)
                            {
                                if (reality.Content.ToString() == "" && reality.Background != Brushes.Yellow)
                                {
                                    reality.Background = Brushes.Yellow;
                                }
                            }
                        }
                        else
                        {
                            correct1.Background = Brushes.Black;
                        }
                    }

                    Button correct2 = (Button)FindName(diagName2);
                    if (correct2 != null)
                    {
                        if (correct2.Content.ToString() == "" && correct2.Background != Brushes.Yellow)
                        {
                            correct2.Background = Brushes.Yellow;
                        }
                        else if (correct2.Content.ToString() != "" && correct2.Background != Brushes.Yellow && correct2.Foreground == Brushes.Green)
                        {
                            temp1 = bName[0];
                            temp2 = (int)Char.GetNumericValue(bName[1]);
                            temp1--;
                            temp2++;
                            temp1--;
                            temp2++;
                            diagName2 = temp1.ToString() + temp2;
                            Button reality = (Button)FindName(diagName2);
                            if (reality != null)
                            {
                                if (reality.Content.ToString() == "" && reality.Background != Brushes.Yellow)
                                {
                                    reality.Background = Brushes.Yellow;
                                }
                            }
                        }
                        else
                        {
                            correct2.Background = Brushes.Black;
                        }
                        
                    }
                    last = button;
                    diag1 = (Button)FindName(diagName1);
                    diag2 = (Button)FindName(diagName2);
                }
            }
        }
        private void ComputerAction()
        {
            Button rand;
            System.Threading.Thread.Sleep(50);
            rand = greenPieces[new Random().Next(0,greenPieces.Count)];
            //REMOVE NON TAKING OPTIONS for each piece if there is an enemy piece capturable then show player moveable pieces by turning them blue returning MUST TAKE PIECE? then add if blue turn all blue black and show options as yellow and call clickmethod
            //WHAT NEEDS ADDING IS TAKING PIECES AND REPLAY AND UNDO AND REDO AND FORCING JUMPING AND MULTI JUMPING AND KINGS
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

        private void ComputerPlace()
        {
            Button rand = greenYellow[new Random().Next(greenYellow.Count)];
            clickMethod(rand);
            foreach(Button item in greenMoveable.ToList())
            {
                greenMoveable.Remove(item);
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
                //greenSwitch = true;
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
                MessageBox.Show("Green AI On");
            }
            else if (sender == vsRedAI)
            {
                //redSwitch = true;
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
                MessageBox.Show("Red AI On");
            }
        }

        private void tempSet_Click(object sender, RoutedEventArgs e)
        {
            //if((Button)FindName(button.name[0]--.toString()+button.name[1]--.toString()).content.toString()==O)
            //Button benjamin = (Button)FindName("A1");
            //pieces.Add(benjamin);
            //MessageBox.Show(pieces.Contains(A1).ToString());
            pieces.Add(A2);
            pieces.Add(A4);
            pieces.Add(A6);
            pieces.Add(A8);
            pieces.Add(B1);
            pieces.Add(B3);
            pieces.Add(B5);
            pieces.Add(B7);
            pieces.Add(C2);
            pieces.Add(C4);
            pieces.Add(C6);
            pieces.Add(C8);
            pieces.Add(F1);
            pieces.Add(F3);
            pieces.Add(F5);
            pieces.Add(F7);
            pieces.Add(G2);
            pieces.Add(G4);
            pieces.Add(G6);
            pieces.Add(G8);
            pieces.Add(H1);
            pieces.Add(H3);
            pieces.Add(H5);
            pieces.Add(H7);
        }
    }
}
