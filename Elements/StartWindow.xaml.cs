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
    public partial class StartWindow : Window
    {

        ImageBrush backgroundSprite = new ImageBrush();
        ImageBrush button_backgroundSprite = new ImageBrush();
        ImageBrush elementsSprite = new ImageBrush();

        public StartWindow()
        {
            InitializeComponent();
            backgroundSprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Images", "background.png"), UriKind.RelativeOrAbsolute));
            myCanvas.Background = backgroundSprite;

            button_backgroundSprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Images", "button.png"), UriKind.RelativeOrAbsolute));
            b1.Background = button_backgroundSprite;
            b2.Background = button_backgroundSprite;
            sound_button.Background= button_backgroundSprite;
            elementsSprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Images", "elements.png"), UriKind.RelativeOrAbsolute));
            elements.Background = elementsSprite;



            if (Commons.firstStart)
            {
                Commons.mp.Open(new Uri(System.IO.Path.Combine("Sound", "Farm.mp3"), UriKind.RelativeOrAbsolute));
                Commons.mp.Volume = 0.015;
                Commons.mp.Play();

            }
            Commons.firstStart = false;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Commons.isGameOver = false;
            Commons.lives = 5;
            Commons.totalScore = 0;
            Commons.winFire = false;
            Commons.winWater = false;
            Commons.winWind = false;
            Commons.winGround = false;
            
            this.Hide();
            MainWindow main = new MainWindow();
            main.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ScoreBoardWindow main = new ScoreBoardWindow();
            main.Show();
        }

        private void sound_button_Click(object sender, RoutedEventArgs e)
        {
            if (Commons.isPlaying)
            {
                Commons. mp.Stop();
            }
            else
            {
                Commons.mp.Play();
            }
            Commons.isPlaying = !Commons.isPlaying;
        }
    }
}
