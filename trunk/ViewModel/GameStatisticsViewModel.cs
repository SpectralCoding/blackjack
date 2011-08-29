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

namespace BlackJack.ViewModel {
	/// <summary>
	/// Provides logic for Game Statistics.
	/// </summary>
	public class GameStatisticsViewModel : ViewModelBase {
		private TableViewModel m_Parent;
		private GameStatisticsModel m_GameStatisticsModel;

		public long CardsDealt {
			get {
				return m_GameStatisticsModel.CardsDealt;
			}
			set {
				m_GameStatisticsModel.CardsDealt = value;
				if (!m_Parent.HouseRulesVM.FastMode) {
					OnPropertyChanged("CardsDealt");
				}
			}
		}
		public long HandsPlayed {
			get {
				return m_GameStatisticsModel.HandsPlayed;
			}
			set {
				m_GameStatisticsModel.HandsPlayed = value;
				if (!m_Parent.HouseRulesVM.FastMode) {
					OnPropertyChanged("HandsPlayed");
				}
			}
		}
		public long RoundsPlayed {
			get {
				return m_GameStatisticsModel.RoundsPlayed;
			}
			set {
				m_GameStatisticsModel.RoundsPlayed = value;
				if (!m_Parent.HouseRulesVM.FastMode) {
					OnPropertyChanged("RoundsPlayed");
				}
			}
		}
		public long ShoesShuffled {
			get {
				return m_GameStatisticsModel.ShoesShuffled;
			}
			set {
				m_GameStatisticsModel.ShoesShuffled = value;
				UpdateRates();
				OnPropertyChanged("ShoesShuffled");
			}
		}
		public string CardsDealtPerSec {
			get {
				if (CardsDealt > 0) {
					return String.Format("({0:0}/sec)", (CardsDealt / DateTime.Now.Subtract(m_Parent.StartTime).TotalSeconds));
				} else {
					return "(0/sec)";
				}
			}
		}
		public string HandsPlayedPerSec {
			get {
				if (HandsPlayed > 0) {
					return String.Format("({0:0}/sec)", (HandsPlayed / DateTime.Now.Subtract(m_Parent.StartTime).TotalSeconds));
				} else {
					return "(0/sec)";
				}
			}
		}
		public string RoundsPlayedPerSec {
			get {
				if (RoundsPlayed > 0) {
					return String.Format("({0:#.#}/sec)", (RoundsPlayed / DateTime.Now.Subtract(m_Parent.StartTime).TotalSeconds));
				} else {
					return "(0/sec)";
				}
			}
		}
		public string ShoesShuffledPerSec {
			get {
				if (ShoesShuffled > 1) {
					return String.Format("({0:#.#}/sec)", (ShoesShuffled / DateTime.Now.Subtract(m_Parent.StartTime).TotalSeconds));
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
			m_Parent = Parent;
			m_GameStatisticsModel = new GameStatisticsModel();
			Reset();
		}

		public void Reset() {
			CardsDealt = 0;
			HandsPlayed = 0;
			RoundsPlayed = 0;
			ShoesShuffled = 0;
		}

		public void UpdateRates() {
			if (m_Parent.HouseRulesVM.FastMode) {
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
