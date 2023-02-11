using MvvmHelpers;
using System;

namespace CSE_681_Project_1.Main
{
	public class DataManager : ObservableObject
	{
		#region member variables

		private static readonly Lazy<DataManager> _instance = new Lazy<DataManager>(() => new DataManager());
		private GameInfo _selectedGame = new();

		#endregion member variables

		#region properties

		public static DataManager Instance
		{
			get => _instance.Value;
		}

		public ModifyCollectionIndex<GameInfo> AllGames
		{
			get;
			set;
		} = new ModifyCollectionIndex<GameInfo>();

		public GameInfo SelectedGame
		{
			get => _selectedGame;
			set
			{
				_selectedGame = value;
				OnPropertyChanged(nameof(SelectedGame));
			}
		}

		#endregion properties
	}
}