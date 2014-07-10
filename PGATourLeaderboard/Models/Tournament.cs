using System;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PGATourLeaderboard
{
	public class Tournament
	{
		#region Properties

		public string Id { get; set; }
		public string Name { get; set; }
		public float Purse { get; set; }
		public float WinnerShare { get; set; }
		public int FedExPoints { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		#endregion

		#region Constructors

		public Tournament ()
		{
		}

		public Tournament (XElement t)
		{
			var id = t.Attributes ().Where (a => a.Name.LocalName == "id").FirstOrDefault ();
			var name = t.Attributes ().Where (a => a.Name.LocalName == "name").FirstOrDefault ();
			var purse = t.Attributes ().Where (a => a.Name.LocalName == "purse").FirstOrDefault ();
			var winnerShare = t.Attributes ().Where (a => a.Name.LocalName == "winning_share").FirstOrDefault ();
			var fedExPoints = t.Attributes ().Where (a => a.Name.LocalName == "points").FirstOrDefault ();
			var startDate = t.Attributes ().Where (a => a.Name.LocalName == "start_date").FirstOrDefault ();
			var endDate = t.Attributes ().Where (a => a.Name.LocalName == "end_date").FirstOrDefault ();

			Id = (id == null) ? string.Empty : id.Value;
			Name = (name == null) ? string.Empty : name.Value;
			Purse = (purse == null) ? 0 : float.Parse (purse.Value);
			WinnerShare = (winnerShare == null) ? 0 : float.Parse(winnerShare.Value);
			FedExPoints = (fedExPoints == null) ? 0 : int.Parse (fedExPoints.Value);
			StartDate = (startDate == null) ? DateTime.MinValue : DateTime.Parse (startDate.Value);
			EndDate = (endDate == null) ? DateTime.MaxValue : DateTime.Parse (endDate.Value);
		}

		#endregion
	}

	public static class TournamentManager
	{
		#region Properties, Constants, etc.

		private static readonly string TOURNAMENTS_URI = "https://api.sportsdatallc.org/golf-t1/schedule/pga/2014/tournaments/schedule.xml?api_key={0}";
		private static readonly XNamespace TOURNAMENTS_NAMESPACE = "http://feed.elasticstats.com/schema/golf/schedule-v1.0.xsd";

		public static IEnumerable<Tournament> Tournaments { get; set; }

		#endregion

		#region Public Methods

		public static async Task<IEnumerable<Tournament>> GetTournaments ()
		{
			if (Tournaments == null) {
				Tournaments = await Task.Factory.StartNew(() => {
					return XDocument.Load (string.Format (TOURNAMENTS_URI, API.KEY))
						.Descendants (TOURNAMENTS_NAMESPACE + "tournament")
						.Select (t => new Tournament (t))
						.ToList ();
				});
			}

			return Tournaments;
		}

		#endregion
	}
}