namespace CSE_681_Project_1
{
	public class TeamInfo
	{
		public int firstDowns { get; set; }
		public int fourthDownAtt { get; set; }
		public int fourthDownConver { get; set; }
		public int fumblesLost { get; set; }
		public ulong gameCode { get; set; }

		public string gameDate { get; set; } = "";
		public int interceptionsThrown { get; set; }

		public int passAtt { get; set; }
		public int passComp { get; set; }
		public int passYds { get; set; }
		public int penalties { get; set; }
		public int penaltYds { get; set; }
		public int rushAtt { get; set; }
		public int rushYds { get; set; }
		public int score { get; set; }
		public ulong statIdCode { get; set; }
		public int teamCode { get; set; }
		public int thirdDownConver { get; set; }
		public int thridDownAtt { get; set; }
		public int timePoss { get; set; }
	}
}