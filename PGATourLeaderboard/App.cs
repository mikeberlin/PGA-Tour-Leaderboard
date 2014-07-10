using System;
using Xamarin.Forms;
using System.IO;

namespace PGATourLeaderboard
{
	public class App
	{
		public static Page GetMainPage ()
		{
			return new NavigationPage (new MainPage ());
		}
	}
}