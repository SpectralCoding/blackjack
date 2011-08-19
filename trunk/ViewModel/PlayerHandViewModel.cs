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
		private MasterViewModel m_ParentMasterViewModel;
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
				OnPropertyChanged("StringCount");
				OnPropertyChanged("IndicatorColor");
			}
		}
		/// <summary>
		/// Gets a value indicating the string representation of the current count of the hand.
		/// </summary>
		public string StringCount {
			get {
				return m_PlayerHandModel.Count.ToString();
			}
		}
		/// <summary>
		/// Gets a value indicating the color of the hand count.
		/// </summary>
		public string IndicatorColor {
			get {
				if (Count == 0) {
					return "#FFFFFF";		// White
				} else if (Count < 14) {
					return "#FF8080";		// Light Red
				} else if (Count < 18) {
					return "#FFD280";	// Light Orange
				} else if (Count < 22) {
					return "#90EE90";		// Light Green
				} else if (Count > 21) {
					return "#FF8080";		// Light Red
				} else {
					return "White";		// White
				}
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
			}
		}
		/// <summary>
		/// Gets a value indicating the visibility of the hand.
		/// </summary>
		public System.Windows.Visibility Visibility {
			get {
				if ((m_ParentPlayerViewModel.PlayerMode == PlayerMode.NotPlaying) || (HandMode == HandMode.NotPlaying)) {
					return System.Windows.Visibility.Hidden;
				} else {
					return System.Windows.Visibility.Visible;
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
		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the PlayerHandViewModel class.
		/// </summary>
		/// <param name="ParentMVM">Placeholder for the parent MasterViewModel.</param>
		/// <param name="ParentPVM">Placeholder for the parent PlayerViewModel.</param>
		public PlayerHandViewModel(MasterViewModel ParentMVM, PlayerViewModel ParentPVM) {
			m_ParentMasterViewModel = ParentMVM;
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
			int tempCount;
			foreach (CardInHand CurrentCIH in Hand) {
				if (CurrentCIH.Card.Type == CardType.Ace) {
					CurrentCIH.Card.IsHigh = false;
				}
			}
			do {
				tempCount = 0;
				foreach (CardInHand CurrentCIH in Hand) {
					tempCount += CurrentCIH.Card.PlayValue;
				}
			} while (CanGetCloserTo21(tempCount));
			Count = tempCount;
		}

		/// <summary>
		/// Determines whether or not it is possible for a hand to get closer to 21.
		/// </summary>
		/// <param name="CurrentCount">The current count of the card.</param>
		/// <returns>A boolean indicating whether or not it is possible to get to 21.</returns>
		private bool CanGetCloserTo21(int CurrentCount) {
			bool HasLowAce = false;
			foreach (CardInHand CurrentCIH in Hand) {
				if (CurrentCIH.Card.Type == CardType.Ace) {
					if (!CurrentCIH.Card.IsHigh) {
						HasLowAce = true;
					}
				}
			}
			if (HasLowAce) {
				if (CurrentCount < 12) {
					foreach (CardInHand CurrentCIH in Hand) {
						if (CurrentCIH.Card.Type == CardType.Ace) {
							if (!CurrentCIH.Card.IsHigh) {
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

		/// <summary>
		/// Determines whether or not the player can legally accept a card.
		/// </summary>
		/// <returns>A boolean indicated whether or not the player can legally accept a card.</returns>
		private bool CanAcceptCard() {
			if (Count < 21) {
				if (HandMode != HandMode.NotPlaying) {
					if (HandMode == HandMode.Normal) {
						// Normal hand with less than 21
						return true;
					} else {
						// Hand is a double down with less than 21
						if (Hand.Count == 2) {
							// If two cards
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
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Determines whether or not a player wants to draw a card.
		/// </summary>
		/// <returns>A boolean indicating whether or not a player wants to draw a card.</returns>
		public bool WantsCard() {
			return CanAcceptCard();
		}

		/// <summary>
		/// Adds a card to the player's hand.
		/// </summary>
		/// <param name="DealtCard">The card that the player is dealt.</param>
		public void RecieveCard(Card DealtCard) {
			Hand.Add(new CardInHand(this, DealtCard));
			CalculateCount();
			SetCardPositions();
		}

		/// <summary>
		/// Clears and resets the hand.
		/// </summary>
		public void ClearHand() {
			HandMode = HandMode.NotPlaying;
			Hand.Clear();
			CalculateCount();
		}
		#endregion
	}
}
