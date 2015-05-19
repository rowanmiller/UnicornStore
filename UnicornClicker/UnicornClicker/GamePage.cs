using System;
using Xamarin.Forms;

namespace XamarinFormsTest
{
	public class GamePage : ContentPage
	{
		private static readonly int _gameDuration = 5;
		private int _clickCount = 0;
		private int _secondsRemaining = _gameDuration;
		private bool _playing = true;
		private Label _timeLabel = new Label();

		public GamePage ()
		{
			var message = new Label {
				Text = "Click the unicorn!"
			};

			UpdateTimeLabel ();

			var unicorn = new ImageCell {
				ImageSource = ImageSource.FromFile ("Unicorn.png"),
			};

			unicorn.Tapped += (sender, e) => {
				if(_playing){
					_clickCount++;
					message.Text = string.Format ("Clicked {0} times", _clickCount);
				}
			};

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.Center,
				Children = { 
					new TableView {
						Intent = TableIntent.Form,
						RowHeight = 300,
						Root = new TableRoot { new TableSection { unicorn } }
					}, 
					message,
					_timeLabel
				}
			};

			Device.StartTimer (TimeSpan.FromSeconds (1), HandleSecondTick);
		}

		private bool HandleSecondTick()
		{
			_secondsRemaining--;
			if (_secondsRemaining > 0) {
				UpdateTimeLabel ();
				return true;
			} else {
				_playing = false;
				_timeLabel.Text = "Game complete (saving results...)";
				using (var db = new GameContext ()) {
					db.Games.Add (new Game {
						Clicks = _clickCount,
						Duration = _gameDuration,
						ClicksPerSecond = (double)_clickCount / _gameDuration,
						Played = DateTime.Now
					});
					db.SaveChanges ();
				}
				_timeLabel.Text = "Game complete (results saved)";

				return false;
			}
		}

		private void UpdateTimeLabel()
		{
			_timeLabel.Text = string.Format ("{0} seconds left", _secondsRemaining);
		}
	}
}

