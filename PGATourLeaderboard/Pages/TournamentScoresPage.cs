using System;

using Xamarin.Forms;
using System.Threading.Tasks;

namespace PGATourLeaderboard
{
	public class TournamentScoresPage : ContentPage
	{
		public TournamentScoresPage (Tournament tournament)
		{
			this.Title = "Loaderboard";

			var loader = new ActivityIndicator {
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				Color = Color.FromRgb(0, 61, 125),
				IsRunning = true,
			};

			this.Content = loader;

			GetScores (tournament.Id);
		}

		private async Task GetScores(string tournamentId)
		{
			var listView = new ListView {
				ItemsSource = await new PlayerManager(tournamentId).GetPlayerScores()
			};

			var cell = new DataTemplate (typeof(TextCell));
			cell.SetBinding (TextCell.TextProperty, "FullName");
			cell.SetBinding (TextCell.DetailProperty, "ScoreDisplay");

			listView.ItemTemplate = cell;

			this.Content = listView;
		}
	}
}