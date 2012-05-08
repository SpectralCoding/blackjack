//-----------------------------------------------------------------------
// <copyright file="GameStatisticsViewModel.cs" company="SpectralCoding">
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
using System.Linq;
using System.Text;
using BlackJack.HouseLogic;
using BlackJack.Utilities;

namespace BlackJack.ViewModel {
	/// <summary>
	/// Provides logic for Game Statistics.
	/// </summary>
	public class GameStatisticsViewModel : ViewModelBase {
		private TableViewModel m_MasterParent;
		private GameStatisticsModel m_GameStatisticsModel;

		/// <summary>
		/// Gets or sets a value indicating the number of cards dealt.
		/// </summary>
		public long CardsDealt {
			get {
				return m_GameStatisticsModel.CardsDealt;
			}
			set {
				m_GameStatisticsModel.CardsDealt = value;
				if (!m_MasterParent.HouseRulesVM.FastMode) { OnPropertyChanged("CardsDealt"); }
			}
		}
		/// <summary>
		/// Gets or sets a value indicating the number of hands played.
		/// </summary>
		public long HandsPlayed {
			get {
				return m_GameStatisticsModel.HandsPlayed;
			}
			set {
				m_GameStatisticsModel.HandsPlayed = value;
				if (!m_MasterParent.HouseRulesVM.FastMode) { OnPropertyChanged("HandsPlayed"); }
			}
		}
		/// <summary>
		/// Gets or sets a value indicating the number of rounds played.
		/// </summary>
		public long RoundsPlayed {
			get {
				return m_GameStatisticsModel.RoundsPlayed;
			}
			set {
				m_GameStatisticsModel.RoundsPlayed = value;
				if (!m_MasterParent.HouseRulesVM.FastMode) { OnPropertyChanged("RoundsPlayed"); }
			}
		}
		/// <summary>
		/// Gets or sets a value indicating the number of shoes shuffled.
		/// </summary>
		public long ShoesShuffled {
			get {
				return m_GameStatisticsModel.ShoesShuffled;
			}
			set {
				m_GameStatisticsModel.ShoesShuffled = value;
				if (m_MasterParent.HouseRulesVM.FastMode) {
					if (m_GameStatisticsModel.ShoesShuffled % 10 == 0) {
						UpdateRates();
						OnPropertyChanged("ShoesShuffled");
					}
				} else {
					UpdateRates();
					OnPropertyChanged("ShoesShuffled");
				}
			}
		}
		/// <summary>
		/// Gets a value indicating the average number of cards dealt per second.
		/// </summary>
		public string CardsDealtPerSec {
			get {
				if (CardsDealt > 0) {
					return String.Format("({0:0}/sec)", (CardsDealt / DateTime.Now.Subtract(m_MasterParent.StartTime).TotalSeconds));
				} else {
					return "(0/sec)";
				}
			}
		}
		/// <summary>
		/// Gets a value indicating the average number of hands played per second.
		/// </summary>
		public string HandsPlayedPerSec {
			get {
				if (HandsPlayed > 0) {
					return String.Format("({0:0}/sec)", (HandsPlayed / DateTime.Now.Subtract(m_MasterParent.StartTime).TotalSeconds));
				} else {
					return "(0/sec)";
				}
			}
		}
		/// <summary>
		/// Gets a value indicating the average number of rounds played per second.
		/// </summary>
		public string RoundsPlayedPerSec {
			get {
				if (RoundsPlayed > 0) {
					return String.Format("({0:#.#}/sec)", (RoundsPlayed / DateTime.Now.Subtract(m_MasterParent.StartTime).TotalSeconds));
				} else {
					return "(0/sec)";
				}
			}
		}
		/// <summary>
		/// Gets a value indicating the average number of shoes shuffled per second.
		/// </summary>
		public string ShoesShuffledPerSec {
			get {
				if (ShoesShuffled > 1) {
					return String.Format("({0:#.#}/sec)", (ShoesShuffled / DateTime.Now.Subtract(m_MasterParent.StartTime).TotalSeconds));
				} else {
					return "(0/sec)";
				}
			}
		}
		/// <summary>
		/// Initializes a new instance of the GameStatisticsViewModel class.
		/// </summary>
		/// <param name="Parent">Placeholder for the parent object.</param>
		public GameStatisticsViewModel(TableViewModel Parent) {
			m_MasterParent = Parent;
			m_GameStatisticsModel = new GameStatisticsModel();
			Reset();
		}

		/// <summary>
		/// Resets all game wide statistics and adds a line to the log.
		/// </summary>
		public void Reset() {
			CardsDealt = 0;
			HandsPlayed = 0;
			RoundsPlayed = 0;
			ShoesShuffled = 0;
			m_MasterParent.LoggingVM.AddItem(LogActionType.Information, "Game Statistics Reset.");
		}

		/// <summary>
		/// Updates the per-second rates.
		/// </summary>
		public void UpdateRates() {
			if (!m_MasterParent.HouseRulesVM.FastMode) {
				OnPropertyChanged("CardsDealt");
				OnPropertyChanged("HandsPlayed");
				OnPropertyChanged("RoundsPlayed");
			}
			OnPropertyChanged("CardsDealtPerSec");
			OnPropertyChanged("HandsPlayedPerSec");
			OnPropertyChanged("RoundsPlayedPerSec");
			OnPropertyChanged("ShoesShuffledPerSec");
		}
	}
}
