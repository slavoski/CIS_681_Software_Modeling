using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Windows;

namespace CSE_681_Project_1.Main
{
	public class DataManager : ObservableObject
	{
		#region member variables

		private static readonly Lazy<DataManager> _instance = new Lazy<DataManager>(() => new DataManager());
		private MatchUpStatsViewModel _matchUp = new(new());
		private GameInfoViewModel _selectedGame = new(new());
		private Visibility _visibility = Visibility.Collapsed;

		#endregion member variables

		#region properties

		public static DataManager Instance
		{
			get => _instance.Value;
		}

		public ModifyCollectionIndex<GameInfoViewModel> AllGames
		{
			get;
			set;
		} = new ModifyCollectionIndex<GameInfoViewModel>();

		public MatchUpStatsViewModel MatchUp
		{
			get => _matchUp;
			set
			{
				_matchUp = value;
				OnPropertyChanged(nameof(MatchUp));
			}
		}

		public GameInfoViewModel SelectedGame
		{
			get => _selectedGame;
			set
			{
				_selectedGame = value;
				OnPropertyChanged(nameof(SelectedGame));
			}
		}

		public Visibility Visibility
		{
			get => _visibility;
			set
			{
				_visibility = value;
				OnPropertyChanged(nameof(Visibility));
			}
		}

		#endregion properties

		#region methods

		public void ClearData()
		{
			MatchUp?.MatchUpStats?.Clear();
			AllGames.Clear();
		}

		public string ParseData(string data)
		{
			var result = string.Empty;
			if (!string.IsNullOrWhiteSpace(data))
			{
				var deserializedData = JsonConvert.DeserializeObject<ObservableRangeCollection<GameInfo>>(data);

				if (deserializedData != null)
				{
					AllGames.ReplaceRange(deserializedData.Select(g => new GameInfoViewModel(g)) ?? new ObservableRangeCollection<GameInfoViewModel>());
					OnPropertyChanged(nameof(AllGames));
				}
				else
				{
					result = "Deserialized data was empty";
					ClearData();
				}
			}
			else
			{
				result = "Deserialized data was empty";
				ClearData();
			}

			return result;
		}

		public string ParseData(MatchUpStats matchUp)
		{
			var result = string.Empty;

			if (matchUp.matchUpStats.Any())
			{
				MatchUp = new MatchUpStatsViewModel(matchUp);
				AllGames.ReplaceRange(matchUp.matchUpStats.Select(g => new GameInfoViewModel(g)));
				Visibility = Visibility.Visible;
			}
			else
			{
				AllGames.Clear();
				Visibility = Visibility.Collapsed;
			}

			return result;
		}

		public string SaveData()
		{
			var result = string.Empty;
			if (MatchUp.MatchUpStats != null && MatchUp.MatchUpStats.Any())
			{
				result = JsonConvert.SerializeObject(MatchUp.JSONDeserialize, Formatting.Indented);
			}
			else
			{
				result = JsonConvert.SerializeObject(AllGames.Select(p => p.JSONDeserialize), Formatting.Indented);
			}

			return result;
		}

		#endregion methods
	}
}