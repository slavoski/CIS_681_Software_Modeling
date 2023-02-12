using System;
using System.ComponentModel;

namespace CSE_681_Project_1
{
	public class GameInfo
	{
		[Description("Date")]
		public DateTime date { get; set; }

		public TeamInfo homeStats { get; set; } = new TeamInfo();

		[Description("Home Team")]
		public string homeTeamName { get; set; } = "";

		[Description("Is Final")]
		public bool isFinal { get; set; } = false;

		[Description("Neutral")]
		public bool neutral { get; set; } = false;

		public TeamInfo visStats { get; set; } = new TeamInfo();

		[Description("Visitor Team")]
		public string visTeamName { get; set; } = "";
	}
}