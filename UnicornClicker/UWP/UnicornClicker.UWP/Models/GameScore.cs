using System;

namespace UnicornClicker.UWP.Models
{
    public class GameScore
    {
        public int GameScoreId { get; set; }
        public DateTime Played { get; set; }
        public int Duration { get; set; }
        public int Clicks { get; set; }
        public double ClicksPerSecond { get; set; }
    }
}
