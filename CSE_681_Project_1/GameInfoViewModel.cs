using MvvmHelpers;
using System;

namespace CSE_681_Project_1
{
	public class GameInfoViewModel : BaseViewModel
	{
		#region member variables

		private GameInfo _gameInfo;
		private TeamInfoViewModel _homeTeamStats;
		private TeamInfoViewModel _visTeamStats;
		private string _winningTeam;
		private int _winningTeamGameCode;

		#endregion member variables

		#region Properties

		public DateTime Date => _gameInfo.date;
		public string FinalScore => (HomeTeamWon ? _homeTeamStats.Score : _visTeamStats.Score) + " - " + (HomeTeamWon ? _visTeamStats.Score : _homeTeamStats.Score);
		public string HomeTeamName => _gameInfo.homeTeamName;
		public TeamInfoViewModel HomeTeamStats => _homeTeamStats;
		public bool HomeTeamWon => _homeTeamStats.Score > _visTeamStats.Score;
		public string HomeVsTeam => HomeTeamName + " vs " + VisitorTeamName;
		public bool IsFinal => _gameInfo.isFinal;
		public GameInfo JSONDeserialize => _gameInfo;
		public bool Neutral => _gameInfo.neutral;
		public string VisitorTeamName => _gameInfo.visTeamName;
		public TeamInfoViewModel VisTeamStats => _visTeamStats;
		public string WinningTeam => _winningTeam;
		public int WinningTeamGameCode => _winningTeamGameCode;

		#endregion Properties

		#region constructor / destructor

		public GameInfoViewModel(GameInfo gameInfo)
		{
			_gameInfo = gameInfo;
			_homeTeamStats = new TeamInfoViewModel(gameInfo.homeStats);
			_visTeamStats = new TeamInfoViewModel(gameInfo.visStats);

			_winningTeam = "Winning Team: " + (HomeTeamWon ? HomeTeamName : VisitorTeamName);
			_winningTeamGameCode = HomeTeamWon ? _homeTeamStats.TeamCode : _visTeamStats.TeamCode;
		}

		#endregion constructor / destructor
	}
}