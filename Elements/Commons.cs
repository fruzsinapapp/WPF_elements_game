using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Elements
{
    public class Commons
    {
        public static int totalScore = 0;
        public static int lives = 5;

        public static bool isGameOver = false;

        public static bool winFire = false;
        public static bool winWater = false;
        public static bool winGround = false;
        public static bool winWind = false;

        public static bool allMapsComplete = false;

        public static bool firstStart= true;
        public static bool isPlaying = true;
        public static MediaPlayer mp = new MediaPlayer();
    }
}
