using MvvmHelpers;

namespace CSE_681_Project_1
{
	public class ModifyCollectionIndex<T> : ObservableRangeCollection<T>
	{
		#region methods

		public T GoBack(T selectedItem) => ChangeIndex(selectedItem);

		public T GoForward(T selectedItem) => ChangeIndex(selectedItem, true);

		private T ChangeIndex(T selectedItem, bool forward = false)
		{
			if (Items.Count > 0)
			{
				var newIndex = Items.IndexOf(selectedItem);
				newIndex = forward ? newIndex + 1 : newIndex - 1;

				if (newIndex < 0)
				{
					newIndex = Items.Count - 1;
				}
				else if (newIndex >= Items.Count)
				{
					newIndex = 0;
				}
				selectedItem = Items[newIndex];
			}
			return selectedItem;
		}

		#endregion methods
	}
}