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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ImageBrush backgroundSprite = new ImageBrush();
        ImageBrush button_backgroundSprite = new ImageBrush();
        public MainWindow()
        {
            InitializeComponent();
            backgroundSprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Images", "background.png"), UriKind.RelativeOrAbsolute));
            myGrid.Background = backgroundSprite;

            button_backgroundSprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Images", "button.png"), UriKind.RelativeOrAbsolute));
            b1.Background = button_backgroundSprite;
            b2.Background = button_backgroundSprite;
            b3.Background = button_backgroundSprite;
            b4.Background = button_backgroundSprite;
            b5.Background = button_backgroundSprite;
            restart_button.Background= button_backgroundSprite;
            save_score.Background = button_backgroundSprite;


        }

        private void Button_Click_back(object sender, RoutedEventArgs e)
        {
            this.Hide();
            StartWindow start = new StartWindow();
            start.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Hide();
            StartWindow start = new StartWindow();
            start.Show();
        }

        private void Button_Click_Water(object sender, RoutedEventArgs e)
        {
            if (Commons.winWater)
            {
                MessageBox.Show("You already won this map");
            }
            else if (Commons.isGameOver)
            {
                MessageBox.Show("You have lost the game, please restart");
            }
            else
            {
                this.Hide();
                WaterMapWindow waterMap = new WaterMapWindow();
                waterMap.Show();
            }

        }

        private void Button_Click_Fire(object sender, RoutedEventArgs e)
        {
            if (Commons.winFire)
            {
                MessageBox.Show("You already won this map");
            }
            else if (Commons.isGameOver)
            {
                MessageBox.Show("You have lost the game, please restart");
            }
            else
            {
                this.Hide();
                FireMapWindow fireMap = new FireMapWindow();
                fireMap.Show();
            }

        }
        private void Button_Click_Ground(object sender, RoutedEventArgs e)
        {
            if (Commons.winGround)
            {
                MessageBox.Show("You already won this map");
            }
            else if (Commons.isGameOver)
            {
                MessageBox.Show("You have lost the game, please restart");
            }
            else
            {
                this.Hide();
                GroundMapWindow groundMap = new GroundMapWindow();
                groundMap.Show();
            }

        }

        private void Button_Click_Wind(object sender, RoutedEventArgs e)
        {
            if (Commons.winWind)
            {
                MessageBox.Show("You already won this map");
            }
            else if (Commons.isGameOver)
            {
                MessageBox.Show("You have lost the game, please restart");
            }
            else
            {
                this.Hide();
                WindMapWindow windMap = new WindMapWindow();
                windMap.Show();
            }

        }

        private void restart_button_Click(object sender, RoutedEventArgs e)
        {
            Commons.isGameOver = false;
            Commons.lives = 5;
            Commons.totalScore = 0;
            Commons.winFire = false;
            Commons.winWater = false;
            Commons.winWind = false;
            Commons.winGround = false;
        }

        private void save_score_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            YouAreWinner youLost = new YouAreWinner();
            youLost.Show();
        }
    }
}
