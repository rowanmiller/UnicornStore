using System;

namespace XamarinFormsTest
{
	public class Game
	{
		public int GameId { get; set; }
		public int Clicks { get; set; }
		public int Duration { get; set; }
		public double ClicksPerSecond { get; set; }
		public DateTime Played { get; set; }
	}
}

