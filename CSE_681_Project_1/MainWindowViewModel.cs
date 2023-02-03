using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System.IO;

namespace CSE_681_Project_1.Main
{
	public class MainWindowViewModel : BaseViewModel
	{
		#region member variables

		private string _fileName = "";
		private bool _isFileLoaded;
		private bool _isFileParsed;
		private string _mainFile = "Load File to Edit";
		private GameInfo _selectedGame;
		private string m_fullFilePath = "";

		#endregion member variables

		#region properties

		public ObservableRangeCollection<GameInfo> AllGames
		{
			get;
			set;
		} = new ObservableRangeCollection<GameInfo>();

		public string FileName
		{
			get => _fileName;
			set
			{
				_fileName = value;
				OnPropertyChanged(nameof(FileName));
			}
		}

		public bool IsFileLoaded
		{
			get => _isFileLoaded;
			set
			{
				_isFileLoaded = value;
				OnPropertyChanged(nameof(IsFileLoaded));
			}
		}

		public bool IsFileParsed
		{
			get => _isFileParsed && _isFileLoaded;
			set
			{
				_isFileParsed = value;
				OnPropertyChanged(nameof(IsFileParsed));
			}
		}

		public string MainFile
		{
			get => _mainFile;
			set
			{
				_mainFile = value;
				OnPropertyChanged(nameof(MainFile));
			}
		}

		public GameInfo SelectedGame
		{
			get => _selectedGame;
			set
			{
				_selectedGame = value;
				OnPropertyChanged(nameof(SelectedGame));
			}
		}

		public SnackbarMessageQueue SnackBoxMessage
		{
			get;
			set;
		} = new SnackbarMessageQueue();

		#endregion properties

		#region Commands

		public Command CreateNewFileCommand
		{
			get;
			set;
		}

		public Command GoBackCommand
		{
			get;
			set;
		}

		public Command GoForwardCommand
		{
			get;
			set;
		}

		public Command OpenCommand
		{
			get;
			set;
		}

		public Command ParseJsonCommand
		{
			get;
			set;
		}

		public Command SaveCommand
		{
			get;
			set;
		}

		#endregion Commands

		#region constructor / destructor

		public MainWindowViewModel()
		{
			InitializeCommands();
		}

		#endregion constructor / destructor

		#region methods

		private void ChangeIndex(bool forward = false)
		{
			if (AllGames.Count > 0)
			{
				var oldIndex = AllGames.IndexOf(SelectedGame);
				var newIndex = AllGames.IndexOf(SelectedGame);

				oldIndex = oldIndex < 0 ? 0 : oldIndex;
				newIndex = forward ? newIndex + 1 : newIndex - 1;

				if (newIndex < 0)
				{
					newIndex = AllGames.Count - 1;
				}
				else if (newIndex >= AllGames.Count)
				{
					newIndex = 0;
				}
				SelectedGame = AllGames[newIndex];
			}
		}

		private void CreateNewFile()
		{
			SaveFileDialog sfd = new SaveFileDialog()
			{
				DefaultExt = ".json",
				FileName = "JSON",
				Filter = "JSON (.json)|*.json",
				OverwritePrompt = true,
				Title = "Save JSON File"
			};

			if (sfd.ShowDialog() == true)
			{
				//File.WriteAllText(sfd.FileName, "");
				//OpenFileWithPath(sfd.FileName, sfd.SafeFileName);
			}
		}

		private void GoBack()
		{
			ChangeIndex();
		}

		private void GoForward()
		{
			ChangeIndex(true);
		}

		private void InitializeCommands()
		{
			OpenCommand = new Command(() => OpenFile());
			SaveCommand = new Command(() => SaveFile());
			ParseJsonCommand = new Command(() => ParseJson());
			CreateNewFileCommand = new Command(() => CreateNewFile());
			GoBackCommand = new Command(() => GoBack());
			GoForwardCommand = new Command(() => GoForward());
		}

		private void OpenFile()
		{
			var openFileDialog = new OpenFileDialog()
			{
				DefaultExt = ".json",
				Filter = "JSON (.json)|*.json|Text (.txt)|*.txt",
				Multiselect = false
			};

			if (openFileDialog.ShowDialog() == true)
			{
				OpenFileWithPath(openFileDialog.FileName, openFileDialog.SafeFileName);
			}
		}

		private void OpenFileWithPath(string _fullFilePathName, string _fileName)
		{
			m_fullFilePath = _fullFilePathName;
			FileName = _fileName;
			using (StreamReader streamReader = new StreamReader(m_fullFilePath))
			{
				MainFile = streamReader.ReadToEnd();
				AllGames.ReplaceRange(JsonConvert.DeserializeObject<ObservableRangeCollection<GameInfo>>(MainFile));

				IsFileLoaded = true;
				IsFileParsed = false;
			}
		}

		private void ParseJson()
		{
		}

		private void SaveFile()
		{
			File.WriteAllText(m_fullFilePath, MainFile);
		}

		#endregion methods
	}
}