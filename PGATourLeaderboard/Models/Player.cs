using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace PGATourLeaderboard
{
	public class Player
	{
		#region Properties

		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Country { get; set; }
		public int Position { get; set; }
		public int Score { get; set; }
		public int Strokes { get; set; }
		public double Money { get; set; }
		public double Points { get; set; }

		public string FullName
		{
			get {
				return string.Format ("{0} {1}", FirstName, LastName);
			}
		}

		public string ScoreDisplay
		{
			get {
				if (Score == 0)
					return "E";
				else if (Score > 0)
					return string.Format ("+{0}", Score);
				else
					return string.Format ("{0}", Score);
			}
		}

		#endregion

		#region Constructors

		public Player (XElement t)
		{
			var id = t.Attributes ().Where (a => a.Name.LocalName == "id").FirstOrDefault ();
			var firstName = t.Attributes ().Where (a => a.Name.LocalName == "first_name").FirstOrDefault ();
			var lastName = t.Attributes ().Where (a => a.Name.LocalName == "last_name").FirstOrDefault ();
			var country = t.Attributes ().Where (a => a.Name.LocalName == "country").FirstOrDefault ();
			var position = t.Attributes ().Where (a => a.Name.LocalName == "position").FirstOrDefault ();
			var score = t.Attributes ().Where (a => a.Name.LocalName == "score").FirstOrDefault ();
			var strokes = t.Attributes ().Where (a => a.Name.LocalName == "strokes").FirstOrDefault ();
			var money = t.Attributes ().Where (a => a.Name.LocalName == "money").FirstOrDefault ();
			var points = t.Attributes ().Where (a => a.Name.LocalName == "points").FirstOrDefault ();

			Id = (id == null) ? string.Empty : id.Value;
			FirstName = (firstName == null) ? string.Empty : firstName.Value;
			LastName = (lastName == null) ? string.Empty : lastName.Value;
			Country = (country == null) ? string.Empty : country.Value;
			Position = (position == null) ? 0 : int.Parse (position.Value);
			Score = (score == null) ? 0 : int.Parse (score.Value);
			Strokes = (strokes == null) ? 0 : int.Parse (strokes.Value);
			Money = (money == null) ? 0 : double.Parse (money.Value);
			Points = (points == null) ? 0 : double.Parse (points.Value);

			// TODO: Fill in Rounds property...
		}

		#endregion
	}

	public class PlayerManager
	{
		#region Properties, Constants, etc.

		private readonly string PlayerScoresBaseUri = "https://api.sportsdatallc.org/golf-t1/leaderboard/pga/2014/tournaments/{0}/leaderboard.xml?api_key={1}";
		private readonly XNamespace PlayerScoreNamespace = "http://feed.elasticstats.com/schema/golf/tournament/leaderboard-v1.0.xsd";

		public string TournamentId { get; set; }
		public IEnumerable<Player> PlayerScores { get; set; }

		#endregion

		#region Constructors

		public PlayerManager (string tournamentId)
		{
			TournamentId = tournamentId;
		}

		#endregion

		#region Public Methods

		public async Task<IEnumerable<Player>> GetPlayerScores ()
		{
			PlayerScores = await Task.Factory.StartNew (() => {
				if (PlayerScores == null) {
					PlayerScores = XDocument.Load (string.Format (PlayerScoresBaseUri, TournamentId, API.KEY))
						.Descendants (PlayerScoreNamespace + "leaderboard")
						.Descendants (PlayerScoreNamespace + "player")
						.Select (ps => new Player (ps))
						.ToList ();
				}

				return PlayerScores;
			});

			return PlayerScores;
		}

		#endregion
	}
}