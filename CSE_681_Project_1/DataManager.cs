using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace CSE_681_Project_1.Main
{
	public class DataManager : ObservableObject
	{
		#region member variables

		private static readonly Lazy<DataManager> _instance = new Lazy<DataManager>(() => new DataManager());
		private GameInfoViewModel _selectedGame = new(new());

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

		public GameInfoViewModel SelectedGame
		{
			get => _selectedGame;
			set
			{
				_selectedGame = value;
				OnPropertyChanged(nameof(SelectedGame));
			}
		}

		#endregion properties

		#region methods

		public string SaveData => JsonConvert.SerializeObject(DataManager.Instance.AllGames.Select(p => p.JSONDeserialize), Formatting.Indented);

		public void ClearData()
		{
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

		#endregion methods
	}
}