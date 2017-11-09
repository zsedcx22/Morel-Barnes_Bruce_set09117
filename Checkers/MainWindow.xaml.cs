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

        private List<string> replay = new List<string>(); //change to stack maybe for additional brownie points maybe use queue for this and/or doing the replay
        private string last = ""; //change to Button
        private string diag1 = ""; //change to Button
        private string diag2 = ""; //change to Button
        private int turn = 0;

        private bool blueExists = false;

        private bool greenSwitch = false;
        private List<string> greenPieces = new List<string>(); //change to Button
        private List<string> greenYellow = new List<string>(); //change to Button
        private List<string> greenMoveable = new List<string>(); //change to Button

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
                //MessageBox.Show(CharactersBetween('A','B')[1].ToString());
                //if last blue then CharactersBetween(last.name[0],button.name[0])[1]
                blueExists = false;
                foreach (Button piece in redPieces)
                {
                    piece.Background = Brushes.Black;
                }
                Button prev = (Button)FindName(last);
                button.Content = "O";
                button.Foreground = prev.Foreground;
                button.Background = prev.Background;
                prev.Foreground = Brushes.Black;
                prev.Content = "";
                if (diag1 != "")
                {
                    if (diag2 != "")
                    {
                        Button diagBtn1 = (Button)FindName(diag1);
                        Button diagBtn2 = (Button)FindName(diag2);
                        if (diagBtn1 != null)
                        {
                            diagBtn1.Background = Brushes.Black;
                        }
                        if (diagBtn2 != null)
                        {
                            diagBtn2.Background = Brushes.Black;
                        }
                    }
                    else
                    {
                        Button diagBtn1 = (Button)FindName(diag1);
                        if (diagBtn1 != null)
                        {
                            diagBtn1.Background = Brushes.Black;
                        }
                    }
                }
                replay.Add(last + "," + bName);
                MessageBox.Show(replay[turn]);
                diag1 = "";
                diag2 = "";
                turn++;
                if (greenSwitch == true && button.Foreground == Brushes.Red)
                {
                    ComputerAction();
                }
                else if (greenSwitch == true && button.Foreground == Brushes.Green)
                {
                    greenPieces.Remove(last);
                    greenPieces.Add(bName);
                }
            }
            else if (button.Background == Brushes.Blue) //BLUE IS FOR TAKING
            {
                
                if (button.Foreground == Brushes.Red && button.Content.ToString() == "O")
                {
                    char tmp1 = button.Name[0];
                    char tmp2 = button.Name[1];
                    tmp1--;
                    tmp2--;
                    Button tmp = (Button)FindName(tmp1.ToString() + tmp2.ToString());
                    if (tmp != null)
                    {
                        if (tmp.Content.ToString() != "" && tmp.Foreground == Brushes.Green)
                        {
                            tmp1--;
                            tmp2--;
                            if (tmp != null)
                            {
                                if (tmp.Content.ToString() == "")
                                {
                                    tmp.Background = Brushes.Yellow;
                                }
                            }
                        }
                    }
                }
            }
            else if(button.Foreground == Brushes.Green && ((turn % 2 != 0) == true)) //GREEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEN
            {
                if(diag1 != "")
                {
                    if(diag2!="")
                    {
                        Button diagBtn1 = (Button)FindName(diag1);
                        Button diagBtn2 = (Button)FindName(diag2);
                        if (diagBtn1 != null)
                        {
                            diagBtn1.Background = Brushes.Black;
                        }
                        if (diagBtn2 != null)
                        {
                            diagBtn2.Background = Brushes.Black;
                        }
                    }
                    else
                    {
                        Button diagBtn1 = (Button)FindName(diag1);
                        if (diagBtn1 != null)
                        {
                            diagBtn1.Background = Brushes.Black;
                        }
                    }
                }
                diag1 = "";
                diag2 = "";

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
                        greenYellow.Add(diagName1);
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
                                greenYellow.Add(diagName1);
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
                        greenYellow.Add(diagName2);
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
                                greenYellow.Add(diagName2);
                            }
                        }
                    }
                    else
                    {
                        correct2.Background = Brushes.Black;
                    }
                }
                last = button.Name;
                diag1 = diagName1;
                diag2 = diagName2;
                if (greenSwitch == true)
                {
                    if (greenYellow.Count != 0)
                    {
                        ComputerPlace();
                        foreach(string yellow in greenYellow.ToList())
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
            else if ((button.Foreground == Brushes.Red) && ((turn % 2 == 0) == true)) //REEEEEEEEEEEEEEEEEEEEED
            {

                //IF NO BLUES
                if (blueExists == false)
                {
                    foreach (Button piece in redPieces)
                    {
                        char tmp1 = piece.Name[0];
                        char tmp2 = piece.Name[1];
                        tmp1--;
                        tmp2--;
                        string full = tmp1.ToString() + tmp2.ToString();
                        Button tmp = (Button)FindName(full);

                        if (tmp != null)
                        {
                            MessageBox.Show(piece.Name);
                            MessageBox.Show(tmp.Content.ToString());
                            if (tmp.Content.ToString() != "" && tmp.Foreground == Brushes.Green)
                            {
                                MessageBox.Show("WoWeE");
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
                    }
                    if (blueExists == true)
                    {
                        MessageBox.Show("You must select a piece with a blue background as you must take a piece.");
                    }
                }
                else
                {
                    MessageBox.Show("You must select a piece with a blue background as you must take a piece.");
                }

                if (blueExists == false)
                {
                    if (diag1 != "")
                    {
                        if (diag2 != "")
                        {
                            Button diagBtn1 = (Button)FindName(diag1);
                            Button diagBtn2 = (Button)FindName(diag2);
                            if (diagBtn1 != null)
                            {
                                diagBtn1.Background = Brushes.Black;
                            }
                            if (diagBtn2 != null)
                            {
                                diagBtn2.Background = Brushes.Black;
                            }
                        }
                        else
                        {
                            Button diagBtn1 = (Button)FindName(diag1);
                            diagBtn1.Background = Brushes.Black;
                        }
                    }
                    diag1 = "";
                    diag2 = "";

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
                    last = button.Name;
                    diag1 = diagName1;
                    diag2 = diagName2;
                }
            }
            
        }
        public int count;
        private void ComputerAction()
        {
            string rand;
            System.Threading.Thread.Sleep(50);
            rand = greenPieces[new Random().Next(0,greenPieces.Count)];
            count++;
            //REMOVE NON TAKING OPTIONS for each piece if there is an enemy piece capturable then show player moveable pieces by turning them blue returning MUST TAKE PIECE? then add if blue turn all blue black and show options as yellow and call clickmethod
            //WHAT NEEDS ADDING IS TAKING PIECES AND REPLAY AND UNDO AND REDO AND FORCING JUMPING AND MULTI JUMPING AND KINGS
            if (greenMoveable.Contains(rand) == false)
            {
                Button btn = (Button)FindName(rand);
                greenMoveable.Add(rand);
                clickMethod(btn);
            }
            else
            {
                ComputerAction();
            }
        }

        private void ComputerPlace()
        {
            string rand = greenYellow[new Random().Next(greenYellow.Count)];
            Button btn = (Button)FindName(rand);
            clickMethod(btn);
            foreach(string item in greenMoveable.ToList())
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
            greenSwitch = true;
            greenPieces.Add("A2");
            greenPieces.Add("A4");
            greenPieces.Add("A6");
            greenPieces.Add("A8");
            greenPieces.Add("B1");
            greenPieces.Add("B3");
            greenPieces.Add("B5");
            greenPieces.Add("B7");
            greenPieces.Add("C2");
            greenPieces.Add("C4");
            greenPieces.Add("C6");
            greenPieces.Add("C8");
            aiNotice.Content = "AI On";
        }

        private static char[] CharactersBetween(char start, char end)
        {
            return Enumerable.Range(start, end - start + 1).Select(c => (char)c).ToArray();
        }

        private void tempSet_Click(object sender, RoutedEventArgs e)
        {

            redPieces.Add((Button)FindName("F1"));
            redPieces.Add((Button)FindName("F3"));
            redPieces.Add((Button)FindName("F5"));
            redPieces.Add((Button)FindName("F7"));
            redPieces.Add((Button)FindName("G2"));
            redPieces.Add((Button)FindName("G4"));
            redPieces.Add((Button)FindName("G6"));
            redPieces.Add((Button)FindName("G8"));
            redPieces.Add((Button)FindName("H1"));
            redPieces.Add((Button)FindName("H3"));
            redPieces.Add((Button)FindName("H5"));
            redPieces.Add((Button)FindName("H7"));
        }
    }
}
