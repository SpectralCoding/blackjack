//-----------------------------------------------------------------------
// <copyright file="MasterViewModel.cs" company="SpectralCoding">
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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using BlackJack.CardLogic;
using BlackJack.TableLogic;
using BlackJack.Utilities;

namespace BlackJack.ViewModel {
	/// <summary>
	/// Provides logic for the entire application.
	/// </summary>
	public class TableViewModel : ViewModelBase {
		#region Private ViewModel Fields
		private TableModel m_TableModel;
		private BenchmarkViewModel m_BenchmarkViewModel;
		private DealerViewModel m_DealerViewModel;
		private GameStatisticsViewModel m_GameStatisticsViewModel;
		private HouseRulesViewModel m_HouseRulesViewModel;
		private LoggingViewModel m_LoggingViewModel;
		private ObservableCollection<PlayerViewModel> m_PlayerViewModel;
		private ObservableCollection<PlayerStatisticsViewModel> m_PlayerStatisticsViewModel;
		private PlayerStrategyViewModel m_PlayerStrategyViewModel;
		private ShoeViewModel m_ShoeViewModel;
		#endregion

		#region Public ViewModel Properties
		public BenchmarkViewModel BenchmarkVM {
			get {
				return m_BenchmarkViewModel;
			}
			set {
				m_BenchmarkViewModel = value;
				OnPropertyChanged("BenchmarkVM");
			}
		}
		public DealerViewModel DealerVM {
			get {
				return m_DealerViewModel;
			}
			set {
				m_DealerViewModel = value;
				OnPropertyChanged("DealerVM");
			}
		}
		public GameStatisticsViewModel GameStatisticsVM {
			get {
				return m_GameStatisticsViewModel;
			}
			set {
				m_GameStatisticsViewModel = value;
				OnPropertyChanged("GameStatisticsVM");
			}
		}
		public HouseRulesViewModel HouseRulesVM {
			get {
				return m_HouseRulesViewModel;
			}
			set {
				m_HouseRulesViewModel = value;
				OnPropertyChanged("HouseRulesVM");
			}
		}
		public LoggingViewModel LoggingVM {
			get {
				return m_LoggingViewModel;
			}
			set {
				m_LoggingViewModel = value;
				OnPropertyChanged("LoggingVM");
			}
		}
		public ObservableCollection<PlayerViewModel> PlayerVM {
			get {
				return m_PlayerViewModel;
			}
			set {
				m_PlayerViewModel = value;
				OnPropertyChanged("PlayerVM");
			}
		}
		public ObservableCollection<PlayerStatisticsViewModel> PlayerStatisticsVM {
			get {
				return m_PlayerStatisticsViewModel;
			}
			set {
				m_PlayerStatisticsViewModel = value;
				OnPropertyChanged("PlayerStatisticsVM");
			}
		}
		public PlayerStrategyViewModel PlayerStrategyVM {
			get {
				return m_PlayerStrategyViewModel;
			}
			set {
				m_PlayerStrategyViewModel = value;
				OnPropertyChanged("PlayerStrategyVM");
			}
		}
		public ShoeViewModel ShoeVM {
			get {
				return m_ShoeViewModel;
			}
			set {
				m_ShoeViewModel = value;
				OnPropertyChanged("ShoeVM");
			}
		}
		#endregion

		/// <summary>
		/// Initializes a new instance of the MasterViewModel class.
		/// </summary>
		public TableViewModel() {
			#region Member Instantiation
			m_BenchmarkViewModel = new BenchmarkViewModel(this);
			m_DealerViewModel = new DealerViewModel(this);
			m_GameStatisticsViewModel = new GameStatisticsViewModel(this);
			m_HouseRulesViewModel = new HouseRulesViewModel(this);
			m_LoggingViewModel = new LoggingViewModel(this);
			m_PlayerViewModel = new ObservableCollection<PlayerViewModel>();
			m_PlayerStatisticsViewModel = new ObservableCollection<PlayerStatisticsViewModel>();
			for (int i = 0; i < 7; i++) {
				m_PlayerViewModel.Add(new PlayerViewModel(this, (i + 1)));
				m_PlayerStatisticsViewModel.Add(new PlayerStatisticsViewModel(this, (i + 1)));
			}
			m_PlayerStrategyViewModel = new PlayerStrategyViewModel(this);
			m_ShoeViewModel = new ShoeViewModel(this);
			#endregion
		}

		/// <summary>
		/// Starts a game.
		/// </summary>
		public void StartGame() {
			ClearTable();
			
		}

		/// <summary>
		/// Deals cards to all players.
		/// </summary>
		public void DealCards() {
			for (int player = 0; player < 7; player++) {
				for (int hand = 0; hand < 4; hand++) {
					if (PlayerVM[player].PlayerHandVM[hand].WantsCard()) {
						PlayerVM[player].PlayerHandVM[hand].RecieveCard(ShoeVM.DrawCard());
					}
				}
			}
		}

		/// <summary>
		/// Clears all the player's hands.
		/// </summary>
		public void ClearTable() {
			for (int i = 0; i < 7; i++) {
				PlayerVM[i].ResetHands();
			}
		}
	}
}
