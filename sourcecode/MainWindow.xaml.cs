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

        private Queue<string> replay = new Queue<string>(); //queue to store each move done so far
        private Button last; //stores the last button selected
        private Button diag1; //stores a diagonal (used for determining legal moves)
        private Button diag2; //stores a diagonal (used for determining legal moves)
        private Button diag3; //stores a diagonal (used for determining legal moves)
        private Button diag4; //stores a diagonal (used for determining legal moves)
        private int turn = 0; //stores who's turn it is
        private bool blueExists = false; //a boolean to tell whether a piece can be taken

        private bool greenSwitch = false; //a boolean to tell whether the green computer player is on or off
        private List<Button> greenPieces = new List<Button>(); //a list to store the location of green's pieces
        //next 3 only relevant to computer player
        private List<Button> greenYellow = new List<Button>(); //a list to tell if the selected piece has any valid moves
        private List<Button> greenMoveable = new List<Button>(); //a list to tell if the selected piece has already been selected before (i.e. if there were no valid moves from that piece)
        private List<Button> greenBlue = new List<Button>(); //a list to tell what pieces can take another piece

        private bool redSwitch = false; //a boolean to tell whether the red computer player is on or off
        private List<Button> redPieces = new List<Button>(); //a list to store the location of red's pieces
        //next 3 only relevant to computer player
        private List<Button> redYellow = new List<Button>(); //a list to tell if the selected piece has any valid moves
        private List<Button> redMoveable = new List<Button>(); //a list to tell if the selected piece has already been selected before (i.e. if there were no valid moves from that piece)
        private List<Button> redBlue = new List<Button>(); //a list to tell what pieces can take another piece

        private void clickMethod(Button button) //main method that all buttons in the checkers board grid call when clicked
        {
            string bName = button.Name; //string to store the name of the button
            if (button.Background == Brushes.Yellow) //if the selected button was a location that was valid for a piece to move to
            {
                blueExists = false;
                bool extraJump = false; //boolean to tell whether after a taking of a piece, the turn should change to the other player
                bool lastBlue = false; //tells the system whether the last button selected was able to take a piece
                if (last.Background == Brushes.Blue) //if the last button selected could take another piece then remove the piece that it jumped over from the relevant list
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

                //reset the background colours of each piece in both lists
                foreach (Button piece in redPieces)
                {
                    piece.Background = Brushes.Black;
                }
                foreach (Button piece in greenPieces)
                {
                    piece.Background = Brushes.Black;
                }

                //if the piece reaches an end then make it a king if it didn't just set the current button's value to the last one's (note that validation for team is not required as pawns cannot move backwards)
                if (button.Name[0] == 'A' || button.Name[0] == 'H')
                {
                    button.Content = "K";
                }
                else
                {
                    button.Content = last.Content;
                }
                //move the last selected button to the currently selected button's location
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
                
                replay.Enqueue(last.Name + "|" + bName); //add the move to the replay queue

                if (lastBlue == true) //if the last selection could have taken a piece
                {
                    bool newBlue = false;
                    if (redPieces.Contains(button)) //if a red piece in a taking position is selected then show only valid options
                    {
                        char tmp1 = button.Name[0];
                        char tmp2 = button.Name[1];
                        tmp1--;
                        tmp2--;
                        Button tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the current one
                        if (tmp != null)
                        {
                            if (greenPieces.Contains(tmp))
                            {
                                tmp1--;
                                tmp2--;
                                Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the one diagonal to the current one
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
                        tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the current one
                        if (tmp != null)
                        {
                            if (greenPieces.Contains(tmp))
                            {
                                tmp1--;
                                tmp2++;
                                Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the one diagonal to the current one
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
                            tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the current one
                            if (tmp != null)
                            {
                                if (greenPieces.Contains(tmp))
                                {
                                    tmp1++;
                                    tmp2++;
                                    Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the one diagonal to the current one
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
                            tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the current one
                            if (tmp != null)
                            {
                                if (greenPieces.Contains(tmp))
                                {
                                    tmp1++;
                                    tmp2--;
                                    Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the one diagonal to the current one
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
                    else if (greenPieces.Contains(button)) //if a green piece in a taking position is selecteded then show only valid options
                    {
                        char tmp1 = button.Name[0];
                        char tmp2 = button.Name[1];
                        tmp1++;
                        tmp2++;
                        Button tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the current one
                        if (tmp != null)
                        {
                            if (redPieces.Contains(tmp))
                            {
                                tmp1++;
                                tmp2++;
                                Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the one diagonal to the current one
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
                        tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the current one
                        if (tmp != null)
                        {
                            if (redPieces.Contains(tmp))
                            {
                                tmp1++;
                                tmp2--;
                                Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the one diagonal to the current one
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
                            tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the current one
                            if (tmp != null)
                            {
                                if (redPieces.Contains(tmp))
                                {
                                    tmp1--;
                                    tmp2--;
                                    Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the one diagonal to the current one
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
                            tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the current one
                            if (tmp != null)
                            {
                                if (redPieces.Contains(tmp))
                                {
                                    tmp1--;
                                    tmp2++;
                                    Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the one diagonal to the current one
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
                    if (newBlue == true) //then if the current button can take another piece, force the user to take the other piece
                    {
                        button.Background = Brushes.Blue;
                        clickMethod(button);
                        extraJump = true;
                    }
                }

                DiagReset(); //reset the colours of each of the pieces diagonal to the current piece
                
                if (extraJump == false) //if the current player is not taking multiple pieces then pass the turn to the other player
                {
                    turn++;
                }
                lastBlue = false;

                if (greenPieces.Count == 0) //if the green player has no pieces then declare red as the winner and give them the option of saving their replay
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
                else if (redPieces.Count == 0) //if the red player has no pieces then declare green as the winner and give them the option of saving their replay
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

                if (greenSwitch == true && redPieces.Contains(button)) //if the greencomputer player is active and the last moved button was red then reset all of the temporary computer player lists and make the computer player select a piece
                {
                    greenMoveable = new List<Button>();
                    greenYellow = new List<Button>();
                    greenBlue = new List<Button>();
                    ComputerAction();
                }

                if (redSwitch == true && greenPieces.Contains(button)) //if the red computer player is active and the last moved button was green then reset all of the temporary computer player lists and make the computer player select a piece
                {
                    redMoveable = new List<Button>();
                    redYellow = new List<Button>();
                    redBlue = new List<Button>();
                    ComputerAction();
                }
            }
            else if (button.Background == Brushes.Blue) //blue means that the selected piece can take an opposing piece
            {
                if (redPieces.Contains(button)) //if a red piece in a taking position is selected then show only valid options
                {
                    char tmp1 = button.Name[0];
                    char tmp2 = button.Name[1];
                    tmp1--;
                    tmp2--;
                    Button tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the current one
                    if (tmp != null)
                    {
                        if (greenPieces.Contains(tmp))
                        {
                            tmp1--;
                            tmp2--;
                            Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the one diagonal to the current one and add it as a move option
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
                    tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the current one
                    if (tmp != null)
                    {
                        if (greenPieces.Contains(tmp))
                        {
                            tmp1--;
                            tmp2++;
                            Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the one diagonal to the current one and add it as a move option
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
                        tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the current one
                        if (tmp != null)
                        {
                            if (greenPieces.Contains(tmp))
                            {
                                tmp1++;
                                tmp2++;
                                Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the one diagonal to the current one and add it as a move option
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
                        tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the current one
                        if (tmp != null)
                        {
                            if (greenPieces.Contains(tmp))
                            {
                                tmp1++;
                                tmp2--;
                                Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the one diagonal to the current one and add it as a move option
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
                else if (greenPieces.Contains(button)) //if a green piece in a taking position is selected then show only valid options
                {
                    char tmp1 = button.Name[0];
                    char tmp2 = button.Name[1];
                    tmp1++;
                    tmp2++;
                    Button tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the current one
                    if (tmp != null)
                    {
                        if (redPieces.Contains(tmp))
                        {
                            tmp1++;
                            tmp2++;
                            Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the one diagonal to the current one and add it as a move option
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
                    tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the current one
                    if (tmp != null)
                    {
                        if (redPieces.Contains(tmp))
                        {
                            tmp1++;
                            tmp2--;
                            Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the one diagonal to the current one and add it as a move option
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
                        tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the current one
                        if (tmp != null)
                        {
                            if (redPieces.Contains(tmp))
                            {
                                tmp1--;
                                tmp2--;
                                Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the one diagonal to the current one and add it as a move option
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
                        tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the current one
                        if (tmp != null)
                        {
                            if (redPieces.Contains(tmp))
                            {
                                tmp1--;
                                tmp2++;
                                Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the one diagonal to the current one and add it as a move option
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
            else if (button.Foreground == Brushes.Green && ((turn % 2 != 0) == true)) //if the piece selected belongs to green and it's their turn
            {
                if (blueExists == false) //if a piece that can take a piece does not exist
                {
                    foreach (Button piece in greenPieces) //for each piece that green has check if it can take a red piece
                    {
                        char tmp1 = piece.Name[0];
                        char tmp2 = piece.Name[1];
                        tmp1++;
                        tmp2++;
                        Button tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the current one
                        if (tmp != null)
                        {
                            if (redPieces.Contains(tmp))
                            {
                                tmp1++;
                                tmp2++;
                                Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the one diagonal to the current one
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
                        tmp1++;
                        tmp2--;
                        tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the current one
                        if (tmp != null)
                        {
                            if (redPieces.Contains(tmp))
                            {
                                tmp1++;
                                tmp2--;
                                Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the one diagonal to the current one
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
                            tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the current one
                            if (tmp != null)
                            {
                                if (redPieces.Contains(tmp))
                                {
                                    tmp1--;
                                    tmp2--;
                                    Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the one diagonal to the current one
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
                            tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the current one
                            if (tmp != null)
                            {
                                if (redPieces.Contains(tmp))
                                {
                                    tmp1--;
                                    tmp2++;
                                    Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the one diagonal to the current one
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
                        if (button.Background == Brushes.Blue) //if the current button can take a piece then show the player their options for taking a piece
                        {
                            clickMethod(button);
                        }
                        else
                        {
                            if (greenSwitch == true) //if the computer player is active then make it select a piece
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
                    DiagReset(); //reset all the buttons diagonal to the current button
                    char temp1 = bName[0];
                    int temp2 = (int)Char.GetNumericValue(bName[1]);
                    temp1++;
                    temp2--;
                    diag1 = (Button)FindName(temp1.ToString() + temp2); //find the button diagonal to the current one and add it as a move option
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
                    diag2 = (Button)FindName(temp1.ToString() + temp2); //find the button diagonal to the current one and add it as a move option
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
                        diag3 = (Button)FindName(temp1.ToString() + temp2); //find the button diagonal to the current one and add it as a move option
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
                        diag4 = (Button)FindName(temp1.ToString() + temp2); //find the button diagonal to the current one and add it as a move option
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
                    if (greenSwitch == true) //if the computer player is active then if the selected piece can be moved, then move it and reset the list, otherwise make it select again
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
            else if ((button.Foreground == Brushes.Red) && ((turn % 2 == 0) == true)) //if the selected piece belongs to red and it's their turn
            {
                if (blueExists == false) //if a piece that can take a piece does not exist
                {
                    foreach (Button piece in redPieces) //for each piece that red has check if it can take a green piece
                    {
                        char tmp1 = piece.Name[0];
                        char tmp2 = piece.Name[1];
                        tmp1--;
                        tmp2--;
                        Button tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the current one
                        if (tmp != null)
                        {
                            if (greenPieces.Contains(tmp))
                            {
                                tmp1--;
                                tmp2--;
                                Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the one diagonal to the current one
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
                        tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the current one
                        if (tmp != null)
                        {
                            if (greenPieces.Contains(tmp))
                            {
                                tmp1--;
                                tmp2++;
                                Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the one diagonal to the current one
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
                            tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the current one
                            if (tmp != null)
                            {
                                if (greenPieces.Contains(tmp))
                                {
                                    tmp1++;
                                    tmp2++;
                                    Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the one diagonal to the current one
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
                            tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the current one
                            if (tmp != null)
                            {
                                if (greenPieces.Contains(tmp))
                                {
                                    tmp1++;
                                    tmp2--;
                                    Button dat = (Button)FindName(tmp1.ToString() + tmp2.ToString()); //find the button diagonal to the one diagonal to the current one
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
                        if (button.Background == Brushes.Blue) //if the current button can take a piece then show the player their options for taking a piece
                        {
                            clickMethod(button);
                        }
                        else
                        {
                            if (redSwitch == true) //if the computer player is active then make it select a piece
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
                    DiagReset(); //reset all buttons that are diagonal to the current button
                    char temp1 = bName[0];
                    int temp2 = (int)Char.GetNumericValue(bName[1]);
                    temp1--;
                    temp2--;
                    diag1 = (Button)FindName(temp1.ToString() + temp2); //find the button diagonal to the current one and add it as a move option
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
                    diag2 = (Button)FindName(temp1.ToString() + temp2); //find the button diagonal to the current one and add it as a move option
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
                        diag3 = (Button)FindName(temp1.ToString() + temp2); //find the button diagonal to the current one and add it as a move option
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
                        diag4 = (Button)FindName(temp1.ToString() + temp2); //find the button diagonal to the current one and add it as a move option
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
                    if (redSwitch == true) //if the computer player is active then if the selected piece can be moved, then move it and reset the list, otherwise make it select again
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

        private void ComputerAction() //function for making the computer player randomly select pieces
        {
            if (greenSwitch == true && ((turn % 2 == 0) == false)) //if the green computer player is active and if it's their turn
            {
                if (greenBlue.Count == 0) //if no green pieces can take opposing pieces
                {
                    //randomly select a green owned button until it can take a valid move from that button
                    Button rand;
                    System.Threading.Thread.Sleep(50); //note that this is required because of the Random() object being based on the system clock, making it loop infinitely without this pause
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
                else //if there are one or more green pieces that can take opposing pieces then
                {
                    //randomly select a piece that can take an opposing piece and force it to take a piece
                    Button rand;
                    System.Threading.Thread.Sleep(50); //note that this is required because of the Random() object being based on the system clock, making it loop infinitely without this pause
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

            if (redSwitch == true && ((turn % 2 == 0) == true)) //if the red computer player is active and if it's their turn
            {
                if (redBlue.Count == 0) //if no red pieces can take opposing pieces
                {
                    //randomly select a red owned button until it can take a valid move from that button
                    Button rand;
                    System.Threading.Thread.Sleep(50); //note that this is required because of the Random() object being based on the system clock, making it loop infinitely without this pause
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
                else //if there are one or more red pieces that can take opposing pieces then
                {
                    //randomly select a piece that can take an opposing piece and force it to take a piece
                    Button rand;
                    System.Threading.Thread.Sleep(50); //note that this is required because of the Random() object being based on the system clock, making it loop infinitely without this pause
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

        private void ComputerPlace() //the computer player function to finalise where a piece is to be moved
        {
            if (greenSwitch == true && ((turn % 2 == 0) == false)) //if the green computer player is active and if it's their turn
            {
                //randomly select a location that can be moved to and move there
                Button rand = greenYellow[new Random().Next(0, greenYellow.Count)];
                System.Threading.Thread.Sleep(50);
                MessageBox.Show(last.Name + "|" + rand.Name); //display what the move was (for the benefit of spectators mostly)
                clickMethod(rand);
                greenMoveable = new List<Button>();
            }
            else if (redSwitch == true && ((turn % 2 == 0) == true)) //if the red computer player is active and if it's their turn
            {
                //randomly select a location that can be moved to and move there
                Button rand = redYellow[new Random().Next(0, redYellow.Count)];
                System.Threading.Thread.Sleep(50);
                MessageBox.Show(last.Name + "|" + rand.Name); //display what the move was (for the benefit of spectators mostly)
                clickMethod(rand);
                redMoveable = new List<Button>();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) //the event handler for when any of the buttons in the grid are selected
        {
            clickMethod((Button)sender);
        }

        private void vsAI_Click(object sender, RoutedEventArgs e) //when one of the computer players' buttons are selected
        {
            if (sender == vsGreenAI) //if it was the green one, turn it on
            {
                greenSwitch = true;
                MessageBox.Show("Green AI On");
            }
            else if (sender == vsRedAI) //if it was the red one, turn it on
            {
                redSwitch = true;
                MessageBox.Show("Red AI On");
            }
        }

        private void DiagReset() //the function used to reset the diagonals of the relevant button
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

        private void ReplaySetup() //called when the load replay button is clicked
        {
            replay = new Queue<string>();
            if (System.IO.File.Exists(inputBox.Text) == true && inputBox.Text.Substring(inputBox.Text.Length - 3) == ".rp") //if the file with the name entered and with a .rp extention exists
            {
                foreach (string line in System.IO.File.ReadAllLines(inputBox.Text)) //fill a queue with the contents of the file
                {
                    replay.Enqueue(line);
                }
            }
            CheckersWindow.Hide();
            Replay replayWin = new Replay(replay);
            replayWin.Show(); //show the dedicated replay window
        }

        private void tempSet_Click(object sender, RoutedEventArgs e) //initialises both sides' lists and if the red computer player is enabled then make it make an action (since red always starts)
        {
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
