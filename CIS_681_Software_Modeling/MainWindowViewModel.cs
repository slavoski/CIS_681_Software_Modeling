using MvvmHelpers;
using MvvmHelpers.Commands;

namespace CIS_681_Software_Modeling
{
	public class MainWindowViewModel : BaseViewModel
	{
		#region member variables

		private string _content;
		private string _defaultContent = "Hello World!";
		private string _newContent;

		#endregion member variables

		#region Commands

		public string Content
		{
			get => _content;
			set
			{
				_content = value;
				OnPropertyChanged(nameof(Content));
			}
		}

		public Command DisplayOutputCommand
		{
			get;
			set;
		}

		public string NewContent
		{
			get => _newContent;
			set
			{
				_newContent = value;
				OnPropertyChanged(nameof(NewContent));
			}
		}

		#endregion Commands

		#region constructor / destructor

		public MainWindowViewModel()
		{
			DisplayOutputCommand = new Command(() => DisplayOutput());
			Content = NewContent = _defaultContent;
		}

		#endregion constructor / destructor

		#region methods

		private void DisplayOutput()
		{
			Content = NewContent;
		}

		#endregion methods
	}
}