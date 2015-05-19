using System;
using System.Linq;
using Xamarin.Forms;

namespace XamarinFormsTest
{
	public class ScoreHistoryPage : ContentPage
	{
		public ScoreHistoryPage ()
		{
			var layout = new StackLayout { Padding = 5 };

			using (var db = new GameContext ()) {
				var games = db.Games.OrderByDescending (g => g.ClicksPerSecond).Take (5).ToList();

				layout.Children.Add (new Label { 
					Text = string.Format("Your best {0} scores.", games.Count) ,
					FontSize = 20
				});

				foreach (var game in games) {
					layout.Children.Add (new Frame {
						Padding = 10,
						Content = new StackLayout {
							Children = {
								new Label { 
									Text = string.Format ("{0} clicks per second", game.ClicksPerSecond)
								},
								new Label { 
									Text = string.Format ("Played {0}", game.Played),
									FontSize = 10
								}
							}
						}
					});
				}
			}

			Content = layout;
		}
	}
}

