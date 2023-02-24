using CSE_681_Project_1.Enums;
using CSE_681_Project_1.Extensions;
using MaterialDesignThemes.Wpf;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace CSE_681_Project_1.Main
{
	public class MainWindowViewModel : BaseViewModel
	{
		#region member variables

		private readonly Visibility _isEditGameDialogVisible = Visibility.Hidden;
		private Visibility _isAddGameDialogVisible = Visibility.Hidden;
		private bool _isDialogSaveFileVisible;
		private GameInfo _newGameInfo = new();
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

		public bool IsDialogSaveFileVisible
		{
			get => _isDialogSaveFileVisible;
			set
			{
				_isDialogSaveFileVisible = value;
				OnPropertyChanged(nameof(IsDialogSaveFileVisible));
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

		public string SelectedTeam => DataManager.Instance.SelectedGame != null ? ((Teams)SelectedTeamIndex + 1).Description() : "";

		public int SelectedTeamIndex
		{
			get;
			set;
		} = 0;

		public int SelectedYearIndex
		{
			get;
			set;
		} = 19;

		public SnackbarMessageQueue SnackBoxMessage
		{
			get;
			set;
		} = new SnackbarMessageQueue();

		public string URL => $"https://sports.snoozle.net/search/nfl/searchHandler?fileType=inline&statType=teamStats&season={Year}&teamName={SelectedTeamIndex + 1}";
		public int Year => SelectedYearIndex + (int)TimeLine.One;

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

		public Command ParseFromWebCommand
		{
			get;
			private set;
		}

		public Command SaveCommand
		{
			get;
			private set;
		}

		public Command SaveFileDialogCommand
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

		#endregion constructor / destructor

		#region methods

		public static string GetEnumDescription(Teams value)
		{
			FieldInfo fi = value.GetType().GetField(value.ToString());
			var results = string.Empty;
			if (fi != null)
			{
				DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

				if (attributes != null && attributes.Any())
				{
					results = attributes.First().Description;
				}
			}

			return results;
		}

		public async Task CreateGame()
		{
			FileManagement.IsFileLoaded = true;

			DataManager.Instance.AllGames.Add(new GameInfoViewModel(NewGameInfo));

			NewGameInfo = new();

			IsAddGameDialogVisible = Visibility.Hidden;

			FileManagement.MainFile = DataManager.Instance.SaveData();

			await Task.CompletedTask;
		}

		public void SaveFileDialog()
		{
			SnackBarManager.SnackBoxMessage.Enqueue(FileManagement.SaveFile(DataManager.Instance.SaveData()));
		}

		private void InitializeCommands()
		{
			CreateGameCommand = new Command(async () => await CreateGame());

			OpenCommand = new Command(() =>
				{
					FileManagement.IsFileLoaded = false;
					DataManager.Instance.SelectedGame = new(new());
					var result = FileManagement.OpenFile();

					if (!(string.Empty == result))
					{
						SnackBarManager.SnackBoxMessage.Enqueue(result);
					}
					else
					{
						DataManager.Instance.ParseData(FileManagement.MainFile);
						SnackBarManager.SnackBoxMessage.Enqueue("File Loaded Succesfully");
					}

					OnPropertyChanged(nameof(GameInfoHeader));
					SelectedIndex = 0;
				});

			SaveCommand = new Command(() =>
				{
					IsDialogSaveFileVisible = true;
				});
			CreateNewFileCommand = new Command(() =>
			{
				SnackBarManager.SnackBoxMessage.Enqueue(FileManagement.CreateNewFile(DataManager.Instance.SaveData()));

				if (string.IsNullOrEmpty(FileManagement.MainFile))
				{
					DataManager.Instance.ClearData();
				}

				DataManager.Instance.ParseData(FileManagement.MainFile);
			});
			GoBackCommand = new Command(() =>
				{
					DataManager.Instance.SelectedGame = DataManager.Instance.AllGames.GoBack(DataManager.Instance.SelectedGame);
				});

			GoForwardCommand = new Command(() =>
				{
					DataManager.Instance.SelectedGame = DataManager.Instance.AllGames.GoForward(DataManager.Instance.SelectedGame);
				});

			ChangeNewGameVisibilityCommand = new Command(() => IsAddGameDialogVisible = Visibility.Visible);

			SaveFileDialogCommand = new Command(() =>
			{
				DialogHost.CloseDialogCommand.Execute(null, null);
				DrawerHost.CloseDrawerCommand.Execute(null, null);
				SaveFileDialog();
			});

			ParseFromWebCommand = new Command(() =>
			{
				var results = WebManagement.Instance.GetJsonFromURL<MatchUpStats>(URL);
				FileManagement.MainFile = results.Item1;
				DataManager.Instance.ParseData(results.Item2);
				OnPropertyChanged(nameof(SelectedTeam));
			});
		}

		#endregion methods

		#region event handlers

		private void Instance_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(DataManager.Instance.SelectedGame) && FileManagement.IsFileLoaded == true)
			{
				SelectedIndex = 1;
				OnPropertyChanged(nameof(GameInfoHeader));
			}
			else if (e.PropertyName == nameof(DataManager.Instance.MatchUp))
			{
				var selectedTeamCode = DataManager.Instance.MatchUp.MatchUpStats.FirstOrDefault(g => SelectedTeam.Contains(g.homeTeamName))?.homeStats.teamCode ?? 0;
				DataManager.Instance.MatchUp.SetSelectedGameCode(selectedTeamCode);
			}
		}

		#endregion event handlers
	}
}