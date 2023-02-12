using MvvmHelpers;

namespace CSE_681_Project_1
{
	public class TeamInfoViewModel : BaseViewModel
	{
		#region member variables

		private TeamInfo _teamInfo;

		#endregion member variables

		#region properties

		public int FirstDowns => _teamInfo.firstDowns;
		public int FourthDownAttempts => _teamInfo.fourthDownAtt;
		public int FourthDownConversions => _teamInfo.fourthDownConver;
		public int FumblesLost => _teamInfo.fumblesLost;
		public ulong GameCode => _teamInfo.gameCode;
		public string GameDate => _teamInfo.gameDate;
		public int InterceptionsThrown => _teamInfo.interceptionsThrown;
		public int PassAttempts => _teamInfo.passAtt;
		public int PassesCompleted => _teamInfo.passComp;
		public int PassYards => _teamInfo.passYds;
		public int Penalties => _teamInfo.penalties;
		public int PenaltyYards => _teamInfo.penaltYds;
		public int RushAttempts => _teamInfo.rushAtt;
		public int RushYards => _teamInfo.rushYds;
		public int Score => _teamInfo.score;
		public ulong StatIDCode => _teamInfo.statIdCode;
		public int TeamCode => _teamInfo.teamCode;
		public int ThirdDownAttempts => _teamInfo.thridDownAtt;
		public int ThirdDownConversions => _teamInfo.thirdDownConver;

		public int TimePossed => _teamInfo.timePoss;

		#endregion properties

		public TeamInfoViewModel(TeamInfo teamInfo)
		{
			_teamInfo = teamInfo;
		}
	}
}