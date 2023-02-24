using MvvmHelpers;
using System.Collections.Generic;
using System.Linq;

namespace CSE_681_Project_1
{
	public class MatchUpStatsViewModel : BaseViewModel
	{
		#region member variables

		private readonly List<GameInfoViewModel> _gameInfoViewModel;
		private readonly MatchUpStats _matchUpStats;
		private int _totalLosses;
		private int _totalWins;

		#endregion member variables

		#region Properties

		public MatchUpStats JSONDeserialize => _matchUpStats;
		public List<GameInfo> MatchUpStats => _matchUpStats.matchUpStats;
		public bool Success => _matchUpStats.success;

		public int TotalLosses
		{
			get => _totalLosses;
			set
			{
				_totalLosses = value;
				OnPropertyChanged(nameof(TotalLosses));
			}
		}

		public int TotalWins
		{
			get => _totalWins;
			set
			{
				_totalWins = value;
				OnPropertyChanged(nameof(TotalWins));
			}
		}

		#endregion Properties

		#region constructor / destructor

		public MatchUpStatsViewModel(MatchUpStats matchUpStats)
		{
			_matchUpStats = matchUpStats;
			_gameInfoViewModel = matchUpStats?.matchUpStats?.Select(g => new GameInfoViewModel(g)).ToList();
		}

		#endregion constructor / destructor

		#region methods

		public void SetSelectedGameCode(int selectedTeamGameCode)
		{
			TotalWins = _gameInfoViewModel.Where(p => p.WinningTeamGameCode == selectedTeamGameCode)?.Count() ?? 0;
			TotalLosses = _gameInfoViewModel.Where(p => p.WinningTeamGameCode != selectedTeamGameCode)?.Count() ?? 0;
		}

		#endregion methods
	}
}