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

        public List<string> replay = new List<string>(); //change to stack maybe for additional brownie points maybe use queue for this and/or doing the replay
        public string last = "";
        public string diag1 = "";
        public string diag2 = "";
        public int turn = 0;
        public bool aiSwitch = false;
        public List<string> aiPieces = new List<string>();
        public List<string> aiYellow = new List<string>();
        public List<string> aiMoveable = new List<string>();


        private void clickMethod(Button button)
        {
            string bName = button.Name;
            string diagName1 = "";
            string diagName2 = "";

            if (button.Background == Brushes.Yellow)
            {
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
                if (aiSwitch == true && button.Foreground == Brushes.Red)
                {
                    ComputerAction();
                }
                else if (aiSwitch == true && button.Foreground == Brushes.Green)
                {
                    aiPieces.Remove(last);
                    aiPieces.Add(bName);
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
                        aiYellow.Add(diagName1);
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
                                aiYellow.Add(diagName1);
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
                        aiYellow.Add(diagName2);
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
                                aiYellow.Add(diagName2);
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
                if (aiSwitch == true)
                {
                    if (aiYellow.Count != 0)
                    {
                        ComputerPlace();
                        foreach(string yellow in aiYellow.ToList())
                        {
                            aiYellow.Remove(yellow);
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
        public int count;
        private void ComputerAction()
        {
            string rand;
            System.Threading.Thread.Sleep(50);
            rand = aiPieces[new Random().Next(0,aiPieces.Count)];
            count++;
            //WHAT NEEDS ADDING IS TAKING PIECES AND REPLAY AND UNDO AND REDO AND FORCING JUMPING AND MULTI JUMPING AND KINGS
            if (aiMoveable.Contains(rand) == false)
            {
                Button btn = (Button)FindName(rand);
                aiMoveable.Add(rand);
                clickMethod(btn);
            }
            else
            {
                ComputerAction();
            }
        }

        private void ComputerPlace()
        {
            string rand = aiYellow[new Random().Next(aiYellow.Count)];
            Button btn = (Button)FindName(rand);
            clickMethod(btn);
            foreach(string item in aiMoveable.ToList())
            {
                aiMoveable.Remove(item);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            clickMethod((Button)sender);
        }

        private void vsAI_Click(object sender, RoutedEventArgs e)
        {
            aiSwitch = true;
            aiPieces.Add("A2");
            aiPieces.Add("A4");
            aiPieces.Add("A6");
            aiPieces.Add("A8");
            aiPieces.Add("B1");
            aiPieces.Add("B3");
            aiPieces.Add("B5");
            aiPieces.Add("B7");
            aiPieces.Add("C2");
            aiPieces.Add("C4");
            aiPieces.Add("C6");
            aiPieces.Add("C8");
            aiNotice.Content = "AI On";
        }
    }
}
