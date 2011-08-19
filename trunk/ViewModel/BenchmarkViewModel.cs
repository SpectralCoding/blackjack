//-----------------------------------------------------------------------
// <copyright file="BenchmarkViewModel.cs" company="SpectralCoding">
//		Copyright (c) SpectralCoding. All rights reserved.
//		Repeatedly violating our Copyright (c) will bring the full
//		extent of the law, which may ultimately result in permanent
//		imprisonment at hard labor, and/or death by extreme slow
//		torture, and/or lethal experimental medical therapies.
// </copyright>
// <author>Caesar Kabalan</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BlackJack.CardLogic;
using BlackJack.HouseLogic;
using BlackJack.Utilities;
using BlackJack.ViewModel;

namespace BlackJack.ViewModel {
	/// <summary>
	/// Provides logic for the benchmarking class.
	/// </summary>
	public class BenchmarkViewModel : ViewModelBase {
		#region Private Fields
		private BenchmarkModel m_BenchmarkModel;
		private TableViewModel m_Parent;
		private string m_RunType;
		private string m_RunSubType;
		private int m_SecondsPerBenchmark;
		private Dictionary<BenchmarkType, BenchmarkResults> BenchmarkData {
			get {
				return m_BenchmarkModel.BenchmarkData;
			}
			set {
				m_BenchmarkModel.BenchmarkData = value;
				OnPropertyChanged("BenchmarkData");
				OnPropertyChanged("DataTable");
			}
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets a value indicating whether or not a benchmark may be started.
		/// </summary>
		public bool CanStartBenchmark {
			get {
				return m_BenchmarkModel.CanStartBenchmark;
			}
			private set {
				m_BenchmarkModel.CanStartBenchmark = value;
				OnPropertyChanged("CanStartBenchmark");
			}
		}
		/// <summary>
		/// Gets a DataTable with the current benchmark information.
		/// </summary>
		public DataTable DataTable {
			get {
				DataTable ReturnTable = new DataTable("BenchmarkData");
				DataRow RowToAdd;
				ReturnTable.Columns.Add(new DataColumn("Benchmark", typeof(string)));
				ReturnTable.Columns.Add(new DataColumn("Per Sec", typeof(double)));
				foreach (KeyValuePair<BenchmarkType, BenchmarkResults> KVPair in BenchmarkData) {
					RowToAdd = ReturnTable.NewRow();
					RowToAdd[0] = KVPair.Value.Name;
					RowToAdd[1] = KVPair.Value.IterationsPerSecond;
					ReturnTable.Rows.Add(RowToAdd);
				}
				return ReturnTable;
			}
		}
		/// <summary>
		/// Gets or sets a value indicating the type of Benchmark to run.
		/// </summary>
		public string RunType {
			get { return m_RunType; }
			set {
				m_RunType = value;
				OnPropertyChanged("RunType");
			}
		}
		/// <summary>
		/// Gets or sets a value indicating the Subtype of the Benchmark to run.
		/// </summary>
		public string RunSubType {
			get { return m_RunSubType; }
			set {
				m_RunSubType = value;
				OnPropertyChanged("RunSubType");
			}
		}
		/// <summary>
		/// Gets or sets a value indicating the number of seconds to run each Benchmark.
		/// </summary>
		public int SecondsPerBenchmark {
			get { return m_SecondsPerBenchmark; }
			set {
				m_SecondsPerBenchmark = value;
				OnPropertyChanged("SecondsPerBenchmark");
			}
		}
		#endregion

		/// <summary>
		/// Initializes a new instance of the BenchmarkViewModel class.
		/// </summary>
		/// <param name="Parent">Placeholder for the parent object.</param>
		public BenchmarkViewModel(TableViewModel Parent) {
			m_Parent = Parent;
			m_BenchmarkModel = new BenchmarkModel();
			SecondsPerBenchmark = 3;
			RunType = "Create Shoe";
			RunSubType = "Fisher-Yates Shfle";
		}

		/// <summary>
		/// Runs all benchmarks available for the specified time per benchmark.
		/// </summary>
		public void RunAllBenchmarks() {
			RunSingleBenchmark(BenchmarkType.CreateShoeFisherYates);
			RunSingleBenchmark(BenchmarkType.NoShuffle);
		}

		/// <summary>
		/// Runs a single benchmark.
		/// </summary>
		public void RunBenchmark() {
			RunSingleBenchmark(GetBenchmarkType());
		}

		/// <summary>
		/// Runs a specific benchmark for a specified number of seconds.
		/// </summary>
		/// <param name="Benchmark">Type of Benchmark to run.</param>
		private void RunSingleBenchmark(BenchmarkType Benchmark) {
			CanStartBenchmark = false;
			BenchmarkResults Results = new BenchmarkResults();
			DateTime StartTime, EndTime;
			TableViewModel tempMasterViewModel = new TableViewModel();
			#region Temp MasterViewModel Settings
			tempMasterViewModel.HouseRulesVM.DecksInShoe = 7;
			tempMasterViewModel.HouseRulesVM.ShoePenetration = 0.75;
			#endregion
			Results.Type = Benchmark;
			Results.Iterations = 0;
			StartTime = DateTime.Now;
			EndTime = StartTime.AddSeconds(SecondsPerBenchmark);
			switch (Benchmark) {
				case BenchmarkType.CreateShoeFisherYates:
					#region Create Shoe - Fisher-Yates Shuffle
					Results.Name = "Create Shoe - Fisher-Yates Shuffle";
					tempMasterViewModel.HouseRulesVM.ShuffleMode = ShuffleMode.FisherYates;
					while (DateTime.Now < EndTime) {
						ShoeViewModel Shoe = new ShoeViewModel(tempMasterViewModel, true);
						Results.Iterations++;
					}
					break;
					#endregion
				case BenchmarkType.NoShuffle:
					#region Create Shoe - No Shuffle
					Results.Name = "Create Shoe - No Shuffle";
					tempMasterViewModel.HouseRulesVM.ShuffleMode = ShuffleMode.NoShuffle;
					while (DateTime.Now < EndTime) {
						ShoeViewModel Shoe = new ShoeViewModel(tempMasterViewModel, true);
						Results.Iterations++;
					}
					break;
					#endregion
			}
			Results.RunTime = DateTime.Now.Subtract(StartTime).TotalMilliseconds / 1000;
			Results.IterationsPerSecond = Math.Round(Results.Iterations / Results.RunTime, 2);
			Results.RunTime = Math.Round(Results.RunTime, 2);
			BenchmarkData[Benchmark] = Results;
			m_Parent.LoggingVM.AddItem(LogActionType.Benchmark, Results);
			OnPropertyChanged("DataTable");
			tempMasterViewModel.Dispose();
			CanStartBenchmark = true;
		}

		/// <summary>
		/// Resolves a type and subtype text to a BenchmarkType enum.
		/// </summary>
		/// <returns>BenchmarkType object.</returns>
		private BenchmarkType GetBenchmarkType() {
			switch (m_RunType) {
				case "Create Shoe":
					switch (m_RunSubType) {
						case "Fisher-Yates Shfle":
							return BenchmarkType.CreateShoeFisherYates;
						case "No Shuffle":
							return BenchmarkType.NoShuffle;
					}
					break;
			}
			return BenchmarkType.None;
		}
	}
}
