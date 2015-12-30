using System.Collections.Generic;
using System.Linq;
using UnicornClicker.UWP.Models;

namespace UnicornClicker.UWP.ViewModels
{
    public class ScoresViewModel
    {
        public ScoresViewModel()
        {
            using (var db = new UnicornClickerContext())
            {
                this.TopScores = db.GameScores
                    .OrderByDescending(s => s.ClicksPerSecond)
                    .Take(5)
                    .ToList();
            }
        }

        public IEnumerable<GameScore> TopScores { get; private set; }
    }
}
