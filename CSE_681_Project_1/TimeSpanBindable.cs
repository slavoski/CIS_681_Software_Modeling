using MvvmHelpers;
using System;

namespace CSE_681_Project_1
{
	public class TimeSpanBindable<T> : BaseViewModel
	{
		#region member variables

		private TimeSpan _timeSpan;

		#endregion member variables

		public TimeSpanBindable()
		{ }

		public TimeSpanBindable(string value)
		{
			_timeSpan = TimeSpan.Parse(value);
		}

		public TimeSpanBindable(int value)
		{
			_timeSpan = TimeSpan.FromSeconds(value);
		}

		public int Hours
		{
			get => _timeSpan.Hours;
			set
			{
				_timeSpan = new TimeSpan(value, Minutes, Seconds);
				OnPropertyChanged(nameof(Hours));
				OnPropertyChanged(nameof(TotalSeconds));
			}
		}

		public int Minutes
		{
			get => _timeSpan.Minutes;
			set
			{
				_timeSpan = new TimeSpan(Hours, value, Seconds);
				OnPropertyChanged(nameof(Minutes));
				OnPropertyChanged(nameof(TotalSeconds));
			}
		}

		public int Seconds
		{
			get => _timeSpan.Seconds;
			set
			{
				_timeSpan = new TimeSpan(Hours, Minutes, value);
				OnPropertyChanged(nameof(Seconds));
				OnPropertyChanged(nameof(TotalSeconds));
			}
		}

		public double TotalSeconds
		{
			get => _timeSpan.TotalSeconds;
			set
			{
				_timeSpan = TimeSpan.FromSeconds(value);

				OnPropertyChanged(nameof(Hours));
				OnPropertyChanged(nameof(Minutes));
				OnPropertyChanged(nameof(Seconds));
				OnPropertyChanged(nameof(TotalSeconds));
			}
		}
	}
}