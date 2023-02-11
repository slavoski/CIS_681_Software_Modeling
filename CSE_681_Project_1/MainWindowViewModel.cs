using MaterialDesignThemes.Wpf;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System.Threading.Tasks;
using System.Windows;

namespace CSE_681_Project_1.Main
{
	public class MainWindowViewModel : BaseViewModel
	{
		#region member variables

		private Visibility _isAddGameDialogVisible = Visibility.Hidden;
		private Visibility _isEditGameDialogVisible = Visibility.Hidden;
		private GameInfo _newGameInfo = new GameInfo();
		private int _selectedIndex;

		#endregion member variables

		#region properties

		public FileManagement FileManagement
		{
			get;
			set;
		} = new FileManagement();

		public string GameInfoHeader => DataManager.Instance.SelectedGame?.HomeVsTeam ?? "";

		public Visibility IsAddGameDialogVisible
		{
			get => _isAddGameDialogVisible;
			set
			{
				_isAddGameDialogVisible = value;
				OnPropertyChanged(nameof(IsAddGameDialogVisible));
			}
		}

		public Visibility IsEditGameDialogVisible
		{
			get => _isEditGameDialogVisible;
			set
			{
				_isAddGameDialogVisible = value;
				OnPropertyChanged(nameof(_isEditGameDialogVisible));
			}
		}

		public GameInfo NewGameInfo
		{
			get => _newGameInfo;
			set
			{
				_newGameInfo = value;
				OnPropertyChanged(nameof(NewGameInfo));
			}
		}

		public int SelectedIndex
		{
			get => _selectedIndex;
			set
			{
				_selectedIndex = value;
				OnPropertyChanged(nameof(SelectedIndex));
			}
		}

		public SnackbarMessageQueue SnackBoxMessage
		{
			get;
			set;
		} = new SnackbarMessageQueue();

		#endregion properties

		#region Commands

		public Command ChangeNewGameVisibilityCommand
		{
			get;
			private set;
		}

		public Command CreateGameCommand
		{
			get;
			private set;
		}

		public Command CreateNewFileCommand
		{
			get;
			private set;
		}

		public Command GoBackCommand
		{
			get;
			private set;
		}

		public Command GoForwardCommand
		{
			get;
			private set;
		}

		public Command OpenCommand
		{
			get;
			private set;
		}

		public Command SaveCommand
		{
			get;
			private set;
		}

		#endregion Commands

		#region constructor / destructor

		public MainWindowViewModel()
		{
			InitializeCommands();
			DataManager.Instance.PropertyChanged += Instance_PropertyChanged;
		}

		private void Instance_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(DataManager.Instance.SelectedGame) && FileManagement.IsFileLoaded == true)
			{
				SelectedIndex = 1;
				OnPropertyChanged(nameof(GameInfoHeader));
			}
		}

		#endregion constructor / destructor

		#region methods

		public async Task CreateGame()
		{
			FileManagement.IsFileLoaded = true;

			DataManager.Instance.AllGames.Add(NewGameInfo);

			NewGameInfo = new GameInfo();

			IsAddGameDialogVisible = Visibility.Hidden;

			await Task.CompletedTask;
		}

		private void InitializeCommands()
		{
			CreateGameCommand = new Command(async () => await CreateGame());

			OpenCommand = new Command(() =>
				{
					FileManagement.IsFileLoaded = false;
					DataManager.Instance.SelectedGame = new GameInfo();
					FileManagement.OpenFile(DataManager.Instance.AllGames);
				});

			SaveCommand = new Command(() => FileManagement.SaveFile(DataManager.Instance.AllGames));
			CreateNewFileCommand = new Command(() => FileManagement.CreateNewFile());
			GoBackCommand = new Command(() =>
				{
					DataManager.Instance.SelectedGame = DataManager.Instance.AllGames.GoBack(DataManager.Instance.SelectedGame);
				});

			GoForwardCommand = new Command(() =>
				{
					DataManager.Instance.SelectedGame = DataManager.Instance.AllGames.GoForward(DataManager.Instance.SelectedGame);
				});

			ChangeNewGameVisibilityCommand = new Command(() => IsAddGameDialogVisible = Visibility.Visible);
		}

		#endregion methods
	}
}