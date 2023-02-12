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

		#endregion member variables

		#region Properties

		public DateTime Date => _gameInfo.date;
		public string HomeTeamName => _gameInfo.homeTeamName;
		public TeamInfoViewModel HomeTeamStats => _homeTeamStats;
		public string HomeVsTeam => HomeTeamName + " vs " + VisitorTeamName;
		public bool IsFinal => _gameInfo.isFinal;
		public bool Neutral => _gameInfo.neutral;
		public string VisitorTeamName => _gameInfo.visTeamName;
		public TeamInfoViewModel VisTeamStats => _visTeamStats;

		#endregion Properties

		#region constructor / destructor

		public GameInfoViewModel(GameInfo gameInfo)
		{
			_gameInfo = gameInfo;
			_homeTeamStats = new TeamInfoViewModel(gameInfo.homeStats);
			_visTeamStats = new TeamInfoViewModel(gameInfo.visStats);
		}

		#endregion constructor / destructor
	}
}