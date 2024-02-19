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
    /// Interaction logic for YouAreWinner.xaml
    /// </summary>
    public partial class YouAreWinner : Window
    {
        ImageBrush backgroundSprite = new ImageBrush();
        ImageBrush button_backgroundSprite = new ImageBrush();
        public YouAreWinner()
        {
            InitializeComponent();
            backgroundSprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Images", "win.png"), UriKind.RelativeOrAbsolute));
            myGrid.Background = backgroundSprite;

            button_backgroundSprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Images", "button.png"), UriKind.RelativeOrAbsolute));
            save_button.Background = button_backgroundSprite;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            ScoreBoard scoreBoard = new ScoreBoard();
            scoreBoard.SaveHighScore(this.textBox.Text, Commons.totalScore);
            this.Hide();
            MainWindow mwin = new MainWindow();
            mwin.ShowDialog();
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Hide();
            MainWindow main = new MainWindow();
            main.Show();
        }
    }
}

