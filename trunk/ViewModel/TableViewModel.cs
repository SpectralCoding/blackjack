﻿//-----------------------------------------------------------------------
// <copyright file="TableViewModel.cs" company="SpectralCoding">
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
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
		private long RoundCount = 0;
		#endregion

		#region Public ViewModel Properties
		public BenchmarkViewModel BenchmarkVM {
			get {
				return m_TableModel.BenchmarkViewModel;
			}
			set {
				m_TableModel.BenchmarkViewModel = value;
				if (!HouseRulesVM.FastMode) { OnPropertyChanged("BenchmarkVM"); }
			}
		}
		public ResourcesViewModel ResourcesVM {
			get {
				return m_TableModel.ResourcesViewModel;
			}
			set {
				m_TableModel.ResourcesViewModel = value;
				if (!HouseRulesVM.FastMode) { OnPropertyChanged("ResourcesViewModel"); }
			}
		}
		public DealerViewModel DealerVM {
			get {
				return m_TableModel.DealerViewModel;
			}
			set {
				m_TableModel.DealerViewModel = value;
				if (!HouseRulesVM.FastMode) { OnPropertyChanged("DealerVM"); }
			}
		}
		public GameStatisticsViewModel GameStatisticsVM {
			get {
				return m_TableModel.GameStatisticsViewModel;
			}
			set {
				m_TableModel.GameStatisticsViewModel = value;
				if (!HouseRulesVM.FastMode) { OnPropertyChanged("GameStatisticsVM"); }
			}
		}
		public HouseRulesViewModel HouseRulesVM {
			get {
				return m_TableModel.HouseRulesViewModel;
			}
			set {
				m_TableModel.HouseRulesViewModel = value;
				if (!HouseRulesVM.FastMode) { OnPropertyChanged("HouseRulesVM"); }
			}
		}
		public LoggingViewModel LoggingVM {
			get {
				return m_TableModel.LoggingViewModel;
			}
			set {
				m_TableModel.LoggingViewModel = value;
				if (!HouseRulesVM.FastMode) { OnPropertyChanged("LoggingVM"); }
			}
		}
		public ObservableCollection<PlayerViewModel> PlayerVM {
			get {
				return m_TableModel.PlayerViewModel;
			}
			set {
				m_TableModel.PlayerViewModel = value;
				if (!HouseRulesVM.FastMode) { OnPropertyChanged("PlayerVM"); }
			}
		}
		public ObservableCollection<PlayerStatisticsViewModel> PlayerStatisticsVM {
			get {
				return m_TableModel.PlayerStatisticsViewModel;
			}
			set {
				m_TableModel.PlayerStatisticsViewModel = value;
				if (!HouseRulesVM.FastMode) { OnPropertyChanged("PlayerStatisticsVM"); }
			}
		}
		public PlayerStrategyViewModel PlayerStrategyVM {
			get {
				return m_TableModel.PlayerStrategyViewModel;
			}
			set {
				m_TableModel.PlayerStrategyViewModel = value;
				if (!HouseRulesVM.FastMode) { OnPropertyChanged("PlayerStrategyVM"); }
			}
		}
		public ShoeViewModel ShoeVM {
			get {
				return m_TableModel.ShoeViewModel;
			}
			set {
				m_TableModel.ShoeViewModel = value;
				if (!HouseRulesVM.FastMode) { OnPropertyChanged("ShoeVM"); }
			}
		}
		public PlayerViewModel CurrentPlayerVM {
			get {
				return m_TableModel.CurrentPlayerVM;
			}
			set {
				m_TableModel.CurrentPlayerVM = value;
				if (!HouseRulesVM.FastMode) { OnPropertyChanged("CurrentPlayerVM"); }
			}
		}
		public PlayerHandViewModel CurrentPlayerHandVM {
			get {
				return m_TableModel.CurrentPlayerHandVM;
			}
			set {
				m_TableModel.CurrentPlayerHandVM = value;
				if (!HouseRulesVM.FastMode) { OnPropertyChanged("CurrentPlayerHandVM"); }
			}
		}
		/// <summary>
		/// Gets or sets a value indicating whether or not we can deal the cards.
		/// </summary>
		public bool CanDealCards {
			get {
				return m_TableModel.CanDealCards;
			}
			set {
				m_TableModel.CanDealCards = value;
				if (!HouseRulesVM.FastMode) { OnPropertyChanged("CanDealCards"); }
			}
		}
		/// <summary>
		/// Gets or sets a value indicating whether or not we can start a game.
		/// </summary>
		public bool CanStartGame {
			get {
				return m_TableModel.CanStartGame;
			}
			set {
				m_TableModel.CanStartGame = value;
				if (!HouseRulesVM.FastMode) { OnPropertyChanged("CanStartGame"); }
			}
		}
		/// <summary>
		/// Gets or sets a value indicating whether or not a game is currently in progress.
		/// </summary>
		public bool GameInProgress {
			get {
				return m_TableModel.GameInProgress;
			}
			set {
				m_TableModel.GameInProgress = value;
				if (!HouseRulesVM.FastMode) { OnPropertyChanged("GameInProgress"); }
			}
		}
		/// <summary>
		/// Gets or sets a value indicating when the current game was started.
		/// </summary>
		public DateTime StartTime {
			get {
				return m_TableModel.StartTime;
			}
			set {
				m_TableModel.StartTime = value;
				if (!HouseRulesVM.FastMode) { OnPropertyChanged("StartTime"); }
			}
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Advances play in whatever way play should be advances. This is basically the "dealer function" which directs automated play.
		/// </summary>
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

		/// <summary>
		/// Advances play to the next player (or dealer).
		/// </summary>
		/// <param name="CurrentPlayer">The player currently active.</param>
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
				DealerVM.IsActive = true;
			} else {
				// This was the last hand of the round.
				CurrentPlayerVM = new PlayerViewModel(this, -1);
				CurrentPlayerHandVM = CurrentPlayerVM.PlayerHandVM[0];
				DealerVM.IsActive = true;
			}
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Initializes a new instance of the TableViewModel class.
		/// </summary>
		public TableViewModel() {
			#region Member Instantiation
			m_TableModel = new TableModel();
			m_TableModel.ResourcesViewModel = new ResourcesViewModel(this);
			m_TableModel.BenchmarkViewModel = new BenchmarkViewModel(this);
			m_TableModel.DealerViewModel = new DealerViewModel(this);
			m_TableModel.HouseRulesViewModel = new HouseRulesViewModel(this);
			m_TableModel.LoggingViewModel = new LoggingViewModel(this);
			m_TableModel.GameStatisticsViewModel = new GameStatisticsViewModel(this);
			m_TableModel.PlayerViewModel = new ObservableCollection<PlayerViewModel>();
			m_TableModel.PlayerStatisticsViewModel = new ObservableCollection<PlayerStatisticsViewModel>();
			for (int i = 0; i < 7; i++) {
				m_TableModel.PlayerViewModel.Add(new PlayerViewModel(this, (i + 1)));
				m_TableModel.PlayerStatisticsViewModel.Add(new PlayerStatisticsViewModel(this, (i + 1)));
			}
			m_TableModel.ShoeViewModel = new ShoeViewModel(this);
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
			StartTime = DateTime.Now;
			ClearTable();
			GameStatisticsVM.Reset();
			ShoeVM.ResetShoe();
			CanStartGame = true;
			GameInProgress = true;
			LoggingVM.AddItem(LogActionType.Information, "Game Started.");
		}

		/// <summary>
		/// Clears all the player's hands.
		/// </summary>
		public void ClearTable() {
			for (int i = 0; i < 7; i++) {
				PlayerVM[i].ResetHands();
			}
			DealerVM.Reset();
			LoggingVM.AddItem(LogActionType.Information, "Table Cleared.");
			CanDealCards = true;
		}
	
		/// <summary>
		/// Deals cards to all players.
		/// </summary>
		public void DealCards() {
			for (int player = 0; player < 7; player++) {
				PlayerVM[player].PlayerHandVM[0].RecieveCard(ShoeVM.DrawCard());
			}
			DealerVM.DealerHandVM.RecieveCard(ShoeVM.DrawCard(), false);
			for (int player = 0; player < 7; player++) {
				PlayerVM[player].PlayerHandVM[0].RecieveCard(ShoeVM.DrawCard());
			}
			DealerVM.DealerHandVM.RecieveCard(ShoeVM.DrawCard(), true);
			CanDealCards = false;
			PlayerVM[0].IsActive = true;
			CurrentPlayerVM = PlayerVM[0];
			PlayerVM[0].PlayerHandVM[0].IsActive = true;
			CurrentPlayerHandVM = PlayerVM[0].PlayerHandVM[0];
		}

		/// <summary>
		/// Deals a card to the next player.
		/// </summary>
		public void DealACard() {
			if (CanDealCards) {
				for (int player = 0; player < 7; player++) {
					if (PlayerVM[player].PlayerMode != PlayerMode.NotPlaying) {
						for (int hand = 0; hand < 4; hand++) {
							if (PlayerVM[player].PlayerHandVM[hand].HandMode != HandMode.NotPlaying) {
								if (PlayerVM[player].PlayerHandVM[hand].Hand.Count == 0) {
									PlayerVM[player].PlayerHandVM[hand].RecieveCard(ShoeVM.DrawCard());
									return;
								}
							}
						}
					}
				}
				if (DealerVM.DealerHandVM.Hand.Count == 0) {
					DealerVM.DealerHandVM.RecieveCard(ShoeVM.DrawCard(), false);
					return;
				}
				for (int player = 0; player < 7; player++) {
					if (PlayerVM[player].PlayerMode != PlayerMode.NotPlaying) {
						for (int hand = 0; hand < 4; hand++) {
							if (PlayerVM[player].PlayerHandVM[hand].HandMode != HandMode.NotPlaying) {
								if (PlayerVM[player].PlayerHandVM[hand].Hand.Count == 1) {
									PlayerVM[player].PlayerHandVM[hand].RecieveCard(ShoeVM.DrawCard());
									return;
								}
							}
						}
					}
				}
				if (DealerVM.DealerHandVM.Hand.Count == 1) {
					DealerVM.DealerHandVM.RecieveCard(ShoeVM.DrawCard(), true);
					return;
				}
				CanDealCards = false;
				PlayerVM[0].IsActive = true;
				CurrentPlayerVM = PlayerVM[0];
				PlayerVM[0].PlayerHandVM[0].IsActive = true;
				CurrentPlayerHandVM = PlayerVM[0].PlayerHandVM[0];
				return;
			}
		}

		/// <summary>
		/// Determines what the next step of play should be and executes it.
		/// </summary>
		public void NextStep() {
			if (!GameInProgress) {
				StartGame();
				return;
			} else {
				if (CanDealCards) {
					DealACard();
					return;
				} else {
					for (int player = 0; player < 7; player++) {
						if (PlayerVM[player].IsActive) {
							PlayerPlayStrategy();
							return;
						}
					}
					if (DealerVM.IsActive) {
						DealerPlay();
						return;
					} else {
						int handsLeft;
						int tempHand;
						for (int player = 0; player < 7; player++) {
							for (int hand = 0; hand < 3; hand++) {
								if (PlayerVM[player].PlayerHandVM[hand].Hand.Count > 0) {
									PlayerVM[player].PlayerHandVM[hand].ResetHand();
									handsLeft = 3;
									for (tempHand = 0; tempHand < 3; tempHand++) {
										if (PlayerVM[player].PlayerHandVM[tempHand].Hand.Count == 0) {
											handsLeft--;
										}
									}
									if (handsLeft == 0) {
										PlayerVM[player].ResetHands();
									}
									return;
								}
							}
						}
						DealerVM.Reset();
						CanDealCards = true;
						RoundCount++;
						return;
					}
				}
			}
		}

		/// <summary>
		/// The dealer plays his hand.
		/// </summary>
		public void DealerPlay() {
			DealerVM.Play();
			DealerVM.IsActive = false;
			if (ShoeVM.NeedToShuffle) {
				ShoeVM.ResetShoe();
			}
		}
		#endregion

		#region Player Controls
		/// <summary>
		/// Player stands so play moves on.
		/// </summary>
		public void PlayerStand() {
			AdvancePlay();
		}

		/// <summary>
		/// Player hits and receives a card. If they bust, have 21, or blackjack then advance play.
		/// </summary>
		public void PlayerHit() {
			if (CurrentPlayerHandVM.CanAcceptCard) {
				CurrentPlayerHandVM.Hit(ShoeVM.DrawCard());
				if ((CurrentPlayerHandVM.Count >= 21) || (CurrentPlayerHandVM.IsBlackJack)) {
					AdvancePlay();
				}
			} else {
				MessageBox.Show("You cannot hit.");
			}
		}

		/// <summary>
		/// Player doubles down and draws a card.
		/// </summary>
		public void PlayerDoubleDown() {
			// Don't forget to increase bet!
			if (CurrentPlayerHandVM.CanDoubleDown) {
				CurrentPlayerHandVM.DoubleDown(ShoeVM.DrawCard());
				AdvancePlay();
			}
		}

		/// <summary>
		/// Player doubles down for less and draws a card.
		/// </summary>
		public void PlayerDoubleForLess() {
			// Don't forget to ask player how much they want to bet
			if (CurrentPlayerHandVM.CanDoubleDown) {
				CurrentPlayerHandVM.DoubleDown(ShoeVM.DrawCard());
				AdvancePlay();
			}
		}

		/// <summary>
		/// The players hand is split into two hands.
		/// </summary>
		public void PlayerSplit() {
			// Don't forget to check if they have the cash to split.
			for (int i = 0; i < 4; i++) {
				if (CurrentPlayerVM.IsActive) {
					CurrentPlayerVM.Split(i);
					return;
				}
			}
		}

		/// <summary>
		/// The player surrenders and recieves half his bet back.
		/// </summary>
		public void PlayerSurrender() {
			// Give the Player half his bet back
			AdvancePlay();
		}

		/// <summary>
		/// The player gets his suggested action and executes it.
		/// </summary>
		public void PlayerPlayStrategy() {
			switch (CurrentPlayerHandVM.GetSuggestedAction()) {
				case PlayerAction.Stand:
					PlayerStand();
					break;
				case PlayerAction.Surrender:
					PlayerSurrender();
					break;
				case PlayerAction.Hit:
					PlayerHit();
					break;
				case PlayerAction.DoubleDown:
					PlayerDoubleDown();
					break;
				case PlayerAction.DoubleForLess:
					PlayerDoubleForLess();
					break;
				case PlayerAction.Split:
					PlayerSplit();
					break;
			}
		}
		#endregion
		#endregion
	}
}
