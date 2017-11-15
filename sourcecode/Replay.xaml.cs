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
using System.Windows.Shapes;

namespace Checkers
{
    /// <summary>
    /// Interaction logic for Replay.xaml
    /// </summary>
    public partial class Replay : Window
    {
        private Queue<string> replayQ = new Queue<string>(); //fill a queue with the replay's actions
        public Replay(Queue<string> rp)
        {
            InitializeComponent();
            replayQ = rp;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (replayQ.Count != 0) //play out the game step by step every time any button is clicked
            {
                string move = replayQ.Dequeue();
                MessageBox.Show(move);
                if (move[1] - move[4] == 2 || move[4] - move[1] == 2)
                {
                    char x = (char)((move[0] + move[3]) / 2);
                    char y = (char)((move[1] + move[4]) / 2);
                    Button remove = (Button)FindName(x.ToString() + y.ToString()); //finds the button that is getting captured
                    remove.Content = "";
                }
                Button move1 = (Button)FindName(move[0].ToString() + move[1].ToString()); //finds the button that is moving from this location
                Button move2 = (Button)FindName(move[3].ToString() + move[4].ToString()); //finds the button that is moving to this location
                if (move2.Name[0] == 'A' || move2.Name[0] == 'H') //if a pieces reaches an end make it a king
                {
                    move2.Content = "K";
                }
                else
                {
                    move2.Content = move1.Content;
                }
                move1.Content = "";
                if (move1.Foreground == Brushes.Red)
                {
                    move2.Foreground = Brushes.Red;
                }
                else if (move1.Foreground == Brushes.Green)
                {
                    move2.Foreground = Brushes.Green;
                }
            }
            else
            {
                MessageBox.Show("End of Replay."); //if there are no more moves then if any button is clicked say as much
            }
        }
    }
}
