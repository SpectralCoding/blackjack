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
using System.Threading;
using System.Threading.Tasks;
using BlackJack.CardLogic;
using BlackJack.TableLogic;
using BlackJack.Utilities;
using System.Windows;

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

		public PlayerViewModel CurrentPlayerVM {
			get {
				return m_TableModel.CurrentPlayerVM;
			}
			set {
				m_TableModel.CurrentPlayerVM = value;
				OnPropertyChanged("CurrentPlayerVM");
			}
		}
		public PlayerHandViewModel CurrentPlayerHandVM {
			get {
				return m_TableModel.CurrentPlayerHandVM;
			}
			set {
				m_TableModel.CurrentPlayerHandVM = value;
				OnPropertyChanged("CurrentPlayerHandVM");
			}
		}
		public bool CanDealCards {
			get {
				return m_TableModel.CanDealCards;
			}
			set {
				m_TableModel.CanDealCards = value;
				OnPropertyChanged("CanDealCards");
			}
		}
		public bool CanStartGame {
			get {
				return m_TableModel.CanStartGame;
			}
			set {
				m_TableModel.CanStartGame = value;
				OnPropertyChanged("CanStartGame");
			}
		}
		#endregion

		#region Private Methods
		private void AdvancePlay() {
			for (int player = 0; player < PlayerVM.Count; player++) {
				// For each player check if if they are playing
				if (PlayerVM[player].PlayerMode != PlayerMode.NotPlaying) {
					// If they are playing, check if they are active
					if (PlayerVM[player].IsActive) {
						// If they are active
						for (int hand = 0; hand < PlayerVM[player].PlayerHandVM.Length; hand++) {
							// For each hand that the current player has check if it's playing
							if (PlayerVM[player].PlayerHandVM[hand].HandMode != HandMode.NotPlaying) {
								// If they are playing, check if the hand is active
								if (PlayerVM[player].PlayerHandVM[hand].IsActive) {
									// If it is
									if (hand < 3) {
										// And we arent on the last possible hand
										if (PlayerVM[player].PlayerHandVM[hand + 1].HandMode != HandMode.NotPlaying) {
											// Check if the next hand is possible
											// If it is then advance to it
											PlayerVM[player].PlayerHandVM[hand].IsActive = false;
											PlayerVM[player].PlayerHandVM[hand + 1].IsActive = true;
											CurrentPlayerHandVM = PlayerVM[player].PlayerHandVM[hand + 1];
											return;
										} else {
											// Otherwise this is the last hand for the player so advance to the next player
											PlayerVM[player].IsActive = false;
											PlayerVM[player].PlayerHandVM[hand].IsActive = false;
											AdvanceToNextPlayer(player);
											return;
										}
									} else {
										// This is the last hand for the player so advance to the next player.
										PlayerVM[player].IsActive = false;
										PlayerVM[player].PlayerHandVM[hand].IsActive = false;
										AdvanceToNextPlayer(player);
										return;
									}
								}
							}
						}
					}
				}
			}
			System.Windows.MessageBox.Show("No Player is currently active.");
		}
		private void AdvanceToNextPlayer(int CurrentPlayer) {
			if (CurrentPlayer < 6) {
				for (int tempplayer = CurrentPlayer + 1; tempplayer < PlayerVM.Count; tempplayer++) {
					if (PlayerVM[tempplayer].PlayerMode != PlayerMode.NotPlaying) {
						// This is the next player that is playing
						// Set the new hand as the current hand.
						PlayerVM[tempplayer].IsActive = true;
						CurrentPlayerVM = PlayerVM[tempplayer];
						PlayerVM[tempplayer].PlayerHandVM[0].IsActive = true;
						CurrentPlayerHandVM = PlayerVM[tempplayer].PlayerHandVM[0];
						return;
					}
				}
				// This was the last hand of the round.
				CurrentPlayerVM = new PlayerViewModel(this, -1);
				CurrentPlayerHandVM = CurrentPlayerVM.PlayerHandVM[0];
				System.Windows.MessageBox.Show("Round over. Start a new game.");
			} else {
				// This was the last hand of the round.
				CurrentPlayerVM = new PlayerViewModel(this, -1);
				CurrentPlayerHandVM = CurrentPlayerVM.PlayerHandVM[0];
				System.Windows.MessageBox.Show("Round over. Start a new game.");
			}
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Initializes a new instance of the MasterViewModel class.
		/// </summary>
		public TableViewModel() {
			#region Member Instantiation
			m_TableModel = new TableModel();
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
			CurrentPlayerVM = new PlayerViewModel(this, -1);
			CurrentPlayerHandVM = CurrentPlayerVM.PlayerHandVM[0];
			CanDealCards = false;
			CanStartGame = true;
		}

		#region Dealer Controls
		/// <summary>
		/// Starts a game.
		/// </summary>
		public void StartGame() {
			// This should reset everything about the game:
			//		Re-Shuffle the Shoe
			//		Min/Max Bets
			//		Everyone's Cash
			//		etc...
			CanStartGame = false;
			ShoeVM.ResetShoe();
			ClearTable();
			CanStartGame = true;
		}

		/// <summary>
		/// Clears all the player's hands.
		/// </summary>
		public void ClearTable() {
			for (int i = 0; i < 7; i++) {
				PlayerVM[i].ResetHands();
			}
			CanDealCards = true;
		}
	
		/// <summary>
		/// Deals cards to all players.
		/// </summary>
		public void DealCards() {
			for (int player = 0; player < 7; player++) {
				PlayerVM[player].PlayerHandVM[0].RecieveCard(ShoeVM.DrawCard());
				if (HouseRulesVM.CasinoMode) { Thread.Sleep(500); }
			}
			ShoeVM.DrawCard();		// Use this as the dealer's holecard
			for (int player = 0; player < 7; player++) {
				PlayerVM[player].PlayerHandVM[0].RecieveCard(ShoeVM.DrawCard());
				if (HouseRulesVM.CasinoMode) { Thread.Sleep(500); }
			}
			ShoeVM.DrawCard();		// Use this as the dealer's upcard
			CanDealCards = false;
			PlayerVM[0].IsActive = true;
			CurrentPlayerVM = PlayerVM[0];
			PlayerVM[0].PlayerHandVM[0].IsActive = true;
			CurrentPlayerHandVM = PlayerVM[0].PlayerHandVM[0];
		}
		#endregion

		#region Player Controls
		public void PlayerStand() {
			AdvancePlay();
		}

		public void PlayerHit() {
			if (CurrentPlayerHandVM.CanAcceptCard) {
				CurrentPlayerHandVM.Hit(ShoeVM.DrawCard());
			} else {
				MessageBox.Show("You cannot hit.");
			}
		}

		public void PlayerDoubleDown() {
			// Don't forget to increase bet!
			if (CurrentPlayerHandVM.CanDoubleDown) {
				CurrentPlayerHandVM.DoubleDown(ShoeVM.DrawCard());
				AdvancePlay();
			}
		}

		public void PlayerDoubleForLess() {
			// Don't forget to ask player how much they want to bet
			if (CurrentPlayerHandVM.CanDoubleDown) {
				CurrentPlayerHandVM.DoubleDown(ShoeVM.DrawCard());
				AdvancePlay();
			}
		}

		public void PlayerSplit() {
			// Don't forget to check if they have the cash to split.
			for (int i = 0; i < 4; i++) {
				if (CurrentPlayerVM.IsActive) {
					CurrentPlayerVM.Split(i);
					return;
				}
			}
		}

		public void PlayerSurrender() {
			// Give the Player half his bet back
			AdvancePlay();
		}

		public void PlayerPlayStrategy() {

		}
		#endregion
		#endregion
	}
}
