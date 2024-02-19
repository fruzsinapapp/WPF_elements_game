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
using System.Windows.Threading;

namespace Elements
{
    /// <summary>
    /// Interaction logic for WindMapWindow.xaml
    /// </summary>
    public partial class WindMapWindow : Window
    {
        #region ImageBrushes
        ImageBrush playerSprite = new ImageBrush();
        ImageBrush enemySprite = new ImageBrush();
        ImageBrush groundSprite = new ImageBrush();
        ImageBrush fireSprite = new ImageBrush();

        ImageBrush doorSprite = new ImageBrush();
        ImageBrush coinSprite = new ImageBrush();
        ImageBrush scoreSprite = new ImageBrush();

        ImageBrush keySprite = new ImageBrush();

        ImageBrush backgroundSprite = new ImageBrush();
        ImageBrush button_backgroundSprite = new ImageBrush();
        ImageBrush heartSprite = new ImageBrush();
        ImageBrush canFireSprite = new ImageBrush();
        ImageBrush canWaterSprite = new ImageBrush();
        ImageBrush canGroundSprite = new ImageBrush();
        #endregion

        Rect playerHitBox;
        Rect platformHitBox;

        Rect enterHitBox;
        Rect zoneHitBox;
        Rect coinHitBox;
        Rect keyHitBox;
        Rect windEnemyHitBox;

        DispatcherTimer gameTimer = new DispatcherTimer();

        double startPosX = 0;
        double startPosY = 0;

        bool goLeft = false;
        bool goRight = false;
        bool jumping = false;


        bool haveKey = false;

        bool hit = false;

        bool isAttacking = false;

        int jumpSpeed = 10;
        int force = 8;
        int ground_speed1 = 5;
        int ground_speed2 = 5;
        int enemy_speed = 8;

        double spriteIndex = 0;
        public WindMapWindow()
        {
            InitializeComponent();
            myCanvas.Focus();

            gameTimer.Tick += GameEngine;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);

            enemySprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Wind", "bidle_1.png"), UriKind.RelativeOrAbsolute));
            WindEnemy.Fill = enemySprite;

            playerSprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Player", "01_idle_1.png"), UriKind.RelativeOrAbsolute));
            Player.Fill = playerSprite;

            groundSprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Images", "brownssmall.png"), UriKind.RelativeOrAbsolute));
            groundSprite.Viewport = new Rect(0, 0, 50, 50);
            groundSprite.ViewportUnits = BrushMappingMode.Absolute;
            groundSprite.TileMode = TileMode.Tile;


            foreach (var x in myCanvas.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "platform")
                    x.Fill = groundSprite;
            }
            
            backgroundSprite.Viewport = new Rect(0, 0, 500, 750);
            backgroundSprite.ViewportUnits = BrushMappingMode.Absolute;
            backgroundSprite.TileMode = TileMode.Tile;
            backgroundSprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Images", "backg.png"), UriKind.RelativeOrAbsolute));
            myCanvas.Background = backgroundSprite;

            button_backgroundSprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Images", "button.png"), UriKind.RelativeOrAbsolute));
            back_button.Background = button_backgroundSprite;

            scoreSprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Images", "coin.png"), UriKind.RelativeOrAbsolute));
            score.Fill = scoreSprite;

            coinSprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Images", "coin.png"), UriKind.RelativeOrAbsolute));
            foreach (var x in myCanvas.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "coin")
                    x.Fill = coinSprite;
            }

            heartSprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Images", "heart.png"), UriKind.RelativeOrAbsolute));
            foreach (var x in myCanvas.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "heart")
                    x.Fill = heartSprite;
            }

            keySprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Images", "key.png"), UriKind.RelativeOrAbsolute));
            key.Fill = keySprite;

            StartGame();
        }
        public void StartGame()
        {
            gameTimer.Start();

            startPosX = 1460;
            startPosY = 650;
        }
        private void GameEngine(object sender, EventArgs e)
        {
            if (haveKey)
            {
                doorSprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Images", "door_open.png"), UriKind.RelativeOrAbsolute));
                door.Fill = doorSprite;
            }
            else
            {
                doorSprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Images", "door.png"), UriKind.RelativeOrAbsolute));
                door.Fill = doorSprite;
            }

            playerHitBox = new Rect(Canvas.GetLeft(Player), Canvas.GetTop(Player), Player.Width, Player.Height);

            enterHitBox = new Rect(Canvas.GetLeft(enter), Canvas.GetTop(enter), enter.Width, enter.Height);
            zoneHitBox = new Rect(Canvas.GetLeft(zone), Canvas.GetTop(zone), zone.Width, zone.Height);
            keyHitBox = new Rect(Canvas.GetLeft(key), Canvas.GetTop(key), key.Width, key.Height);
            windEnemyHitBox = new Rect(Canvas.GetLeft(WindEnemy), Canvas.GetTop(WindEnemy), WindEnemy.Width, WindEnemy.Height);

            Canvas.SetTop(Player, Canvas.GetTop(Player) + jumpSpeed);

            if (Commons.lives <= 0)
            {
                gameTimer.Stop();
                Commons.isGameOver = true;
            }

            if (jumping && force < 0)
                jumping = false;

            if (goLeft)
                Canvas.SetLeft(Player, Canvas.GetLeft(Player) - 10);

            if (goRight)
                Canvas.SetLeft(Player, Canvas.GetLeft(Player) + 10);

            if (jumping)
            {
                jumpSpeed = -30;
                force -= 1;
            }
            else
                jumpSpeed = 12;


            if (Commons.isGameOver)
            {
                Window.GetWindow(this).IsEnabled = false;
                Window.GetWindow(this).Close();
            }


            Rectangle coinToRemove = new Rectangle();
            Rectangle heartToRemove = new Rectangle();

            score_label.Content = $"Score: {Commons.totalScore}";
            lives_label.Content = $"Lives: {Commons.lives}";

            hit = false;

            foreach (var x in myCanvas.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "platform")
                {
                    platformHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (platformHitBox.IntersectsWith(playerHitBox) && !jumping)
                    {
                        force = 8;
                        Canvas.SetTop(Player, platformHitBox.Top - Player.Height);
                    }
                }

                if ((string)x.Tag == "coin")
                {
                    coinHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (coinHitBox.IntersectsWith(playerHitBox) && !jumping)
                    {
                        coinToRemove = x;
                        Commons.totalScore++;
                    }
                }

                if ((string)x.Name == "enter")
                {
                    if (haveKey && enterHitBox.IntersectsWith(playerHitBox))
                    {
                        Commons.winWind = true;

                        if (Commons.winWind && Commons.winWater && Commons.winGround && Commons.winFire)
                        {
                            gameTimer.Stop();
                            this.Hide();
                            YouAreWinner xouWin = new YouAreWinner();
                            xouWin.Show();
                        }
                        else
                        {
                            gameTimer.Stop();
                            gameOver_label.Content = "You won!";
                        }
                    }
                }

                if ((string)x.Name == "zone")
                {
                    if (zoneHitBox.IntersectsWith(playerHitBox))
                    {
                        isAttacking = true;
                    }

                    else
                    {
                        isAttacking = false;
                    }

                }

                if ((string)x.Name == "key")
                {
                    if (keyHitBox.IntersectsWith(playerHitBox))
                    {
                        haveKey = true;
                    }
                }

                if ((string)x.Tag == "Enemy")
                {
                    if (windEnemyHitBox.IntersectsWith(playerHitBox) && !hit)
                    {
                        hit = !hit;
                        if (Commons.lives > 0)
                            Commons.lives--;

                        Canvas.SetLeft(Player, startPosX);
                        Canvas.SetTop(Player, startPosY);
                    }

                }
            }

            myCanvas.Children.Remove(coinToRemove);
            if (haveKey)
            {
                myCanvas.Children.Remove(key);
            }

            Canvas.SetLeft(g6, Canvas.GetLeft(g6) - ground_speed1);
            if (Canvas.GetLeft(g6) < 700 || Canvas.GetLeft(g6) > 1000)
                ground_speed1 = -ground_speed1;

            Canvas.SetTop(g2, Canvas.GetTop(g2) + ground_speed2);
            if (Canvas.GetTop(g2) < 300 || Canvas.GetTop(g2) > 700)
                ground_speed2 = -ground_speed2;

            spriteIndex += .5;
            if (spriteIndex > 8)
                spriteIndex = 1;

            if (isAttacking)
            {
                double playerPos = Canvas.GetLeft(Player);
                double enemyPos = Canvas.GetLeft(WindEnemy);
                double turn = playerPos - enemyPos;

                if (turn <= 0)
                {
                    Canvas.SetLeft(WindEnemy, Canvas.GetLeft(WindEnemy) - enemy_speed);
                }
                else
                {
                    Canvas.SetLeft(WindEnemy, Canvas.GetLeft(WindEnemy) + enemy_speed);
                }

                RunAttackSprite(spriteIndex);
            }


            else
            {
                RunEnemyIdleSprite(spriteIndex, WindEnemy);
            }

            RunPlayerIdleSprite(spriteIndex, playerSprite);

            if (goRight)
            {
                RunPlayerRunSprite(spriteIndex, playerSprite);
            }
            if (goLeft)
            {
                RunPlayerRunToLeftSprite(spriteIndex, playerSprite);
            }

            if (jumping)
                RunPlayerJumpSprite(spriteIndex, playerSprite);

        }

        #region ANIMATION
        private void RunAttackSprite(double v)
        {
            switch (v)
            {
                case 1:
                    enemySprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Wind", "1_atk_1.png"), UriKind.RelativeOrAbsolute));
                    break;
                case 2:
                    enemySprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Wind", "1_atk_2.png"), UriKind.RelativeOrAbsolute));
                    break;
                case 3:
                    enemySprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Wind", "1_atk_3.png"), UriKind.RelativeOrAbsolute));
                    break;
                case 4:
                    enemySprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Wind", "1_atk_4.png"), UriKind.RelativeOrAbsolute));
                    break;
                case 5:
                    enemySprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Wind", "1_atk_5.png"), UriKind.RelativeOrAbsolute));
                    break;
                case 6:
                    enemySprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Wind", "1_atk_6.png"), UriKind.RelativeOrAbsolute));
                    break;
                case 7:
                    enemySprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Wind", "1_atk_7.png"), UriKind.RelativeOrAbsolute));
                    break;
                case 8:
                    enemySprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Wind", "1_atk_8.png"), UriKind.RelativeOrAbsolute));
                    break;
            }
            WindEnemy.Fill = enemySprite;
        }
        private void RunEnemyIdleSprite(double v, Rectangle target)
        {
            switch (v)
            {
                case 1:
                    enemySprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Wind", "bidle_1.png"), UriKind.RelativeOrAbsolute));
                    break;
                case 2:
                    enemySprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Wind", "bidle_2.png"), UriKind.RelativeOrAbsolute));
                    break;
                case 3:
                    enemySprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Wind", "bidle_3.png"), UriKind.RelativeOrAbsolute));
                    break;
                case 4:
                    enemySprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Wind", "bidle_4.png"), UriKind.RelativeOrAbsolute));
                    break;

                case 5:
                    enemySprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Wind", "bidle_5.png"), UriKind.RelativeOrAbsolute));
                    break;
                case 6:
                    enemySprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Wind", "bidle_6.png"), UriKind.RelativeOrAbsolute));
                    break;
                case 7:
                    enemySprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Wind", "bidle_7.png"), UriKind.RelativeOrAbsolute));
                    break;
                case 8:
                    enemySprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Wind", "bidle_8.png"), UriKind.RelativeOrAbsolute));
                    break;
            }
            target.Fill = enemySprite;
        }

        private void RunPlayerIdleSprite(double v, ImageBrush sprite)
        {
            switch (v)
            {
                case 1:
                    sprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Player", "01_idle_1.png"), UriKind.RelativeOrAbsolute));
                    break;
                case 2:
                    sprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Player", "01_idle_2.png"), UriKind.RelativeOrAbsolute));
                    break;
                case 3:
                    sprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Player", "01_idle_3.png"), UriKind.RelativeOrAbsolute));
                    break;
                case 4:
                    sprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Player", "01_idle_4.png"), UriKind.RelativeOrAbsolute));
                    break;

                case 5:
                    sprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Player", "01_idle_5.png"), UriKind.RelativeOrAbsolute));
                    break;
                case 6:
                    sprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Player", "01_idle_6.png"), UriKind.RelativeOrAbsolute));
                    break;
                case 7:
                    sprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Player", "01_idle_7.png"), UriKind.RelativeOrAbsolute));
                    break;
                case 8:
                    sprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Player", "01_idle_8.png"), UriKind.RelativeOrAbsolute));
                    break;
            }
            Player.Fill = sprite;
        }
        private void RunPlayerJumpSprite(double v, ImageBrush sprite)
        {
            switch (v)
            {
                case 1:
                    sprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Player", "03_jump_1.png"), UriKind.RelativeOrAbsolute));
                    break;
                case 2:
                    sprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Player", "03_jump_2.png"), UriKind.RelativeOrAbsolute));
                    break;
                case 3:
                    sprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Player", "03_jump_3.png"), UriKind.RelativeOrAbsolute));
                    break;
                case 4:
                    sprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Player", "03_jump_4.png"), UriKind.RelativeOrAbsolute));
                    break;

                case 5:
                    sprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Player", "03_jump_5.png"), UriKind.RelativeOrAbsolute));
                    break;
                case 6:
                    sprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Player", "03_jump_6.png"), UriKind.RelativeOrAbsolute));
                    break;
                case 7:
                    sprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Player", "03_jump_7.png"), UriKind.RelativeOrAbsolute));
                    break;
                case 8:
                    sprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Player", "03_jump_8.png"), UriKind.RelativeOrAbsolute));
                    break;
            }
            Player.Fill = sprite;
        }

        private void RunPlayerRunSprite(double v, ImageBrush sprite)
        {
            switch (v)
            {
                case 1:
                    sprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Player", "02_run_1.png"), UriKind.RelativeOrAbsolute));
                    break;
                case 2:
                    sprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Player", "02_run_2.png"), UriKind.RelativeOrAbsolute));
                    break;
                case 3:
                    sprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Player", "02_run_3.png"), UriKind.RelativeOrAbsolute));
                    break;
                case 4:
                    sprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Player", "02_run_4.png"), UriKind.RelativeOrAbsolute));
                    break;

                case 5:
                    sprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Player", "02_run_5.png"), UriKind.RelativeOrAbsolute));
                    break;
                case 6:
                    sprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Player", "02_run_6.png"), UriKind.RelativeOrAbsolute));
                    break;
                case 7:
                    sprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Player", "02_run_7.png"), UriKind.RelativeOrAbsolute));
                    break;
                case 8:
                    sprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Player", "02_run_8.png"), UriKind.RelativeOrAbsolute));
                    break;
            }
            Player.Fill = sprite;
        }

        private void RunPlayerRunToLeftSprite(double v, ImageBrush sprite)
        {
            switch (v)
            {
                case 1:
                    sprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Player", "b02_run_1.png"), UriKind.RelativeOrAbsolute));
                    break;
                case 2:
                    sprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Player", "b02_run_2.png"), UriKind.RelativeOrAbsolute));
                    break;
                case 3:
                    sprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Player", "b02_run_3.png"), UriKind.RelativeOrAbsolute));
                    break;
                case 4:
                    sprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Player", "b02_run_4.png"), UriKind.RelativeOrAbsolute));
                    break;

                case 5:
                    sprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Player", "b02_run_5.png"), UriKind.RelativeOrAbsolute));
                    break;
                case 6:
                    sprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Player", "b02_run_6.png"), UriKind.RelativeOrAbsolute));
                    break;
                case 7:
                    sprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Player", "b02_run_7.png"), UriKind.RelativeOrAbsolute));
                    break;
                case 8:
                    sprite.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Characters/Player", "b02_run_8.png"), UriKind.RelativeOrAbsolute));
                    break;
            }
            Player.Fill = sprite;
        }
        #endregion

        #region KEYS
        private void canvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                goLeft = true;
            }
            if (e.Key == Key.Right)
            {
                goRight = true;
            }
            if (e.Key == Key.Up && !jumping)
            {
                jumping = true;
            }
        }
        private void canvas_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                goLeft = false;
            }
            if (e.Key == Key.Right)
            {
                goRight = false;
            }
            if (jumping)
            {
                jumping = false;
            }
        }
        #endregion
        private void Window_Closed(object sender, EventArgs e)
        {
            if (IsEnabled == true)
            {
                MainWindow main = new MainWindow();
                main.Show();
            }
            else
            {
                /*
                YouAreLooser youLost = new YouAreLooser();
                youLost.ShowDialog();
                */

                this.Hide();
                YouAreLooser youLost = new YouAreLooser();
                youLost.Show();
            }
        }
        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow main = new MainWindow();
            main.Show();
        }
    }
}
