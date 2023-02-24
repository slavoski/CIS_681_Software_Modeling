using Microsoft.Win32;
using MvvmHelpers;
using System.IO;
using System.Linq;

namespace CSE_681_Project_1
{
	public class FileManagement : ObservableObject
	{
		#region member variables

		private string _fileName = "";
		private bool _isFileLoaded = true;
		private bool _isFileParsed;
		private string _mainFile = "";
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

		public string CreateNewFile(string dataToWrite, bool resetMainFile = true)
		{
			var result = "File Was Not Saved";
			SaveFileDialog sfd = new SaveFileDialog()
			{
				DefaultExt = ".json",
				FileName = "",
				Filter = "JSON (.json)|*.json",
				OverwritePrompt = true,
				Title = "Save JSON File"
			};

			if (sfd.ShowDialog() == true)
			{
				m_fullFilePath = sfd.FileName;
				result = SaveFile(dataToWrite);
				result += "\n";
				result += OpenFileWithPath(sfd.FileName, sfd.SafeFileName);
				MainFile = resetMainFile ? "" : MainFile;
			}

			return result;
		}

		public string OpenFile()
		{
			var result = string.Empty;
			var openFileDialog = new OpenFileDialog()
			{
				DefaultExt = ".json",
				Filter = "JSON (.json)|*.json|Text (.txt)|*.txt",
				Multiselect = false
			};

			if (openFileDialog.ShowDialog() == true)
			{
				result = OpenFileWithPath(openFileDialog.FileName, openFileDialog.SafeFileName);
			}
			else
			{
				result += "File Not Loaded";
				IsFileLoaded = true;
			}

			return result;
		}

		public string OpenFileWithPath(string _fullFilePathName, string _fileName)
		{
			m_fullFilePath = _fullFilePathName;
			FileName = _fileName;
			var result = string.Empty;
			using (StreamReader streamReader = new StreamReader(m_fullFilePath))
			{
				MainFile = streamReader.ReadToEnd();

				if (!MainFile.Any())
				{
					result = "File Was Empty";
				}

				IsFileLoaded = true;
				IsFileParsed = false;
			}

			return result;
		}

		public string SaveFile(string dataToSave)
		{
			var result = "Data Saved Succesfully!";
			if (!string.IsNullOrEmpty(dataToSave))
			{
				if (string.IsNullOrEmpty(m_fullFilePath))
				{
					CreateNewFile(dataToSave, false);
				}
				else
				{
					File.WriteAllText(m_fullFilePath, dataToSave);
				}
			}
			else
			{
				result = "The Data to Save cannot be empty";
			}
			return result;
		}

		#endregion methods
	}
}