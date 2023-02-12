using MvvmHelpers;
using System;

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
	}
}