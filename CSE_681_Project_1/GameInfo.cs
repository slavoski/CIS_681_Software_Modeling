using System;

namespace CSE_681_Project_1
{
	public class GameInfo
	{
		public DateTime date { get; set; }
		public TeamInfo homeStats { get; set; } = new TeamInfo();
		public string homeTeamName { get; set; } = "";
		public string HomeVsTeam => homeTeamName + " vs " + visTeamName;
		public bool isFinal { get; set; } = false;
		public bool neutral { get; set; } = false;
		public TeamInfo visStats { get; set; } = new TeamInfo();

		public string visTeamName { get; set; } = "";
	}
}