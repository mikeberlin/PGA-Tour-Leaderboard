using System;
using Xamarin.Forms;
using System.Reflection;

namespace PGATourLeaderboard
{
	public class MainPage : ContentPage
	{
		public MainPage()
		{
			this.Title = "PGA Tour Leaderboard";
			this.BackgroundColor = Color.White;

			var logoImage = new Image {
				Aspect = Aspect.AspectFill,
				Source = ImageSource.FromResource ("PGATourLeaderboard.pgatour.png"),
				WidthRequest = 250.0f,
				HeightRequest = 300.0f,
			};

			var viewTournaments = new Button {
				HorizontalOptions = LayoutOptions.Center,
				BackgroundColor = Color.FromRgb(0, 61, 125),
				BorderRadius = 5,
				WidthRequest = 150.0f,
				TextColor = Color.White,
				Text = "View Tournaments",
			};

			viewTournaments.Clicked += async (object sender, EventArgs e) => {
				await Navigation.PushAsync(new TournamentsPage());
			};

			var layout = new StackLayout {
				Children = {
					logoImage,
					viewTournaments,
				},

				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
			};

			this.Content = layout;
		}
	}
}