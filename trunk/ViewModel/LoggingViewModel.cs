//-----------------------------------------------------------------------
// <copyright file="LoggingViewModel.cs" company="SpectralCoding">
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
using System.ComponentModel;
using System.Linq;
using System.Text;
using BlackJack.Utilities;

namespace BlackJack.ViewModel {
	/// <summary>
	/// Provides logic for Logging.
	/// </summary>
	public class LoggingViewModel : ViewModelBase {
		private LoggingModel m_LoggingModel;
		private TableViewModel m_ParentMasterViewModel;

		#region Public Properties
		/// <summary>
		/// Gets or sets a value indicating the list of items in the log.
		/// </summary>
		private List<LogItem> LogItems {
			get {
				return m_LoggingModel.LogItems;
			}
			set {
				m_LoggingModel.LogItems = value;
				OnPropertyChanged("Log");
			}
		}
		/// <summary>
		/// Gets or sets the Log.
		/// </summary>
		public string Log {
			get {
				string returnStr = string.Empty;
				foreach (LogItem CurrentItem in LogItems) {
					returnStr += string.Format("[{0:HH:mm:ss.fff}] {1}\n", CurrentItem.Time, CurrentItem.Message);
				}
				return returnStr.TrimEnd(new char[] { '\n' });
			}
			set {
			}
		}
		#endregion

		/// <summary>
		/// Initializes a new instance of the LoggingViewModel class.
		/// </summary>
		/// <param name="Parent">Placeholder for the parent object.</param>
		public LoggingViewModel(TableViewModel Parent) {
			m_ParentMasterViewModel = Parent;
			m_LoggingModel = new LoggingModel();
		}

		/// <summary>
		/// Adds and item to the log.
		/// </summary>
		/// <param name="ActionType">Type of action which this log pretains to.</param>
		/// <param name="ParamObject">Objects containing information needed to generate a message.</param>
		public void AddItem(LogActionType ActionType, params object[] ParamObject) {
			string LogMessage = string.Empty;
			switch (ActionType) {
				case LogActionType.Information:
					if (ParamObject[0].GetType() == typeof(string)) {
						LogMessage = ParamObject[0].ToString();
					}
					break;
				case LogActionType.ShoeShuffle:
					if (ParamObject[0].GetType() == typeof(ShoeViewModel)) {
						ShoeViewModel tempShoe = (ShoeViewModel)ParamObject[0];
						LogMessage = string.Format("Shuffled {0} deck shoe (Mode: {1}).", m_ParentMasterViewModel.HouseRulesVM.DecksInShoe, m_ParentMasterViewModel.HouseRulesVM.ShuffleMode.ToString());
					}
					break;
				case LogActionType.Benchmark:
					if (ParamObject[0].GetType() == typeof(BenchmarkResults)) {
						BenchmarkResults tempResults = (BenchmarkResults)ParamObject[0];
						LogMessage = string.Format("Benchmark \"{0}\" Finished: {1}/sec", tempResults.Name, tempResults.IterationsPerSecond.ToString());
					}
					break;
			}
			LogItem tempLI = new LogItem();
			tempLI.Type = ActionType;
			tempLI.Message = LogMessage;
			tempLI.Time = DateTime.Now;
			LogItems.Add(tempLI);
			OnPropertyChanged("Log");
		}
	}
}
