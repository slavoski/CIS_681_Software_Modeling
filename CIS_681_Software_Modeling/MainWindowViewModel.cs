using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CIS_681_Software_Modeling
{
	public class MainWindowViewModel : BaseViewModel
	{
		#region member variables

		private string _content;
		private string _newContent;
		private int _numberOfProcesses = 2;
		private int _numberOfThreads = 4;
		private List<int> _numbers;
		private List<Thread> _threads = new List<Thread>();

		#endregion member variables

		#region Properties

		public string Content
		{
			get => _content;
			set
			{
				_content = value;
				OnPropertyChanged(nameof(Content));
			}
		}

		public int NumberOfProcesses
		{
			get => _numberOfProcesses;
			set => SetProperty(ref _numberOfProcesses, value);
		}

		public int NumberOfThreads

		{
			get => _numberOfThreads;
			set => SetProperty(ref _numberOfThreads, value);
		}

		#endregion Properties

		#region Commands

		public Command MultiProcessCommand
		{
			get;
			set;
		}

		public Command MultiThreadCommand
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
			MultiProcessCommand = new Command(() => MultiProcess());
			MultiThreadCommand = new Command(() => MultiThread());
			_numbers = Enumerable.Range(1, 100000000).ToList();
		}

		#endregion constructor / destructor

		#region methods

		private void CalculateNewValue(List<int> currRange)
		{
			currRange.ForEach(range =>
			{
				range *= 12342134;
				Console.WriteLine(range);
			});
		}

		private int CheckValue(int currValue, int maxValue = 10)
		{
			if (currValue <= 0)
			{
				currValue = 1;
			}
			else if (currValue > maxValue)
			{
				currValue = maxValue;
			}

			return currValue;
		}

		private void InitializeThreads()
		{
			var range = _numbers.Count / _numberOfThreads;

			for (int i = 0; i < NumberOfThreads - 1; i++)
			{
				_threads.Add(new Thread(() =>
				{
					CalculateNewValue(_numbers.GetRange(i * range, range));
				}));
			}
		}

		private async void MultiProcess()
		{
			var numOfTotalProcessors = Environment.ProcessorCount;

			NumberOfProcesses = CheckValue(NumberOfProcesses, numOfTotalProcessors);
			Content = "Running Multi Process";

			Stopwatch stopwatch = Stopwatch.StartNew();

			await Task.Run(() =>
			{
				Parallel.ForEach(_numbers, number =>
				{
					number *= 12342134;
					Console.WriteLine(number);
				});

				stopwatch.Stop();
			});

			Content = $"Time to Completion: {stopwatch.Elapsed} ";
		}

		private async void MultiThread()
		{
			NumberOfThreads = CheckValue(NumberOfThreads, 100);
			InitializeThreads();
			Content = "Running Multi Threading";

			Stopwatch stopwatch = Stopwatch.StartNew();

			await Task.Run(() =>
			{
				_threads.ForEach(thread => { thread.Start(); });
				_threads.ForEach(thread => { thread.Join(); });

				stopwatch.Stop();
			});

			Content = $"Time to Completion: {stopwatch.Elapsed} ";

			_threads.ForEach(thread => thread = null);
			_threads.Clear();
		}

		#endregion methods
	}
}