using MaterialDesignThemes.Wpf;

namespace CSE_681_Project_1.Main
{
	public static class SnackBarManager
	{
		public static SnackbarMessageQueue SnackBoxMessage
		{
			get;
			set;
		} = new SnackbarMessageQueue();
	}
}