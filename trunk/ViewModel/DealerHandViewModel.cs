//-----------------------------------------------------------------------
// <copyright file="DealerHandViewModel.cs" company="SpectralCoding">
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
using BlackJack.HouseLogic;
using BlackJack.Utilities;

namespace BlackJack.ViewModel {
	/// <summary>
	/// Provides logic for the Dealer Hand.
	/// </summary>
	public class DealerHandViewModel : ViewModelBase {
		#region Private Fields
		private TableViewModel m_MasterParent;
		private DealerViewModel m_ParentDealerViewModel;
		private DealerHandModel m_DealerHandModel;
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets a value indicating the contents of the hand.
		/// </summary>
		public ObservableCollection<DealerCardInHand> Hand {
			get {
				return m_DealerHandModel.Hand;
			}
		}
		/// <summary>
		/// Gets a value containing information for displaying the hand on the table.
		/// </summary>
		public ObservableCollection<DealerCardInHand> VisibleHand {
			get {
				if (!m_MasterParent.HouseRulesVM.FastMode) {
					return m_DealerHandModel.Hand;
				} else {
					return m_DealerHandModel.BlankHand;
				}
			}
		}
		/// <summary>
		/// Gets or sets a value indicating the current count of the hand.
		/// </summary>
		public int Count {
			get {
				return m_DealerHandModel.Count;
			}
			set {
				m_DealerHandModel.Count = value;
				if (!m_MasterParent.HouseRulesVM.FastMode) {
					OnPropertyChanged("Count");
					OnPropertyChanged("CanAcceptCard");
				}
			}
		}
		public string StringCount {
			get {
				return m_DealerHandModel.StringCount;
			}
			set {
				m_DealerHandModel.StringCount = value;
				if (!m_MasterParent.HouseRulesVM.FastMode) { OnPropertyChanged("StringCount"); }
			}
		}
		/// <summary>
		/// Gets or sets a value indicating whether the current hand is active.
		/// </summary>
		public bool IsActive {
			get {
				return m_DealerHandModel.IsActive;
			}
			set {
				m_DealerHandModel.IsActive = value;
				if (!m_MasterParent.HouseRulesVM.FastMode) {
					OnPropertyChanged("IsActive");
					OnPropertyChanged("CanAcceptCard");
				}
			}
		}
		/// <summary>
		/// Gets a value indicating whether or not the player can legally accept a card.
		/// </summary>
		/// <returns>A boolean indicated whether or not the player can legally accept a card.</returns>
		public bool CanAcceptCard {
			get {
				if (IsActive) {
					if (Count < 21) {
						return true;
					} else {
						// Has 21 or More
						return false;
					}
				} else {
					return false;
				}
			}
		}
		/// <summary>
		/// Gets or sets a value indicating whether or not the dealer has blackjack.
		/// </summary>
		public bool IsBlackJack {
			get {
				return m_DealerHandModel.IsBlackJack;
			}
			set {
				m_DealerHandModel.IsBlackJack = value;
				if (!m_MasterParent.HouseRulesVM.FastMode) { OnPropertyChanged("IsBlackJack"); }
			}
		}
		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the DealerHandViewModel class.
		/// </summary>
		/// <param name="ParentTVM">Placeholder for the parent MasterViewModel.</param>
		/// <param name="ParentDVM">Placeholder for the parent DealerViewModel.</param>
		public DealerHandViewModel(TableViewModel ParentTVM, DealerViewModel ParentDVM) {
			m_MasterParent = ParentTVM;
			m_ParentDealerViewModel = ParentDVM;
			m_DealerHandModel = new DealerHandModel();
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Sets the card positions for all cards in all hands.
		/// </summary>
		private void SetCardPositions() {
			for (int i = 0; i < Hand.Count; i++) {
				Hand[i].SetPosition(i, Hand.Count);
			}
		}

		/// <summary>
		/// Calculates the count in a particulair hand.
		/// </summary>
		private void CalculateCount() {
			bool hasAce = false;
			int tempCount;
			// Set all Aces as low.
			foreach (DealerCardInHand CurrentCIH in Hand) {
				if (CurrentCIH.Card.Type == CardType.Ace) {
					CurrentCIH.Card.IsHigh = false;
					hasAce = true;
				}
			}
			string tempStrCount = String.Empty;
			do {
				// Add up all the cards in the hand.
				tempCount = 0;
				foreach (DealerCardInHand CurrentCIH in Hand) {
					tempCount += CurrentCIH.Card.PlayValue;
				}
				if (tempStrCount == String.Empty) {
					tempStrCount = tempCount.ToString();
				}
			} while (MakeAceHighIfPossible(tempCount));
			if ((hasAce) && (Hand.Count == 2) && (tempCount == 21)) {
				// If it's BlackJack
				tempStrCount = "BlackJack";
				IsBlackJack = true;
			}
			if (tempStrCount != "BlackJack" && tempStrCount != "21") {
				if (tempStrCount != tempCount.ToString()) {
					tempStrCount += " / " + tempCount;
				}
			}
			Count = tempCount;
			StringCount = tempStrCount;
			if (!m_MasterParent.HouseRulesVM.FastMode) {
				OnPropertyChanged("Hand");
			}
		}

		/// <summary>
		/// Determines whether or not it is possible for a hand to get closer to 21.
		/// </summary>
		/// <param name="CurrentCount">The current count of the card.</param>
		/// <returns>A boolean indicating whether or not it is possible to get to 21.</returns>
		private bool MakeAceHighIfPossible(int CurrentCount) {
			bool HasLowAce = false;
			// Determine if there is a low ace.
			foreach (DealerCardInHand CurrentCIH in Hand) {
				if (CurrentCIH.Card.Type == CardType.Ace) {
					if (!CurrentCIH.Card.IsHigh) {
						HasLowAce = true;
					}
				}
			}
			if (HasLowAce) {
				if (CurrentCount < 12) {
					// If there is a low ace and having a high ace will not bust you (and therefor be advantageous)
					foreach (DealerCardInHand CurrentCIH in Hand) {
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
		public void Reset() {
			IsActive = false;
			if (Hand.Count > 0) {
				m_MasterParent.GameStatisticsVM.HandsPlayed++;
				m_MasterParent.GameStatisticsVM.RoundsPlayed++;
			}
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
		/// <param name="ShowCard">Whether or not the card is shown initially.</param>
		public void RecieveCard(Card DealtCard, bool ShowCard) {
			Hand.Add(new DealerCardInHand(this, m_MasterParent, DealtCard, ShowCard));
			m_MasterParent.GameStatisticsVM.CardsDealt++;
			CalculateCount();
			SetCardPositions();
		}

		/// <summary>
		/// Provides an abstraction for the RecieveCard method.
		/// </summary>
		/// <param name="DealtCard">The card that the player is dealt.</param>
		public void Hit(Card DealtCard) {
			RecieveCard(DealtCard, true);
		}

		/// <summary>
		/// Changes all of the dealers cards to showing (face up).
		/// </summary>
		public void ShowAll() {
			for (int i = 0; i < Hand.Count; i++) {
				if (!Hand[i].IsShowing) {
					Hand[i].IsShowing = true;
				}
			}
			if (!m_MasterParent.HouseRulesVM.FastMode) {
				OnPropertyChanged("Hand");
				OnPropertyChanged("VisibleHand");
			}
		}

		#endregion
	}
}
