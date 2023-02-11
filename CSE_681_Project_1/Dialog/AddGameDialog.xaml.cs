using MvvmHelpers.Commands;
using System;
using System.Windows;

namespace CSE_681_Project_1.Dialog
{
	/// <summary>
	/// Interaction logic for AddInvestorDialog.xaml
	/// </summary>
	public partial class AddGameDialog
	{
		public static readonly DependencyProperty DialogTitleProperty = DependencyProperty.Register(nameof(DialogTitle), typeof(string), typeof(AddGameDialog), new UIPropertyMetadata(string.Empty));
		public static readonly DependencyProperty IsBusinessProperty = DependencyProperty.Register(nameof(IsBusiness), typeof(bool), typeof(AddGameDialog), new UIPropertyMetadata(false));
		public static readonly DependencyProperty NewItemProperty = DependencyProperty.Register(nameof(NewItem), typeof(GameInfo), typeof(AddGameDialog), null);
		public static readonly DependencyProperty OnCompletedProperty = DependencyProperty.Register(nameof(CompletedCommand), typeof(Command), typeof(AddGameDialog), null);

		#region commands

		public Command CancelCommand
		{
			get;
			private set;
		}

		public Command CreateCommand
		{
			get;
			private set;
		}

		public Command DoNothingCommand
		{
			get;
			private set;
		}

		#endregion commands

		#region constructor / destructor

		public AddGameDialog()
		{
			InitializeComponent();
			CancelCommand = new Command(() => Cancel());
			CreateCommand = new Command(() => CreateItem());
			DoNothingCommand = new Command(() => DoNothing());
		}

		private void DoNothing()
		{
			//todo
		}

		#endregion constructor / destructor

		#region properties

		public Command CompletedCommand
		{
			get => (Command)GetValue(OnCompletedProperty);
			set => SetValue(OnCompletedProperty, value);
		}

		public string DialogTitle
		{
			get => (string)GetValue(DialogTitleProperty);
			set => SetValue(DialogTitleProperty, value);
		}

		public bool IsBusiness
		{
			get => (bool)GetValue(IsBusinessProperty);
			set => SetValue(IsBusinessProperty, value);
		}

		public GameInfo NewItem
		{
			get => (GameInfo)GetValue(NewItemProperty);
			set => SetValue(NewItemProperty, value);
		}

		#endregion properties

		#region methods

		public void Cancel() => Visibility = Visibility.Hidden;

		public void CreateItem()
		{
			if (NewItem.homeTeamName != String.Empty && CompletedCommand.CanExecute(this))
			{
				CompletedCommand?.Execute(this);
			}
			else
			{
				SnackBarManager.SnackBoxMessage.Enqueue("Home Team Name is required for New Game.");
			}
		}

		#endregion methods
	}
}