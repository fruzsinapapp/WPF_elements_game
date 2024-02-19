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

namespace Elements
{
    /// <summary>
    /// Interaction logic for ScoreBoardWindow.xaml
    /// </summary>
    public partial class ScoreBoardWindow : Window
    {
        ImageBrush backgroundSprite = new ImageBrush();
        ImageBrush button_backgroundSprite = new ImageBrush();
        public ScoreBoardWindow()
        {
            InitializeComponent();
            backgroundSprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Images", "scoreboard.png"), UriKind.RelativeOrAbsolute));
            myGrid.Background = backgroundSprite;
            button_backgroundSprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Images", "button.png"), UriKind.RelativeOrAbsolute));
            button.Background = button_backgroundSprite;
            ScoreBoard scoreBoard = new ScoreBoard();
            List<string> highscores = new List<string>();
            int counter = 1;
            foreach (var item in scoreBoard.GetHighScores())
            {
                highscores.Add(item);
            }

            int[] scores = new int[highscores.Count];
            string[] names = new string[highscores.Count];
            int num1 = 0;
            foreach (var item in highscores)
            {
                names[num1] = item.Split(':')[0];
                scores[num1] = int.Parse(item.Split(':')[1]);
                num1++;
            }

            int temp1;
            string temp2;
            for (int i = 0; i < scores.Length; i++)
            {
                for (int j = i + 1; j < scores.Length; j++)
                {
                    if (scores[i] < scores[j])
                    {
                        temp1 = scores[i];
                        scores[i] = scores[j];
                        scores[j] = temp1;

                        temp2 = names[i];
                        names[i] = names[j];
                        names[j] = temp2;
                    }
                }
            }

            int num2 = 0;
            foreach (var item in scores)
            {
                this.listbox.Items.Add(counter.ToString() + ". " + names[num2] + " " + item);
                num2++;
                counter++;
            }
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            StartWindow startwin = new StartWindow();
            this.Hide();
            startwin.ShowDialog();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Hide();
            StartWindow start = new StartWindow();
            start.Show();
        }
    }
}

