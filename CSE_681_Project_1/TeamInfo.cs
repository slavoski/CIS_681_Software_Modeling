using System.ComponentModel;

namespace CSE_681_Project_1
{
	public class TeamInfo
	{
		[Description("First Downs")]
		public int firstDowns { get; set; }

		[Description("Fourth Down Attempts")]
		public int fourthDownAtt { get; set; }

		[Description("Fourth Down Conversions")]
		public int fourthDownConver { get; set; }

		[Description("Fumbles Lost")]
		public int fumblesLost { get; set; }

		[Description("Game Code")]
		public ulong gameCode { get; set; }

		[Description("Game Date")]
		public string gameDate { get; set; } = "";

		[Description("Interceptions Thrown")]
		public int interceptionsThrown { get; set; }

		[Description("Pass Attempts")]
		public int passAtt { get; set; }

		[Description("Passes Completed")]
		public int passComp { get; set; }

		[Description("Pass Yards")]
		public int passYds { get; set; }

		[Description("Penalties")]
		public int penalties { get; set; }

		[Description("Penalty Yards")]
		public int penaltYds { get; set; }

		[Description("Rush Attempts")]
		public int rushAtt { get; set; }

		[Description("Rush Yards")]
		public int rushYds { get; set; }

		[Description("Score")]
		public int score { get; set; }

		[Description("Stat ID Code")]
		public ulong statIdCode { get; set; }

		[Description("Team Code")]
		public int teamCode { get; set; }

		[Description("Third Down Conversions")]
		public int thirdDownConver { get; set; }

		[Description("Third Down Attempts")]
		public int thridDownAtt { get; set; }

		[Description("Time Possesed")]
		public int timePoss { get; set; }
	}
}