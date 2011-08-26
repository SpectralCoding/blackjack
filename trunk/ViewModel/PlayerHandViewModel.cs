//-----------------------------------------------------------------------
// <copyright file="PlayerHandViewModel.cs" company="SpectralCoding">
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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using BlackJack.CardLogic;
using BlackJack.PlayerLogic;
using BlackJack.Utilities;

namespace BlackJack.ViewModel {
	/// <summary>
	/// Provides logic for the Player Hand.
	/// </summary>
	public class PlayerHandViewModel : ViewModelBase {
		#region Private Fields
		private TableViewModel m_ParentMasterViewModel;
		private PlayerViewModel m_ParentPlayerViewModel;
		private PlayerHandModel m_PlayerHandModel;
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets a value indicating the contents of the hand.
		/// </summary>
		public ObservableCollection<CardInHand> Hand {
			get {
				return m_PlayerHandModel.Hand;
			}
		}
		/// <summary>
		/// Gets or sets a value indicating the current bet of the hand.
		/// </summary>
		public decimal Bet {
			get {
				return m_PlayerHandModel.Bet;
			}
			set {
				m_PlayerHandModel.Bet = value;
				OnPropertyChanged("Bet");
			}
		}
		/// <summary>
		/// Gets or sets a value indicating the current count of the hand.
		/// </summary>
		public int Count {
			get {
				return m_PlayerHandModel.Count;
			}
			set {
				m_PlayerHandModel.Count = value;
				OnPropertyChanged("Count");
				OnPropertyChanged("CanAcceptCard");
				OnPropertyChanged("CanSplit");
			}
		}
		public string StringCount {
			get {
				return m_PlayerHandModel.StringCount;
			}
			set {
				m_PlayerHandModel.StringCount = value;
				OnPropertyChanged("StringCount");
			}
		}
		/// <summary>
		/// Gets or sets a value indicating the current mode that the hand is in.
		/// </summary>
		public HandMode HandMode {
			get {
				return m_PlayerHandModel.HandMode;
			}
			set {
				m_PlayerHandModel.HandMode = value;
				OnPropertyChanged("HandMode");
				OnPropertyChanged("Visibility");
				OnPropertyChanged("Scale");
				OnPropertyChanged("CanAcceptCard");
			}
		}
		/// <summary>
		/// Gets a value indicating the visibility of the hand.
		/// </summary>
		public Visibility Visibility {
			get {
				if ((m_ParentPlayerViewModel.PlayerMode == PlayerMode.NotPlaying) || (HandMode == HandMode.NotPlaying)) {
					return Visibility.Hidden;
				} else {
					return Visibility.Visible;
				}
			}
		}
		/// <summary>
		/// Gets a value indicating the scale of t he hand.
		/// </summary>
		public double Scale {
			get {
				if ((m_ParentPlayerViewModel.PlayerMode == PlayerMode.Normal) || (m_ParentPlayerViewModel.PlayerMode == PlayerMode.DoubleDown)) {
					return 1.0;
				} else {
					return 0.5;
				}
			}
		}
		public bool IsActive {
			get {
				return m_PlayerHandModel.IsActive;
			}
			set {
				m_PlayerHandModel.IsActive = value;
				OnPropertyChanged("IsActive");
				OnPropertyChanged("CanAcceptCard");
				OnPropertyChanged("CanDoubleDown");
				OnPropertyChanged("CanDoubleDownForLess");
				OnPropertyChanged("CanSplit");
				OnPropertyChanged("CanSurrender");
			}
		}
		/// <summary>
		/// Determines whether or not the player can legally accept a card.
		/// </summary>
		/// <returns>A boolean indicated whether or not the player can legally accept a card.</returns>
		public bool CanAcceptCard {
			get {
				if (IsActive) {
					if (Count < 21) {
						if (HandMode != HandMode.NotPlaying) {
							if (HandMode == HandMode.Normal) {
								// Normal hand with less than 21
								return true;
							} else {
								// Hand is a double down with less than 21
								if (Hand.Count <= 2) {
									// If less than or two cards
									return true;
								} else {
									// If three cards
									return false;
								}
							}
						} else {
							// Is NotPlaying
							return false;
						}
					} else {
						// Has 21 or More
						return false;
					}
				} else {
					return false;
				}
			}
		}
		public bool CanDoubleDown {
			get {
				// Will need to check if they have enough money.
				return CanDoubleDownForLess;
			}
		}
		public bool CanDoubleDownForLess {
			get {
				// Will need to check if they have some amount of money.
				if (IsActive) {
					if (CanAcceptCard) {
						if (Hand.Count == 2) {
							return true;
						}
					}
				}
				return false;
			}
		}
		public bool CanSplit {
			get {
				if (Hand.Count == 2) {
					int[] cardtotals = new int[10];
					foreach (CardInHand currentCIH in Hand) {
						switch (currentCIH.Card.Type) {
							case CardType.Ace: cardtotals[0]++; break;
							case CardType.Two: cardtotals[1]++; break;
							case CardType.Three: cardtotals[2]++; break;
							case CardType.Four: cardtotals[3]++; break;
							case CardType.Five: cardtotals[4]++; break;
							case CardType.Six: cardtotals[5]++; break;
							case CardType.Seven: cardtotals[6]++; break;
							case CardType.Eight: cardtotals[7]++; break;
							case CardType.Nine: cardtotals[8]++; break;
							case CardType.Ten: cardtotals[9]++; break;
							case CardType.Jack: cardtotals[9]++; break;
							case CardType.Queen: cardtotals[9]++; break;
							case CardType.King: cardtotals[9]++; break;
						}
					}
					for (int i = 0; i < 10; i++) {
						if (cardtotals[i] == 2) { return m_ParentPlayerViewModel.CanSplit(i); }
					}
					return false;
				} else {
					return false;
				}
			}
		}
		public bool CanSurrender {
			get {
				return m_PlayerHandModel.CanSurrender;
			}
			set {
				m_PlayerHandModel.CanSurrender = value;
				OnPropertyChanged("CanSurrender");
			}
		}
		public bool IsBlackJack {
			get {
				return m_PlayerHandModel.IsBlackJack;
			}
			set {
				m_PlayerHandModel.IsBlackJack = value;
				OnPropertyChanged("IsBlackJack");
			}
		}
		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the PlayerHandViewModel class.
		/// </summary>
		/// <param name="ParentTVM">Placeholder for the parent MasterViewModel.</param>
		/// <param name="ParentPVM">Placeholder for the parent PlayerViewModel.</param>
		public PlayerHandViewModel(TableViewModel ParentTVM, PlayerViewModel ParentPVM) {
			m_ParentMasterViewModel = ParentTVM;
			m_ParentPlayerViewModel = ParentPVM;
			m_PlayerHandModel = new PlayerHandModel();
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Sets the card positions for all cards in all hands.
		/// </summary>
		private void SetCardPositions() {
			for (int i = 0; i < Hand.Count; i++) {
				Hand[i].SetPosition(i);
			}
		}

		/// <summary>
		/// Calculates the count in a particulair hand.
		/// </summary>
		private void CalculateCount() {
			bool hasAce = false;
			int tempCount;
			// Set all Aces as low.
			foreach (CardInHand CurrentCIH in Hand) {
				if (CurrentCIH.Card.Type == CardType.Ace) {
					CurrentCIH.Card.IsHigh = false;
					hasAce = true;
				}
			}
			string tempStrCount = String.Empty;
			do {
				// Add up all the cards in the hand.
				tempCount = 0;
				foreach (CardInHand CurrentCIH in Hand) {
					tempCount += CurrentCIH.Card.PlayValue;
				}
				if (tempStrCount == String.Empty) {
					tempStrCount = tempCount.ToString();
				}
			} while (MakeAceHighIfPossible(tempCount));
			if ((hasAce) && (Hand.Count == 2) && (tempCount == 21)) {
				// If it's BlackJack
				if (m_ParentPlayerViewModel.PlayerMode == PlayerMode.Normal) {
					// It's BlackJack
					tempStrCount = "BlackJack";
					IsBlackJack = true;
				} else {
					// It's not really BlackJack on a Split
					tempStrCount = "21";
				}
			}
			if (tempStrCount != "BlackJack" && tempStrCount != "21") {
				if (tempStrCount != tempCount.ToString()) {
					tempStrCount += " / " + tempCount;
				}
			}
			Count = tempCount;
			StringCount = tempStrCount;
		}

		/// <summary>
		/// Determines whether or not it is possible for a hand to get closer to 21.
		/// </summary>
		/// <param name="CurrentCount">The current count of the card.</param>
		/// <returns>A boolean indicating whether or not it is possible to get to 21.</returns>
		private bool MakeAceHighIfPossible(int CurrentCount) {
			bool HasLowAce = false;
			// Determine if there is a low ace.
			foreach (CardInHand CurrentCIH in Hand) {
				if (CurrentCIH.Card.Type == CardType.Ace) {
					if (!CurrentCIH.Card.IsHigh) {
						HasLowAce = true;
					}
				}
			}
			if (HasLowAce) {
				if (CurrentCount < 12) {
					// If there is a low ace and having a high ace will not bust you (and therefor be advantageous)
					foreach (CardInHand CurrentCIH in Hand) {
						if (CurrentCIH.Card.Type == CardType.Ace) {
							if (!CurrentCIH.Card.IsHigh) {
								// Find an ace and make it high.
								CurrentCIH.Card.IsHigh = true;
								return true;
							}
							break;
						}
					}
				}
			}
			return false;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Clears and resets the hand.
		/// </summary>
		public void ResetHand() {
			IsActive = false;
			CanSurrender = false;
			HandMode = HandMode.NotPlaying;
			Hand.Clear();
			CalculateCount();
		}

		/// <summary>
		/// Determines whether or not a player wants to draw a card.
		/// </summary>
		/// <returns>A boolean indicating whether or not a player wants to draw a card.</returns>
		public bool WantsCard() {
			return CanAcceptCard;
		}

		/// <summary>
		/// Adds a card to the player's hand.
		/// </summary>
		/// <param name="DealtCard">The card that the player is dealt.</param>
		public void RecieveCard(Card DealtCard) {
			Hand.Add(new CardInHand(this, DealtCard));
			if (Hand.Count == 2) {
				CanSurrender = true;
			} else {
				CanSurrender = false;
			}
			CalculateCount();
			SetCardPositions();
		}

		public void Hit(Card DealtCard) {
			CanSurrender = false;
			RecieveCard(DealtCard);
		}

		public void DoubleDown(Card DealtCard) {
			CanSurrender = false;
			HandMode = HandMode.DoubleDown;
			RecieveCard(DealtCard);
		}

		public Card SplitHand() {
			// This should only be called from PlayerViewModel.
			// This removes the second card from the hand and returns it.
			CanSurrender = false;
			Card tempCard = Hand[1].Card;
			Hand.RemoveAt(1);
			CalculateCount();
			OnPropertyChanged("Scale");
			return tempCard;
		}
		#endregion
	}
}
