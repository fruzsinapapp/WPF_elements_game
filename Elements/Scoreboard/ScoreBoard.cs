using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Elements
{
    public class ScoreBoard : Commons
    {
        public static List<int> Scores = new List<int>();


        private readonly XDocument highscoresXml;

        public ScoreBoard()
        {
            this.highscoresXml = XDocument.Load("highscores.xml");
        }

        public List<string> GetHighScores()
        {
            List<string> highscores = new List<string>();
            foreach (var item in this.highscoresXml.Root.Descendants("highscore"))
            {
                highscores.Add(item.Value);
            }

            return highscores;
        }

        public void SaveHighScore(string name, int score)
        {
            string asd = name + ": " + score.ToString();
            this.highscoresXml.Root.Add(new XElement("highscore", asd));
            this.highscoresXml.Save("highscores.xml");
        }
    }
}
