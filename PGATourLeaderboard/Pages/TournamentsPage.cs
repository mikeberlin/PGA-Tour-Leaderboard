using System;

using Xamarin.Forms;
using System.Threading.Tasks;

namespace PGATourLeaderboard
{
	public class TournamentsPage : ContentPage
	{
		public TournamentsPage ()
		{
			this.Title = "Tournaments";

			var loader = new ActivityIndicator {
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				Color = Color.FromRgb(0, 61, 125),
				IsRunning = true,
			};

			this.Content = loader;

			GetTournaments ();
		}

		private async Task GetTournaments()
		{
			var listView = new ListView {
				ItemsSource = await TournamentManager.GetTournaments (),
			};

			var cell = new DataTemplate (typeof(TextCell));
			cell.SetBinding (TextCell.TextProperty, "Name");

			listView.ItemTemplate = cell;
			listView.ItemTapped += (object sender, ItemTappedEventArgs e) => {
				var itemSelected = (Tournament)listView.SelectedItem;
				this.Navigation.PushAsync(new TournamentScoresPage(itemSelected));

				listView.SelectedItem = null;
			};

			this.Content = listView;
		}
	}
}