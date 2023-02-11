﻿using Microsoft.Win32;
using MvvmHelpers;
using Newtonsoft.Json;
using System.IO;

namespace CSE_681_Project_1
{
	public class FileManagement : ObservableObject
	{
		#region member variables

		private string _fileName = "";
		private bool _isFileLoaded;
		private bool _isFileParsed;
		private string _mainFile = "Load File to Edit";
		private string m_fullFilePath = "";

		#endregion member variables

		#region properties

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

		#endregion properties

		#region methods

		public void CreateNewFile()
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

		public void OpenFile(ObservableRangeCollection<GameInfo> allGames)
		{
			var openFileDialog = new OpenFileDialog()
			{
				DefaultExt = ".json",
				Filter = "JSON (.json)|*.json|Text (.txt)|*.txt",
				Multiselect = false
			};

			if (openFileDialog.ShowDialog() == true)
			{
				OpenFileWithPath(openFileDialog.FileName, openFileDialog.SafeFileName, allGames);
			}
			else
			{
				IsFileLoaded = true;
			}
		}

		public void OpenFileWithPath(string _fullFilePathName, string _fileName, ObservableRangeCollection<GameInfo> allGames)
		{
			m_fullFilePath = _fullFilePathName;
			FileName = _fileName;
			using (StreamReader streamReader = new StreamReader(m_fullFilePath))
			{
				MainFile = streamReader.ReadToEnd();
				allGames.ReplaceRange(JsonConvert.DeserializeObject<ObservableRangeCollection<GameInfo>>(MainFile));

				IsFileLoaded = true;
				IsFileParsed = false;
			}
		}

		public void SaveFile(ObservableRangeCollection<GameInfo> allGames)
		{
			File.WriteAllText(m_fullFilePath, MainFile);
		}

		#endregion methods
	}
}